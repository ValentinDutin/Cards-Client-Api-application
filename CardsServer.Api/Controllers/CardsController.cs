using Microsoft.AspNetCore.Mvc;
using MediatR;
using Common.Models;
using CardsServer.Application.Queries.GetCardsQuery;
using CardsServer.Application.Queries.GetCardByIdQuery;
using CardsServer.Application.Commands.AddCardCommand;
using CardsServer.Application.Commands.DeleteCardByIdCommand;
using CardsServer.Application.Commands.DeleteAllCardsCommand;

namespace CardsServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CardsController> _logger;
        public CardsController(IMediator mediator, ILogger<CardsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var query = new GetCardsQuery();
            var cards = await _mediator.Send(query);
            _logger.LogInformation("Request GetCards is recieved");
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(Guid id)
        {
            try
            {
                var query = new GetCardByIdQuery(id);
                var card = await _mediator.Send(query);
                return Ok(card);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card item)
        {
            try
            {
                var command = new AddCardCommand(item);
                await _mediator.Send(command);
                _logger.LogInformation("Card is added sucessfully");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardById(Guid id)
        {
            try
            {
                var command = new DeleteCardByIdCommand(id);
                var isDeleted = await _mediator.Send(command);
                if (isDeleted)
                {
                    _logger.LogInformation("Deleted Card By Id : " + id);
                    return Ok(isDeleted);
                }
                _logger.LogError("Deleted card is not found");
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllCards()
        {
            try
            {
                var command = new DeleteAllCardsCommand();
                await _mediator.Send(command);
                _logger.LogInformation("Delete all cards");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}