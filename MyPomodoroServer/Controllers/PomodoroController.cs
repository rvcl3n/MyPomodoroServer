using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;

namespace MyPomodoroServer.Controllers
{
    [Route("api/pomodoro")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        public PomodoroController()
        {
        }       

        [HttpGet]
        public IActionResult GetAllPomodoros()
        {
            var pomodoros = new List<Pomodoro>() {
                new Pomodoro(){
                    Id=new Guid(),
                    StartTime="Start",
                    FinishTime="Finish",
                    Description="SomeText"
                }
            };

            return Ok(pomodoros);
        }
    }
}
