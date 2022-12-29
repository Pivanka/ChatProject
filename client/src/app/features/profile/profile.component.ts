import { Component, OnInit } from '@angular/core';
import { USER } from 'src/app/models/authorization.models';
import { ProfileService } from './profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  user!: USER;
  userEmail = localStorage.getItem('email') as string;
  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.getUser();
  }

  getUser(){
    this.profileService.getUser(this.userEmail).subscribe(
      res => {
        this.user = res;
      },
      err => { alert(err.errore)}
    )
  }
}
