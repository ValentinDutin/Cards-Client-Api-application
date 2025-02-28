﻿using Microsoft.AspNetCore.Mvc;
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
        private ILogger<CardsController> _logger;
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
            var query = new GetCardByIdQuery(id);
            var card = await _mediator.Send(query);
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card item)
        {
            var command = new AddCardCommand(item);
            await _mediator.Send(command);
            _logger.LogInformation("Card is added sucessfully");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardById(Guid id)
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

        [HttpDelete]
        public async Task<IActionResult> DeleteAllCards()
        {
            var command = new DeleteAllCardsCommand();
            await _mediator.Send(command);
            _logger.LogInformation("Delete all cards");
            return Ok();
        }
    }
}
