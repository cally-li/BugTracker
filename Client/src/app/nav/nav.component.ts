import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}
  displayMessage: string = 'Welcome';

  constructor(public accountService: AccountService, private router:Router) { }

  ngOnInit(): void {
    this.displayName();
  }

  //append user's first name to welcome message
  displayName() {
    const _user: User = JSON.parse(localStorage.getItem('user'));
    if (_user) {
      this.displayMessage += ', ' + _user.firstName;
    }
  }

  //search bar method
  search() {
    console.log(this.model);
  }

  //logout 
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('');
  }

}
