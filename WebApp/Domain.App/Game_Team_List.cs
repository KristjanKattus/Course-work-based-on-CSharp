using System;
using Domain.Base;

namespace Domain.App
{
    public class Game_Team_List : DomainEntityId
    {
        public Guid? PersonId { get; set; }
        public Person? Person { get; set; }
        
        public Guid? TeamPersonId { get; set; }
        public Team_Person? TeamPerson { get; set; }
        

        public Guid GameTeamId { get; set; }
        public Game_Team? GameTeam { get; set; }
        

        public Guid? RoleId { get; set; } 
        public Role? Role { get; set; }
        
        public bool InStartingLineup { get; set; }
        public bool Staff { get; set; }
        
        
        
    }
}