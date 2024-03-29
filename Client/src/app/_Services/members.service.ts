import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../Models/Member';
const httpoptions={
  headers:new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
 baseurl=environment.apiurl;
  constructor(private http:HttpClient) { }

  getMembers(){
    return this.http.get<Member[]>(this.baseurl+'users'); 
  }
  getMember(username:string){
    return this.http.get<Member>(this.baseurl+'users/'+ username); 
  }
  // getHttpOptions(){
  //   const userString=localStorage.getItem('user');
  //   if(!userString) return;
  //   const user=JSON.parse(userString);
  //   return{
  //     headers: new HttpHeaders({
  //       Authorization:'Bearer ' +user.token
  //     })
  //   }
  // }
}
