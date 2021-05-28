using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Game_Event : DomainEntityId
    {

        public Guid GameId { get; set; }
        public Game? Game { get; set; }


        public Guid GameTeamListId { get; set; }
        public Game_Team_List? GameTeamList { get; set; }

        

        public Guid EventTypeId { get; set; }
        public Event_Type? EventType { get; set; }
        

        public int GameTime { get; set; }

        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
        
    }
}