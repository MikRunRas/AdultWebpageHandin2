using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginExample.Data;
using LoginExample.Data.Impl;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AdultWebpageHandin2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserData _userService;
        public UsersController(IUserData _userService)
        {
            this._userService = _userService;
        }
        [HttpGet]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
               
            Console.WriteLine("Here");
            try
            {
                var user = await _userService.ValidateUser(username, password);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
            
        /*
        public async Task<ActionResult<IList<User>>>
            GetUsers([FromQuery] string? username, [FromQuery] string? password)
        {
            try
            {
                IList<User> users = await _userService.GetUsersAsync();
                if (username != null)
                {
                    users = users.Where(user => user.UserName == username).ToList();
                }
                if (password != null)
                {
                    users = users.Where(user => user.Password == password).ToList();
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }*/
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                await _userService.RemoveUserAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User added = await _userService.AddUserAsync(user);
                return Created($"/{added.id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> UpdateUser([FromBody] User user)
        {
            try
            {
                User updatedUser = await _userService.UpdateAsync(user);
                return Ok(updatedUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}