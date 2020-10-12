export class Constants {
  public static WARNING_MESSAGE_DELETE: string =
    'You will not be able to recover this record!';
  public static WARNING_TITLE_DELETE: string = 'Are you sure?';
  public static WARNING_cONFIRM_BTN_DELETE: string = 'Yes, delete it!';
  public static POWERS_MULTISELECT_PLACEHOLDER: string = 'Select powers';
  public static ALLIES_MULTISELECT_PLACEHLODER: string = 'Select allies';
  public static HERO_ALLY = 'Allies';
  public static NAME = 'Name';
  public static HERO_TYPE = 'Type';
  public static HERO_POWER = 'Powers';
  public static EMPTY_STRING = '';
  public static HERO = 'Hero';
  public static VILLAIN = 'Villain';
  public static INVALID_PARAMETER_MESSAGE =
    'Make sure that all fields are filled out correctly! Chack again ';
  public static HERO_SELECTION_HINT = 'Select a hero type';
  public static ELEMENT_SELECTION_HINT = 'Select an element';
  public static POWER_TRAIT = 'Trait';
  public static POWER_ELEMENT = 'Element';
  public static POWER_DETAILS = 'Details';
  public static CSS_MODAL_CLASS = 'gray modal-lg';
  public static POWER_STRENGTH = 'Strength';
  public static SUCCESS_UPLOAD_MESSAGE = 'Successfully uploaded!';
  public static ERROR_UPLOAD_MESSAGE = 'Choose another image!';
  public static FILE_PREFIX = 'file';
  public static USER_EMAIL = 'Email';
  public static USER_PASSWORD = 'Password';
  public static USER_AGE = 'Age';
  public static USER_AVATAR = 'Profile Picture';
  public static PASSWORDS_NOT_CORRESPONDING =
    "Password and confirmation password doesn't match!";

  public static HERO_SAVED = 'Hero successfully added!';
  public static HERO_EDITED = 'Hero successfully edited!';

  public static VILLAIN_SAVED = 'Villain successfully added!';
  public static VILLAIN_EDITED = 'Villain successfully edited!';

  public static POWER_SAVED = 'Power successfully added!';
  public static POWER_EDITED = 'Power successfully edited!';

  public static USER_REGISTRATION = 'Account created';
  public static PASSWORD_RESETED = 'Check your email to see your new password!';
  public static PASSWORD_CHANGED =
    'Your password has been changed successfully!';
  public static LOGIN_ERROR = 'Filling all fields is mandatory';
  public static USER_PROFILE_EDITED =
    'Your profile has been successfully updated!';
  public static HEROES_EXPORT = 'Heroes';
  public static POWERS_EXPORT = 'Powers';
  public static PASSWORD_STRENGTH =
    'Password. Password should be atleast 8 characters long and should contain one number, one character and one special character';

  public static AUTH_CURRENT_USER_KEY = 'currentUser';

  public static APP_URL = 'https://localhost:44324/';
  public static DATE_FORMAT = 'MM/DD/YYYY';
  public static INVALID_DATE = '01-01-0001';
  public static POPUP_POSITION = 'under';

  public static VILLAIN_DETAIL_REDIRECT_ROUTE = 'home/villain-detail';
  public static COLORS_BIRTHDAY = {
    red: {
      primary: '#ad2121',
      secondary: '#FAE3E3',
    },
    blue: {
      primary: '#1e90ff',
      secondary: '#D1E8FF',
    },
    yellow: {
      primary: '#e3bc08',
      secondary: '#FDF1BA',
    },
  };

  public static PIE = 3.14159265;

  public static TRAININGTIME = 30;

  public static YOU_WON_MESSAGE = 'You won!';
  public static YOU_LOST_MESSAGE = 'You lost!';
  public static EQUAL_SCORE_MESSAGE = 'Equal score';

  public static HEROES_SCORE = 'Heroes score: ';
  public static VILLAINS_SCORE = 'Villains score: ';
  public static INCOMPATIBLE_LEVELS = "Your levels are not compatible";
  public static SELECT_VILLAIN_FIRST_MESSAGE = "Select villain by clicking first";
}
