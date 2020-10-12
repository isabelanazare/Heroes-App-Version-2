import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Constants } from 'src/app/utils/constants';
import { UserDataService } from 'src/app/service/user-data.service';

@Component({
  selector: 'app-account-validation',
  templateUrl: './account-validation.component.html',
  styleUrls: ['./account-validation.component.scss'],
})
export class AccountValidationComponent implements OnInit {
  private _token: string = Constants.EMPTY_STRING;

  constructor(
    private route: ActivatedRoute,
    private userDataService: UserDataService
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this._token = params.token;
      this._sendUserToken();
    });
  }

  private _sendUserToken() {
    if (!this._token) {
      return;
    }
    this.userDataService.sendUserToken(this._token).subscribe();
  }
}
