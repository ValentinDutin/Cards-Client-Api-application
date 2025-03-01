using CardsServer.Data;
using Common.Models;
using MediatR;

namespace CardsServer.Application.Queries.GetCardByIdQuery
{
    public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, Card>
    {
        private readonly CardsRepository _cardsRepository;
        public GetCardByIdQueryHandler(CardsRepository cardsRepository)
        {
            _cardsRepository = cardsRepository;
        }
        public async Task<Card> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cardsRepository.GetCardByIdAsync(request.Id);
        }
    }
}