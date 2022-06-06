namespace RockPaperScissors.Models
{
    public class GameMapper : IGameRepository
    {
        public static Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

        public Guid AddGame(Game game)
        {
            _games.Add(game.GameId, game);
            return game.GameId;
        }

        public Game Find(Guid id)
        {
            _games.TryGetValue(id, out var game);
            return game;
        }

        public void UpdateGame(Game game)
        {
            _games.Remove(game.GameId);
            _games.Add(game.GameId, game);
        }

    }
}
