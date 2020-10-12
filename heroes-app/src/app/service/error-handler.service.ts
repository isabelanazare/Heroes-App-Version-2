import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { ProblemDetails } from '../models/problem-details';
import Swal from 'sweetalert2';
import { Hero } from '../models/hero';
import { Constants } from '../utils/constants';
import { Power } from '../models/power';
import { UserData } from '../models/user-data';
import { ResetPasswordDto } from '../models/password';
import { Villain } from '../models/villain';

@Injectable({
  providedIn: 'root',
})
export abstract class ErrorHandlerService extends ProblemDetails {
  public errorMessage = Constants.EMPTY_STRING;
  private _passwordPattern: RegExp = new RegExp(
    '^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$'
  );

  constructor() {
    super();
  }

  public handleError<T>(_ = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.detail = error.error.Detail;
      this.setErrorAlert(this.detail);
      return of(result as T);
    };
  }

  public displaySuccess(message: string) {
    Swal.fire(message);
  }

  public displayInvalidParameterError(message: string) {
    this.setErrorAlert(message);
  }

  public displayPasswordMatchingError() {
    this.setErrorAlert(Constants.PASSWORDS_NOT_CORRESPONDING);
  }

  public setErrorAlert(recivedError) {
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: recivedError,
    });
  }

  public checkLoginFields(user: UserData): boolean {
    if (!user.password || !user.username) {
      this.errorMessage = Constants.LOGIN_ERROR;
      return false;
    }
    return true;
  }

  public checkHeroFields(hero: Hero): boolean {
    if (!hero.name) {
      this.errorMessage = Constants.NAME;
      return false;
    }
    if (!hero.typeId) {
      this.errorMessage = Constants.HERO_TYPE;
      return false;
    }
    if (!hero.powers) {
      this.errorMessage = Constants.HERO_POWER;
      return false;
    }
    if (!hero.allies) {
      this.errorMessage = Constants.HERO_ALLY;
      return false;
    }
    return true;
  }

  public checkVillainFields(villain: Villain): boolean {
    if (!villain.name) {
      this.errorMessage = Constants.NAME;
      return false;
    }
    return true;
  }

  public checkPowerFields(power: Power): boolean {
    if (!power.name) {
      this.errorMessage = Constants.NAME;
      return false;
    }
    if (!power.mainTrait) {
      this.errorMessage = Constants.POWER_TRAIT;
      return false;
    }
    if (power.strength < 0 || power.strength > 100) {
      this.errorMessage = Constants.POWER_STRENGTH;
      return false;
    }
    if (!power.elementId) {
      this.errorMessage = Constants.POWER_ELEMENT;
      return false;
    }
    if (!power.details) {
      this.errorMessage = Constants.POWER_ELEMENT;
      return false;
    }
    return true;
  }

  public checkUserFields(user: UserData): boolean {
    if (!user.fullName) {
      this.errorMessage = Constants.NAME;
      return false;
    }
    if (!user.username) {
      this.errorMessage = Constants.USER_EMAIL;
      return false;
    }
    if (!user.password || !user.confirmationPassword) {
      this.errorMessage = Constants.USER_PASSWORD;
      return false;
    }
    if (!this._passwordPattern.test(user.password)) {
      this.errorMessage = Constants.PASSWORD_STRENGTH;
      return false;
    }
    return true;
  }

  public checkUserProfileFields(user: UserData): boolean {
    if (!user.fullName) {
      this.errorMessage = Constants.NAME;
      return false;
    }
    if (!user.age || user?.age < 0) {
      this.errorMessage = Constants.USER_AGE;
      return false;
    }
    if (!user.avatarPath) {
      this.errorMessage = Constants.USER_AVATAR;
      return false;
    }
    return true;
  }

  public checkNewPasswordFields(password: ResetPasswordDto): boolean {
    if (
      !password.actualPassword ||
      !password.newConfirmPassword ||
      !password.newPassword
      || !this._passwordPattern.test(password.newPassword)
    ) {
      return false;
    }
    return true;
  }

  public checkPasswordsMatching(
    password: string,
    confirmPassword: string
  ): boolean {
    if (password !== confirmPassword) {
      return false;
    }
    return true;
  }
}
