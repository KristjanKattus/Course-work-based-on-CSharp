using System;

namespace PublicApi.DTO.v1
{
    public class AddGame
    {
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;

        public Guid LeagueId { get; set; }

        public Guid? HomeTeamId { get; set; }


        public Guid? AwayTeamId { get; set; }
    }
}