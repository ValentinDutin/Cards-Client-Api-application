﻿using MediatR;
using CommonFiles.Models;

namespace CardsServer.Application.Commands.AddCardCommand
{
    public class AddCardCommand : IRequest<Unit>
    {
        public Card Card { get; set; }
        public AddCardCommand(Card item)
        {
            Card = item;
        }
    }
}