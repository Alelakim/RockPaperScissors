using Microsoft.AspNetCore.Mvc;
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

        public async Task<Game?> GetGameAsync(Guid id)
        {
            var game = await _context.Games.FindAsync(id);

            return game;
        }

        public async Task<Guid> CreateNewGame(string name)
        {
            var game = new Game { Id = Guid.NewGuid(), PlayerOne = new Player { Name = name } };
            _context.Games.Add(game);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return game.Id;
        }

        public bool ValidGamesContext()
        {
            if (_context.Games == null)
            {
                return false;
            }
            return true;
        }
    }
}
