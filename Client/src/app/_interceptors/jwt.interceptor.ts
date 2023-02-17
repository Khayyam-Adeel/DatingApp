import { Injectable } from '@angular/core';
import { AccountService } from'../_Services/account.service';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/Operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private AccountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.AccountService.currentuser$.pipe(take(1)).subscribe({
      next:user=>{
        if(user){
          request=request.clone({
            setHeaders:{
              Authorization:`Bearer ${ user.token}`
            }
          })
        }
      }
    })
    return next.handle(request);
  }
}
