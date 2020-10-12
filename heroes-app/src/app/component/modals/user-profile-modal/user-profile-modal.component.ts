import { Component, OnInit } from '@angular/core';
import { UserDataService } from 'src/app/service/user-data.service';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { UserProfileBase } from 'src/app/utils/user-profile-base';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { Constants } from 'src/app/utils/constants';
import { FileService } from 'src/app/service/file.service';
import { ResetPasswordDto } from 'src/app/models/password';
import { UserData } from 'src/app/models/user-data';

@Component({
  selector: 'app-user-profile-modal',
  templateUrl: './user-profile-modal.component.html',
  styleUrls: ['./user-profile-modal.component.css'],
})
export class UserProfileModalComponent
  extends UserProfileBase
  implements OnInit {
  public wantSecurityChanges: boolean = false;
  public resetPasswordDto: ResetPasswordDto = new ResetPasswordDto();
  public userForSave: UserData = new UserData();

  constructor(
    private errorHandlerService: ErrorHandlerService,
    private userDataService: UserDataService,
    protected fileService: FileService,
    protected authenticationService: AuthenticationService
  ) {
    super(fileService, authenticationService);
    this.authenticationService.currentUser.subscribe((u) => {
      this.newUser = u;
      this.userAvatar = u.avatarPath;
    });
  }

  public ngOnInit(): void {}

  public onSave() {
    if (this.wantSecurityChanges) {
      this._onChangePassword();
    } else {
      this._updateProfile();
    }
  }

  public onClose() {
    if (this.wantSecurityChanges) {
      this.wantSecurityChanges = false;
    } else {
      this.onCancel();
    }
  }

  private _updateProfile() {
    this.newUser.avatarPath = this.userAvatar;
    if (!this.errorHandlerService.checkUserProfileFields(this.newUser)) {
      this.errorHandlerService.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE +
          this.errorHandlerService.errorMessage
      );
    } else {
      this.userForSave = this.newUser;
      this.userForSave.avatarPath = this.userForSave.avatarPath.substr(
        Constants.APP_URL.length
      );
      this.userDataService
        .updateUserProfile(this.userForSave)
        .subscribe((user) => {
          this.newUser = user;
          this.newUser.avatarPath = Constants.APP_URL + user.avatarPath;
          this.authenticationService.setCurrentUserValue(this.newUser);
          this.close.emit(true);
        });
    }
  }

  public displayChangePasswordScreen() {
    this.wantSecurityChanges = true;
  }

  private _onChangePassword() {
    if (this._checkNewPassword()) {
      this.userDataService
        .resetPassword(
          this.newUser.email,
          this.resetPasswordDto.actualPassword,
          this.resetPasswordDto.newPassword,
          false
        )
        .subscribe((_) => {
          this.wantSecurityChanges = false;
          this.resetPasswordDto = new ResetPasswordDto();
        });
    }
  }

  private _checkNewPassword(): boolean {
    if (
      !this.errorHandlerService.checkPasswordsMatching(
        this.resetPasswordDto.newPassword,
        this.resetPasswordDto.newConfirmPassword
      )
    ) {
      this.errorHandlerService.displayPasswordMatchingError();
      return false;
    } else if (
      !this.errorHandlerService.checkNewPasswordFields(this.resetPasswordDto)
    ) {
      this.errorHandlerService.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE + Constants.PASSWORD_STRENGTH
      );
      return false;
    }
    return true;
  }
}
