namespace GameStuff.Models
{
    public interface IGameStuffUnitOfWork
    {
        Repository<Game> Games { get; }
        Repository<Developer> Developers { get; }
        Repository<GameDeveloper> GameDevelopers { get; }
        Repository<Genre> Genres { get; }

        void DeleteCurrentGameDevelopers(Game game);
        void LoadNewGameDevelopers(Game game, int[] authorids);
        void Save();
    }
}
