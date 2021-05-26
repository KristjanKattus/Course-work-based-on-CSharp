using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GamePart
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GamePart), Name = nameof(GamePartType))]
        public PublicApi.DTO.v1.GamePartType GamePartType { get; set; } = default!;
        
        public Guid GameId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GamePart), Name = nameof(Game))]
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GamePart), Name = nameof(NumberInOrder))]
        public int NumberInOrder { get; set; }
    }
}