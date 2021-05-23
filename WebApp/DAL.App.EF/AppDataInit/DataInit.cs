using System;
using System.Collections.Generic;
using System.Linq;
using DAL.App.DTO;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Person = Domain.App.Person;
using Stadium = DAL.App.DTO.Stadium;

namespace DAL.App.EF.AppDataInit
{
    public static class DataInit
    {
        public static void DropDataBase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }
        public static void MigrateDataBase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ILogger logger)
        {
            if (userManager.Users.Any())
            {
                return;
            }
            
            foreach (var (roleName, displayName) in InitialData.UserRoles)
            {
                var userRole = roleManager.FindByNameAsync(roleName).Result;
                if (userRole == null)
                {
                    userRole = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = displayName
                    };

                    var result = roleManager.CreateAsync(userRole).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed");
                    }
                    logger.LogInformation("Seeded role {Role}", roleName);
                }
            }
            

            
            foreach (var userInfo in InitialData.Users)
            {
                var appUser = userManager.FindByNameAsync(userInfo.name).Result;
                if (appUser == null)
                {
                    appUser = new AppUser()
                    {
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        Firstname = userInfo.firstName,
                        Lastname = userInfo.lastName,
                        Gender = userInfo.gender,
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(appUser, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed");
                    }
                    logger.LogInformation("Seeded user {User}", userInfo.name);

                }

                var roleResult = userManager.AddToRolesAsync(appUser, userInfo.roles).Result;
            }
            
            //
            // var user = new AppUser();
            // user.Email = "admin@kkattus.com";
            // user.Firstname = "Admin";
            // user.Lastname = "kkattus.com";
            // user.UserName = user.Email;
            //
            // result = userManager.CreateAsync(user, "Foo.bar1").Result;
            // if (!result.Succeeded)
            // {
            //     foreach (var identityError in result.Errors)
            //     {
            //         Console.WriteLine("Cant create user! Error: " + identityError.Description);
            //     }
            // }
            //
            // result = userManager.AddToRoleAsync(user, "Admin").Result;
            // if (!result.Succeeded)
            // {
            //     foreach (var identityError in result.Errors)
            //     {
            //         Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
            //     }
            // }
/*
            result = userManager.UpdateAsync(user).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant update user! Error: " + identityError.Description);
                }
            }
*/            
        }
        public static void SeedAppData(AppDbContext ctx, ILogger logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            SeedClubs(ctx, logger);
            SeedLeagues(ctx, logger);
            SeedGamePartTypes(ctx, logger);
            SeedEventTypes(ctx, logger);
            SeedRoles(ctx, logger);
            SeedPersons(ctx, logger, userManager, roleManager);
            SeedStadiums(ctx, logger);
        }

        private static void SeedStadiums(AppDbContext ctx, ILogger logger)
        {
            if (ctx.Stadiums.Any())
            {
                return;
            }
            foreach (var stadiumData in InitialData.Stadiums)
            {
                var stadium = new Domain.App.Stadium
                {
                    Name = stadiumData.Name,
                    StadiumArea = stadiumData.stadiumArea,
                    Since = stadiumData.Since,
                    PitchType = stadiumData.PitchType,
                    Category = stadiumData.Category
                };
                ctx.Stadiums.Add(stadium);
            }
            ctx.SaveChanges();
        }

        private static void SeedPersons(AppDbContext ctx, ILogger logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (ctx.TeamPersons.Any())
            {
                return;
            }
            var team = new Domain.App.Team
            {
                Name = "Team1"
            };
            foreach (var personData in InitialData.Persons)
            {
                var person = new Domain.App.Person
                {
                    FirstName = personData.Firstname,
                    LastName = personData.Lastname,
                    Date = personData.Date,
                    Sex = personData.Sex,
                    AppUserId = userManager.Users.First().Id
                };

                var teamPerson = new Domain.App.Team_Person
                {
                    Person = person,
                    Team = team,
                    RoleId = Guid.Parse("5C32ED6A-028E-4FF6-22AE-08D91D1F6373")
                    
                };
                ctx.TeamPersons.Add(teamPerson);
            }
            ctx.SaveChanges();
        }

        private static void SeedLeagues(AppDbContext ctx, ILogger logger)
        {
            if (ctx.Leagues.Any())
            {
                return;
            }

            foreach (var leagueData in InitialData.Leagues)
            {
                var league = new Domain.App.League
                {
                    Name = leagueData.Name,
                    Duration = leagueData.Duration
                };

                ctx.Leagues.Add(league);
            }
            ctx.SaveChanges();
        }

        private static void SeedGamePartTypes(AppDbContext ctx, ILogger logger)
        {
            if (ctx.GamePartTypes.Any())
            {
                return;
            }

            foreach (var gamePartTypeData in InitialData.GamePartTypes)
            {
                var gamePartType = new Domain.App.Game_Part_Type
                {
                    Name = gamePartTypeData.Name
                };
                ctx.GamePartTypes.Add(gamePartType);
            }
            ctx.SaveChanges();
        }
        
        
        private static void SeedEventTypes(AppDbContext ctx, ILogger logger)
        {
            if (ctx.EventTypes.Any())
            {
                return;
            }

            foreach (var eventTypesData in InitialData.EventTypes)
            {
                var eventTypes = new Domain.App.Event_Type
                {
                    Name = eventTypesData.Name
                };
                ctx.EventTypes.Add(eventTypes);
            }
            ctx.SaveChanges();
        }

        private static void SeedClubs(AppDbContext ctx, ILogger logger)
        {
            if (ctx.Clubs.Any())
            {
                return;
            }

            foreach (var clubData in InitialData.Clubs)
            {
                var club = new Domain.App.Club
                {
                    Name = clubData.Name,
                    Since = clubData.since
                };

                ctx.Clubs.Add(club);
                logger.LogInformation($"Seeded club {club}", club.Name);
            }
            ctx.SaveChanges();
        }

        private static void SeedRoles(AppDbContext ctx, ILogger logger)
        {
            if (ctx.FRoles.Any())
            {
                return;
            }

            foreach (var roleData in InitialData.Roles)
            {
                var role = new Domain.App.Role
                {
                    Name = roleData.Name,
                    Since = roleData.since
                };
                ctx.FRoles.Add(role);
            }
            ctx.SaveChanges();
        }
        
        
        
    }
}