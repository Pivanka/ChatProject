import { Component, Input, OnInit } from '@angular/core';
import { GROUP } from 'src/app/models/chat.models';

@Component({
  selector: 'app-group-item',
  templateUrl: './group-item.component.html',
  styleUrls: ['./group-item.component.scss']
})
export class GroupItemComponent implements OnInit {

  @Input() group!: GROUP
  constructor() { }

  ngOnInit() {
  }

}
