import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',  //metadata
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  //class property (any type, initialized to empty obj) : for storing data that user enters into the login form
  model: any = {}

  //inject account service
  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  //login method: pass in model with login credentials from form to the login method of account service
  login() {
    this.accountService.login(this.model).subscribe(
      response => {
        this.router.navigateByUrl('/home');
      },
      error => {
        console.log(error);
        this.toastr.error(error.error)
    })
  }
}
