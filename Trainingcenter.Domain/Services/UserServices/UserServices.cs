using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Trainingcenter.Domain.DomainModels;
using Trainingcenter.Domain.DTOs.PortfolioDTO_s;
using Trainingcenter.Domain.DTOs.UserDTOs;
using Trainingcenter.Domain.Repositories;
using Trainingcenter.Domain.Services.PortfolioServices;

namespace Trainingcenter.Domain.Services.UserServices
{
    public class UserServices : IUserServices
    {
        #region DependencyInjection

        private readonly IGenericRepository _genericRepo;
        private readonly IUserRepository _userRepo;
        private readonly IPortfolioServices _portfolioService;

        public UserServices(IGenericRepository genericRepo, IUserRepository userRepo, IPortfolioServices portfolioService)
        {
            _genericRepo = genericRepo;
            _userRepo = userRepo;
            _portfolioService = portfolioService;
        }

        #endregion

        #region Services

        public async Task<UserDTO> Login(UserToLoginDTO userToLogin)
        {
            try
            {
                userToLogin.Username = userToLogin.Username.ToLower();
                var userFromDB = await _userRepo.GetFromUsernameAsync(userToLogin.Username);

                if (userFromDB == null)
                {
                    return null;
                }
                if (!VerifyPasswordHash(userToLogin.Password, userFromDB.PasswordHash, userFromDB.PasswordSalt))
                {
                    return null;
                }
                

                userFromDB.LastActive = DateTime.Now;
                await _genericRepo.UpdateAsync(userFromDB);

                return ConvertUser(userFromDB);
            }
            catch
            {
                throw new Exception("UserService failed to login server");
            }
        }

        public async Task<UserDTO> Register(UserToRegisterDTO userToRegister)
        {
            try
            {
                //Set username to lowercase
                userToRegister.Username = userToRegister.Username.ToLower();

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
                userToCreate.VerificationKey = GetUniqueKey(128);
                userToCreate.IsVerified = false;


                //Create the user
                await _genericRepo.AddAsync(userToCreate);

                //Convert user
                var createdUser = await _userRepo.GetFromUsernameAsync(userToRegister.Username);

                //Create default portfolio
                var defaultPortfolio = new PortfolioToCreateDTO
                {
                    Name = "default",
                };
                await _portfolioService.CreatePortfolioAsync(defaultPortfolio, createdUser.UserId, true);

                var userToReturn = new UserDTO
                {
                    UserId = createdUser.UserId,
                    Username = createdUser.Username,
                    VerificationKey = createdUser.VerificationKey,
                    Email = createdUser.Email
                    //More fields here
                };

                return userToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception("UserService failed to register user");
            }
        }

        public async Task<UserDTO> UpdateUser(UserToUpdateDTO userToUpdate, int userId)
        {
            User user = await _userRepo.GetFromIdAsync(userId);
            
            if(user == null)
            {
                return null;
            }

            //Add check for empty strings
            if (userToUpdate.Password != null)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userToUpdate.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
                

            if(userToUpdate.FirstName != null)
                user.FirstName = userToUpdate.FirstName;

            if (userToUpdate.LastName != null)
                user.LastName = userToUpdate.LastName;

            if (userToUpdate.Phone != null)
                user.Phone = userToUpdate.Phone;

            if (userToUpdate.Email != null && IsValidEmail(userToUpdate.Email))
                user.Email = userToUpdate.Email;

            if (userToUpdate.PictureURL != null)
                user.PictureURL = userToUpdate.PictureURL;

            if (userToUpdate.Description != null)
                user.Description = userToUpdate.Description;

            return ConvertUser(await _genericRepo.UpdateAsync(user));
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _userRepo.GetFromUsernameAsync(username.ToLower()) == null)
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

        public async Task<UserDTO> ValidateUser(string key)
        {
            User user = await _userRepo.ValidateUser(key);

            if (user == null)
            {
                return null;
            }

            user.VerificationKey = null;
            user.IsVerified = true;

            return ConvertUser(await _genericRepo.UpdateAsync(user));
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var user = ConvertUser(await _userRepo.GetFromIdAsync(userId));
            return user;
        }

        public async Task<UserDTO> GetPublicUserById(int userId)
        {
            var user = ConvertPublicUser(await _userRepo.GetFromIdAsync(userId));
            return user;
        }

        public async Task<List<UserDTO>> SearchUser(string username)
        {
            var allUsers = await _userRepo.GetAll();
            var foundUsers = new List<UserDTO>();

            foreach (User user in allUsers)
            {
                if (user.Username.Contains(username))
                {
                    foundUsers.Add(ConvertPublicUser(user));
                }
            }
            return foundUsers;
        }

        public async Task<UserDTO> DeleteUser(int userId, string password)
        {
            var user = await _userRepo.GetFromIdAsync(userId);

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return ConvertUser(await _genericRepo.DeleteAsync(user));
        }

        #endregion

        #region Helpers

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion

        #region Converters

        private UserDTO ConvertUser(User user)
        {
            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Phone = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                VerificationKey = user.VerificationKey,
                IsVerified = user.IsVerified,
                PictureURL = user.PictureURL,
                Description = user.Description
            };
            return userDTO;
        }

        private UserDTO ConvertPublicUser(User user)
        {
            var publicUserDTO = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                PictureURL = user.PictureURL,
                Description = user.Description
            };

            return publicUserDTO;

        }

        #endregion
        private static string GetUniqueKey(int size)
        {
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
