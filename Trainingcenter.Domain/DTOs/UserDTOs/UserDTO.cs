using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PictureURL { get; set; }
        public string VerificationKey { get; set; }
        public bool IsVerified { get; set; }
    }
}
