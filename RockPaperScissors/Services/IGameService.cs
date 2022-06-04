using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        public Task<Game?> GetGameAsync(Guid id);
        public Task<Guid> CreateNewGame(string name);
        bool ValidGamesContext();
    }
}
