using OrganizationsAPI.Appllication.DTOs.UserDTOs.LoginUserDTOs;
using OrganizationsAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces.UserServices
{
    public interface IUserService
    {
        public Result<string> RegisterUser(LoginUserRequestDTO userDTO);
        public Result<string> LoginUser(LoginUserRequestDTO userDTO);
        public Result<string> DeleteUser(string username);
    }
}
