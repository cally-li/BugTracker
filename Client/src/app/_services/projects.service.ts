import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Project } from '../_models/project';
import { User } from '../_models/user';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class ProjectsService {

  baseUrl=environment.apiUrl;

  //use the service to store project data to avoid API call on every page reload
  projects:Project[]= [];
  user:User;

  constructor(private accountService: AccountService, private http:HttpClient) {
    //take(1) unsubscribes after 1 value has been received
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
    this.getProjectsByUser();
   }

  //get project by user
  getProjectsByUser(){

    if (this.projects.length>0) return of(this.projects);
    
    return this.http.get<Project[]>(this.baseUrl + 'projects/all/'+ this.user.email).pipe(
      map(projects=>{
        this.projects=projects;
        return projects;
      })
    )
  }

  //get project by Id
  getProjectById(id){
    //return observable of the project if local projects [] is populated
    if (this.projects.length>0) return of(this.projects.find(p=>p.id==id));

    //otherwise, call API
    return this.http.get<Project>(this.baseUrl + 'projects/'+ id).pipe(
      map(project=>{
        return project;
      })
    )
  }


}
