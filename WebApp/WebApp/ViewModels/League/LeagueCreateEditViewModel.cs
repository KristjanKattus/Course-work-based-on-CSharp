
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.League
{
    public class LeagueCreateEditViewModel
    {
        public PublicApi.DTO.v1.League League { get; set; } = default!;
    }
}