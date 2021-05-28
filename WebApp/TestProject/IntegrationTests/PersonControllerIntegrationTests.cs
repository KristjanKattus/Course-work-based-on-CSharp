using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DTO.App;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class PersonControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        private const string PerIndexUri = "/Persons";
        private const string PerCreateUri = "/Persons/Create";


        public PersonControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
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
        }

        [Fact]
        public async Task Search_Persons()
        {
            // ARRANGE
            var regFormValues = new Dictionary<string, string>()
            {
                ["Person_Firstname"] = "jüri",
            };

            // ACT
            var httpResponse = await LogIn(PerIndexUri);
            httpResponse = await SearchPerson(httpResponse, regFormValues);
            httpResponse.EnsureSuccessStatusCode();
            var elemCollection = await HtmlHelpers.ResponseToElemCollection(httpResponse, ".searched-person");

            // ASSERT
            elemCollection.Should().NotBeNull();
            elemCollection!.Length.Should().Be(3);

            elemCollection[0].Children[1].TextContent.Trim().Should().Be("Jüri");
            elemCollection[0].Children[2].TextContent.Trim().Should().Be("Paat");
            elemCollection[0].Children[3].TextContent.Trim().Should().Be("3856735735756");
        }
        
        [Fact]
        public async Task Create_Person()
        {
            // ARRANGE
            var formValues = new Dictionary<string, string>()
            {
                ["Person_Firstname"] = "Taavi",
                ["Person_Lastname"] = "Tuul",
                ["Person_IdentificationCode"] = "99999999999",
            };

            // ACT
            var httpResponse = await LogIn(PerCreateUri);
            httpResponse = await CreatePerson(httpResponse, formValues);
            httpResponse.EnsureSuccessStatusCode();
            httpResponse = await _client.GetAsync(PerIndexUri);
            httpResponse = await SearchPerson(httpResponse, formValues);
            httpResponse.EnsureSuccessStatusCode();
            var elemCollection = await HtmlHelpers.ResponseToElemCollection(httpResponse ,".searched-person");

            // ASSERT
            elemCollection.Should().NotBeNull();
            elemCollection!.Length.Should().Be(1);

            elemCollection[0].Children[1].TextContent.Trim().Should().Be(formValues["Person_Firstname"]);
            elemCollection[0].Children[2].TextContent.Trim().Should().Be(formValues["Person_Lastname"]);
            elemCollection[0].Children[3].TextContent.Trim().Should().Be(formValues["Person_IdentificationCode"]);
        }
        
        [Fact]
        public async Task Create_Invalid_Person_Empty_Firstname()
        {
            // ARRANGE
            var formValues = new Dictionary<string, string>()
            {
                ["Person_Firstname"] = "",
                ["Person_Lastname"] = "Kask",
                ["Person_IdentificationCode"] = "5702394657",
            };

            // ACT
            var httpResponse = await LogIn(PerCreateUri);
            httpResponse = await CreatePerson(httpResponse, formValues);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Create_Invalid_Person_Empty_Lastname()
        {
            // ARRANGE
            var formValues = new Dictionary<string, string>()
            {
                ["Person_Firstname"] = "Lembitu",
                ["Person_Lastname"] = "",
                ["Person_IdentificationCode"] = "5702394657",
            };

            // ACT
            var httpResponse = await LogIn(PerCreateUri);
            httpResponse = await CreatePerson(httpResponse, formValues);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Create_Invalid_Person_Empty_IdentificationCode()
        {
            // ARRANGE
            var formValues = new Dictionary<string, string>()
            {
                ["Person_Firstname"] = "Viivi",
                ["Person_Lastname"] = "Jalgratas",
                ["Person_IdentificationCode"] = "",
            };

            // ACT
            var httpResponse = await LogIn(PerCreateUri);
            httpResponse = await CreatePerson(httpResponse, formValues);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Create_Invalid_Person_To_Long_Firstname()
        {
            // ARRANGE
            var formValues = new Dictionary<string, string>()
            {
                ["Person_Firstname"] = "Adolph Blaine Charles David Earl Frederick Gerald Hubert Irvin John Kenneth Lloyd Martin Nero Oliver Paul Quincy Randolph Sherman Thomas Uncas Victor William Xerxes Yancy Zeus",
                ["Person_Lastname"] = "Tõuks",
                ["Person_IdentificationCode"] = "5702394657",
            };

            // ACT
            var httpResponse = await LogIn(PerCreateUri);
            httpResponse = await CreatePerson(httpResponse, formValues);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<HttpResponseMessage> LogIn(string uri)
        {
            // ACT
            var response = await _client.RedirectByUri(uri);

            var getRegisterDocument = await HtmlHelpers.GetDocumentAsync(response);

            var logForm = (IHtmlFormElement) getRegisterDocument.QuerySelector("#account");
            var logFormValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = "admin@bloody.ee",
                ["Input_Password"] = "Foo.bar1",
                ["Input_RememberMe"] = "false"
            };

            response = await _client.SendAsync(logForm, logFormValues);
            response.EnsureSuccessStatusCode();

            return response;
        }

        private async Task<HttpResponseMessage> SearchPerson(
            HttpResponseMessage httpResponseMessage,
            Dictionary<string, string> regFormValues)
        {
            var getSearchDocument = await HtmlHelpers.GetDocumentAsync(httpResponseMessage);

            var regForm = (IHtmlFormElement) getSearchDocument.QuerySelector("#search-form");

            var response = await _client.SendAsync(regForm, regFormValues);
            return response;
        }

        private async Task<HttpResponseMessage> CreatePerson(
            HttpResponseMessage httpResponse,
            Dictionary<string, string> formValues)
        {
            var getCreateDocument = await HtmlHelpers.GetDocumentAsync(httpResponse);

            var regForm = (IHtmlFormElement) getCreateDocument.QuerySelector("#create-person");

            var personTypeId = ((IHtmlOptionElement) getCreateDocument.QuerySelector("#personTypes").Children[1]).Value;
            formValues.Add("Person_PersonTypeId", personTypeId);

            var response = await _client.SendAsync(regForm, formValues);
            return response;
        }
    }
}