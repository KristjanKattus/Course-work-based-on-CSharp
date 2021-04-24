using System;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class League
    {
        public Guid Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; } = default!;

        public int Duration { get; set; }

        [MaxLength(128)]
        public string? Description { get; set; }
    }
}