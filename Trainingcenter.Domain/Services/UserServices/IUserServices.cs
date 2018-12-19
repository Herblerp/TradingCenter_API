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
        Task<UserDTO> ValidateUser(string key);
        Task<List<UserDTO>> SearchUser(string username);
        Task<UserDTO> GetPublicUserById(int userId);
        Task<UserDTO> DeleteUser(int userId, string password);
        bool IsValidEmail(string email);
    }
}
