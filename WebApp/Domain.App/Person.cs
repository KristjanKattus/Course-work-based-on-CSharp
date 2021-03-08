using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.App
{
    public class Person
    {
        
        
        [MaxLength(32)] public string FirstName { get; set; } = default!;
        
        [MaxLength(48)] public string LastName { get; set; } = default!;

        public DateTime Date { get; set; } = default!;

        public Char Sex { get; set; } = default!;
        
        
    }
}