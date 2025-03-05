using CardsServer.Data.Services;
using CommonFiles.Models;
using MediatR;

namespace CardsServer.Application.Queries.GetCardByIdQuery
{
    public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, Card>
    {
        private readonly ICardRepository _cardRepository;
        public GetCardByIdQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<Card> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cardRepository.GetCardByIdAsync(request.Id);
        }
    }
}