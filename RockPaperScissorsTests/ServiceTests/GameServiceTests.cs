using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using RockPaperScissors.Models;
using RockPaperScissors.Repository;
using RockPaperScissors.Services;
using System;
using System.Collections.Generic;

namespace RockPaperScissorsTests
{
    public class GameServiceTests
    {
        private GameService _gameService;
        private IRockPaperScissorService _rockPaperScissorService;
        private IGameRepository _gameRepository;
        [SetUp]
        public void Setup()
        {
            _rockPaperScissorService = Substitute.For<IRockPaperScissorService>();
            _gameRepository = Substitute.For<IGameRepository>();
            _gameService = new GameService(_gameRepository, _rockPaperScissorService);
        }

        [Test]
        public void GetGame_ShouldReturnErrorInfo_WhenGameIsNull()
        {
            //Act
            var result = _gameService.GetGame(Guid.NewGuid());
            
            //Assert
            result.ErrorInfo.Should().Contain("Game or players are not defiened, please check the id or create a new game");
        }

        [Test]
        public void JoinGame_ShouldReturnErrorInfo_WhenGameIsNull()
        {
            //Act
            var result = _gameService.JoinGame(Guid.NewGuid(), "Mumin");

            //Assert
            result.ErrorInfo.Should().Contain("The game you are trying to join does not exist");
        }

        [Test]
        public void JoinGame_ShouldReturnErrorInfo_WhenToManyPlayers()
        {
            //Arrange
            _gameRepository.Find(Guid.NewGuid()).ReturnsForAnyArgs(new Game { Players = new List<Player>() { new Player { Name = "Mumin" }, { new Player { Name = "Morran" } } } });

            //Act
            var result = _gameService.JoinGame(Guid.NewGuid(), "Lilla My");

            //Assert
            result.ErrorInfo.Should().Contain("There is already two players playing this game");
        }

        [Test]
        public void JoinGame_ShouldReturnErrorInfo_WhenSecondPlayerNotHavingUniqueName()
        {
            //Arrange
            _gameRepository.Find(Guid.NewGuid()).ReturnsForAnyArgs(new Game { Players = new List<Player>() { new Player { Name = "Mumin" } } });

            //Act
            var result = _gameService.JoinGame(Guid.NewGuid(), "Mumin");

            //Assert
            result.ErrorInfo.Should().Contain("The first player already enter the given name, please select a unique name");
        }


        [Test]
        public void MakeAMove_ShouldReturnErrorInfo_WhenNoGameWithTheGivenId()
        {
            //Act
            var result = _gameService.MakeAMove(Guid.NewGuid(), "Lilla My", "Rock");

            //Assert
            result.ErrorInfo.Should().Contain("The game you are trying to make move on does not exist");
        }

        [Test]
        public void MakeAMove_ShouldReturnErrorInfo_WhenPlayerNameDoesNotExist()
        {
            //Arrange
            _gameRepository.Find(Guid.NewGuid()).ReturnsForAnyArgs(new Game { Players = new List<Player>() { new Player { Name = "Mumin" }, new Player { Name = "Morran" } } });

            //Act
            var result = _gameService.MakeAMove(Guid.NewGuid(), "Lilla My", "Rock");

            //Assert
            result.ErrorInfo.Should().Contain("is not at player of the game");
        }

        [Test]
        public void MakeAMove_ShouldReturnErrorInfo_WhenMoveIsNotRockPaperOrScissors()
        {
            //Arrange
            _gameRepository.Find(Guid.NewGuid()).ReturnsForAnyArgs(new Game { Players = new List<Player>() { new Player { Name = "Mumin", Move = "rock" }, { new Player { Name = "Morran" } } } });

            //Act
            var result = _gameService.MakeAMove(Guid.NewGuid(), "Morran", "Mu");

            //Assert
            result.ErrorInfo.Should().Contain("Mind your spelling and please enter a correct move: Rock, Paper or Sciccors");
        }

    }
}