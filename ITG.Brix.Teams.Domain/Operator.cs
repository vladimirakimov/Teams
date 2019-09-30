using ITG.Brix.Teams.Domain.Bases;
using ITG.Brix.Teams.Domain.Internal;
using System;

namespace ITG.Brix.Teams.Domain
{
    public class Operator : Entity
    {
        public string Login { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Operator(Guid id,
                        string login,
                        string firstName,
                        string lastName)
        {
            if (id == default(Guid))
            {
                throw Error.Argument(string.Format("{0} can't be default guid.", nameof(id)));
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(login)));
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(firstName)));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be empty.", nameof(lastName)));
            }

            Id = id;
            Login = login;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
