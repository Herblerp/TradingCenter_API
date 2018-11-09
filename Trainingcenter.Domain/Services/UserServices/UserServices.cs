using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.UserDTOs;
using Trainingcenter.Domain.Repositories;

namespace Trainingcenter.Domain.Services.UserServices
{
    class UserServices : IUserServices
    {
        #region DependencyInjection

        private readonly IGenericRepository _genericRepo;
        private readonly IUserRepository _userRepo;

        public UserServices(IGenericRepository genericRepo, IUserRepository userRepo)
        {
            _genericRepo = genericRepo;
            _userRepo = userRepo;
        }

        #endregion

        #region ServiceMethods

        public async Task<UserDTO> Login(UserToLoginDTO userToLogin)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> Register(UserToRegisterDTO userToRegister)
        {
            try
            {

                //Set username to lowercase
                userToRegister.Username = userToRegister.Username.ToLower();

                //Check input
                if (await UserExixts(userToRegister.Username))
                {
                    return null;
                }
                if (IsValidEmail(userToRegister.Email))
                {
                    return null;
                }

                //Hash password
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userToRegister.Password, out passwordHash, out passwordSalt);

                //Prepare to save user in databse
                var userToCreate = new User
                {
                    Username = userToRegister.Username,
                    Email = userToRegister.Email
                };
                userToCreate.PasswordHash = passwordHash;
                userToCreate.PasswordSalt = passwordSalt;
                userToCreate.CreatedOn = DateTime.Now;

                //Save the user
                await _genericRepo.AddAsync(userToRegister);

                //Prepare to return user 
                var createdUser = await _userRepo.GetFromUsernameAsync(userToRegister.Username);

                var userToReturn = new UserDTO
                {
                    UserId = createdUser.UserId,
                    Username = createdUser.Username
                };

                return userToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("UserService failed to register user");
            }
        }
        #endregion

        #region Helpers

        public async Task<bool> UserExixts(string username)
        {
            if (await _userRepo.GetFromUsernameAsync(username) == null)
            {
                return false;
            }
            return true;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion
    }
}
