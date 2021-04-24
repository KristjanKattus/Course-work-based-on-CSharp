using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Stadium
    {
        public Guid Id { get; set; }
        public Guid AreaId { get; set; }

        public PublicApi.DTO.v1.StadiumArea StadiumArea { get; set; } = default!;

        [MaxLength(32)] 
        public string Name { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        public string PitchType { get; set; } = default!;

        public int Category { get; set; }

        public ICollection<PublicApi.DTO.v1.Game>? Games { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
    }
}