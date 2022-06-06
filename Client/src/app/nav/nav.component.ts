import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { take } from 'rxjs/operators';
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
  user:User;
  
  constructor(public accountService: AccountService, private router:Router) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);

  }

  ngOnInit(): void {
    this.displayName();
  }

  //append user's first name to welcome message
  displayName() {
    if (this.user) {
      this.displayMessage += ', ' + this.user.firstName;
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
