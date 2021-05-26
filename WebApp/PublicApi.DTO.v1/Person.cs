using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Person
    {
        public Guid Id { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Person), Name = nameof(FirstName))]
        [MaxLength(32)] public string FirstName { get; set; } = default!;
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Person), Name = nameof(LastName))]
        [MaxLength(48)] public string LastName { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Person), Name = nameof(Date))]
        public DateTime Date { get; set; } = default!;

        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Person), Name = nameof(Sex))]
        public Char Sex { get; set; } = default!;
        
        public Guid AppUserId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.Person), Name = nameof(AppUser))]
        public AppUser? AppUser { get; set; }
    }
}