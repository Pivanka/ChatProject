import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { USER } from "src/app/models/authorization.models";
import { ProfileService } from "src/app/features/profile/profile.service";

@Component({
  selector: 'add-friend-dialog',
  templateUrl: 'add-friend-dialog.component.html',
  styleUrls: ['./add-friend-dialog.component.scss']
})
export class AddFriendDialog implements OnInit {

  constructor(private profileService: ProfileService) {}

  groupForm!: FormGroup;
  users!: FormControl;

  friends!: Array<USER>;
  userEmail = localStorage.getItem('email') as string;

  ngOnInit() {
    this.createFormControls();
    this.createForm();
    this.getFriends(this.userEmail);
  }

  createFormControls(){
    this.users = new FormControl('');
  }

  createForm() {
    this.groupForm = new FormGroup({
        users: this.users,
    });
  }

  getFriends(userEmail: string) {
    this.profileService.getFriends(userEmail).subscribe(
      res => { this.friends = res },
      err => { alert(err.error)}
    )
  }

}
