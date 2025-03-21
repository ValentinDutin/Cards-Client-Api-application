﻿using MediatR;
using CommonFiles.Models;

namespace CardsServer.Application.Queries.GetCardsQuery
{
    public class GetCardsQuery : IRequest<List<Card>>
    {
        public GetCardsQuery() {}
    }
}