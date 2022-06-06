using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public class GameService : IGameService
    {
        private readonly IRockPaperScissorService _rockPaperScissorService;
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository, IRockPaperScissorService rockPaperScissorService)
        {
            _gameRepository = gameRepository;
            _rockPaperScissorService = rockPaperScissorService;
        }

        //public async Task<GameResponse?> GetGameAsync(Guid id)
        public GameResponse GetGameAsync(Guid id)
        {
            var game = _gameRepository.Find(id);

            //snyggare null check?
            if (game == null || game.Players == null) 
            {
                return new GameResponse { ErrorInfo = "Game or players are not defiened, please check the id or create a new game"};
            }

            return new GameResponse { Id = game.GameId, Players = game.Players.Select(x => x.Name).ToList() };
        }

        //public async Task<GameResponse> CreateNewGame(string name)
        public GameResponse CreateNewGame(string name)
        {
            var game = new Game { GameId = Guid.NewGuid(), Players = new List<Player>() { new Player { Name = name } } };
            _gameRepository.AddGame(game);

            return new GameResponse { Id = game.GameId };
        }
        //REMOVE AYNS FRÅN NAMN
        public GameResponse JoinGameAsync(Guid id, string name)
        {

            var game = _gameRepository.Find(id);
            if (game == null) return new GameResponse { ErrorInfo = "The game you are trying to join does not exist" };
            if (game.Players.Count >= 2) return new GameResponse { ErrorInfo = "There is already two players playing this game" };

            game.Players.Add(new Player { Name = name });
            _gameRepository.UpdateGame(game);
            return new GameResponse { Id = game.GameId, Players = game.Players.Select(x => x.Name).ToList() };
        }
        // REMOVE ASYNC FRÅN NAMN
        public GameResponse MakeAMoveAsync(Guid id, string name, string move)
        {
            var game = _gameRepository.Find(id);
            if (game == null) return new GameResponse { ErrorInfo = "The game you are trying to join does not exist" };
            if (!game.Players.Any(x => x.Name.ToLower().Equals(name.ToLower()))) return new GameResponse { ErrorInfo = name + " is not at player of the game" };

            game.Players.Where(x => x.Name.ToLower().Equals(name.ToLower())).Select(y => y.Move = move);

            if (game.Players.Any(x => !string.IsNullOrEmpty(x.Move)))
            {
                game.Result = _rockPaperScissorService.RunGame(game.Players);
            }

            _gameRepository.UpdateGame(game);

            return new GameResponse { Id = id, Players = game.Players.Select(x => x.Name).ToList(), Result = game.Result };

        }

    }
}
