using Fabrit.Heroes.Business.Services.Contracts;
using System;
using System.Threading.Tasks;

using System.Net.Mail;
using Fabrit.Heroes.Infrastructure.Common;
using Fabrit.Heroes.Infrastructure.Common.Email;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using System.Web;
using Fabrit.Heroes.Infrastructure.Common.Password;
using Fabrit.Heroes.Data.Entities.User;

namespace Fabrit.Heroes.Business.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendActivationEmail(User user, string token)
        {
            try
            {
                var emailContent = CreateAccountActivationEmail(user, token);
                var mail = ConfigureMailMessage(emailContent);
                var smtpServer = ConfigureStmpClient();

                await smtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new SendEmailException(ex.Message, ex);
            }
        }

        public async Task SendResetPasswordEmail(User user, bool isForgotten)
        {
            try
            {
                var emailContent = CreateResetPasswordEmail(user, isForgotten);
                var mail = ConfigureMailMessage(emailContent);
                var smtpServer = ConfigureStmpClient();

                await smtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new SendEmailException(ex.Message, ex);
            }
        }

        private Mail CreateAccountActivationEmail(User user, string token)
        {
            return new Mail
            {
                From = Constants.FROM_EMAIL_ADDRESS,
                To = user.Username,
                Subject = Constants.ACCOUNT_ACTIVATION_SUBJECT,
                Body = Constants.ACCOUNT_ACTIVATION_BODY + Constants.ACCOUNT_ACTIVATION_LINK + token
            };
        }

        private Mail CreateResetPasswordEmail(User user, bool isForgotten)
        {
            var mail = new Mail
            {
                From = Constants.FROM_EMAIL_ADDRESS,
                To = user.Username,
                Subject = Constants.RESET_PASSWORD_SUBJECT,
            };

            mail.Body = isForgotten ? mail.Body = Constants.TEMPORARY_PASSWORD_MESSAGE + user.Password : Constants.RESET_PASSWORD_MESSAGE;

            return mail;
        }

        private MailMessage ConfigureMailMessage(Mail email)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(email.From);
            mail.To.Add(email.To);
            mail.Subject = email.Subject;
            mail.Body = email.Body;

            return mail;
        }

        private SmtpClient ConfigureStmpClient()
        {
            var smtpServer = new SmtpClient(Constants.SMTP_CLIENT);
            smtpServer.Port = Constants.SMTP_PORT;
            smtpServer.Credentials = new System.Net.NetworkCredential(Constants.EMAIL_USERNAME, Constants.EMAIL_PASSWORD);
            smtpServer.EnableSsl = true;

            return smtpServer;
        }

        public async Task SendNewEntityActivation(EntityAccountType type, User user, string token)
        {
            try
            {
                var emailContent = CreateNewEntityAccountActivationEmail(type, user, token);
                var mail = ConfigureMailMessage(emailContent);
                var smtpServer = ConfigureStmpClient();

                await smtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new SendEmailException(ex.Message, ex);
            }
        }

        private Mail CreateNewEntityAccountActivationEmail(EntityAccountType type, User user, string token)
        {
            return new Mail
            {
                From = Constants.FROM_EMAIL_ADDRESS,
                To = user.Username,
                Subject = Constants.ACCOUNT_ACTIVATION_SUBJECT,
                Body = "Welcome to Fabrit heroes! \nYou are a " + type.ToString() + "\nYour new Password is: " + user.Password + "\n" + Constants.ACCOUNT_ACTIVATION_BODY + Constants.ACCOUNT_ACTIVATION_LINK + token
            };
        }
    }
}

