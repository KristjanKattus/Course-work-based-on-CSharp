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
    public class IdentityIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;


        public IdentityIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
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
        public async Task TestAuthAction_AuthRedirect()
        {
            // ARRANGE
            var uri = "/Persons";
            
            // ACT
            var getTestResponse = await _client.GetAsync(uri);
            
            // ASSERT
            Assert.Equal(302, (int) getTestResponse.StatusCode);
        }
        
        [Fact]
        public async Task Register_Success()
        {
            // ARRANGE
            var uri = "/Identity/Account/Register";
            var formValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = "test@user.ee",
                ["Input_Password"] = "Foo.bar1",
                ["Input_ConfirmPassword"] = "Foo.bar1",
                ["Input_FirstName"] = "Jarmo",
                ["Input_LastName"] = "Kuusk",
            };
            
            // ACT
            var httpResponse = await RegisterAccount(uri, formValues);

            // ASSERT
            httpResponse.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task Register_Invalid_Email()
        {
            // ARRANGE
            var uri = "/Identity/Account/Register";
            var formValues = new Dictionary<string, string>()
            {
                ["Input_Email"] = "",
                ["Input_Password"] = "Foo.bar1",
                ["Input_ConfirmPassword"] = "Foo.bar1",
                ["Input_FirstName"] = "Jarmo",
                ["Input_LastName"] = "Kuusk",
            };
            
            // ACT
            var httpResponse = await RegisterAccount(uri, formValues);

            // ASSERT
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        private async Task<HttpResponseMessage> RegisterAccount(string uri, Dictionary<string, string> formValues)
        {
            var httpResponse = await _client.GetAsync(uri);
            // httpResponse.StatusCode.Should().Be(HttpStatusCode.Found);
            // httpResponse = await _client.RedirectByHttpResponse(httpResponse);
            httpResponse.EnsureSuccessStatusCode();

            var elemCollection = await HtmlHelpers.ResponseToElemCollection(httpResponse, "#register-form");

            elemCollection.Should().NotBeEmpty();
            var httpElem = (IHtmlFormElement) elemCollection.First();

            httpResponse = await _client.SendAsync(httpElem, formValues);
            return httpResponse;
        }
        
    }
}