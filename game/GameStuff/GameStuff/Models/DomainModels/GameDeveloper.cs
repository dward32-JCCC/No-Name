namespace GameStuff.Models
{
    public class GameDeveloper
    {
        public int GameId { get; set; }
        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }
        public Game Game { get; set; }
    }
}
