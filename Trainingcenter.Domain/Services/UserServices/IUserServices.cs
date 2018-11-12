using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DTOs.UserDTOs;

namespace Trainingcenter.Domain.Services
{
    public interface IUserServices
    {
        Task<UserDTO> Register(UserToRegisterDTO userToRegister);
        Task<UserDTO> Login(UserToLoginDTO userToLogin);
        Task<bool> UserExists(string username);
        bool IsValidEmail(string email);
    }
}
