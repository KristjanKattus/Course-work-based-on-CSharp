using AutoMapper;

namespace PublicApi.DTO.v1.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Club, ClubAdd>().ReverseMap();
            CreateMap<BLL.App.DTO.Club, Club>().ReverseMap();
            CreateMap<BLL.App.DTO.ClubTeam, ClubTeam>().ReverseMap();
            CreateMap<BLL.App.DTO.EventType, EventType>().ReverseMap();
            CreateMap<BLL.App.DTO.Game, Game>().ReverseMap();
            CreateMap<BLL.App.DTO.GameEvent, GameEvent>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePart, GamePart>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePartType, GamePartType>().ReverseMap();
            CreateMap<BLL.App.DTO.GamePersonnel, GamePersonnel>().ReverseMap();
            CreateMap<BLL.App.DTO.GameTeam, GameTeam>().ReverseMap();
            CreateMap<BLL.App.DTO.GameTeamList, GameTeamList>().ReverseMap();
            CreateMap<BLL.App.DTO.League, LeagueTeam>().ReverseMap();
            CreateMap<BLL.App.DTO.Person, Person>().ReverseMap();
            CreateMap<BLL.App.DTO.Role, Role>().ReverseMap();
            CreateMap<BLL.App.DTO.Stadium, Stadium>().ReverseMap();
            CreateMap<BLL.App.DTO.StadiumArea, StadiumArea>().ReverseMap();
            CreateMap<BLL.App.DTO.Team, Team>().ReverseMap();
            CreateMap<BLL.App.DTO.TeamPerson, TeamPerson>().ReverseMap();
        }
    }
}