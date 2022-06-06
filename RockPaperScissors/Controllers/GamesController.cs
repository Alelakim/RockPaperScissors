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
            //se till att retunera state (eller bara resultat med info?) och vilka som spelar
            var response = _gameService.GetGameAsync(id);

            if (response == null) return NotFound();
            return Ok(response);
        }

        [HttpPost]
        //public async Task<ActionResult<GameResponse>> CreateGame([FromBody] string name)
        public ActionResult<GameResponse> CreateGame([FromBody] string name)
        {
            if(string.IsNullOrEmpty(name)) return BadRequest("Please enter a name for player one");

            var response = _gameService.CreateNewGame(name);
            if(!string.IsNullOrEmpty(response.ErrorInfo)) return BadRequest(response.ErrorInfo);

            return CreatedAtAction("CreateGame", new { id = response.Id });
        }

        [HttpPost("{id}/join")]
        public IActionResult JoinGame(Guid id, [FromBody] string name)
        {
            if (id == Guid.Empty || string.IsNullOrEmpty(name)) return BadRequest("Please check that you entered the ID and the name of the player");

            var response = _gameService.JoinGameAsync(id, name);
            if (!string.IsNullOrEmpty(response.ErrorInfo)) return BadRequest(response.ErrorInfo);
            return Ok(response);

        }

        [HttpPost("{id}/move")]
        public IActionResult MakeAMove(Guid id, [FromBody] JObject json)
        {
            var jsonBody = json;
            var name = "";
            var move = "";
            if (id == Guid.Empty || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(move)) return BadRequest("Please check that you entered the ID, your name and your move");

            var response = _gameService.MakeAMoveAsync(id, name, move);
            return Ok(response);
        }
    }
}
