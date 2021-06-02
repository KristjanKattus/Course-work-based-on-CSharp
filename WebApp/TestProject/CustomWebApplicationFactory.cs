using System;
using System.Linq;
using DAL.App.EF;
using DAL.App.EF.AppDataInit;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                
                var logger = db.GetService<ILogger<Startup>>();

                db.Database.EnsureCreated();

                using var userManager = scopedServices.GetService<UserManager<AppUser>>();
                using var roleManager = scopedServices.GetService<RoleManager<AppRole>>();

                
                DataInit.SeedIdentity(userManager!, roleManager!, logger);
                DataInit.SeedAppData(db, logger, userManager!, roleManager!);

                var leagueId = Guid.Parse("1AF2336C-8A54-4A4C-0CEA-08D922301FB4");

                var league = new League()
                {
                    Id = leagueId,
                    Name = "leagu1"
                };
                var game = new Game()
                {
                    LeagueId = leagueId,
                    GameLength = 90,
                    MatchRound = 1,
                };
                var team1 = new Team{Name = "team1"};
                var team2 = new Team{Name = "team2"};

                var gameteam1 = new Game_Team()
                {
                    Game = game,
                    Team = team1,
                    Hometeam = true
                };
                var gameteam2 = new Game_Team()
                {
                    Game = game,
                    Team = team2,
                };
                var leagueTeam1 = new League_Team()
                {
                    League = league,
                    Team = team1
                };
                
                var leagueTeam2 = new League_Team()
                {
                    League = league,
                    Team = team2
                };
                db.LeagueTeams.Add(leagueTeam1);
                db.LeagueTeams.Add(leagueTeam2);
                db.GameTeams.Add(gameteam1);
                db.GameTeams.Add(gameteam2);

                db.SaveChangesAsync();
            });
        }
    }
}