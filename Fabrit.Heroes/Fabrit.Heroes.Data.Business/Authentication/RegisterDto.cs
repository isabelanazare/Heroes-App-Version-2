using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Authentication
{
    public class RegisterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public string Role { get; set; }
    }
}
