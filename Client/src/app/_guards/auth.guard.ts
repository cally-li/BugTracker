import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private router:Router) { }
  
  canActivate(): boolean {
    if (this.accountService.isLoggedIn() == null) {
      this.router.navigate(['not-authorized']);
      return false;
    }
    return true;
  }
}
