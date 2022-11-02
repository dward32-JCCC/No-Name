using System.Collections.Generic;

namespace GameStuff.Models
{
    public class GameDTO
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    public Dictionary<int, string> Developers { get; set; }

        public void Load(Game game)
        {
            Image = game.Image;
            GameId = game.GameId;
            Title = game.Title;
            Price = game.Price;
            Developers = new Dictionary<int, string>();
            foreach (GameDeveloper ba in game.GameDevelopers) {
                Developers.Add(ba.Developer.DeveloperId, ba.Developer.FullName);
            }
        }
    }

}
