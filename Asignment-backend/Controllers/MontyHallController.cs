using Asignment_backend;
using Asignment_backend.Controllers;
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

            var response = new
            {
                InitialDoorSelection = game.InitialDoorSelection,
                RevealedDoor = revealedDoor,
                FinalDoor = finalDoor,
                IsWin = isWin,
                CorrectDoor = prizeDoor
            };

            return Ok(response);
        }


        [HttpGet("hello")]
        public string HelloWorld()
        {
           

            return "Hello World";
        }

        //[HttpPost("simulate")]
        //public IActionResult SimulateGames([FromBody] MontyHallGame game)
        //{
        //    int numSimulations = game.NumSimulations;
        //    bool changeDoor = game.ChangeDoor;

        //    int wins = 0;
        //    int losses = 0;

        //    for (int i = 0; i < numSimulations; i++)
        //    {
        //        int prizeDoor = random.Next(1, 4); // Fixed number of doors (3)
        //        int[] doors = new int[] { 1, 2, 3 }; // Fixed number of doors (3)

        //        int[] availableDoors = doors.Where(d => d != game.InitialDoorSelection && d != prizeDoor).ToArray();
        //        int revealedDoor = availableDoors[random.Next(0, availableDoors.Length)];

        //        int finalDoor;
        //        if (changeDoor)
        //            finalDoor = doors.Single(d => d != game.InitialDoorSelection && d != revealedDoor);
        //        else
        //            finalDoor = game.InitialDoorSelection;

        //        if (finalDoor == prizeDoor)
        //            wins++;
        //        else
        //            losses++;
        //    }

        //    var response = new
        //    {
        //        NumSimulations = numSimulations,
        //        Wins = wins,
        //        Losses = losses
        //    };

        //    return Ok(response);
        //}

    }
}
