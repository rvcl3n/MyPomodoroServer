using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Contracts;
using AutoMapper;

namespace MyPomodoroServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public UserController(IRepositoryWrapper repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _repository.User.GetAllUsers();

                _logger.LogInfo($"Returned all users from database.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Something went wrong inside GetAllUsers action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUSerById(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserById(id);

                if (user.Id.Equals(Guid.Empty))
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: {id}");
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUSerById action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("external/{id}", Name = "UserByExternalId")]
        public async Task<IActionResult> GetUSerExternalById(string id)
        {
            try
            {
                var user = await _repository.User.GetUserByExternalId(id);

                if (user.Id.Equals(Guid.Empty))
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with External id: {id}");
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUSerExternalById action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from a client.");
                    return BadRequest("Invalid model object");
                }

                 _repository.User.Update(user);
                await _repository.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _repository.User.GetUserById(id);
                if (user.Id.Equals(Guid.Empty))
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                 _repository.User.Delete(user);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("Owner user sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from a client.");
                    return BadRequest("Invalid model object");
                }

                _repository.User.CreateUser(user);
                await _repository.SaveAsync();

                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO loginUser)
        {
            try
            {
                var user = await _repository.User.GetUserByExternalId(loginUser.ExternalId);
                if (user == null)
                {
                    user = _mapper.Map<User>(loginUser);
                    user.Id = Guid.NewGuid();

                    _repository.User.CreateUser(user);
                    await _repository.SaveAsync();
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Login action. {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
