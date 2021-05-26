using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Login
    {
        [Display(ResourceType = typeof(Base.Resources.Areas.Identity.Pages.Account.Register), Name = nameof(Email))]
        public string Email { get; set; } = default!;
        [Display(ResourceType = typeof(Base.Resources.Areas.Identity.Pages.Account.Register), Name = nameof(Password))]
        public string Password { get; set; } = default!;
    }
}