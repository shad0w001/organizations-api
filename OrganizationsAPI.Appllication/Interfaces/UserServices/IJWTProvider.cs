using OrganizationsAPI.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces.UserServices
{
    public interface IJWTProvider
    {
        public Task<string> GenerateTokenAsync(User user);
    }
}
