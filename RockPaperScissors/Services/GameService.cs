using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Models;
using System.Net;

namespace RockPaperScissors.Services
{
    public class GameService
    {
        private readonly GamesContext _context;

        public GameService(GamesContext context)
        {
            _context = context;
        }

        public async Task<GameResponse?> GetGameAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);

            //snyggare null check?
            if (game == null || game.Players == null) 
            {
                return null;
            }

            return new GameResponse { Id = game.Id, Players = game.Players.Select(x => x.Name).ToList() };
        }

        public async Task<GameResponse> CreateNewGame(string name)
        {
            if (!ValidGamesContext()) return new GameResponse { errorInfo = "No valid context" };
            var game = new Game { Id = Guid.NewGuid() };
            game.Players.Add(new Player { Name = name });

            _context.Games.Add(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return new GameResponse { Id = game.Id };
        }

        public async Task<GameResponse> JoinGameAsync(Guid id, string name)
        {
            if (!ValidGamesContext()) return new GameResponse { errorInfo = "No valid context"};
            if (!GameExists(id)) return new GameResponse { errorInfo = "The game you are trying to join does not exist" };

            var game = await _context.Games.FindAsync(id);
            if (game.Players.Count >= 2) return new GameResponse { errorInfo = "There is already two players playing this game" };

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
