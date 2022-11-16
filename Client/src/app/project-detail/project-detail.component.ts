import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { Member } from '../_models/member';
import { Project, Ticket } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';
import { TicketService } from '../_services/ticket.service';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  project$:Observable<Project>;
  tickets$:Observable<Ticket[]>;
  projectId:any = 0;
  assignedPersonnel$:Observable<Member[]>;

  constructor(private projectService: ProjectsService, private userService: UsersService, private activatedRoute:ActivatedRoute, private ticketService:TicketService ) { }

  ngOnInit(): void {
    this.getProject();
    this.getTickets();
    this.getAssignedPersonnel();

  }

  getProject(): void{
      this.activatedRoute.paramMap.subscribe(params => { 
      this.projectId = params.get('id'); 
      this.project$=this.projectService.getProjectById(this.projectId);

    });
  }

  getTickets():void{
    if(this.projectId>0){
      this.tickets$=this.ticketService.getTicketsByProject(this.projectId);
    }
  }
  getAssignedPersonnel():void{
    if(this.projectId>0){
      this.assignedPersonnel$=this.userService.getUsersByProject(this.projectId);
    }
  }

 
}
