import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl=environment.apiUrl;

  //use the service to store user data to avoid API call on every page reload
  members:Member[]= [];

  constructor(private http:HttpClient) { }

  getUsers(){
    //if local members exist already, return the members array as an observable
    if (this.members.length>0) return of(this.members);

    //otherwise, make API call (get request returns members array as an observable)
    return this.http.get<Member[]>(this.baseUrl+ 'users').pipe(
      map(members=>{
        this.members=members;
        return members;
      })
    )
  }

  getUser(email:string){
    const member=this.members.find(x=>x.email===email);
    if (member!== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/'+ email)
  }

  updateUser(member:Member){
    //when updating via client side, also update that member in this service's members[]
    return this.http.put(this.baseUrl+'users', member).pipe(
      map(()=>{
        const index = this.members.indexOf(member);
        this.members[index]=member;
      })
    )
  }

  getUsersByProject(projectId:any){
    return this.http.get<Member[]>(this.baseUrl+ 'users/projects/' + projectId).pipe(
      map(members=>{
        return members;
      })
    )
  }

}
