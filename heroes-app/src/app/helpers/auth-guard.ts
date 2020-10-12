import { catchError, map } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { UserData } from 'src/app/models/user-data';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthenticationService } from 'src/app/service/authentication.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  userDataSubscription: any;
  userData = new UserData();

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService  ) {
    this.userDataSubscription = this.authenticationService.currentUser.subscribe(
      (data) => {
        this.userData = data;
      }
    );
  }

  public canActivate(
    _: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.authenticationService.isAuthenticated().pipe(
      map((_: UserData) => {
        return true;
      }),
      catchError((_) => {
        this.router.navigate([''], {
          queryParams: { returnUrl: state.url },
        });
        return of(false);
      })
    );
  }
}
