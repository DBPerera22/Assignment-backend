using Asignment_backend;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Assignment_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : ControllerBase
    {
        private static readonly Random random = new Random();

        [HttpPost("simulate")]
        public IActionResult SimulateGame(MontyHallGame game)
        {
        
            // Perform the game simulation
            int prizeDoor = random.Next(1, game.NumberOfDoors + 1);
            int[] doors = Enumerable.Range(1, game.NumberOfDoors).ToArray();
            int[] availableDoors = doors.Where(d => d != game.InitialDoorSelection && d != prizeDoor).ToArray();
            int revealedDoor = availableDoors[random.Next(0, availableDoors.Length)];

            // Update the game state
            game.RevealedDoor = revealedDoor;
            game.FinalDoor = game.InitialDoorSelection; // Assuming the player sticks with the initial choice
            game.IsWin = game.FinalDoor == prizeDoor; // Check if the final door matches the prize door

            return Ok(game);
        }


        [HttpGet("reveal")]
        public MontyHallGame RevealDoor([FromQuery] MontyHallGame game)
        {
  
            // Update the game state with the final door selection
            int[] doors = Enumerable.Range(1, game.NumberOfDoors).ToArray();
            game.FinalDoor = doors.Single(d => d != game.InitialDoorSelection && d != game.RevealedDoor);
            game.IsWin = game.FinalDoor == random.Next(1, game.NumberOfDoors + 1);

            return game;
        }
    }
}
