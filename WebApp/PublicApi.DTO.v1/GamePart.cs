using System;

using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GamePart
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public PublicApi.DTO.v1.GamePartType GamePartType { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;

        public int NumberInOrder { get; set; }
    }
}