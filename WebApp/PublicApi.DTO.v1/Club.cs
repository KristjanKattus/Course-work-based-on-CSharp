using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ClubAdd
    {
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
    public class Club
    {
        public Guid Id { get; set; }
        
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        public DateTime Since { get; set; } = DateTime.Now;
        
        public DateTime? Until { get; set; }
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}