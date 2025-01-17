﻿using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class League : DomainEntityId
    {
        [MaxLength(32)]
        public string Name { get; set; } = default!;

        public int Duration { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
    }
}