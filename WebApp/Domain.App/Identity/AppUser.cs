using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [StringLength(128, MinimumLength = 1)]
        public string Firstname { get; set; } = default!;
        [StringLength(128, MinimumLength = 1)]
        public string Lastname { get; set; } = default!;

        public ICollection<Person>? Persons { get; set; }
        
        [Range(1, Int32.MaxValue)]
        public  EGender Gender { get; set; }
        public string FullName => Firstname + " " + Lastname;
        public string FullNameEmail => FullName + " (" + Email + ")";
    }
}