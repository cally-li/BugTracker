import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from '../_models/member';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  members$:Observable<Member[]>;

  constructor(private memberService: UsersService) { }

  ngOnInit(): void {
    this.members$=this.memberService.getUsers();
  }


}
