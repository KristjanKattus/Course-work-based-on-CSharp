using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class ClubTeam : DomainEntityId
    {
        public Guid TeamId { get; set; }
        public Team? Team { get; set; }

        public Guid ClubId { get; set; }
        public Club Club { get; set; } = default!;
        
        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }
        
        [MaxLength(128)] public string? Description { get; set; }
    }
}