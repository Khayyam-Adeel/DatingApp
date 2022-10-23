import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/Operators'
import { User } from '../_Models/User';


//service are injectable : inject this service is any component's constructor
//service  is singalton : If intialized it will stay initialized untill app disposed
@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseurl="https://localhost:5001/api/"
  constructor(private http:HttpClient) { } 
 private CurrentUserSource= new ReplaySubject<User>(1);
 currentuser$= this.CurrentUserSource.asObservable();
  login(model:any){
    return this.http.post(this.baseurl+'account/login',model).pipe(
      map( (response:User)=>{
          const user=response
          if(user){
            localStorage.setItem('user',JSON.stringify(user));
            this.CurrentUserSource.next(user)
          } 
      }
      ) 

    )
  }
  setCurrentUser(user:User){
   this.CurrentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem('user');
    this.CurrentUserSource.next(null)
  }
  register(model:any){
    return this.http.post(this.baseurl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.CurrentUserSource.next(user);
        }
      })
    );
  }
}
