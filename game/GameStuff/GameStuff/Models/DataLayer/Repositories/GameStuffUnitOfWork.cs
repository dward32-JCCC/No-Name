using System.Linq;

namespace GameStuff.Models
{
    public class GameStuffUnitOfWork : IGameStuffUnitOfWork
    {
        private GameStuffContext context { get; set; }
        public GameStuffUnitOfWork(GameStuffContext ctx) => context = ctx;

        private Repository<Game> gameData;
        public Repository<Game> Games {
            get {
                if (gameData == null)
                    gameData = new Repository<Game>(context);
                return gameData;
            }
        }

        private Repository<Developer> DeveloperData;
        public Repository<Developer> Developers {
            get {
                if (DeveloperData == null)
                    DeveloperData = new Repository<Developer>(context);
                return DeveloperData;
            }
        }

        private Repository<GameDeveloper> gamedeveloperData;
        public Repository<GameDeveloper> GameDevelopers {
            get {
                if (gamedeveloperData == null)
                    gamedeveloperData = new Repository<GameDeveloper>(context);
                return gamedeveloperData;
            }
        }

        private Repository<Genre> genreData;
        public Repository<Genre> Genres
        {
            get {
                if (genreData == null)
                    genreData = new Repository<Genre>(context);
                return genreData;
            }
        }

        public void DeleteCurrentGameDevelopers(Game game)
        {
            var currentDevelopers = GameDevelopers.List(new QueryOptions<GameDeveloper> {
                Where = ba => ba.GameId == game.GameId
            });
            foreach (GameDeveloper ba in currentDevelopers) {
                GameDevelopers.Delete(ba);
            }
        }

        public void LoadNewGameDevelopers(Game game, int[] gameids)
        {
            game.GameDevelopers = gameids.Select(i =>
                new GameDeveloper { Game = game, DeveloperId = i })
                .ToList();
        }

        public void Save() => context.SaveChanges();
    }
}