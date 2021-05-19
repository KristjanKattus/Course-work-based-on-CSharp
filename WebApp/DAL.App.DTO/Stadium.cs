using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Domain.Base;

namespace DAL.App.DTO
{
    public class Stadium : DomainEntityId
    {
        public Guid AreaId { get; set; }

        public StadiumArea StadiumArea { get; set; } = default!;

        [MaxLength(32)] 
        public string Name { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        public string PitchType { get; set; } = default!;

        public int Category { get; set; }

        public ICollection<Game>? Games { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
    }
}