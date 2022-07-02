using API.DTOs;
using AutoMapper;
using Core.Interfaces;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepo _categoriesService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoriesRepo categoriesService, IMapper mapper)
        {
            _mapper = mapper;
            _categoriesService = categoriesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KeyValuePairDto>> getCategoryById(int id)
        {
            var category = await _categoriesService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(_mapper.Map<Category, KeyValuePairDto>(category));
        }

        [HttpGet]
        public async Task<ActionResult<KeyValuePairDto>> getCategories()
        {
            var categories = await _categoriesService.GetListAsync();
            return Ok(_mapper.Map<IEnumerable<Category>, IEnumerable<KeyValuePairDto>>(categories));
        }
    }
}