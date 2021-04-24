using System;
using Domain.Base;

namespace BLL.App.DTO
{
    public class GamePart : DomainEntityId
    {
        public Guid TypeId { get; set; }
        public GamePartType GamePartType { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public int NumberInOrder { get; set; }
    }
}