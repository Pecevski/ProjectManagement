using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMApp.Data.Entities;
using PMApp.Models.DTO.Requests;
using PMApp.Models.DTO.Responses;
using PMApp.Services.Contracts;
using PMApp.WEB.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.WEB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
            : base()
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        [Route("All")]
        public async Task<List<UserResponse>> GetAll()
        {
            List<UserResponse> users = new List<UserResponse>();

            foreach (var user in await _userService.GetAll())
            {
                UserResponse userResponse = UserMapper.MapUser(user);
                users.Add(userResponse);
            }

            return users;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("{username}")]
        public async Task<ActionResult<UserResponse>> Get(string username)
        {
            User userFromDB = await _userService.GetUserByUserName(username);
            if (userFromDB == null)
            {
                return NotFound();
            }

            return UserMapper.MapUser(userFromDB);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName(nameof(Get))]
        public async Task<IActionResult> Post(UserRequest user)
        {

            bool result = await _userService.CreateUser(user.UserName, user.Password, user.FirstName, user.LastName);

            if (result)
            {
                User userFromDB = await _userService.GetUserByUserName(user.UserName);

                return CreatedAtAction("Get", "Users", new { id = userFromDB.Id }, user);
            }

            return BadRequest("User already exist");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<IActionResult> Put(string id, UserUpdateRequest userRequest)
        {
            User user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            var updateUser = UserUpdateMapper.MapUserUpdateRequest(userRequest);
            await _userService.Update(id, updateUser);

            return Ok("User updated");
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.Delete(user);
            return NoContent();
        }
    }
}

