using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AvatarPath { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
