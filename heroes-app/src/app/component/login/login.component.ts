import { Component, OnInit } from '@angular/core';
import { DataManagementBase } from 'src/app/utils/select-row';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UserData } from 'src/app/models/user-data';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../service/authentication.service';
import { filter, first } from 'rxjs/operators';
import { Constants } from 'src/app/utils/constants';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends DataManagementBase implements OnInit {
  public currentUser: UserData = new UserData();
  private _returnUrl: string = Constants.EMPTY_STRING;

  constructor(
    protected modalService: BsModalService,
    private route: ActivatedRoute,
    protected router: Router,
    private authenticationService: AuthenticationService,
    private errorHandler: ErrorHandlerService
  ) {
    super(modalService, router);

    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/home']);
    }
  }

  public ngOnInit(): void {
    this._returnUrl = '/home';
    this.route.queryParams
      .pipe(filter((params) => params.returnUrl))
      .subscribe((params) => {
        if (params != null) {
          this._returnUrl = params.returnUrl;
        } else {
          this._returnUrl = '/home';
        }
      });
  }

  public onLogin() {
    if (!this.errorHandler.checkLoginFields(this.currentUser)) {
      this.errorHandler.displayInvalidParameterError(
        this.errorHandler.errorMessage
      );
      return;
    }
    this._loginSubscribe();
  }

  private _loginSubscribe() {
    this.authenticationService
      .login(this.currentUser.username, this.currentUser.password)
      .pipe(first())
      .subscribe(
        (_) => {
          this.router.navigate([this._returnUrl]);
        },
        (error) => {
          this.errorHandler.handleError(error);
        }
      );
  }

  protected _loadData() {}
}
