using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.UserDTOs
{
    public class UserToUpdateDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PictureURL { get; set; }
    }
}
