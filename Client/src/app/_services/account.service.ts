import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';

//service to login, logout, make API calls

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  //api url
  baseUrl = 'https://localhost:7214/api/'
  //observable to store logged in user (store 1 User object when logged in)
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  //inject Http client into this service
  constructor(private http: HttpClient) { }

  //receives credentials from login form, returns an observable
  //pipe method used to persist the login (store user in browser's local storage)
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  //helper method: set the current logged in user (in app module)
  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  //check if user is logged in
  isLoggedIn(){
    var user=localStorage.getItem('user');
    return user;
  }

  //logout - remove persisted user from local storage
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  //receives credentials from register form
  register(model: any) {
    return this.http.post(this.baseUrl+'account/register', model).pipe(
      map((user:User) => { return user})
    )
  }
}
