using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Stadium_Area> StadiumAreas { get; set; } = default!;
        public DbSet<Club> Clubs { get; set; } = default!;
        public DbSet<Club_Team> ClubTeams { get; set; } = default!;
        public DbSet<Event_Type> EventTypes { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
        public DbSet<Game_Event> GameEvents { get; set; } = default!;
        public DbSet<Game_Part> GameParts { get; set; } = default!;
        public DbSet<Game_Personnel> GamePersonnels { get; set; } = default!;
        public DbSet<Game_Team> GameTeams { get; set; } = default!;
        public DbSet<Game_Team_List> GameTeamListMembers { get; set; } = default!;
        public DbSet<League> Leagues { get; set; } = default!;
        public DbSet<League_Team> LeagueTeams { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Role> FRoles { get; set; } = default!;
        public DbSet<Stadium> Stadiums { get; set; } = default!;
        public DbSet<Team> Teams { get; set; } = default!;
        public DbSet<Team_Person> TeamPersons { get; set; } = default!;
        public DbSet<Game_Part_Type> Types { get; set; } = default!;
        
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
    
   
}