using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Authentication;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;
using Fabrit.Heroes.Data.Entities.Hero;
using Fabrit.Heroes.Data.Business.User;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Fabrit.Heroes.Infrastructure.Common.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Fabrit.Heroes.Business.Services
{
    public class UserService : IUserService
    {
        private readonly HeroesDbContext _context;
        private readonly IHashingManager _hashingManager;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IJWTService _jwtService;


        public UserService(HeroesDbContext context, IHashingManager hashingManager, IEmailSenderService emailSenderService, IJWTService jwtService)
        {
            _context = context;
            _hashingManager = hashingManager;
            _emailSenderService = emailSenderService;
            _jwtService = jwtService;
        }

        public IAsyncEnumerable<User> GetAll()
        {
            return _context.Users.AsAsyncEnumerable();
        }

        public async Task<User> FindById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task CreateUser(RegisterDto registerDto)
        {
            User user = new User()
            {
                FullName = registerDto.FullName,
                Email = registerDto.Username,
                Password = _hashingManager.GetHashedPassword(registerDto.Password),
                Username = registerDto.Username,
            };
            await CheckUserFields(user, false);
            //user.Password = _hashingManager.GetHashedPassword(user.Password);
            user.Email = user.Username;
            user.IsActivated = false;
            user.WasPasswordForgotten = false;
            user.WasPasswordChanged = false;
            user.AvatarPath = Constants.DEFAULT_IMAGE_USER;
            var token = _jwtService.GenerateRegisterJWT(user);
            await _emailSenderService.SendActivationEmail(user, token);
            user.RoleId = (int)RoleType.Regular;

            await _context.AddAsync(user);
            await _context.AddAsync(CreateEntityUser(user, registerDto.Role));
            await _context.SaveChangesAsync();
        }

        private Hero CreateEntityUser(User user, string role)
        {
            return new Hero
            {
                AvatarPath = user.AvatarPath,
                Name = user.FullName,
                User = user,
                Birthday = DateTime.Now,
                IsBadGuy = role == Constants.VILLAIN_ROLE,
                TypeId = (int)EntityType.Real,
            };
        }

        public async Task ActivateAccount(string token)
        {
            var email = _jwtService.DecodeRegisterToken(token);

            var user = await FindByEmail(email);

            if (user.IsActivated)
            {
                throw new AccountAlreadyConfirmed();
            }
            user.IsActivated = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserDto userDto)
        {
            User user = new User()
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Password=userDto.Password,
                FullName=userDto.FullName,
                AvatarPath=userDto.AvatarPath,
                Age=userDto.Age,
                Email=userDto.Username
            };
            await CheckUserFields(user, true);
            var dbUser = await FindByUsername(user.Username);
            dbUser.FullName = user.FullName;
            dbUser.Age = user.Age;
            dbUser.AvatarPath = user.AvatarPath;

            _context.Users.Update(dbUser);
            await _context.SaveChangesAsync();
            var hero = await _context.Heroes.FirstOrDefaultAsync(hero => hero.UserId == user.Id);
            hero.AvatarPath = user.AvatarPath;
            _context.Heroes.Update(hero);
            await _context.SaveChangesAsync();
        }

        public async Task ResetPassword(string email, bool isForgotten, string password = null, string newPassword = null)
        {
            if (!isForgotten && string.IsNullOrEmpty(password) && string.IsNullOrEmpty(newPassword))
            {
                throw new InvalidParameterException("Empty password");
            }

            var dbUser = await FindByUsername(email);
            if (isForgotten)
            {
                var randomPassword = GetRandomPassword();
                dbUser.WasPasswordForgotten = true;
                dbUser.WasPasswordChanged = true;
                dbUser.Password = randomPassword;
                await _emailSenderService.SendResetPasswordEmail(dbUser, true);
                dbUser.Password = _hashingManager.GetHashedPassword(randomPassword);
            }
            else
            {
                if (!dbUser.Password.Equals(_hashingManager.GetHashedPassword(password)))
                {
                    throw new UnauthorisedException("Your actual password is incorrect");
                }

                dbUser.WasPasswordForgotten = false;
                dbUser.WasPasswordChanged = false;
                dbUser.Password = _hashingManager.GetHashedPassword(newPassword);
                await _emailSenderService.SendResetPasswordEmail(dbUser, false);
            }

            _context.Users.Update(dbUser);
            await _context.SaveChangesAsync();
        }

        private string GetRandomPassword()
        {
            var random = new Random();
            var passwodValue = random.Next(0, 1_000_000);
            var randomPassword = passwodValue.ToString(Constants.PASSWORD_TEMPLATE);

            return randomPassword;
        }

        private async Task<User> FindByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email.Equals(email))
                                     .FirstOrDefaultAsync();

            return user != null
                ? user
                : throw new UnauthorisedException($"No user with email: {email}");
        }

        private async Task CheckUserFields(User userData, bool isEditMode)
        {
            if ((string.IsNullOrEmpty(userData.FullName) || string.IsNullOrEmpty(userData.Password) || string.IsNullOrEmpty(userData.Username)) && !isEditMode)
            {
                throw new InvalidParameterException();
            }

            if ((string.IsNullOrEmpty(userData.FullName) || userData.Age < 1 || string.IsNullOrEmpty(userData.AvatarPath)) && isEditMode)
            {
                throw new InvalidParameterException();
            }

            if (isEditMode)
            {
                try
                {
                    Convert.ToInt32(userData.Age.ToString());
                }
                catch
                {
                    throw new InvalidParameterException();
                }
            }

            try
            {
                var userFromDb = await FindByUsername(userData.Username);
                if (userFromDb != null && userFromDb.Id != userData.Id)
                {
                    throw new DuplicateException($"There is a user with the same username: {userData.Username}");
                }
            }
            catch { }
        }

        private async Task<User> FindByUsername(string username)
        {
            var user = await _context.Users
                .Where(u => u.Username.Equals(username))
                .FirstOrDefaultAsync();

            return user != null
                ? user
                : throw new EntityNotFoundException($"No user with username/email {username}");
        }

        public async Task CreateHeroUser(User user)
        {
            user.Password = GetRandomPassword();
            await CheckUserFields(user, false);
            user.IsActivated = false;
            user.WasPasswordForgotten = false;
            user.WasPasswordChanged = false;
            user.AvatarPath = Constants.DEFAULT_IMAGE_HERO;
            var token = _jwtService.GenerateRegisterJWT(user);
            await _emailSenderService.SendNewEntityActivation(EntityAccountType.Hero, user, token);
            user.Password = _hashingManager.GetHashedPassword(user.Password);
            user.RoleId = (int)RoleType.Regular;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteUserById(int id)
        {
            var user = await _context.Users.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new EntityNotFoundException($"There is no user with id: {id}");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
