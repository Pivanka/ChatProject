import { Component, Input, OnInit } from '@angular/core';
import { USER } from 'src/app/models/authorization.models';
import { ProfileService } from '../../profile.service';

@Component({
  selector: 'app-list-friends',
  templateUrl: './list-friends.component.html',
  styleUrls: ['./list-friends.component.scss']
})
export class ListFriendsComponent implements OnInit {

  users!: Array<USER>;
  friends!: Array<USER>;
  userEmail = localStorage.getItem('email') as string;
  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.getFriends(this.userEmail);
    this.getUsers();
  }

  getUsers() {
    this.profileService.getUsers().subscribe(
      res => {
        this.users = res.filter(u => u.email !== this.userEmail);
      },
      err => { alert(err.errore)}
    )
  }

  isFriend(user: USER)
  {
    if(this.friends.find(u => u.email === user.email))
    {
      return true;
    }
    return false;
  }

  getFriends(userEmail: string) {
    this.profileService.getFriends(userEmail).subscribe(
      res => { this.friends = res },
      err => { alert(err.error)}
    )
  }

  deleteFriendHandler(){
    this.getFriends(this.userEmail);
    this.getUsers();
  }
}
