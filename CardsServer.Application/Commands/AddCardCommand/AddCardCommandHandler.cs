using CardsServer.Data.Services;
using MediatR;

namespace CardsServer.Application.Commands.AddCardCommand
{
    public class AddCardCommandHandler : IRequestHandler<AddCardCommand, Unit>
    {
        private readonly ICardRepository _cardRepository;
        public AddCardCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<Unit> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            await _cardRepository.AddCardAsync(request.Card);
            return Unit.Value;
        }
    }
}