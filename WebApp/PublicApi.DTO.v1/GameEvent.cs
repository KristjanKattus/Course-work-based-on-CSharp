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
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameEvent), Name = nameof(Game))]
        public Game? Game { get; set; }

        
        public Guid GameTeamListId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameEvent), Name = nameof(GameTeamList))]
        public GameTeamList? GameTeamList { get; set; }

        public Guid GamePartId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameEvent), Name = nameof(GamePart))]
        public GamePart? GamePart { get; set; }

        public Guid EventTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameEvent), Name = nameof(EventType))]
        public EventType? EventType { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameEvent), Name = nameof(GameTime))]
        public int GameTime { get; set; }

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameEvent), Name = nameof(NumberInOrder))]
        public int NumberInOrder { get; set; }

        [MaxLength(128)]
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Description))]
        public string? Description { get; set; }
        
    }
}