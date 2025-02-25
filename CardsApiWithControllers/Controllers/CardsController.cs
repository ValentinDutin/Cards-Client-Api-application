using Microsoft.AspNetCore.Mvc;
using Common.Models;
using CardsApiWithControllers.Services;

namespace CardsApiWithControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly ILogger<CardsController> _logger;
        private readonly IDbService _dbService;

        public CardsController(ILogger<CardsController> logger, IDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        // GET: /cards
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Request recieved");
            return Ok(_dbService.GetData());
        }

        // POST: /cards
        [HttpPost]
        public ActionResult Add(Card item)
        {
            if (_dbService.InsertCard(item))
            {
                _logger.LogInformation("Succesfully added card");
                return Ok();
            }
            _logger.LogError("Card adding is failed");
            return StatusCode(500);
        }
    }
}
