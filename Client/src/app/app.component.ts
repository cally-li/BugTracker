import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
//AppComp implements OnInit interface
export class AppComponent implements OnInit
{

  title = 'BugTracker';
  users: any;

  //constructor: make Http service available w. DI
  constructor(public accountService: AccountService) { }

  //lifecycle event-initialization
  ngOnInit()
  {
    this.setCurrentUser();
  }


  //persist the login -set user in local storage
  setCurrentUser() {
    const _user: User = JSON.parse(localStorage.getItem('user'));
      if (_user) {
        this.accountService.setCurrentUser(_user);
       
      }
  }


}
