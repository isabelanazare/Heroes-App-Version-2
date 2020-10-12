import { Component, OnInit } from '@angular/core';
import { UserDataService } from 'src/app/service/user-data.service';
import { Constants } from 'src/app/utils/constants';
import { ModalBase } from '../../../modals/modal-base';

@Component({
  selector: 'app-reset-password-modal',
  templateUrl: './reset-password-modal.component.html',
  styleUrls: ['./reset-password-modal.component.css'],
})
export class ResetPasswordModalComponent extends ModalBase implements OnInit {
  public userEmail: string = Constants.EMPTY_STRING;

  constructor(private userDataService: UserDataService) {
    super();
  }

  public ngOnInit(): void {}

  public onSave() {
    this._resetPassword();
  }

  private _resetPassword() {
    this.userDataService
      .resetPassword(
        this.userEmail,
        Constants.EMPTY_STRING,
        Constants.EMPTY_STRING,
        true
      )
      .subscribe((_) => {
        this.close.emit(true);
        this.userEmail = Constants.EMPTY_STRING;
      });
  }
}
