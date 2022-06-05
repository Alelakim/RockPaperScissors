using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public interface IGameService
    {
        public Task<GameResponse?> GetGameAsync(Guid id);
        public Task<GameResponse> CreateNewGame(string name);
        Task<GameResponse> JoinGameAsync(Guid id, string name);
        Task<GameResponse> MakeAMoveAsync(Guid id, string name, string move);
    }
}
