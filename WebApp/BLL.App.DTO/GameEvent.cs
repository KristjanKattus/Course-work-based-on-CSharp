using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class GameEvent : DomainEntityId
    {

        public Guid GameId { get; set; }
        public Game? Game { get; set; } 

        public Guid GameTeamListId { get; set; }
        public GameTeamList? GameTeamList { get; set; }



        public Guid EventTypeId { get; set; }
        public EventType? EventType { get; set; }

        public int GameTime { get; set; }

        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
        
    }
}