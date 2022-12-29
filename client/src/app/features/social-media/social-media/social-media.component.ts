import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GROUP, MESSAGE } from 'src/app/models/chat.models';
import { SocialMediaService } from '../social-media.service';

@Component({
  selector: 'app-social-media',
  templateUrl: './social-media.component.html',
  styleUrls: ['./social-media.component.scss']
})
export class SocialMediaComponent implements OnInit {

  groups!: Array<GROUP>;
  messages!: Array<MESSAGE>;
  groupId!: number;
  groupName!: string;
  filterBy!: string;
  userEmail!: string;

  constructor(private activatedRoute: ActivatedRoute,
    private socialMediaService: SocialMediaService) {}

  ngOnInit(): void {
    this.userEmail = localStorage.getItem('email') as string;
    this.getGroups();
  }

  getGroups(){
    this.socialMediaService.getGroups(this.userEmail).subscribe(
      res => { this.groups = res },
      err => { alert(err.error)}
    )
  }

  getChat(){
    this.socialMediaService.getMessages(this.groupId).subscribe(
      res => { this.messages = res; },
      err => { alert(err.error)}
    )
  }
  getGroupId(){

    if(this.activatedRoute.firstChild)
    {
     this.activatedRoute.firstChild.params.subscribe(
        params => {
          this.groupId = +params['id'];
        })
    }
    const name = this.groups.find(x => x.id == this.groupId) as GROUP;
    this.groupName = name.groupName;
    this.getChat();
  }

  filter(){
    console.log(this.filterBy)
    this.socialMediaService.searchGroups(this.filterBy).subscribe(
      groups => {
        this.groups = [...groups];
      })
  }
}
