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
        public async Task<ActionResult<GameResponse>> GetGame(Guid id)
        {
            //se till att retunera state (eller bara resultat med info?) och vilka som spelar
            var response = await _gameService.GetGameAsync(id).ConfigureAwait(false);

            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameResponse>> CreateGame([FromBody] string name)
        {
            if(string.IsNullOrEmpty(name)) return BadRequest("Please enter a name for player one");

            var response = await _gameService.CreateNewGame(name).ConfigureAwait(false);
            if(!string.IsNullOrEmpty(response.ErrorInfo)) return BadRequest(response.ErrorInfo);

            return CreatedAtAction("CreateGame", new { id = response.Id });
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGame(Guid id, [FromBody] string name)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(name)) return BadRequest("Please check that you entered the ID and the name of the player");

            var response = await _gameService.JoinGameAsync(id, name).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(response.ErrorInfo)) return BadRequest(response.ErrorInfo);
            return Ok(response);

        }

        [HttpPost("{id}/move")]
        public async Task<IActionResult> MakeAMove(Guid id, string json)
        {
            var jsonBody = JObject.Parse(json);
            var name = "";
            var move = "";
            if (id == Guid.Empty || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(move)) return BadRequest("Please check that you entered the ID, your name and your move");

            var response = await _gameService.MakeAMoveAsync(id, name, move).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
