using System.Net;
using API.DTOs;
using AutoMapper;
using Core.Interfaces;
using Core.Entites;
using Core.Entites.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineRepo _medicineService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthRepo _authService;

        public MedicinesController(IMedicineRepo medicineService, IUnitOfWork uow,
         IMapper mapper, UserManager<ApplicationUser> userManager, IAuthRepo authService)
        {
            _authService = authService;
            _userManager = userManager;
            _mapper = mapper;
            _uow = uow;
            _medicineService = medicineService;
        }


        [HttpGet]
        public async Task<ActionResult<QueryResultDto<MedicineDto>>> getMedicines([FromQuery] QueryDto queryDto)
        {
            var query = _mapper.Map<QueryDto, Query>(queryDto);

            var medicine = await _medicineService.getAllAsync(query);

            return Ok(_mapper.Map<QueryResult<Medicine>, QueryResultDto<MedicineDto>>(medicine));
        }

        [HttpGet("user/{email}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<QueryResultDto<MedicineDto>>> getUserMedicines([FromQuery] QueryDto queryDto, string email)
        {

            var token = getToken();

            if (await _authService.ValidateUserTokenAsync(token, email) || _authService.ValidateAdminTokenAsync(token))
            {
                var query = _mapper.Map<QueryDto, Query>(queryDto);

                var medicine = await _medicineService.getAllAsync(query);

                return Ok(_mapper.Map<QueryResult<Medicine>, QueryResultDto<MedicineDto>>(medicine));
            }

            return BadRequest("Invalid User");

        }

        [HttpGet("{id}", Name = "getMedicineById")]
        public async Task<ActionResult<MedicineDto>> getMedicineById(int id)
        {

            var medicine = await _medicineService.getByIdAsync(id);
            return Ok(_mapper.Map<Medicine, MedicineDto>(medicine));
        }

        [HttpPost]
        [Authorize(Roles = Roles.User)]
        public async Task<ActionResult<MedicineDto>> addMedicine([FromBody] MedicineToAddDto medicineToAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var medicine = _mapper.Map<MedicineToAddDto, Medicine>(medicineToAdd);

            var user = await _userManager.FindByEmailAsync(medicineToAdd.UserEmail);

            if (user == null)
                return BadRequest();

            medicine.ApplicationUserId = user.Id;

            await _medicineService.addAsync(medicine);

            await _uow.CompleteAsync();

            var medicineEntity = await _medicineService.getByIdAsync(medicine.Id);

            var medicineToReturn = _mapper.Map<Medicine, MedicineDto>(medicineEntity);
            return CreatedAtRoute("getMedicineById", new { id = medicineToReturn.Id }, medicineToReturn);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> EditMedicine([FromBody] MedicineToAddDto medicineToAdd, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(medicineToAdd.UserEmail);

            if (user == null)
                return BadRequest();

            var token = getToken();

            if (await _authService.ValidateUserTokenAsync(token, medicineToAdd.UserEmail))
            {
                var medicine = await _medicineService.getByIdAsync(id);

                if (medicine == null)
                    return NotFound();

                if (medicine.ApplicationUserId != user.Id)
                    return BadRequest("Invalid User");

                _mapper.Map(medicineToAdd, medicine);
                await _uow.CompleteAsync();

                return NoContent();
            }

            return BadRequest("Invalid User");


        }

        [HttpDelete("{UserEmail}/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> deleteMedicine(int id, string UserEmail)
        {

            var user = await _userManager.FindByEmailAsync(UserEmail);

            if (user == null)
                return BadRequest();

            var token = getToken();

            if (await _authService.ValidateUserTokenAsync(token, UserEmail) || _authService.ValidateAdminTokenAsync(token))
            {

                var medicine = await _medicineService.getByIdAsync(id);

                if (medicine == null)
                    return NotFound();

                await _medicineService.deleteAsync(id);
                await _uow.CompleteAsync();

                return NoContent();

            }
            return BadRequest("Invalid User");

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