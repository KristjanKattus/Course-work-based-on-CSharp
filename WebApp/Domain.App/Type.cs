﻿using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Type : DomainEntityId
    {
        [MaxLength(32)]
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}