
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IStadiumAreaService StadiumAreas { get; }
        IClubService Clubs { get; }
        IClubTeamService ClubTeams { get; }
        IEventTypeService EventTypes { get; }
        IGameEventService GameEvents { get; }
        IGamePartService GameParts { get; }
        IGamePersonnelService GamePersonnel { get; }
        IGameService Games { get; }
        IGameTeamListService GameTeamLists { get; }
        IGameTeamService GameTeams { get; }
        ILeagueService Leagues { get; }
        ILeagueTeamService LeagueTeams { get; }
        IPersonService Persons { get; }
        IRoleService Roles { get; }
        IStadiumService Stadiums { get; }
        ITeamPersonService TeamPersons { get; }
        ITeamService Teams { get; }
        IGamePartTypeService GamePartTypes { get; }
    }
}