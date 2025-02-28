using Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsServer.Application.Queries.GetCardByIdQuery
{
    public class GetCardByIdQuery : IRequest<Card>
    {
        public Guid Id { get; set; }
        public GetCardByIdQuery(Guid id) { Id = id; }
    }
}
