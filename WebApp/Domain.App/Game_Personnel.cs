using System;
using Domain.Base;

namespace Domain.App
{
    public class Game_Personnel : DomainEntityId
    {

        public Guid PersonId { get; set; }

        public Person Person { get; set; } = default!;
        

        public Guid GameId { get; set; }

        public Game Game { get; set; } = default!;
        

        public Guid RoleId { get; set; }

        public Role Role { get; set; } = default!;
        

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }
        
    }
}