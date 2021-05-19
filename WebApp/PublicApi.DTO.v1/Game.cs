using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid StadiumId { get; set; }
        public PublicApi.DTO.v1.Stadium? Stadium { get; set; }

        public int GameLength { get; set; }

        public ICollection<PublicApi.DTO.v1.GameEvent>? GameEvents { get; set; }
        public int MatchRound { get; set; }
    }
}