import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { USER } from 'src/app/models/authorization.models';
import { ProfileService } from '../../profile.service';

@Component({
  selector: 'app-user-item',
  templateUrl: './user-item.component.html',
  styleUrls: ['./user-item.component.scss']
})
export class UserItemComponent implements OnInit {

  @Input() user!: USER;
  @Input() isFriend: boolean = false;
  @Output() addFriendEvent: EventEmitter<any> = new EventEmitter<any>();
  @Output() deleteFriendEvent: EventEmitter<any> = new EventEmitter<any>();

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
  }

  addFriend(friendEmail: string)
  {
    const userEmail = localStorage.getItem('email') as string;
    this.profileService.addFriend(userEmail, friendEmail)
    .subscribe(
      () => { this.addFriendEvent.emit() },
      err => { alert(err)}
    )
  }

  deleteFriend(friendEmail: string)
  {
    const userEmail = localStorage.getItem('email') as string;
    this.profileService.deleteFriend(userEmail, friendEmail)
    .subscribe(
      () => { this.deleteFriendEvent.emit() },
      err => { alert(err)}
    )
  }
}
