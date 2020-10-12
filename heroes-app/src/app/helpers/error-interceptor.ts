import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { AuthenticationService } from 'src/app/service/authentication.service';
import { catchError } from 'rxjs/operators';
import { ErrorHandlerService } from 'src/app/service/error-handler.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor extends ErrorHandlerService implements HttpInterceptor {
    constructor(
        private authenticationService: AuthenticationService,
        private router: Router
    ) {
        super();
    }

    public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(err => {
            if (err.status === 401) {
                this.authenticationService.logout();
                this.router.navigate(['/unauthorised '], {});
            }
            
            return throwError(err);
        }));
    }
}
