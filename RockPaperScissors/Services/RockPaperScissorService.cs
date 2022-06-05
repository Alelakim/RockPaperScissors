using RockPaperScissors.Models;

namespace RockPaperScissors.Services
{
    public class RockPaperScissorService : IRockPaperScissorService
    {
        public RockPaperScissorService()
        {

        }

        public Result RunGame(List<Player> players)
        {
            var playerOne = players[0];
            var playerTwo = players[1];

            //var result = playerOne.Move.ToLower() switch
            //{
            //"rock" => if (playerTwo.Move.ToLower().Equals("rock")) { return new Result { Draw = true }; }

            switch (playerOne.Move.ToLower())

            {
                case "rock":
                    if (playerTwo.Move.ToLower().Equals("rock"))
                    {
                        return new Result { Draw = true };
                    }
                    else if (playerTwo.Move.ToLower().Equals("paper"))
                    {
                        return new Result { Winner = playerOne, Loser = playerTwo };
                    }
                    else
                    {
                        return new Result { Winner = playerTwo, Loser = playerOne };
                    }

                case "paper":

                    if (playerTwo.Move.ToLower().Equals("rock"))

                    {
                        return new Result { Winner = playerOne, Loser = playerTwo };

                    }

                    else if (playerTwo.Move.ToLower().Equals("paper"))

                    {
                        return new Result { Draw = true };

                    }

                    else

                    {
                        return new Result { Winner = playerTwo, Loser = playerOne };

                    }

                case "scissors":

                    if (playerTwo.Move.ToLower().Equals("rock"))

                    {
                        return new Result { Winner = playerTwo, Loser = playerOne };

                    }

                    else if (playerTwo.Move.ToLower().Equals("paper"))

                    {
                        return new Result { Winner = playerOne, Loser = playerTwo };

                    }

                    else

                    {
                        return new Result { Draw = true };

                    }

                default:

                    return null;

            }


        }
    }
}

