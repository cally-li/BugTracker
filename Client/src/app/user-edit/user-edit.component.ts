import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from '../_models/member';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  member:Member;
  user:User;
  @ViewChild('editForm') editForm: NgForm;
  //access browser event to enable "leave site?" message when navigating to different URL upon unsaved changes
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private accountService:AccountService, private memberService:UsersService,
    private toastr: ToastrService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
  }


  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.memberService.getUser(this.user.email).subscribe(member => {
      this.member=member;
    })
  }

  updateMember() {
    this.memberService.updateUser(this.member).subscribe(() => {
      this.toastr.success('Profile updated successfully');
      //reset form values after form submission
      this.editForm.reset(this.member);
    })
  }

}
