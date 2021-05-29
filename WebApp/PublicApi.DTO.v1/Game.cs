using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid StadiumId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Game), Name = nameof(Stadium))]
        public Stadium? Stadium { get; set; }

        public Guid LeagueId { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Game), Name = nameof(GameLength))]
        public int GameLength { get; set; } = 90;
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Game), Name = nameof(GameEvents))]
        public ICollection<GameEvent>? GameEvents { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Game), Name = nameof(MatchRound))]
        public int MatchRound { get; set; }
        
    }
}