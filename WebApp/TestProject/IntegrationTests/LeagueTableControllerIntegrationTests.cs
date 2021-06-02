using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Helpers;
using WebApp;
using Xunit;
using Xunit.Abstractions;

namespace TestProject.IntegrationTests
{
    public class LeagueTableControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<WebApp.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApp.Startup> _factory;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

        private const string PerIndexUri = "/LeagueTable";

        public LeagueTableControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory
            , ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
            _client = factory.WithWebHostBuilder(builder =>
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
        public async Task LeagueTable_HasSuccessStatusCode()
        {
            // ARRANGE
            var uri = "/Home";

            
            var getHomeResponseMessage = await _client.GetAsync(uri);
            getHomeResponseMessage.EnsureSuccessStatusCode();
            
            // ACT
            var getHomeDocument = await HtmlHelpers.GetDocumentAsync(getHomeResponseMessage);
            var homeBody = (IHtmlInputElement) getHomeDocument.QuerySelector("#leagueId");

            var leagueId = homeBody.Value;
            // ASSERT
            var leagueTableUri = $"/LeagueTable/Index/{leagueId}";
            var getLeagueTableResponseMessage = await _client.GetAsync(leagueTableUri);
            
            getLeagueTableResponseMessage.EnsureSuccessStatusCode();
            Assert.InRange((int) getLeagueTableResponseMessage.StatusCode, 200, 299);
            var getCompetitionsDocument = await HtmlHelpers.GetDocumentAsync(getLeagueTableResponseMessage);
            var leagueTeamsBody = (IHtmlTableElement) getCompetitionsDocument.QuerySelector("#leagueTeams");
            
            
            var competitionsBody = (IHtmlTableElement) getCompetitionsDocument.QuerySelector("#dataTable");
            // ASSERT
            leagueTeamsBody.ChildElementCount.Should().BeGreaterThan(0);
            competitionsBody.ChildElementCount.Should().BeGreaterThan(0);
            // await Search_GameTeams(leagueId);
        }

        public async Task Search_GameTeams(string leagueId)
        {
            // ARRANGE
            var uri = $"/LeagueTable/{leagueId}";
            var getCompetitionsResponse = await _client.GetAsync(uri);
            getCompetitionsResponse.EnsureSuccessStatusCode();
            
            // ACT
            
            
        }
    }
}