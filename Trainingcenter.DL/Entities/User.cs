using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.DL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PassWordSalt { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public Picture Picture { get; set; }
        public List<Order> Orders { get; set; }
        public List<ExchangeKey> ExchangeKeys { get; set; }
    }
}
