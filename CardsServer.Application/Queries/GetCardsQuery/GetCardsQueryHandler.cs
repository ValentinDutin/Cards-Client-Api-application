using CardsServer.Data;
using Common.Models;
using MediatR;

namespace CardsServer.Application.Queries.GetCardsQuery
{
    public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, List<Card>>
    {
        private readonly CardsRepository _cardsRepository;
        public GetCardsQueryHandler(CardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository ?? throw new ArgumentNullException(nameof(cardsRepository));
        }
        public async Task<List<Card>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            return await _cardsRepository.GetCardsAsync();
        }
    }
}
