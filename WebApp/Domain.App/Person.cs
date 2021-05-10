using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Person : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        
        
        [MaxLength(32)] public string FirstName { get; set; } = default!;
        
        [MaxLength(48)] public string LastName { get; set; } = default!;

        public DateTime Date { get; set; } = default!;

        public Char Sex { get; set; } = default!;

        
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;


    }
}