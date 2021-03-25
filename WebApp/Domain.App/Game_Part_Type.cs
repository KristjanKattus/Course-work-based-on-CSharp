using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Game_Part_Type : DomainEntityId
    {
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}