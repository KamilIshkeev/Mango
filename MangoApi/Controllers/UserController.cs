﻿using MangoApi.Interfaces;
using MangoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("Top")]
        public async Task<IActionResult> GetTopUsers()
        {
            var users = await _userService.GetTopUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var result = await _userService.CreateUserAsync(user);
            if (!result)
                return BadRequest("Email already exists");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            var result = await _userService.UpdateUserAsync(user);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPost("authentication")]
        public async Task<IActionResult> Authenticate([FromBody] User loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authenticatedUser = await _userService.AuthenticateAsync(loginRequest.Login, loginRequest.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            return Ok(authenticatedUser);
        }
    }
}
