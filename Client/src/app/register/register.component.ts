import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  model: any = {};

  //array of validation errors
  validationErrors: string[] = [];
  firstNameError: string = '';
  lastNameError: string = '';
  emailError: string = '';
  passwordError: string = '';

  //message upon successful registration
  registered: boolean = false;
  email: string = '';

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe(
      (response: User) => {
        console.log(response);
        if (response) {
          this.registered = true;
          this.email = response.email;
        }
      },
      (error) => {
        console.log(error);
        this.validationErrors = error;
        this.assignErrors();
      }
    );
  }

  //assign errors to variables in template
  assignErrors(): void {
    //loop over array
    for (let i = 0; i < this.validationErrors.length; i++) {
      if (this.validationErrors[i].includes('First')) {
        this.firstNameError = this.validationErrors[i];
      }
      if (this.validationErrors[i].includes('Last')) {
        this.lastNameError = this.validationErrors[i];
      }
      if (this.validationErrors[i].includes('Email')) {
        this.emailError = this.validationErrors[i];
      }
      if (this.validationErrors[i].includes('Password')) {
        this.passwordError = this.validationErrors[i];
      }
    }
  }
}
