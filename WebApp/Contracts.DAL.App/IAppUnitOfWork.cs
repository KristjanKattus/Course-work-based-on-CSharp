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

    }
}