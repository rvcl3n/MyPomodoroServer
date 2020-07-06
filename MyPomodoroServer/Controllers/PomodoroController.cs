using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Contracts;

namespace MyPomodoroServer.Controllers
{
    [Route("api/pomodoro")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public PomodoroController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }       

        [HttpGet]
        public IActionResult GetAllPomodoros()
        {
            var pomodoros = _repository.Pomodoro.GetAllPomodoros();

            /*var pomodoros = new List<Pomodoro>() {
                new Pomodoro(){
                    Id=new Guid(),
                    StartTime="Start",
                    FinishTime="Finish",
                    Description="SomeText"
                }
            };*/

            return Ok(pomodoros);
        }
    }
}
