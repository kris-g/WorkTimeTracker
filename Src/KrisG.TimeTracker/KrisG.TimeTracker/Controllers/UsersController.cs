using AutoMapper;
using KrisG.TimeTracker.Models.Users;
using KrisG.TimeTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrisG.TimeTracker.Entities;

namespace KrisG.TimeTracker.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(
            IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticatedUserModel>> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var userModel = _mapper.Map<AuthenticatedUserModel>(user);

            return userModel;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                // create user
                await _userService.Create(model.Username, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
        {
            var users = await _userService.GetAll();
            var userModels = users.Select(_mapper.Map<UserModel>);

            return Ok(userModels);
        }
    }
}