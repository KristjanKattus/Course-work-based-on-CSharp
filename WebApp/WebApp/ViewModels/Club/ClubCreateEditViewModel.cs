using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Club
{
    public class ClubCreateEditViewModel
    {

        public PublicApi.DTO.v1.Club Club { get; set; } = default!;
        
    }
}