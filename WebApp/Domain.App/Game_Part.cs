using System;
using Domain.Base;

namespace Domain.App
{
    public class Game_Part : DomainEntityId
    {
        public Guid TypeId { get; set; }
        public Type Type { get; set; } = default!;
        
        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public int NumberInOrder { get; set; }
    }
}