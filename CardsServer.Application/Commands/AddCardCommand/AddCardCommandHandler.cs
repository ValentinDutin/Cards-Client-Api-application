using CardsServer.Data;
using MediatR;

namespace CardsServer.Application.Commands.AddCardCommand
{
    public class AddCardCommandHandler : IRequestHandler<AddCardCommand, Unit>
    {
        private readonly CardsRepository _cardsRepository;
        public AddCardCommandHandler(CardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository;
        }
        public async Task<Unit> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {
            await _cardsRepository.AddCardAsync(request.Card);
            return Unit.Value;
        }
    }
}