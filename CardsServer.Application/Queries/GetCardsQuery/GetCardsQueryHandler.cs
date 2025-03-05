using CardsServer.Data.Services;
using CommonFiles.Models;
using MediatR;

namespace CardsServer.Application.Queries.GetCardsQuery
{
    public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, List<Card>>
    {
        private readonly ICardRepository _cardRepository;
        public GetCardsQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<List<Card>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            return await _cardRepository.GetCardsAsync();
        }
    }
}
