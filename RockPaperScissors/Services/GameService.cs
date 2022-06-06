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

       
        public GameResponse GetGame(Guid id)
        {
            var game = _gameRepository.Find(id);
            if (game == null || game.Players == null) return new GameResponse { ErrorInfo = "Game or players are not defiened, please check the id or create a new game"};

            return new GameResponse { Id = game.GameId, Players = game.Players.Select(x => x.Name).ToList(), Result = game.Result };
        }

        
        public GameResponse CreateNewGame(string name)
        {
            var game = new Game { GameId = Guid.NewGuid(), Players = new List<Player>() { new Player { Name = name } } };
            _gameRepository.AddGame(game);

            return new GameResponse { Id = game.GameId, Players = game.Players.Select(x => x.Name).ToList() };
        }
       
        public GameResponse JoinGame(Guid id, string name)
        {
            var game = _gameRepository.Find(id);
            if (game == null) return new GameResponse { ErrorInfo = "The game you are trying to join does not exist" };
            if (game.Players.Count >= 2) return new GameResponse { ErrorInfo = "There is already two players playing this game" };
            if (game.Players.Any(x => x.Name.ToLower().Equals(name.ToLower()))) return new GameResponse { ErrorInfo = "The first player already enter the given name, please select a unique name" };

            game.Players.Add(new Player { Name = name });
            _gameRepository.UpdateGame(game);
            return new GameResponse { Id = game.GameId, Players = game.Players.Select(x => x.Name).ToList() };
        }
      
        public GameResponse MakeAMove(Guid id, string name, string move)
        {
            var game = _gameRepository.Find(id);

            if (game == null) return new GameResponse { ErrorInfo = "The game you are trying to join does not exist" };
            if (!game.Players.Any(x => x.Name.ToLower().Equals(name.ToLower()))) return new GameResponse { ErrorInfo = name + " is not at player of the game" };
            if (move.ToLower().Equals("rock") || move.ToLower().Equals("paper") || move.ToLower().Equals("scissors"))
            {
                game.Players.Where(x => x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault().Move = move;

                if (game.Players.Count == 2 && game.Players.All(x => !string.IsNullOrEmpty(x.Move)))
                {
                    game.Result = _rockPaperScissorService.RunGame(game.Players);
                }

                _gameRepository.UpdateGame(game);

                return new GameResponse { Id = id, Players = game.Players.Select(x => x.Name).ToList(), Result = game.Result };
            }
            return new GameResponse { ErrorInfo = "Mind your spelling and please enter a correct move: Rock, Paper or Sciccors" };
        }

    }
}
