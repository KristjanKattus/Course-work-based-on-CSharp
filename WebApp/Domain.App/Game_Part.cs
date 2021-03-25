using System;
using Domain.Base;

namespace Domain.App
{
    public class Game_Part : DomainEntityId
    {
        public Guid TypeId { get; set; }
        public Game_Part_Type GamePartType { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public int NumberInOrder { get; set; }
    }
}