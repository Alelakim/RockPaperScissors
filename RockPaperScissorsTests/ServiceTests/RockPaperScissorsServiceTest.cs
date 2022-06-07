using FluentAssertions;
using NUnit.Framework;
using RockPaperScissors.Models;
using RockPaperScissors.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsTests.ServiceTests
{
    public class RockPaperScissorsServiceTest
    {
        private RockPaperScissorService _service;
        [SetUp]
        public void Setup()
        {
            _service = new RockPaperScissorService();
        }

        [TestCase("rock","scissors")]
        [TestCase("Paper", "rock")]
        [TestCase("scissors", "paper")]
        [Test]
        public void RunGame_ShouldReturnMuminAsWinner_WhenGameIsPlayed(string move, string otherMove)
        {
            //Arrange
            var playerOne = new Player { Name = "Mumin", Move = move };
            var playerTwo = new Player { Name = "Morran", Move = otherMove };
            var players = new List<Player> { playerOne, playerTwo };
            //Act
            var result = _service.RunGame(players);

            //Assert
            result.Winner.Name.Should().Contain(players[0].Name);
        }

        [TestCase("Scissors", "rock")]
        [TestCase("rock", "paper")]
        [TestCase("paper", "scissors")]
        [Test]
        public void RunGame_ShouldReturnMuminAsLoser_WhenGameIsPlayed(string move, string otherMove)
        {
            //Arrange
            var playerOne = new Player { Name = "Mumin", Move = move };
            var playerTwo = new Player { Name = "Morran", Move = otherMove };
            var players = new List<Player> { playerOne, playerTwo };
            //Act
            var result = _service.RunGame(players);

            //Assert
            result.Loser.Name.Should().Contain(players[0].Name);
        }

        [TestCase("scissors", "scissors")]
        [TestCase("rock", "rock")]
        [TestCase("Paper", "paper")]
        [Test]
        public void RunGame_ShouldReturnDraw_WhenSameMoveIsPlayed(string move, string otherMove)
        {
            //Arrange
            var playerOne = new Player { Name = "Mumin", Move = move };
            var playerTwo = new Player { Name = "Morran", Move = otherMove };
            var players = new List<Player> { playerOne, playerTwo };
            //Act
            var result = _service.RunGame(players);

            //Assert
            result.Draw.Should().BeTrue();
        }

        [Test]
        public void RunGame_ShouldReturnNull_WhenNotCorrectMoves()
        {
            //Arrange
            var playerOne = new Player { Name = "Mumin", Move = "rocks" };
            var playerTwo = new Player { Name = "Morran", Move = "papers" };
            var players = new List<Player> { playerOne, playerTwo };
            //Act
            var result = _service.RunGame(players);

            //Assert
            result.Should().BeNull();
        }
    }
}
