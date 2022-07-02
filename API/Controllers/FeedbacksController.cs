using API.DTOs;
using AutoMapper;
using Core.Interfaces;
using Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepo _feedsService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public FeedbacksController(IFeedbackRepo feedsService, IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
            _feedsService = feedsService;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getFeeds()
        {
            return Ok(await _feedsService.GetListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> getFeedById(int id)
        {
            return Ok(await _feedsService.GetFeedbackByIdAsync(id));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFeed(int id)
        {
            await _feedsService.DeleteFeedAsync(id);
            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> AddFeed([FromBody] FeedToAddDto feedToAdd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feed = _mapper.Map<FeedToAddDto, Feedback>(feedToAdd);
            await _feedsService.AddFeedAsync(feed);
            await _uow.CompleteAsync();

            return Ok();
        }

    }
}