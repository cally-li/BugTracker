import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../_models/project';
import { ProjectsService } from '../_services/projects.service';

@Component({
  selector: 'app-user-projects',
  templateUrl: './user-projects.component.html',
  styleUrls: ['./user-projects.component.css']
})
export class UserProjectsComponent implements OnInit {


  projects$:Observable<Project[]>;


  constructor(private projectService:ProjectsService) { }

  ngOnInit(): void {
    this.projects$=this.projectService.getProjectsByUser();
  }

}
