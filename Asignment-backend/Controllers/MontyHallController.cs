using Asignment_backend;
using Assignment_backend;
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
        private readonly ILogger<MontyHallController> _logger;

        public MontyHallController(ILogger<MontyHallController> logger)
        {
            _logger = logger;
        }
        [HttpPost("play")]
        public IActionResult PlayGame([FromBody] MontyHallGame game)
        {
            int prizeDoor = random.Next(1, 4); // Fixed number of doors (3)
            int[] doors = new int[] { 1, 2, 3 }; // Fixed number of doors (3)

            int[] availableDoors = doors.Where(d => d != game.InitialDoorSelection && d != prizeDoor).ToArray();
            int revealedDoor = availableDoors[random.Next(0, availableDoors.Length)];

            int finalDoor;
            if (game.ChangeDoor)
                finalDoor = doors.Single(d => d != game.InitialDoorSelection && d != revealedDoor);
            else
                finalDoor = game.InitialDoorSelection;

            bool isWin = finalDoor == prizeDoor;

            string message = isWin ? "Congratulations! You won the Monty Hall game!" : "";

            var response = new
            {
                InitialDoorSelection = game.InitialDoorSelection,
                RevealedDoor = revealedDoor,
                FinalDoor = finalDoor,
                IsWin = isWin,
                CorrectDoor = prizeDoor,
                Message = message
            };

            return Ok(response);
        }


    }
}
