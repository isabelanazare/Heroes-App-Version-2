
using Fabrit.Heroes.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fabrit.Heroes.Business.Services.Contracts
{
    public enum EntityAccountType
    {
        Hero,
        Vilain
    }

    public interface IEmailSenderService
    {
        Task SendActivationEmail(User user, string token);
        Task SendNewEntityActivation(EntityAccountType type, User user, string token);
        Task SendResetPasswordEmail(User user, bool isForgotten);
    }
}
