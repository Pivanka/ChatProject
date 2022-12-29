import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { USER } from 'src/app/models/authorization.models';
import { ProfileService } from '../../profile.service';
import { GROUP } from 'src/app/models/chat.models';
import {Location} from '@angular/common';

@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit {

  groupForm!: FormGroup;

  name!: FormControl;
  users!: FormControl;
  friends: Array<USER> = new Array<USER>;
  userEmail = localStorage.getItem('email') as string;
  userName = localStorage.getItem('userName') as string;

  constructor(private profileService: ProfileService,
              private router: Router,
              private _location: Location) {
  }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
    this.getFriends(this.userEmail);
  }

  createFormControls(){
    this.name = new FormControl('', [Validators.required]);
    this.users = new FormControl('');
  }

  createForm() {
    this.groupForm = new FormGroup({
        name: this.name,
        users: this.users,
    });
  }

  getFriends(userEmail: string) {
    this.profileService.getFriends(userEmail).subscribe(
      res => { this.friends = res },
      err => { alert(err.error)}
    )
  }

  createGroup(){
    if(this.users.value.length > 0 && this.groupForm.valid)
    {
      const user: USER = {
        userName: this.userName,
        email: this.userEmail
      }
      this.users.value.push(user)
      const group: GROUP = {
        groupName: this.name.value,
        users: this.users.value
      }
      this.profileService.addGroup(group).subscribe(
        res => { this._location.back(); },
        err => { alert(err.error)}
      )
    }
  }

  back(){
    this.router.navigate(['/profile']);
  }
}
