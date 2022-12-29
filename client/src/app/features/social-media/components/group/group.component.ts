import { Component, Input, OnInit, Output } from '@angular/core';
import { USER } from 'src/app/models/authorization.models';
import { GROUP } from 'src/app/models/chat.models';
import { SocialMediaService } from '../../social-media.service';


@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss']
})
export class GroupComponent implements OnInit {

  @Input() group!: GROUP;
  userEmail: string = localStorage.getItem('email') as string;
  userName: string = localStorage.getItem('userName') as string;
  isMember: boolean = true;

  constructor(private socialMediaService: SocialMediaService){}
  ngOnInit(): void {
    this.isMemberGroup();
  }

  isMemberGroup(){
    this.socialMediaService.getGroupUsers(this.group.id as number).subscribe(
    users=>{
      var count: number = 0;
      if(users.find(u => u.email === this.userEmail))
      {
        count++;
      }
      if(count === 0)
      {
        this.isMember = false;
      }
    })
  }

  joinToGroup(){
    const user: USER = {
      userName: this.userName,
      email: this.userEmail
    };
    this.socialMediaService.addUsersToGroup(this.group.id as number, new Array<USER>(user)).subscribe(
      ()=>{
      alert('You join to group succesfully')
      this.isMember = true;
      },
      err => alert(err));
  }
}
