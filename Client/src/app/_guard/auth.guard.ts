import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/Operators';
import { AccountService } from '../_Services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService, private Toast:ToastrService){}
  
  canActivate(): Observable<boolean> {
    return this.accountService.currentuser$.pipe(
      map(user=> {
           if(user) return true;
             this.Toast.error('you are not authorized');
             
      })
    )
    
  }
  
}
