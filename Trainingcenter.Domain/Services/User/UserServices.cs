using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DTOs.User;

namespace Trainingcenter.Domain.Services.User
{
    class UserServices : IUserServices
    {
        public Task<UserDTO> Login(UserToLoginDTO userToLogin)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Register(UserToRegisterDTO userToRegister)
        {
            throw new NotImplementedException();
        }
    }
}
