using System;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameTeamList : DomainEntityId
    {
        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }

        public Guid? TeamPersonId { get; set; }
        public TeamPerson? TeamPerson { get; set; }
        
        public Guid GameTeamId { get; set; }
        public GameTeam? GameTeam { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
        
        public bool InStartingLineup { get; set; }
        public bool Staff { get; set; }
        
        
        
    }
}