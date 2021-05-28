using System;

namespace PublicApi.DTO.v1
{
    public class AddGameMember
    {
        public Guid PersonId { get; set; }
        public PublicApi.DTO.v1.TeamPerson? Person { get; set; }

        public bool PartOfGame { get; set; }
        public bool InStartingLineup { get; set; }
        public bool Staff { get; set; }
    }
}