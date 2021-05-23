using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameEvent : DomainEntityId
    {

        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public Guid GameTeamListId { get; set; }
        public GameTeamList? GameTeamList { get; set; }

        public Guid GamePartId { get; set; }
        public GamePart? GamePart { get; set; }

        public Guid EventTypeId { get; set; }
        public EventType? EventType { get; set; }

        public DateTime GameTime { get; set; }

        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
        
    }
}