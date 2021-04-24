using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IStadiumAreaRepository StadiumAreas { get; }
        IClubRepository Clubs { get; }
        IClubTeamRepository ClubTeams { get; }
        IEventTypeRepository EventTypes { get; }
        IGameEventRepository GameEvents { get; }
        IGamePartRepository GameParts { get; }
        IGamePersonnelRepository GamePersonnel { get; }
        IGameRepository Games { get; }
        IGameTeamListRepository GameTeamLists { get; }
        IGameTeamRepository GameTeams { get; }
        ILeagueRepository Leagues { get; }
        ILeagueTeamRepository LeagueTeams { get; }
        IPersonRepository Persons { get; }
        IRoleRepository Roles { get; }
        IStadiumRepository Stadiums { get; }
        ITeamPersonRepository TeamPersons { get; }
        ITeamRepository Teams { get; }
        IGamePartTypeRepository GamePartTypes { get; }
        
        // IBaseRepository<Stadium_Area> Areas { get; }
        // IBaseRepository<Club> Clubs { get; }
        // IBaseRepository<Club_Team> ClubTeams { get; }
        // IBaseRepository<Event_Type> EventTypes { get; }
        // IBaseRepository<Game_Event> GameEvents { get; }
        // IBaseRepository<Game_Part> GameParts { get; }
        // IBaseRepository<Game_Personnel> GamePersonnel { get; }
        // IBaseRepository<Game> Games { get; }
        // IBaseRepository<Game_Team_List> GameTeamLists { get; }
        // IBaseRepository<Game_Team> GameTeams { get; }
        // IBaseRepository<League> Leagues { get; }
        // IBaseRepository<League_Team> LeagueTeams { get; }
        // IBaseRepository<Person> Persons { get; }
        // IBaseRepository<Role> Roles { get; }
        // IBaseRepository<Stadium> Stadiums { get; }
        // IBaseRepository<Team_Person> TeamPersons { get; }
        // IBaseRepository<Team> Teams { get; }
        // IBaseRepository<Game_Part_Type> GamePartTypes { get; }
        
    }
}