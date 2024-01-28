using OrganizationsAPI.Appllication.DTOs.UserDTOs.LoginUserDTOs;
using OrganizationsAPI.Appllication.Interfaces.UserServices;
using OrganizationsAPI.Domain.Abstractions;
using OrganizationsAPI.Domain.Entities;
using OrganizationsAPI.Domain.Entities.Authentication;
using OrganizationsAPI.Domain.Errors;
using OrganizationsAPI.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationsAPI.Appllication.Services.UserServices
{
    public class UserService : IUserService
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

        public Result<string> RegisterUser(LoginUserRequestDTO userDTO)
        {
            User? alreadyExistingUser = _userRepository.GetUserByName(userDTO.Username).Result;

            if (alreadyExistingUser is null)
            {
                return Result.Failure<string>(UserErrors.UserAlreadyExists);
            }

            User user = new User
            {
                Username = userDTO.Username,
                PasswordHash = _passwordManager.HashPassword(userDTO.Password, out string salt),
                Salt = salt
            };

            _userRepository.Create(user);

            return Result.Success("You have been registered successfully");
        }

        public Result<string> LoginUser(LoginUserRequestDTO userDTO)
        {
            User? user = _userRepository.GetUserByName(userDTO.Username).Result;

            if (user is null)
            {
                return Result.Failure<string>(UserErrors.UserNotFound);
            }

            if (_passwordManager.VerifyPassword(userDTO.Password, user.PasswordHash, user.Salt) is false)
            {
                return Result.Failure<string>(UserErrors.InvalidCredentials);
            }

            var token = _jwtProvider.GenerateToken(user, user.RoleId);

            return Result.Success(token);
        }

        public Result<string> DeleteUser(string username)
        {
            User? user = _userRepository.GetUserByName(username).Result;

            if (user is null)
            {
                return Result.Failure<string>(UserErrors.UserNotFound);
            }

            var affectedRows = _userRepository.SoftDelete(username);

            if(affectedRows.Result == 0)
            {
                return Result.Failure<string>(UserErrors.OperationFailed);
            }

            return Result.Success(username);
        }
    }
}
