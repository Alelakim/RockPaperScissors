namespace RockPaperScissors.Models
{
    public interface IGameRepository
    {
        Guid AddGame(Game game);
        Game Find(Guid id);
        void UpdateGame(Game game);
    }
}
