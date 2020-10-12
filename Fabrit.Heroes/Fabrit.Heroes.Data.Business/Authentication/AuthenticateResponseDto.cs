using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Authentication
{
    public class AuthenticateResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string AvatarPath { get; set; }
        public string Role { get; set; }


        public AuthenticateResponseDto(Fabrit.Heroes.Data.Entities.User.User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Username = user.Username;
            Age = user.Age;
            AvatarPath = Constants.APP_URL + user.AvatarPath;
            Email = user.Email;
            Token = token;
            Role = user.Role.Name;
        }
    }
}
