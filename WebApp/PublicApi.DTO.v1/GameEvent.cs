using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GameEvent
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public PublicApi.DTO.v1.Game Game { get; set; } = default!;

        public Guid GamePersonnelId { get; set; }

        public PublicApi.DTO.v1.GamePersonnel? GamePersonnel { get; set; }

        public Guid GamePartId { get; set; }

        public PublicApi.DTO.v1.GamePart GamePart { get; set; } = default!;

        public Guid EventTypeId { get; set; }

        public PublicApi.DTO.v1.EventType EventType { get; set; } = default!;

        public DateTime GameTime { get; set; }

        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
        
    }
}