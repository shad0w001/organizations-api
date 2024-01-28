using OrganizationsAPI.Appllication.DTOs.LoginUserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Interfaces.UserServices
{
    public interface IUserService
    {
        public Task<string> LoginUser(LoginUserRequestDTO userDTO);
    }
}
