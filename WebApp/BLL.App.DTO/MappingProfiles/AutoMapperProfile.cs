using AutoMapper;

namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Club, DAL.App.DTO.Club>().ReverseMap();
            CreateMap<BLL.App.DTO.ClubTeam, DAL.App.DTO.ClubTeam>().ReverseMap();
            CreateMap<BLL.App.DTO.EventType, DAL.App.DTO.EventType>().ReverseMap();
            CreateMap<BLL.App.DTO.Game, DAL.App.DTO.Game>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePart, DAL.App.DTO.GamePart>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePartType, DAL.App.DTO.GamePartType>().ReverseMap();
            CreateMap<BLL.App.DTO.GameEvent, DAL.App.DTO.GameEvent>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePersonnel, DAL.App.DTO.GamePersonnel>().ReverseMap();
            CreateMap<BLL.App.DTO.GameTeam, DAL.App.DTO.GameTeam>().ReverseMap();
            CreateMap<BLL.App.DTO.GameTeamList, DAL.App.DTO.GameTeamList>().ReverseMap();
            CreateMap<BLL.App.DTO.League, DAL.App.DTO.League>().ReverseMap();
            CreateMap<BLL.App.DTO.LeagueTeam, DAL.App.DTO.LeagueTeam>().ReverseMap();
            CreateMap<BLL.App.DTO.Person, DAL.App.DTO.Person>().ReverseMap();
            CreateMap<BLL.App.DTO.Role, DAL.App.DTO.Role>().ReverseMap();
            CreateMap<BLL.App.DTO.Stadium, DAL.App.DTO.Stadium>().ReverseMap();
            CreateMap<BLL.App.DTO.StadiumArea, DAL.App.DTO.StadiumArea>().ReverseMap();
            CreateMap<BLL.App.DTO.Team, DAL.App.DTO.Team>().ReverseMap();
            CreateMap<BLL.App.DTO.TeamPerson, DAL.App.DTO.TeamPerson>().ReverseMap();
            CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<BLL.App.DTO.Identity.AppRole, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            
        }
    }
}