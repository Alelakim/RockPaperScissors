using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var game = await _gameService.GetGameAsync(id).ConfigureAwait(false);

            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameResponse>> CreateGame([FromBody] string name)
        {
            if(string.IsNullOrEmpty(name)) return BadRequest("Please enter a name for player one");

            var response = await _gameService.CreateNewGame(name).ConfigureAwait(false);
            if(!string.IsNullOrEmpty(response.errorInfo)) return BadRequest(response.errorInfo);

            return CreatedAtAction("GetGame", new { id = response.Id });
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGame(Guid id, [FromBody] string name)
        {
            if (string.IsNullOrEmpty(name) || id == Guid.Empty) return BadRequest("Please check that you entered the ID and the name of the player");

            var response = await _gameService.JoinGameAsync(id, name);
            if (!string.IsNullOrEmpty(response.errorInfo)) return BadRequest(response.errorInfo);
            return Ok(response);

        }

        [HttpPost("{id}/move")]
        public async Task<IActionResult> MakeAMove(Guid id, [FromBody] string move, [FromBody] string name)
        { 
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool GameExists(Guid id)
        {
            return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
