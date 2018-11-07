using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DTOs.User;

namespace Trainingcenter.Domain.Services
{
    interface IUserServices
    {
        Task<UserDTO> Register(UserToRegisterDTO userToRegister);
        Task<UserDTO> Login(UserToLoginDTO userToLogin);
    }
}
