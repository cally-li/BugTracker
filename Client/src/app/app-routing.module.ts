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
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { NotAuthorizedComponent } from './errors/not-authorized/not-authorized.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

const routes: Routes = [

  //guard redirects to home page if logged in
  { path: 'login', component: LoginComponent, canActivate: [AnonymousUserGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [AnonymousUserGuard] },
  //route for error testing
  { path: 'errors', component: TestErrorsComponent },
  { path: 'not-authorized', component: NotAuthorizedComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },

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
      { path: 'user/edit', component: UserEditComponent, canDeactivate: [PreventUnsavedChangesGuard] },
      // { path: 'projects', component: ProjectsComponent },
      // { path: 'tickets', component: TicketsComponent },
      // {path: 'user/edit', component: UserEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
    ]
  },
  
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },

  
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
