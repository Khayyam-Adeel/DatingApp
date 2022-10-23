import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_Services/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Date Rica';
  users:any;

  constructor(private accountService:AccountService){

  }

  ngOnInit() {
    
    // this.getUsers();
    this.setCurrentUser()
  }
  setCurrentUser(){
    const user=JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }

  //Moved to home component
  // getUsers() {
  //   this.http.get('https://localhost:5001/api/users').subscribe({
  //     next: response => this.users = response,
  //     error: error => console.log(error)
  //   })
  // }
  
}
