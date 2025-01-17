﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace BLL.App.DTO
{
    public class StadiumArea : DomainEntityId
    {
        
        [MaxLength(32)]
        public string Name { get; set; } = default!;

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }

        public ICollection<Stadium>? Stadiums { get; set; }
        
    }
}