using System;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GamePart : DomainEntityId
    {
        public Guid TypeId { get; set; }
        public Game_Part_Type GamePartType { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public int NumberInOrder { get; set; }
    }
}