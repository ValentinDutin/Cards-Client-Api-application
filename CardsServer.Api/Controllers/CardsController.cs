using Microsoft.AspNetCore.Mvc;
using MediatR;
using Common.Models;
using CardsServer.Application.GetCardsQuery;
using CardsServer.Application.AddCardCommand;

namespace CardsServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CardsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var query = new GetCardsQuery();
            var cards = await _mediator.Send(query);
            return Ok(cards);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card item)
        {
            var command = new AddCardCommand(item);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
