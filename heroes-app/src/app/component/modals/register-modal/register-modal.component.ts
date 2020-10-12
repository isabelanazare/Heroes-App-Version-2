import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../../../service/user-data.service';
import { Constants } from 'src/app/utils/constants';
import { UserData } from '../../../models/user-data';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { ModalBase } from 'src/app/modals/modal-base';

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html',
  styleUrls: ['./register-modal.component.css'],
})
export class RegisterModalComponent extends ModalBase implements OnInit {
  public newUser: UserData = new UserData();

  constructor(
    private errorHandlerService: ErrorHandlerService,
    private userDataService: UserDataService
  ) {
    super();
  }

  public ngOnInit(): void {
    this.newUser.role = 'Hero';
  }

  public onSave() {
    this._checkAndSubscribeUser();
  }

  private _checkAndSubscribeUser() {
    let isUserValid = this.errorHandlerService.checkUserFields(this.newUser);
    let doesPasswordMatch = this.errorHandlerService.checkPasswordsMatching(
      this.newUser.password,
      this.newUser.confirmationPassword
    );
    if (isUserValid && doesPasswordMatch) {
      this._subscribeUser();
    } else if (!isUserValid) {
      this.errorHandlerService.displayInvalidParameterError(
        Constants.INVALID_PARAMETER_MESSAGE +
          this.errorHandlerService.errorMessage
      );
    } else {
      this.errorHandlerService.displayPasswordMatchingError();
    }
  }

  private _subscribeUser() {
    this.userDataService.createUser(this.newUser).subscribe((_) => {
      this.close.emit(true);
      this.newUser = new UserData();
    });
  }

  public changeRole(event) {
    this.newUser.role = event.target.value;
  }
}
