using CardsServer.Data.Services;
using MediatR;

namespace CardsServer.Application.Commands.DeleteAllCardsCommand
{
    public class DeleteAllCardsCommandHandler : IRequestHandler<DeleteAllCardsCommand, Unit>
    {
        private readonly ICardRepository _cardRepository;
        public DeleteAllCardsCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<Unit> Handle(DeleteAllCardsCommand request, CancellationToken cancellationToken)
        {
             await _cardRepository.DeleteAllCardsAsync();
            return Unit.Value;
        }
    }
}