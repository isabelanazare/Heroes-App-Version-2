import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../service/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    private _enviromentUrl: string = "https://localhost:44324/api/";

    constructor(
        private authencticationService: AuthenticationService
    ) { }

    public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const currentUser = this.authencticationService.currentUserValue;
        const isLoggedIn = currentUser && currentUser.token;
        const isApiUrl = req.url.startsWith(this._enviromentUrl);

        if (isLoggedIn && isApiUrl) {
            req = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(req);
    }
}
