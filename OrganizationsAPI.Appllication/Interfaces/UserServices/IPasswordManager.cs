using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces.UserServices
{
    public interface IPasswordManager
    {
        public string HashPassword(string password, out string salt);
        public bool VerifyPassword(string password, string hashedPassword, string salt);
    }
}
