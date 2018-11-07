using System;
using System.Collections.Generic;
using System.Text;

namespace Trainingcenter.Domain.Entities
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
        public ICollection<Order> Orders { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; }
        public ICollection<ExchangeKey> ExchangeKeys { get; set; }
    }
}
