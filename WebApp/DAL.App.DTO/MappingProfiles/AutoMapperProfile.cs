using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.App.DTO.Club, Domain.App.Club>().ReverseMap();
            CreateMap<DAL.App.DTO.ClubTeam, Domain.App.Club_Team>().ReverseMap();
            CreateMap<DAL.App.DTO.EventType, Domain.App.Event_Type>().ReverseMap();
            CreateMap<DAL.App.DTO.Game, Domain.App.Game>().ReverseMap();
            CreateMap<DAL.App.DTO.GameEvent, Domain.App.Game_Event>().ReverseMap();
            CreateMap<DAL.App.DTO.GamePart, Domain.App.Game_Part>().ReverseMap();
            CreateMap<DAL.App.DTO.GamePartType, Domain.App.Game_Part_Type>().ReverseMap();
            CreateMap<DAL.App.DTO.GamePersonnel, Domain.App.Game_Personnel>().ReverseMap();
            CreateMap<DAL.App.DTO.GameTeam, Domain.App.Game_Team>().ReverseMap();
            CreateMap<DAL.App.DTO.GameTeamList, Domain.App.Game_Team_List>().ReverseMap();
            CreateMap<DAL.App.DTO.League, Domain.App.League>().ReverseMap();
            CreateMap<DAL.App.DTO.LeagueTeam, Domain.App.League_Team>().ReverseMap();
            CreateMap<DAL.App.DTO.Person, Domain.App.Person>().ReverseMap();
            CreateMap<DAL.App.DTO.Role, Domain.App.Role>().ReverseMap();
            CreateMap<DAL.App.DTO.Stadium, Domain.App.Stadium>().ReverseMap();
            CreateMap<DAL.App.DTO.StadiumArea, Domain.App.Stadium_Area>().ReverseMap();
            CreateMap<DAL.App.DTO.Team, Domain.App.Team>().ReverseMap();
            CreateMap<DAL.App.DTO.TeamPerson, Domain.App.Team_Person>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppUser>().ReverseMap();
            
        }
    }
}