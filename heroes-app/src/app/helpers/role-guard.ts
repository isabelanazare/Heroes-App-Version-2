import { UserData } from '../models/user-data';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../service/authentication.service';
import { catchError, map } from 'rxjs/operators';
import { of, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  userDataSubscription: any;
  userData = new UserData();

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    this.userDataSubscription = this.authenticationService.currentUser.subscribe(
      (data) => {
        this.userData = data;
      }
    );
  }

  public canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    const expectedRole = route.data.expectedRole;

    return this.authenticationService.isAuthenticated().pipe(
      map((user: UserData) => {
        if (user.role === expectedRole) return true;
        this.router.navigate(['/unauthorised'], {
          queryParams: { returnUrl: state.url },
        });
        return false;
      }),
      catchError((_) => {
        this.router.navigate(['/unauthorised'], {
          queryParams: { returnUrl: state.url },
        });
        return of(false);
      })
    );
  }
}
