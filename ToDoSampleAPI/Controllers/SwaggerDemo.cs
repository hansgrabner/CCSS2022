using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoSampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SwaggerDemoController : ControllerBase
    {
        [HttpGet]
        public string GetWord()
        {
            return "Hello";
        }
    }
}
