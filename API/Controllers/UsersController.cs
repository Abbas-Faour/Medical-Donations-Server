using API.DTOs;
using AutoMapper;
using Core.Interfaces;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepo _userService;
        private readonly IMapper _mapper;
        private readonly IAuthRepo _authService;
        public UsersController(UserManager<ApplicationUser> userManager, IUserRepo userService, IMapper mapper, IAuthRepo authService)
        {
            _authService = authService;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;

        }

        [HttpPost("Address")]
        [Authorize(Roles = Roles.User)]
        public async Task<IActionResult> ChangeUserAddress(AddressToAddDto address)
        {

            var token = getToken();

            if (!await _authService.ValidateUserTokenAsync(token, address.Email))
                return BadRequest("invalid user");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user = await _userManager.Users
            .Where(e => e.Email.Contains(address.Email))
            .Include(a => a.Address)
            .SingleOrDefaultAsync();

            if (user == null)
                return BadRequest("Invalid User");

            _mapper.Map(address, user.Address);

            await _userManager.UpdateAsync(user);

            return Ok();

        }

        [HttpGet("profile/{email}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<UserProfile>> GetUserProfile(string email)
        {
            var token = getToken();

            if (!await _authService.ValidateUserTokenAsync(token, email))
                return BadRequest("invalid user");

            return Ok(await _userService.UserProfile(email));
        }

        [HttpGet("address/{email}")]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<UserProfile>> GetUserAddress(string email)
        {
            var token = getToken();

            if (!await _authService.ValidateUserTokenAsync(token, email))
                return BadRequest("invalid user");

            var userAddress = _userService.UserAddress(email);

            return Ok(_mapper.Map<Address, AddressDto>(userAddress));
        }


        private string getToken()
        {
            var authurization = HttpContext.Request.Headers["Authorization"].ToList();

            var bearer = authurization[0].Split(' ');

            var token = bearer[1];

            return token;
        }

    }
}