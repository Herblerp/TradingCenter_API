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
        Task<UserDTO> GetUserById(int userId);
        Task<bool> UserExists(string username);
        Task<UserDTO> UpdateUser(UserToUpdateDTO userToUpdate, int userId);
        Task<UserDTO> ValidateUser(int userId);
        Task<List<UserDTO>> SearchUser(string username);
        bool IsValidEmail(string email);
    }
}
