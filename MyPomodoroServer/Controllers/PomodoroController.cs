using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Contracts;
using AutoMapper;

namespace MyPomodoroServer.Controllers
{
    [Route("api/pomodoro")]
    [ApiController]
    public class PomodoroController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public PomodoroController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }       

        [HttpGet]
        public IActionResult GetAllPomodoros()
        {
            var pomodoros = _repository.Pomodoro.GetAllPomodoros();

            return Ok(pomodoros);
        }

        [HttpGet("getbyuser/{id}", Name ="GetByUser")]
        public IActionResult GetUsersPomodoros(string id)
        {
            var pomodoros = _repository.Pomodoro.GetAllPomodorosByUser(id);

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
                    return NotFound();
                }
                else
                {
                    return Ok(pomodoro);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePomodoro(Guid id, [FromBody] PomodoroDTO pomodoroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }


                var pomodoro = _repository.Pomodoro.GetPomodoroById(id);

                //ToDo: Map

                pomodoro.Id = id;
                pomodoro.FinishTime = pomodoroDto.FinishTime;
                pomodoro.Description = pomodoroDto.Description != "" ? pomodoroDto.Description : pomodoro.Description;

                //

                _repository.Pomodoro.Update(pomodoro);
                _repository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePomodoro(Guid id)
        {
            try
            {
                var pomodoro = _repository.Pomodoro.GetPomodoroById(id);
                if (pomodoro.Id.Equals(Guid.Empty))
                {
                    return NotFound();
                }

                _repository.Pomodoro.Delete(pomodoro);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id}")]
        public IActionResult CreatePomodoro(string id, [FromBody] PomodoroDTO pomodoroDTO)
        {
            try
            {
                if (pomodoroDTO == null)
                {
                    return BadRequest("Pomodoro object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var pomodoro = _mapper.Map<Pomodoro>(pomodoroDTO);
                pomodoro.Id =Guid.NewGuid();

                var currentsUser = _repository.User.GetUserByExternalId(id);
                pomodoro.UserId = currentsUser.Id;

                _repository.Pomodoro.CreatePomodoro(pomodoro);

                _repository.Save();

                return Ok(pomodoro.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
