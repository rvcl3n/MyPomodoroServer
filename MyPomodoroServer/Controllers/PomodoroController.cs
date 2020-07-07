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

        [HttpGet("{id}", Name = "PomodoroById")]
        public IActionResult GetPomodoroById(Guid id)
        {
            try
            {
                var pomodoro = _repository.Pomodoro.GetPomodoroById(id);

                if (pomodoro.Id.Equals(Guid.Empty))
                {
                    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    //_logger.LogInfo($"Returned owner with id: {id}");
                    return Ok(pomodoro);
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePomodoro(Guid id, [FromBody] Pomodoro pomodoro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.Pomodoro.Update(pomodoro);
                _repository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
