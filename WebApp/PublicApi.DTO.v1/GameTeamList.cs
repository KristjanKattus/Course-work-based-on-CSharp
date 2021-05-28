using System;
using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GameTeamList
    {
        public Guid Id { get; set; }
        public Guid? PersonId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeamList), Name = nameof(Person))]
        public Person? Person { get; set; }
        
        //TODO translate
        public Guid? TeamPersonId { get; set; }
        public TeamPerson? TeamPerson { get; set; }
        

        public Guid GameTeamId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeamList), Name = nameof(GameTeam))]
        public GameTeam? GameTeam { get; set; }
        
        public Guid? RoleId { get; set; }
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeamList), Name = nameof(Role))]
        public Role? Role { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeamList), Name = nameof(InStartingLineup))]
        public bool InStartingLineup { get; set; }
        
        [Display(ResourceType = typeof(Resources.PublicApi.DTO.v1.GameTeamList), Name = nameof(Staff))]
        public bool Staff { get; set; }
        
        
        
    }
}