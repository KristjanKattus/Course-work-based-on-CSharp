using System;
using DAL.App.DTO;
using Domain.Base;

namespace DAL.App.EF.AppDataInit
{
    public class InitialData
    {
        public static readonly (string roleName, string displayName)[] UserRoles =
        {
            ("Admin", "Admins"),
            ("Referee", "Referees"),
            ("Player", "Players"),
            ("Manager", "Managers"),
        };
        
        public static readonly (string name, string password, string firstName, string lastName, EGender gender, string[] roles)[] Users =
        {
            ("admin@smarty.ee", "Foo.bar1", "Admin", "///", EGender.Male, new []{"Admin"}),
            ("referee@smarty.ee", "Foo.bar1", "Referee", "///", EGender.Male, new []{"Referee"}),
            ("manager@smarty.ee", "Foo.bar1", "Manager", "///", EGender.Male, new []{"Manager"}),
        };
        

        public static readonly (string Name, DateTime since, DateTime until, string Description)[] Clubs =
        {
            ("Tartu JK Tammeka", DateTime.Now, default, ""),
            ("Viljandi JK Tulevik", DateTime.Now, default, ""),
            ("Tallinna FC Flora", DateTime.Now, default, ""),
            ("Tallinna FCI Levadia", DateTime.Now, default, ""),
            ("JK Narva Trans", DateTime.Now, default, ""),
            ("FC Kuressaare", DateTime.Now, default, ""),
        };


        public static readonly (string Name, DateTime since, DateTime until, string? description)[] Roles =
        {
            ("Referee", DateTime.Now, default, ""),
            ("Player", DateTime.Now, default, ""),
            ("Manager", DateTime.Now, default, ""),
            ("Assistant manager", DateTime.Now, default, ""),
            ("Physio", DateTime.Now, default, "")
        };

        public static readonly (string Firstname, string Lastname, DateTime Date, Char Sex)[] Persons =
        {
            ("Steve", "Irving", DateTime.Parse("2000-01-01"), 'M'),
            ("Boy", "Fred", DateTime.Parse("2002-12-05"), 'M'),
            ("Alan", "Smith", DateTime.Parse("1098-5-07"), 'M'),
            ("Tura", "Pop", DateTime.Parse("2001-11-02"), 'M'),
            ("Peter", "Coruch", DateTime.Parse("2003-07-08"), 'M'),
            ("Yo ", "Ming", DateTime.Parse("2000-02-09"), 'M'),
            ("Steve", "Irving", DateTime.Parse("1089-11-11"), 'M'),
        };

        public static readonly (string Name, string? Description)[] EventTypes =
        {
            ("Goal", ""),
            ("Own goal", ""),
            ("Yellow card", ""),
            ("Red card", ""),
            ("Penalty", ""),
            ("Missed penalty", "")
        };
        
        public static readonly (string Name, string? Description)[] GamePartTypes =
        {
            ("First half", ""),
            ("Second half", ""),
            ("First Extra time", ""),
            ("Second Extra time", ""),
            ("Penalties", ""),
            
        };


        public static readonly (string Name, int Duration, string? Description)[] Leagues =
        {
            ("Preemiumliiga", 34, "Estonias first league"),
            ("Esiliiga", 34, "Estonias second league"),
            ("Esiliiga B", 34, "Estonias third league"),
        };
    }
}