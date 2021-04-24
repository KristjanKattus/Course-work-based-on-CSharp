using System;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GamePersonnel : DomainEntityId
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