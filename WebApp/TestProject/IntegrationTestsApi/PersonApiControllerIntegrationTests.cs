using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DTO.App;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTestsApi
{
    public class PersonApiControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        private const string LogInUri = "account/login";
        private const string LeagueTableURL = "/leaguetable/";

        private const string BaseAddressUri = "https://localhost:5051";


        public PersonApiControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
                })
                .CreateClient(new WebApplicationFactoryClientOptions()
                    {
                        // dont follow redirects
                        AllowAutoRedirect = false
                    }
                );

            _client.BaseAddress = new Uri(BaseAddressUri);
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task Api_Search_Persons()
        {
            // ARRANGE
            // var searchPerson = new SearchPerson()
            // {
            //     Firstname = "jüri",
            // };
            // var uri = PersonSearchUri + "?" +
            //           "firstname=" + (searchPerson.Firstname ?? "") +
            //           "&lastname=" + (searchPerson.Lastname ?? "") +
            //           "&identificationcode=" + (searchPerson.IdentificationCode ?? "");
           
            // ACT
            await LogIn();
            var httpResponse = await _client.GetAsync(uri);

            // ASSERT
            httpResponse.EnsureSuccessStatusCode();
            var listPersons = await JsonHelper.DeserializeWithWebDefaults<List<DTO.App.V1.Person>>(httpResponse.Content);
            listPersons.Should().NotBeNull();

            listPersons!.Count.Should().Be(3);
            listPersons![0].Firstname.Should().Be("Jüri");
            listPersons![0].Lastname.Should().Be("Paat");
            listPersons![0].IdentificationCode.Should().Be("3856735735756");
        }
        
        [Fact]
        public async Task Api_Create_Person()
        {
            // ARRANGE
            var person = new DTO.App.V1.Person()
            {
                Firstname = "Raul",
                Lastname = "Suurkõrv",
                IdentificationCode = "954248351877"
            };

            // ACT
            await LogIn();
            var data = _client.ObjToHttpContent(person);
            var response = await _client.PostAsync(PersonCreateUri, data);

            // ASSERT
            response.EnsureSuccessStatusCode();
            var obj = await JsonHelper.DeserializeWithWebDefaults<DTO.App.V1.Person>(response.Content);
            obj.Should().NotBeNull();
            obj!.Firstname.Should().Be(person.Firstname);
            obj!.Lastname.Should().Be(person.Lastname);
            obj!.IdentificationCode.Should().Be(person.IdentificationCode);
        }
        
        [Fact]
        public async Task Api_Create_Invalid_Person_Empty_Firstname()
        {
            // ARRANGE
            var person = new DTO.App()
            {
                Firstname = "",
                Lastname = "Pihel",
                IdentificationCode = "954248351877"
            };

            // ACT
            await LogIn();
            var data = _client.ObjToHttpContent(person);
            var httpResponse = await _client.PostAsync(PersonCreateUri, data);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Api_Create_Invalid_Person_Empty_Lastname()
        {
            // ARRANGE
            var person = new DTO.App.Person()
            {
                Firstname = "Jaana",
                Lastname = "",
                IdentificationCode = "745724795235"
            };

            // ACT
            await LogIn();
            var data = _client.ObjToHttpContent(person);
            var httpResponse = await _client.PostAsync(PersonCreateUri, data);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        

        private async Task<HttpResponseMessage> LogIn()
        {
            // ARRANGE
            var login = new DTO.App.Login()
            {
                Email = "admin@fas.ee",
                Password = "Foo.bar1"
            };

            // ACT
            var content = _client.ObjToHttpContent(login);
            var response = await _client.PostAsync(LogInUri, content);
            response.EnsureSuccessStatusCode();
            await AddAuthHeader(response);
            return response;
        }

        private async Task AddAuthHeader(HttpResponseMessage response)
        {
            var data = await JsonHelper.DeserializeWithWebDefaults<JwtResponse>(response.Content);
            data.Should().NotBeNull();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + data!.Token);
        }
    }
}