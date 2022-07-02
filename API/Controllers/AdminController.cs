using API.DTOs;
using API.DTOs.KeyValuePairs;
using AutoMapper;
using Core.Interfaces;
using Core.Entites;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepo _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepo adminService, IMapper mapper)
        {
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpPost("deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate([FromBody] EmailDto userEmail)
        {
            if (await _adminService.DeactivateUser(userEmail.Email))
            {
                return Ok("User Deactivated Successfully");
            }
            return BadRequest("Unexpected Error!");
        }

        [HttpPost("reactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reactivate([FromBody] EmailDto userEmail)
        {
            if (await _adminService.ReactivateUser(userEmail.Email))
            {
                return Ok("User Reactivated Successfully");
            }
            return BadRequest("Unexpected Error!");
        }

        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromBody] EmailDto userEmail)
        {
            if (await _adminService.DeleteUser(userEmail.Email))
            {
                return Ok("User is Deleted Successfully");
            }
            return BadRequest("Unexpected Error!");
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<QueryResultDto<ApplicationUserDto>>> getMedicines([FromQuery] QueryDto queryDto)
        {
            var query = _mapper.Map<QueryDto, Query>(queryDto);

            var users = await _adminService.GetUsers(query);

            return Ok(_mapper.Map<QueryResult<ApplicationUser>, QueryResultDto<ApplicationUserDto>>(users));
        }
    }
}