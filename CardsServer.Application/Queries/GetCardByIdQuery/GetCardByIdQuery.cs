using Common.Models;
using MediatR;

namespace CardsServer.Application.Queries.GetCardByIdQuery
{
    public class GetCardByIdQuery : IRequest<Card>
    {
        public Guid Id { get; set; }
        public GetCardByIdQuery(Guid id) { Id = id; }
    }
}