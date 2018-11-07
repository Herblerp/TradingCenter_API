using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.DL.Entities
{
    class User
    {
        public int UserId { get; set; }
        public int PictureId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PassWordSalt { get; set; }

        public List<Order> Orders { get; set; }
    }
}
