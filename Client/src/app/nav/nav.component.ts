import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {  ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_Models/User';
import { AccountService } from '../_Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any={}
  constructor(public accountService:AccountService, private router:Router, private toastr:ToastrService) { }
   
  ngOnInit(): void {
    
  }
 login(){
  // console.log(this.model);
  this.accountService.login(this.model).subscribe(Response=>{
     this.router.navigateByUrl('/members');

  },
  );
  
 }

 logout(){
  this.accountService.logout();
  this.router.navigateByUrl('/');

 }

}
