using Microsoft.AspNetCore.Mvc;

namespace GIBDD.Server.Controllers
{


    public class TestObject
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class CorsTestController : ControllerBase
    {
        [HttpGet("test-cors")]
        public IActionResult TestCors()
        {
            Console.WriteLine("Received CORS Test Request");
            // Создаем список объектов
            var testObjects = new List<TestObject>
            {
                new TestObject { Id = 1, Name = "Object 1", Description = "This is the first test object." },
                new TestObject { Id = 2, Name = "Object 2", Description = "This is the second test object." },
                new TestObject { Id = 3, Name = "Object 3", Description = "This is the third test object." }
            };

            // Возвращаем список объектов как результат
            return Ok(testObjects);
        }
    }
}
