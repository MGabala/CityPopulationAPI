using AutoMapper;

using CityPopulationAPI.Models;

using Microsoft.AspNetCore.Mvc;

namespace CityPopulationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly List<City> cities = new List<City>
        {
            new City { Id = 1, Name = "Warsaw", Population = 1800000, Country = "Poland" },
            new City { Id = 2, Name = "Berlin", Population = 3800000, Country = "Germany" },
            new City { Id = 3, Name = "Paris", Population = 2200000, Country = "France" },
            new City { Id = 4, Name = "London", Population = 8900000, Country = "United Kingdom" }
        };
        private readonly List<Region> regions = new List<Region>
        {
            new Region { Country = "Poland", City = "Warsaw" },
            new Region { Country = "Germany", City = "Berlin" },
            new Region { Country = "France", City = "Paris" },
            new Region { Country = "United Kingdom", City = "London" }
        };
        private readonly IMapper _mapper;

        public CityController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetRandomCity()
        {
            Random random = new Random();
            var randomCity = cities[random.Next(cities.Count)];
            var cityDTO = MapToCityDTO(randomCity);
            return Ok(cityDTO);
        }

        [HttpGet("{city_dto}")]
        public IActionResult GetCity(int city_dto)
        {
            var city = cities.FirstOrDefault(c => c.Id == city_dto);
            if (city == null)
            {
                return NotFound();
            }

            var cityDTO = MapToCityDTO(city);
            return Ok(cityDTO);
        }

        [HttpPost]
        public IActionResult AddCity([FromBody] City city)
        {
            // Wstępna walidacja danych
            if (string.IsNullOrEmpty(city.Name) || string.IsNullOrEmpty(city.Country) || city.Population <= 0)
            {
                return BadRequest("City name, country, and population must be provided.");
            }

            city.Id = cities.Count + 1;
            cities.Add(city);
            return Ok(city);
        }

        [HttpGet("region")]
        public IActionResult GetCitiesByRegion([FromQuery] string region)
        {
            if (string.IsNullOrEmpty(region))
            {
                return BadRequest("Region parameter is required.");
            }

            var citiesInRegion = cities.Where(c => regions.Any(r => r.City == c.Name && r.Country == c.Country && r.Country == region)).ToList();
            var cityDTOs = citiesInRegion.Select(c => MapToCityDTO(c)).ToList();
            return Ok(cityDTOs);
        }


        private CityDTO MapToCityDTO(City city)
        {
            var cityDTO = _mapper.Map<CityDTO>(city);
            cityDTO.Region = regions.FirstOrDefault(r => r.City == cityDTO.Name)?.Country!;
            return cityDTO;
        }
    }
}
