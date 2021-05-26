using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GamePersonnel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GamePersonnel), Name = nameof(Person))]
        public PublicApi.DTO.v1.Person Person { get; set; } = default!;
        

        public Guid GameId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GamePersonnel), Name = nameof(Game))]
        public PublicApi.DTO.v1.Game Game { get; set; } = default!;
        

        public Guid RoleId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GamePersonnel), Name = nameof(Role))]
        public PublicApi.DTO.v1.Role Role { get; set; } = default!;
        
        
        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Since))]
        public DateTime Since { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.Common), Name = nameof(Until))]
        public DateTime? Until { get; set; }
        
    }
}