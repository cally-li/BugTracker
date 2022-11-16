import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm:FormGroup;

  //server-side validation errors thrown back from interceptor
  validationErrors: string[]=[];

  //message upon successful registration
  registered: boolean = false;
  email: string = '';


  constructor(
    private accountService: AccountService,
    private fb:FormBuilder,
  ) {}

  ngOnInit(): void {
    this.intitializeForm();
  }
  
  intitializeForm(){
    this.registerForm=this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    })
  }


  register() {
    this.accountService.register(this.registerForm.value).subscribe(
      (response: User) => {
        if (response) {
          this.registered = true;
          this.email = response.email;
        }
      },
      (error) => {
        console.log(error);
        this.validationErrors = error;
      }
    );
  }


  //custom validator - check password matches confirm password
  //if passwords don't match, attach validator error called isMatching to the FormControl
  matchValues(matchTo:string): ValidatorFn {
    return (control:AbstractControl) => {
      return control?.value ===control?.parent?.controls[matchTo].value
        ? null : {isMatching:true}   
    }
  }

}
