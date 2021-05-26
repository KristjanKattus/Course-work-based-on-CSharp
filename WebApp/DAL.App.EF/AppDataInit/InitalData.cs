using System;
using DAL.App.DTO;
using Domain.App;
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

        public static readonly (string name, string password, string firstName, string lastName
            , EGender gender, string[] roles)[] Users =
            {
                ("admin@fas.ee", "Foo.bar1", "Admin", "///", EGender.Male, new[] {"Admin"}),
                ("referee@fas.ee", "Foo.bar1", "Referee", "///", EGender.Male, new[] {"Referee"}),
                ("manager@fas.ee", "Foo.bar1", "Manager", "///", EGender.Male, new[] {"Manager"}),
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


        public static readonly (string[] Name, DateTime since, DateTime until, string? description)[] Roles =
        {
            (new []{"Referee", "Kohtunik"}, DateTime.Now, default, ""),
            (new []{"Player", "Mängija"}, DateTime.Now, default, ""),
            (new []{"Manager", "Treener"}, DateTime.Now, default, ""),
            (new []{"Physio", "Füsioterapeud"}, DateTime.Now, default, "")
        };

        public static readonly (string Firstname, string Lastname, DateTime Date, Char Sex)[] Persons =
        {
            ("Steve", "Irving", DateTime.Parse("2000-01-01"), 'M'),
            ("Boy", "Fred", DateTime.Parse("2002-12-05"), 'M'),
            ("Alan", "Smith", DateTime.Parse("1098-5-07"), 'M'),
            ("Tura", "Pop", DateTime.Parse("2001-11-02"), 'M'),
            ("Peter", "Coruch", DateTime.Parse("2003-07-08"), 'M'),
            ("Yo ", "Ming", DateTime.Parse("2000-02-09"), 'M'),
            ("Steve", "Irving", DateTime.Parse("1089-11-11"), 'M')
        };

        // public static readonly (Person Person, Team Team, Role Role)[] TeamPerson =
        // {
        //     (new Person {FirstName = "Steve", LastName = "Irving", Date = DateTime.Parse("2000-01-01"), Sex = 'M'},
        //         new Team {Name = "Team1League1"}, new Role {Name = "Player"}),
        //     (new Person {FirstName = "Boy", LastName = "Fred", Date = DateTime.Parse("2000-01-01"), Sex = 'M'},
        //         new Team {Name = "Team1League1"}, new Role {Name = "Player"}),
        //     (new Person {FirstName = "Alan", LastName = "Smith", Date = DateTime.Parse("2000-01-01"), Sex = 'M'},
        //         new Team {Name = "Team1League1"}, new Role {Name = "Player"}),
        //     (new Person {FirstName = "Tura", LastName = "Pop", Date = DateTime.Parse("2000-01-01"), Sex = 'M'},
        //         new Team {Name = "Team1League1"}, new Role {Name = "Player"}),
        //
        // };

        // public static readonly (Person Person, )

        public static readonly (string culture, string cultureUI)[] Cultures =
        {
            ("en-GB", "en"),
            ("et", "et")
        };

        public static readonly (Domain.App.Stadium_Area stadiumArea, string Name, DateTime Since
            , string PitchType, int Category)[] Stadiums =
        {
            (new Stadium_Area {Name = "Tartu", Since = DateTime.Now}, "Tamme Staadion", DateTime.Now, "Natural grass", 4),
            (new Stadium_Area {Name = "Tartu", Since = DateTime.Now}, "Annelinna Staadion", DateTime.Now, "Artificial grass", 4),
            (new Stadium_Area {Name = "Tallinn", Since = DateTime.Now}, "A Le Coq Arena", DateTime.Now, "Natural grass", 4),
            (new Stadium_Area {Name = "Tallinn", Since = DateTime.Now}, "Sportland Arena", DateTime.Now, "Artificial grass", 4),
            (new Stadium_Area {Name = "Narva", Since = DateTime.Now}, "Narva staadion", DateTime.Now, "Natural grass", 4),
            (new Stadium_Area {Name = "Kuresaare", Since = DateTime.Now}, "Kuresaare linnataadion", DateTime.Now, "Natural grass", 4),
        };

        public static readonly (string[] Name, string? Description)[] EventTypes =
        {
            (new [] {"Goal", "Värav"}, ""),
            (new [] {"Own goal", "Omavärav"}, ""),
            (new [] {"Yellow card", "Kollane kaart"}, ""),
            (new [] {"Red card", "Punane kaart"}, ""),
            (new [] {"Penalty", "Penalti"}, ""),
            (new [] {"Missed penalty", "Eksitud penalti"}, "")
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