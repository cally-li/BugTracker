import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AnonymousUserGuard implements CanActivate {
  constructor(private router: Router)
  {console.log("Anon guard activated") }

  canActivate(): boolean {

    if(localStorage.getItem('user') != null) {
      this.router.navigate(['home']);
      return false;
    }
    return true;
  }

}
