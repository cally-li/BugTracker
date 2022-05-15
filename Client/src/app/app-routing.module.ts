import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MessagesComponent } from './messages/messages.component';
import { RegisterComponent } from './register/register.component';
import { SiteLayoutComponent } from './site-layout/site-layout.component';
import { AuthGuard } from './_guards/auth.guard';
import { AnonymousUserGuard } from './_guards/anonymous-user.guard';
import { UsersComponent } from './users/users.component';
import { UserInfoComponent } from './user-info/user-info.component';

const routes: Routes = [

  //guard redirects to home page if logged in
  { path: 'login', component: LoginComponent, canActivate: [AnonymousUserGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [AnonymousUserGuard] },

  //redirect root page to login
  { path: '', redirectTo: "/login", pathMatch: 'full' },

  //apply navbar layout to logged-in routes
  {
    path: '',
    component: SiteLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'home', component: HomeComponent },
      { path: 'messages', component: MessagesComponent },
      { path: 'users', component: UsersComponent },
      { path: 'user/:id', component: UserInfoComponent }
    ]
  },

  //create "component: PageNotFoundComponent https://angular.io/guide/router-tutorial
  { path: '**', component: HomeComponent, pathMatch: 'full' },

  
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
