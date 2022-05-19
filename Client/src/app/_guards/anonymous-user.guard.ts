import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AnonymousUserGuard implements CanActivate {
  constructor(private router: Router, private accountService:AccountService) { }

  canActivate(): boolean {
    if (this.accountService.isLoggedIn() != null) {
      this.router.navigate(['home']);
      return false;
    }
    return true;
  }

}
