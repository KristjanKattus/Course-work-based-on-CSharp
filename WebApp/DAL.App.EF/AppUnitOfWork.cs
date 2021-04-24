
using AutoMapper;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;


namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }
        public IStadiumAreaRepository Areas => GetRepository(() => new StadiumAreaRepository(UowDbContext, Mapper));
        public IClubRepository Clubs => GetRepository(() => new ClubRepository(UowDbContext, Mapper));
        public IClubTeamRepository ClubTeams => GetRepository(() => new ClubTeamRepository(UowDbContext, Mapper));
        public IEventTypeRepository EventTypes => GetRepository(() => new EventTypeRepository(UowDbContext, Mapper));
        public IGameEventRepository GameEvents => GetRepository(() => new GameEventRepository(UowDbContext, Mapper));
        public IGamePartRepository GameParts => GetRepository(() => new GamePartRepository(UowDbContext, Mapper));
        public IGamePersonnelRepository GamePersonnel => GetRepository(() => new GamePersonnelRepository(UowDbContext, Mapper));
        public IGameRepository Games => GetRepository(() => new GameRepository(UowDbContext, Mapper));
        public IGameTeamListRepository GameTeamLists => GetRepository(() => new GameTeamListRepository(UowDbContext, Mapper));
        public IGameTeamRepository GameTeams => GetRepository(() => new GameTeamRepository(UowDbContext, Mapper));
        public ILeagueRepository Leagues => GetRepository(() => new LeagueRepository(UowDbContext, Mapper));
        public ILeagueTeamRepository LeagueTeams => GetRepository(() => new LeagueTeamRepository(UowDbContext, Mapper));
        public IPersonRepository Persons => GetRepository(() => new PersonRepository(UowDbContext, Mapper));
        public IRoleRepository Roles => GetRepository(() => new RoleRepository(UowDbContext, Mapper));
        public IStadiumAreaRepository StadiumAreas => GetRepository(() => new StadiumAreaRepository(UowDbContext, Mapper));
        public IStadiumRepository Stadiums => GetRepository(() => new StadiumRepository(UowDbContext, Mapper));
        public ITeamPersonRepository TeamPersons => GetRepository(() => new TeamPersonRepository(UowDbContext, Mapper));
        public ITeamRepository Teams => GetRepository(() => new TeamRepository(UowDbContext, Mapper));
        public IGamePartTypeRepository GamePartTypes => GetRepository(() => new GamePartTypeRepository(UowDbContext, Mapper));
    }
}