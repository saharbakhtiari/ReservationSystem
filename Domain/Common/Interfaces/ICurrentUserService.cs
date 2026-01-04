using System;

namespace Domain.Common.Interfaces
{
    /// <summary>
    /// Service used throughout the application layer to get current user's ID and Name.
    /// </summary>
    public interface ICurrentUserService
    {
        Guid? UserId { get; }

        string UserName { get; }

        string EmailAddress { get; }

        string FirstName { get; }
        string LastName { get; }
        Guid? UserKey { get; }
    }

    public interface IRequestOrginService
    {
        bool IsFromClient { get;}
    }
}
