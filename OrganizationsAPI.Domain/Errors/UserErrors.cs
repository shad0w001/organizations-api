using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Domain.Errors
{
    public static class UserErrors
    {
        public static readonly Error UserAlreadyExists =
            new("User.UserAlreadyExists", "The username has already been taken");

        public static readonly Error UserNotFound =
            new("User.UserNotFound", "The user with the specified user name does not exist");

        public static readonly Error InvalidCredentials =
            new("User.InvalidCredentials", "The password is invalid");

        public static readonly Error OperationFailed =
            new("User.OperationFailed", "The requested opertaion has failed");
    }
}
