using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Trainingcenter.Domain.DTOs.UserDTOs
{
    class UserToRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MaxLength(128, ErrorMessage = "Password must be shorter than 128 characters")]
        [MinLength(8, ErrorMessage = "Password must be longer than 8 characters")]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
