using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public IActionResult GetAllUsers()
        {
            var users = _repository.User.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUSerById(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);

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
        public IActionResult GetUSerExternalById(string id)
        {
            try
            {
                var user = _repository.User.GetUserByExternalId(id);

                if (user.Id.Equals(Guid.Empty))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.User.Update(user);
                _repository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
                if (user.Id.Equals(Guid.Empty))
                {
                    return NotFound();
                }

                _repository.User.Delete(user);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.User.CreateUser(user);
                _repository.Save();

                return Ok(user.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO loginUser)
        {
            try
            {
                var user = _repository.User.GetUserByExternalId(loginUser.ExternalId);
                if (user == null)
                {
                    user = _mapper.Map<User>(loginUser);
                    user.Id = Guid.NewGuid();

                    _repository.User.CreateUser(user);
                    _repository.Save();
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
