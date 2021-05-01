using System;
using System.Collections.Generic;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                var user = userManager.FindByNameAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        DOB = userInfo.DOB,
                        Gender = userInfo.gender,
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed");
                    }
                    logger.LogInformation("Seeded user {User}", userInfo.name);

                }

                var roleResult = userManager.AddToRolesAsync(user, userInfo.roles).Result;
            }
            
            
            var user = new AppUser();
            user.Email = "admin@kkattus.com";
            user.Firstname = "Admin";
            user.Lastname = "kkattus.com";
            user.UserName = user.Email;

            result = userManager.CreateAsync(user, "Foo.bar1").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }

            result = userManager.AddToRoleAsync(user, "Admin").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
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
        public static void SeedAppData(AppDbContext ctx)
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
            }

            ctx.SaveChanges();

        }
    }
}