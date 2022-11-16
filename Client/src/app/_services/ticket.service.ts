import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Ticket } from '../_models/ticket';
import { User } from '../_models/user';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  baseUrl=environment.apiUrl;

  //use the service to store project data to avoid API call on every page reload
  tickets:Ticket[]= [];
  user:User;

  constructor(public accountService: AccountService, private http:HttpClient) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
    this.getAllTickets();
   }

   getAllTickets(){
    return this.http.get<Ticket[]>(this.baseUrl + 'tickets').pipe(
      map(tickets=>{
        this.tickets=tickets;
        return tickets;
      })
    )
   }

  //get tickets by user
  getTicketsByUser(){

    if (this.tickets.length>0)
      return of(this.tickets.filter(t=>t.submitter.email===this.user.email));
    
    return this.http.get<Ticket[]>(this.baseUrl + 'tickets/all/'+ this.user.email).pipe(
      map(tickets=>{
        return tickets;
      })
    )
  }

  
  //get tickets by project
  getTicketsByProject(project_id){
  
    if (this.tickets.length>0)
      return of(this.tickets.filter(t=>t.projectId===project_id));
    
    return this.http.get<Ticket[]>(this.baseUrl + 'tickets/project/'+ project_id).pipe(
      map(tickets=>{
        return tickets;
      })
    )
  }
    
  }



