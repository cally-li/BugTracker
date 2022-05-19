import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {
 
  baseUrl='https://localhost:7214/api/';
  validationErrors: string[] =[];


  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  //test API error responses on client side

 //Not Found
  get404Error(){
    this.http.get(this.baseUrl+ 'buggy/not-found').subscribe(
      response=>{console.log(response);},
      error=>{console.log(error);}
    )
  }

  //bad request
  get400Error(){
    this.http.get(this.baseUrl+ 'buggy/bad-request').subscribe(
      response=>{console.log(response);},
      error=>{console.log(error);}
    )
  }

  //server error
  get500Error(){
    this.http.get(this.baseUrl+ 'buggy/server-error').subscribe(
      response=>{console.log(response);},
      error=>{console.log(error);}
    )
  }

  //Unauthorized
  get401Error(){
    this.http.get(this.baseUrl+ 'buggy/auth').subscribe(
      response=>{console.log(response);},
      error=>{console.log(error);}
    )
  }
  
  //error for empty form fields being submitted
  get400ValidationError(){
    this.http.post(this.baseUrl+ 'account/register', {}).subscribe( //send empty object to URL trigger validation error
      response=>{console.log(response);},
      error=>{
        console.log(error);
        //error returned is array of validation errors
        this.validationErrors=error; 
      }
    )
  }
}
