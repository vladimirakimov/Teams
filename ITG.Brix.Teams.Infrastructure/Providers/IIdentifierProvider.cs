using System;

namespace ITG.Brix.Teams.Infrastructure.Providers
{
    public interface IIdentifierProvider
    {
        Guid Generate();
    }
}
