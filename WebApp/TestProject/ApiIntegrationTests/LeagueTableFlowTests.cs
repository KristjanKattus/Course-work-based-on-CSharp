using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DTO.App;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.ApiIntegrationTests
{
    public class LeagueTableFlowTests
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        private const string ApiUri = "https://localhost:5001/api/v1/";


        public LeagueTableFlowTests(CustomWebApplicationFactory<Startup> factory,
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

            _client.BaseAddress = new Uri(ApiUri);
            _client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        
        private async Task<HttpResponseMessage> LogIn()
        {
            // ARRANGE
            var login = new PublicApi.DTO.v1.Login()
            {
                Email = "admin@fas.com",
                Password = "Foo.bar1"
            };

            // ACT
            var data = JsonHelper.SerializeWithWebDefaults(login)!;
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            const string? loginUri = "Account/Login";

            var response = await _client.PostAsync(loginUri, content);
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