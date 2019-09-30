using ITG.Brix.Teams.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Definitions
{
    public class UpdateOperatorCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public UpdateOperatorCommand(Guid id,
                                     string login,
                                     string firstName,
                                     string lastName)
        {
            Id = id;
            Login = login;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
