
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Role
{
    public class RoleCreateEditViewModel
    {
        public PublicApi.DTO.v1.Role Role { get; set; } = default!;

    }
}