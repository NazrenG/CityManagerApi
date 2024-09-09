using AutoMapper;
using CityManagerApi.Data.Abstract;
using CityManagerApi.Dtos;
using CityManagerApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityManagerApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;

        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        // GET: api/<CitiesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CitiesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item=await _appRepository.GetCitiesAsync(id);

            var dtos= _mapper.Map<IEnumerable<CityForListDto>>(item);
            return Ok(dtos);


             
        }

        // POST api/<CitiesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CityDto value)
        {

            var entity = _mapper.Map<City>(value);
            await _appRepository.AddAsync(entity);
            await _appRepository.SaveAllAsync();
            var returnedDto = _mapper.Map<CityDto>(entity);
            return Ok(returnedDto);

             

        }

        // PUT api/<CitiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CitiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
