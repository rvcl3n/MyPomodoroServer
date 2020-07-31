using System;
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
        private readonly ILoggerManager _logger;
        public PomodoroController(IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }       

        [HttpGet]
        public async Task<IActionResult> GetAllPomodoros()
        {
            try
            {
                var pomodoros = await _repository.Pomodoro.GetAllPomodoros();

                _logger.LogInfo($"Returned all users pomodoros from database.");

                return Ok(pomodoros);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Something went wrong inside GetAllPomodoros action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getbyuser/{id}", Name ="GetByUser")]
        public async Task<IActionResult> GetUsersPomodoros(string id)
        {
            try
            {
                var pomodoros = await _repository.Pomodoro.GetAllPomodorosByUser(id);

                _logger.LogInfo($"Returned all users pomodoros from database.");

                return Ok(pomodoros);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Something went wrong inside GetUsersPomodoros action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "PomodoroById")]
        public async Task<IActionResult> GetPomodoroById(Guid id)
        {
            try
            {
                var pomodoro = await _repository.Pomodoro.GetPomodoroById(id);

                if (pomodoro.Id.Equals(Guid.Empty))
                {
                    _logger.LogError($"Pomodoro with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned pomodoro with id: {id}");
                    return Ok(pomodoro);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetPomodoroById action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePomodoro(Guid id, [FromBody] PomodoroDTO pomodoroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid pomodoro object sent from a client.");
                    return BadRequest("Invalid model object");
                }


                var pomodoro = await _repository.Pomodoro.GetPomodoroById(id);

                //ToDo: Map

                pomodoro.Id = id;
                pomodoro.FinishTime = pomodoroDto.FinishTime;
                pomodoro.Description = pomodoroDto.Description != "" ? pomodoroDto.Description : pomodoro.Description;

                //

                await _repository.Pomodoro.Update(pomodoro);
                await _repository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdatePomodoro action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePomodoro(Guid id)
        {
            try
            {
                var pomodoro = await _repository.Pomodoro.GetPomodoroById(id);
                if (pomodoro.Id.Equals(Guid.Empty))
                {
                    _logger.LogError($"Pomodoro with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                await _repository.Pomodoro.Delete(pomodoro);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeletePomodoro action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CreatePomodoro(string id, [FromBody] PomodoroDTO pomodoroDTO)
        {
            try
            {
                if (pomodoroDTO == null)
                {
                    _logger.LogError("Owner pomodoro sent from client is null.");
                    return BadRequest("Pomodoro object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid pomodoro object sent from a client.");
                    return BadRequest("Invalid model object");
                }

                var pomodoro = _mapper.Map<Pomodoro>(pomodoroDTO);
                pomodoro.Id =Guid.NewGuid();

                var currentsUser = await _repository.User.GetUserByExternalId(id);
                pomodoro.UserId = currentsUser.Id;

                await _repository.Pomodoro.CreatePomodoro(pomodoro);

                await _repository.SaveAsync();

                _logger.LogInfo($"Created a Pomodoro with id: {pomodoro.Id}");

                return Ok(pomodoro.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreatePomodoro action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
