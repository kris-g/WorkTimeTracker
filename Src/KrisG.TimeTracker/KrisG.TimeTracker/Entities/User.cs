using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrisG.TimeTracker.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
    }
}
