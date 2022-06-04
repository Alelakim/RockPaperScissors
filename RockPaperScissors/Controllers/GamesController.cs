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
        public async Task<ActionResult<Game>> GetGame(Guid id)
        {
            //se till att retunera state och vilka som spelar
            var game = await _gameService.GetGameAsync(id).ConfigureAwait(false);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame([FromBody] string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return BadRequest("Please enter a name for player one");
            }
            if (!_gameService.ValidGamesContext())
            {
                return Problem("Entity set 'GameContext.Games' is null.");
            }

            var id = await _gameService.CreateNewGame(name).ConfigureAwait(false);

            return CreatedAtAction("GetGame", new { id = id });
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinGame(Guid id, [FromBody] string name)
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

        [HttpPost("{id}/move")]
        public async Task<IActionResult> MakeGameMove(Guid id, Game game)
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
