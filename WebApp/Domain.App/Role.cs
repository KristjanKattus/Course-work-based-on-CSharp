using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.App
{
    public class Role
    {
        [MaxLength(32)] public string Name { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        [MaxLength(128)] public string? Description { get; set; }
    }
}