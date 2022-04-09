using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoSampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloCampus02Controller : ControllerBase
    {

        private readonly IConfiguration Configuration;
        private readonly ILogger<HelloCampus02Controller> _logger;

        public HelloCampus02Controller(ILogger<HelloCampus02Controller> logger, 
            IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        // GET: api/<HelloCampus02Controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HelloCampus02Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            //Lesen der aus Config
            //ins Log schreiben
            _logger.LogInformation($"Id was {id}");

            var greeting = Configuration["Greeting"];

            return greeting + "git ";
        }

        // POST api/<HelloCampus02Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HelloCampus02Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HelloCampus02Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
