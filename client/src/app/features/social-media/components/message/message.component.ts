import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MESSAGE } from 'src/app/models/chat.models';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  @Input() message!: MESSAGE
  @Output() messageDeleteEvent = new EventEmitter<number>();
  @Output() messageDeleteForUserEvent = new EventEmitter<number>();
  @Output() messageReplyEvent = new EventEmitter<number>();
  @Output() messageEditEvent = new EventEmitter<number>();
  currentUser!: string;

  constructor() {}

  ngOnInit(): void {
    this.currentUser = localStorage.getItem('userName') as string;
  }

  deleteMessage(){
    this.messageDeleteEvent.emit(this.message.id);
  }

  deleteForUserMessage(){
    this.messageDeleteForUserEvent.emit(this.message.id);
  }

  replyMessage(){
    this.messageReplyEvent.emit(this.message.id);
  }

  editMessage(){
    this.messageEditEvent.emit(this.message.id);
  }

  isDeletedForUser(): boolean{

    if(this.message.deletedForUser)
    {
      if(this.currentUser === this.message.userName)
      {
        return false;
      }
    }
    return true;
  }
}
