import { Component, Input, OnInit,EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_Services/account.service';



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model : any={};
  @Output() cancelRegister = new EventEmitter();
  constructor(private accountService:AccountService, private toastr:ToastrService) { }

  ngOnInit(): void {

  }
 register(){
    this.accountService.register(this.model).subscribe(response=>{
      console.log(response);
      this.cancel();
    },error=>{
      this.toastr.error(error.error);
    }
    );
    
 }
 cancel(){
  this.cancelRegister.emit(false);
 }
}
