using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class GameEvent : DomainEntityId
    {

        public Guid GameId { get; set; }
        public Game Game { get; set; } = default!;

        public Guid GamePersonnelId { get; set; }
        public GamePersonnel? GamePersonnel { get; set; }
        
        public Guid GameTeamListId { get; set; }
        public GameTeamList? GameTeamList { get; set; }

        public Guid GamePartId { get; set; }
        public GamePart GamePart { get; set; } = default!;

        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; } = default!;

        public DateTime GameTime { get; set; }

        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
        
    }
}