using MediatR;
using Common.Models;
using System.Security.Cryptography.X509Certificates;

namespace CardsServer.Application.Queries.GetCardsQuery
{
    public class GetCardsQuery : IRequest<List<Card>>
    {
        public GetCardsQuery()
        {
        }
    }
}
