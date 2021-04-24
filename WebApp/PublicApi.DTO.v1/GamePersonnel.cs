using System;
using Domain.Base;

namespace PublicApi.DTO.v1
{
    public class GamePersonnel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public PublicApi.DTO.v1.Person Person { get; set; } = default!;
        

        public Guid GameId { get; set; }

        public PublicApi.DTO.v1.Game Game { get; set; } = default!;
        

        public Guid RoleId { get; set; }

        public PublicApi.DTO.v1.Role Role { get; set; } = default!;
        

        public DateTime Since { get; set; } = DateTime.Now;

        public DateTime? Until { get; set; }
        
    }
}