using OrganizationsAPI.Appllication.DTOs;
using OrganizationsAPI.Appllication.Interfaces.UserServices;
using OrganizationsAPI.Domain.Entities.Authentication;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Services.UserServices
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTProvider _jwtProvider;
        private readonly IPasswordManager _passwordManager;

        public UserService(IUserRepository userRepository, IJWTProvider jwtProvider, IPasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordManager = passwordManager;

        }

        public async Task<string> LoginUser(LoginUserDTO userDTO)
        {
            User? user = _userRepository.GetUserByName(userDTO.Username);

            if (user is null)
            {
                return string.Empty;
            }

            if (_passwordManager.VerifyPassword(userDTO.Password, user.PasswordHash, user.Salt) is false)
            {
                return string.Empty;
            }

            var token = _jwtProvider.GenerateToken(user, user.RoleId);

            return token;
        }
    }
}
