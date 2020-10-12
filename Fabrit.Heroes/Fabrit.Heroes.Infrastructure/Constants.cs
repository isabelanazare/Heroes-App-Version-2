using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Infrastructure.Common
{
    public class Constants
    {
        #region Charts
        public const string HERO_CHART_STACK = "a";
        #endregion Charts

        #region HttpStatus
        public const string HTTP_CREATED = "created";
        public const string HTTP_UPDATED = "updated";
        #endregion HttpStatus

        #region ExceptionTitle
        public const string OUT_OF_RANGE_EXCEPTION_TITLE = "Parameter out of range";
        public const string INVALID_FILE_EXCEPTION = "Inavlid avatar photo";
        public const string SEND_EMAIL_EXCEPTION = "Can't send the email";
        public const string DUPLICATE_EXCEPTION = "Conflict between entities";
        public const string ENTITITY_NOT_FOUND_EXCEPTION = "Entity Not Found";
        public const string ENUM_TYPE_NOT_FOUND_EXCEPTION = "EnumType Not Found";
        public const string INVALID_PARAMETER_EXCEPTION = "Invalid Parameter";
        public const string NULL_PARAMETER_EXCEPTION = "Null Parameter";
        public const string ACCOUNT_ALREADY_CONFIRMED_EXCEPTION = "Account already confirmed";
        public const string UNAUTHORIZED_EXCEPTION = "Unauthorized access";
        public const string AUTHENTICATION_EXCEPTION = "Authentication failed";
        #endregion ExceptionTitle

        #region UploadImage
        public const string HEROES_DIRECTORY = "Heroes";
        public const string IMAGES_DIRECTORY = "Images";
        public const string USERS_DIRECTORY = "Users";
        public const char QUOTATION_MARK = '"';
        public const string APP_URL = "https://localhost:44324/";
        public const string DEFAULT_IMAGE_HERO = "Images\\Default\\defaultHeroPicture.jpg";
        public const string DEFAULT_IMAGE_USER = "Images\\Default\\defaultUserPicture.gif";
        public const string DEFAULT_IMAGE_VILLAIN = "Images\\Default\\defaultVillainPicture.jpg";
        public static readonly List<string> acceptedImageTypes = new List<string>() { "image/jpg", "image/jpeg", "image/pjpeg", "image/gif", "image/x-png", "image/png" };
        #endregion UploadImage

        #region Email
        public const string FROM_EMAIL_ADDRESS = "fabrit.heroes@gmail.com";
        public const string ACCOUNT_ACTIVATION_SUBJECT = "Account activation";
        public const string ACCOUNT_ACTIVATION_BODY = "Click on the bellow link for activate your account \n";
        public const string ACCOUNT_ACTIVATION_LINK = "http://localhost:4200/account-activation?token=";
        public const string SLASH_SYMBOL = "/";
        public const string EMAIL_USERNAME = "fabrit.heroes";
        public const string EMAIL_PASSWORD = "Ciocolata1*";
        public const string SMTP_CLIENT = "smtp.gmail.com";
        public const int SMTP_PORT = 587;
        public const string TEMPORARY_PASSWORD_MESSAGE = "There is your temporary pessword. You MUST change it in your Profile section after login. ";
        public const string RESET_PASSWORD_MESSAGE = "Your password has been changed";
        public const string RESET_PASSWORD_SUBJECT = "Reset password";
        #endregion Email

        #region Authentication
        public const string UNAUTHORIZED_MESSAGE = "Unauthorized";
        public const string AUTHORIZATION_ITEM = "User";
        public const string AUTHORIZATION_HEADER = "Authorization";
        public const string AUTHORIZATION_ID = "id";
        public const string PASSWORD_TEMPLATE = "000000";
        #endregion Authentication

        #region Powers
        public const string STRENGTH_POWER = "Strength";
        public const string SPEED_POWER = "Speed";
        public const string INVISIBILITY_POWER = "Invisibility";
        public const string TELEPATHY_POWER = "Telepathy";
        public const string MADDJSKILLZS_POWER = "Mad DJ Skillz";
        public const string CURSEOFBADPROGRAMMING_POWER = "CurseOfBadProgramming";
        #endregion Powers
        public const string DATE_FORMAT = "MM/dd/yyyy";

        #region HeroRoles
        public const string HERO_ROLE = "Hero";
        public const string VILLAIN_ROLE = "Villain";

        #endregion
        public const int HERO_POWER_MIN_CHANGE_TIME = 1;
        public const string CRON_JOB_SCHEDULE = "*/1 * * * *";

        #region GameResult
        public const string HEROES_WON_MESSAGE = "Heroes won";
        public const string VILLAINS_WON_MESSAGE = "Villains won";
        public const string EQUAL_SCORE_MESSAGE = "It's a tie";
        #endregion

    }

    public enum HeroTypes
    {
        Real = 1,
        Fictional = 2
    }
}
