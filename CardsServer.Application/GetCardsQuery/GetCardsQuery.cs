using MediatR;
using Common.Models;

namespace CardsServer.Application.GetCardsQuery
{
    public class GetCardsQuery : IRequest<List<Card>>
    {
        public GetCardsQuery()
        {
        }
    }
}
