namespace BLL.App.DTO
{
    public class AddGameMember
    {
        public TeamPerson Person { get; set; } = default!;

        public bool PartOfGame { get; set; }
        public bool InStartingLineup { get; set; }
        public bool Staff { get; set; }
    }
}