using System;
using AutoMapper;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public IStadiumAreaService StadiumAreas => GetService<IStadiumAreaService>(() => new StadiumAreaService(Uow, Uow.StadiumAreas, Mapper));
        public IClubService Clubs => GetService<IClubService>(() => new ClubService(Uow, Uow.Clubs, Mapper));
        public IClubTeamService ClubTeams => GetService<IClubTeamService>(() => new ClubTeamService(Uow, Uow.ClubTeams, Mapper));
        public IEventTypeService EventTypes => GetService<IEventTypeService>(() => new EventTypeService(Uow, Uow.EventTypes, Mapper));
        public IGameEventService GameEvents => GetService<IGameEventService>(() => new GameEventService(Uow, Uow.GameEvents, Mapper));
        public IGamePartService GameParts => GetService<IGamePartService>(() => new GamePartService(Uow, Uow.GameParts, Mapper));
        public IGamePersonnelService GamePersonnel => GetService<IGamePersonnelService>(() => new GamePersonnelService(Uow, Uow.GamePersonnel, Mapper));
        public IGameService Games => GetService<IGameService>(() => new GameService(Uow, Uow.Games, Mapper));
        public IGameTeamListService GameTeamLists => GetService<IGameTeamListService>(() => new GameTeamListService(Uow, Uow.GameTeamLists, Mapper));
        public IGameTeamService GameTeams => GetService<IGameTeamService>(() => new GameTeamService(Uow, Uow.GameTeams, Mapper));
        public ILeagueService Leagues => GetService<ILeagueService>(() => new LeagueService(Uow, Uow.Leagues, Mapper));
        public ILeagueTeamService LeagueTeams => GetService<ILeagueTeamService>(() => new LeagueTeamService(Uow, Uow.LeagueTeams, Mapper));
        public IPersonService Persons => GetService<IPersonService>(() => new PersonService(Uow, Uow.Persons, Mapper));
        public IRoleService Roles => GetService<IRoleService>(() => new RoleService(Uow, Uow.Roles, Mapper));
        public IStadiumService Stadiums => GetService<IStadiumService>(() => new StadiumService(Uow, Uow.Stadiums, Mapper));
        public ITeamPersonService TeamPersons => GetService<ITeamPersonService>(() => new TeamPersonService(Uow, Uow.TeamPersons, Mapper));
        public ITeamService Teams => GetService<ITeamService>(() => new TeamService(Uow, Uow.Teams, Mapper));
        public IGamePartTypeService GamePartTypes => GetService<IGamePartTypeService>(() => new GamePartTypeService(Uow, Uow.GamePartTypes, Mapper));
    }
}