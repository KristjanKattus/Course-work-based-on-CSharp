
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Person
{
    public class PersonCreateEditViewModel
    {
        public PublicApi.DTO.v1.Person Person { get; set; } = default!;

    }
}