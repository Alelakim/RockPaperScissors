using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public class GameService : IGameService
    {
        private readonly GamesContext _context;
        private readonly IRockPaperScissorService _rockPaperScissorService;

        public GameService(GamesContext context, IRockPaperScissorService rockPaperScissorService)
        {
            _context = context;
            _rockPaperScissorService = rockPaperScissorService;
        }

        public async Task<GameResponse?> GetGameAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id).ConfigureAwait(false);

            //snyggare null check?
            if (game == null || game.Players == null) 
            {
                return null;
            }

            return new GameResponse { Id = game.Id, Players = game.Players.Select(x => x.Name).ToList() };
        }

        public async Task<GameResponse> CreateNewGame(string name)
        {
            if (!ValidGamesContext()) return new GameResponse { ErrorInfo = "No valid context" };
            var player = new Player { Id = Guid.NewGuid(), Name = name, Move = "" };
            _context.Players.Add(player);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var test = await _context.Players.FindAsync(player.Id).ConfigureAwait(false);

            var game = new Game { Id = Guid.NewGuid(), Name = "Mikaela får spel", Players = new List<Player>() { player }, Result = new Result { Draw = true } };
            // game.Players.Add(new Player { Name = name });
            
            _context.Games.Add(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var testing = game;
            return new GameResponse { Id = game.Id };
        }

        public async Task<GameResponse> JoinGameAsync(Guid id, string name)
        {
            if (!ValidGamesContext()) return new GameResponse { ErrorInfo = "No valid context"};
            if (!GameExists(id)) return new GameResponse { ErrorInfo = "The game you are trying to join does not exist" };

            var game = await _context.Games.FindAsync(id).ConfigureAwait(false);
            if (game.Players.Count >= 2) return new GameResponse { ErrorInfo = "There is already two players playing this game" };

            game.Players.Add(new Player { Name = name });
            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return new GameResponse { Id = game.Id, Players = game.Players.Select(x => x.Name).ToList() };
        }

        public async Task<GameResponse> MakeAMoveAsync(Guid id, string name, string move)
        {
            if (!ValidGamesContext()) return new GameResponse { ErrorInfo = "No valid context" };
            if (!GameExists(id)) return new GameResponse { ErrorInfo = "The game you are trying to join does not exist" };

            var game = await _context.Games.FindAsync(id).ConfigureAwait(false);
            if (!game.Players.Any(x => x.Name.ToLower().Equals(name.ToLower()))) return new GameResponse { ErrorInfo = name + " is not at player of the game" };

            game.Players.Where(x => x.Name.ToLower().Equals(name.ToLower())).Select(y => y.Move = move);

            if (game.Players.Any(x => !string.IsNullOrEmpty(x.Move)))
            {
                game.Result = _rockPaperScissorService.RunGame(game.Players);
            }
            _context.Entry(game).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return new GameResponse { Id = id, Players = game.Players.Select(x => x.Name).ToList(), Result = game.Result };

        }

        public bool ValidGamesContext()
        {
            if (_context.Games == null)
            {
                return false;
            }
            return true;
        }

        private bool GameExists(Guid id)
        {
            return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
