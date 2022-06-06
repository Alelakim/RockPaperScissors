using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RockPaperScissors.Models;
using RockPaperScissors.Services;

namespace RockPaperScissors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{id}")]
        public ActionResult<GameResponse> GetGame(Guid id)
        {
            var response = _gameService.GetGame(id);

            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<GameResponse> CreateGame([FromBody] CreateRequest input)
        {
            if (string.IsNullOrEmpty(input.Name)) return BadRequest("Please enter a name for player one");

            var response = _gameService.CreateNewGame(input.Name);
            if (!string.IsNullOrEmpty(response.ErrorInfo)) return BadRequest(response.ErrorInfo);

            return CreatedAtAction("CreateGame", new { id = response.Id });
        }

        [HttpPost("{id}/join")]
        public IActionResult JoinGame(Guid id, [FromBody] JoinRequest input)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(input.Name)) return BadRequest("Please check that you entered the ID and the name of the player");

            var response = _gameService.JoinGame(id, input.Name);
            if (!string.IsNullOrEmpty(response.ErrorInfo)) return BadRequest(response.ErrorInfo);
            return Ok(response);

        }

        [HttpPost("{id}/move")]
        public IActionResult MakeAMove(Guid id, [FromBody] MoveRequest newMove)
        {            
            if (id == Guid.Empty || string.IsNullOrEmpty(newMove.PlayersName) || string.IsNullOrEmpty(newMove.Move)) return BadRequest("Please check that you entered the ID, your name and your move");

            var response = _gameService.MakeAMove(id, newMove.PlayersName, newMove.Move);
            return Ok(response);
        }
    }
}
