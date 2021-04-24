using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class GameEvent : DomainEntityId
    {

        public Guid GameId { get; set; }

        public Game Game { get; set; } = default!;

        public Guid GamePersonnelId { get; set; }

        public Game_Personnel? GamePersonnel { get; set; }

        public Guid GamePartId { get; set; }

        public Game_Part GamePart { get; set; } = default!;

        public Guid EventTypeId { get; set; }

        public Event_Type EventType { get; set; } = default!;

        public DateTime GameTime { get; set; }

        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
        
    }
}