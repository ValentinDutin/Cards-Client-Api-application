using CardsServer.Data;
using MediatR;

namespace CardsServer.Application.Commands.DeleteAllCardsCommand
{
    public class DeleteAllCardsCommandHandler : IRequestHandler<DeleteAllCardsCommand, Unit>
    {
        private CardsRepository _cardsRepository;
        public DeleteAllCardsCommandHandler(CardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository;
        }
        public async Task<Unit> Handle(DeleteAllCardsCommand request, CancellationToken cancellationToken)
        {
             await _cardsRepository.DeleteAllCards();
            return Unit.Value;
        }
    }
}