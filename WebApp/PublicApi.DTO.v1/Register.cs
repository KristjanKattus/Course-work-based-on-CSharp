using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Register
    {
        [Display(ResourceType = typeof(Base.Resources.Areas.Identity.Pages.Account.Register), Name = nameof(Email))]
        public string Email { get; set; } = default!;
        [Display(ResourceType = typeof(Base.Resources.Areas.Identity.Pages.Account.Register), Name = nameof(Password))] 
        public string Password { get; set; } = default!;
        
        [Display(ResourceType = typeof(Base.Resources.Areas.Identity.Pages.Account.Register), Name = nameof(Firstname))]
        public string Firstname { get; set; }= default!;
        
        [Display(ResourceType = typeof(Base.Resources.Areas.Identity.Pages.Account.Register), Name = nameof(Lastname))]
        public string Lastname { get; set; }= default!;
    }
}