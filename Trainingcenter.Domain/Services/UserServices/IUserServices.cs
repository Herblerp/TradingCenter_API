using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DTOs.UserDTOs;

namespace Trainingcenter.Domain.Services
{
    interface IUserServices
    {
        Task<UserDTO> Register(UserToRegisterDTO userToRegister);
        Task<UserDTO> Login(UserToLoginDTO userToLogin);
        Task<bool> UserExixts(string username);
        bool IsValidEmail(string email);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
