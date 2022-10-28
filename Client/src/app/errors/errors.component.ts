import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-errors',
  templateUrl: './errors.component.html',
  styleUrls: ['./errors.component.css']
})
export class ErrorsComponent implements OnInit {
 baseurl='https://localhost:5001/api/';
 validationError:string[]=[];
  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }
  get401error(){
     this.http.get(this.baseurl+'bugbunny/auth').subscribe(Response=>{
      console.log(Response);
     },error=>{
       console.log(error);   
     });  
  }
  get500error(){
    this.http.get(this.baseurl+'bugbunny/server-error').subscribe(Response=>{
     console.log(Response);
    },error=>{
      console.log(error);   
    });  
 }
 get400error(){
  this.http.get(this.baseurl+'bugbunny/bad-request').subscribe(Response=>{
   console.log(Response);
  },error=>{
    console.log(error);   
  });  
}
get404error(){
  this.http.get(this.baseurl+'bugbunny/not-found').subscribe(Response=>{
   console.log(Response);
  },error=>{
    console.log(error);   
  });  
}
get400validationerror(){
  this.http.post(this.baseurl+'account/register',{}).subscribe(Response=>{
   console.log(Response);
  },error=>{
    console.log(error);  
    this.validationError =error; 
  });  
}

}
