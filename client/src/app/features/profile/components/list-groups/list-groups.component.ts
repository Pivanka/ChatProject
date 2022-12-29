import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GROUP } from 'src/app/models/chat.models';
import { ProfileService } from '../../profile.service';

@Component({
  selector: 'app-list-groups',
  templateUrl: './list-groups.component.html',
  styleUrls: ['./list-groups.component.scss']
})
export class ListGroupsComponent implements OnInit {

  groups!: Array<GROUP>;
  userGroups!: Array<GROUP>;
  userEmail = localStorage.getItem('email') as string;
  constructor(private profileService: ProfileService,
              private router: Router) { }

  ngOnInit() {
    this.getGroups();
    this.getUserGroups();
  }

  getGroups() {
    this.profileService.getGroups().subscribe(
      res => { this.groups = res },
      err => { alert(err.error)}
    )
  }

  getUserGroups() {
    this.profileService.getOwnGroups(this.userEmail).subscribe(
      res => { this.userGroups = res },
      err => { alert(err.error)}
    )
  }

  addNewGroup() {
    this.router.navigate(['/create-group']);
  }
}
