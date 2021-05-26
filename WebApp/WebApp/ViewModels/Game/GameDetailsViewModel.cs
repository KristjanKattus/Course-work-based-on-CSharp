namespace WebApp.ViewModels.Game
{
    public class GameDetailsViewModel
    {
        public PublicApi.DTO.v1.Game? Game { get; set; }

        public PublicApi.DTO.v1.GameTeam? HomeTeam { get; set; }
        
        public PublicApi.DTO.v1.GameTeam? AwayTeam { get; set; }
        
    }
}