
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;


namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
            // Clubs = new BaseRepository<Club,AppDbContext>(uowDbContext);
            // ClubTeams = new BaseRepository<Club_Team,AppDbContext>(uowDbContext);
            // EventTypes = new BaseRepository<Event_Type,AppDbContext>(uowDbContext);
            // GameEvents = new BaseRepository<Game_Event,AppDbContext>(uowDbContext);
            // GameParts = new BaseRepository<Game_Part,AppDbContext>(uowDbContext);
            // GamePersonnel = new BaseRepository<Game_Personnel,AppDbContext>(uowDbContext);
            // Games = new BaseRepository<Game,AppDbContext>(uowDbContext);
            // GameTeamLists = new BaseRepository<Game_Team_List,AppDbContext>(uowDbContext);
            // GameTeams = new BaseRepository<Game_Team,AppDbContext>(uowDbContext);
            // Leagues = new BaseRepository<League,AppDbContext>(uowDbContext);
            // LeagueTeams = new BaseRepository<League_Team,AppDbContext>(uowDbContext);
            // Persons = new BaseRepository<Person,AppDbContext>(uowDbContext);
            // FRoles = new BaseRepository<Role,AppDbContext>(uowDbContext);
            // Stadiums = new BaseRepository<Stadium,AppDbContext>(uowDbContext);
            // TeamPersons = new BaseRepository<Team_Person,AppDbContext>(uowDbContext);
            // Teams = new BaseRepository<Team,AppDbContext>(uowDbContext);
            // GamePartTypes = new BaseRepository<Game_Part_Type,AppDbContext>(uowDbContext);
            // StadiumAreas = new BaseRepository<Stadium_Area, AppDbContext>(uowDbContext);
        }
        
        
        
        
        
        
        

        public IAreaRepository Areas => GetRepository(() => new AreaRepository(UowDbContext));
        public IClubRepository Clubs => GetRepository(() => new ClubRepository(UowDbContext));
        public IClubTeamRepository ClubTeams => GetRepository(() => new ClubTeamRepository(UowDbContext));
        public IEventTypeRepository EventTypes => GetRepository(() => new EventTypeRepository(UowDbContext));
        public IGameEventRepository GameEvents => GetRepository(() => new GameEventRepository(UowDbContext));
        public IGamePartRepository GameParts => GetRepository(() => new GamePartRepository(UowDbContext));
        public IGamePersonnelRepository GamePersonnel => GetRepository(() => new GamePersonnelRepository(UowDbContext));
        public IGameRepository Games => GetRepository(() => new GameRepository(UowDbContext));
        public IGameTeamListRepository GameTeamLists => GetRepository(() => new GameTeamListRepository(UowDbContext));
        public IGameTeamRepository GameTeams => GetRepository(() => new GameTeamRepository(UowDbContext));
        public ILeagueRepository Leagues => GetRepository(() => new LeagueRepository(UowDbContext));
        public ILeagueTeamRepository LeagueTeams => GetRepository(() => new LeagueTeamRepository(UowDbContext));
        public IPersonRepository Persons => GetRepository(() => new PersonRepository(UowDbContext));
        public IRoleRepository Roles => GetRepository(() => new RoleRepository(UowDbContext));
        public IStadiumRepository Stadiums => GetRepository(() => new StadiumRepository(UowDbContext));
        public ITeamPersonRepository TeamPersons => GetRepository(() => new TeamPersonRepository(UowDbContext));
        public ITeamRepository Teams => GetRepository(() => new TeamRepository(UowDbContext));
        public IGamePartTypeRepository GamePartTypes => GetRepository(() => new GamePartTypeRepository(UowDbContext));

        // public IBaseRepository<Stadium_Area> StadiumAreas { get; }
        // public IBaseRepository<Club> Clubs { get; }
        // public IBaseRepository<Club_Team> ClubTeams { get; }
        // public IBaseRepository<Event_Type> EventTypes { get; }
        // public IBaseRepository<Game_Event> GameEvents { get; }
        // public IBaseRepository<Game_Part> GameParts { get; }
        // public IBaseRepository<Game_Personnel> GamePersonnel { get; }
        // public IBaseRepository<Game> Games { get; }
        // public IBaseRepository<Game_Team_List> GameTeamLists { get; }
        // public IBaseRepository<Game_Team> GameTeams { get; }
        // public IBaseRepository<League> Leagues { get; }
        // public IBaseRepository<League_Team> LeagueTeams { get; }
        // public IBaseRepository<Person> Persons { get; }
        // public IBaseRepository<Role> FRoles { get; }
        // public IBaseRepository<Stadium> Stadiums { get; }
        // public IBaseRepository<Team_Person> TeamPersons { get; }
        // public IBaseRepository<Team> Teams { get; }
        // public IBaseRepository<Game_Part_Type> GamePartTypes { get; }
    }
}