import { Component, EventEmitter, Inject, Input, OnInit, Output, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { USER } from 'src/app/models/authorization.models';
import { MESSAGE, MESSAGE_TO_ADD, MESSAGE_TO_UPDATE} from 'src/app/models/chat.models';
import { SocialMediaService } from '../../social-media.service';
import { AddFriendDialog } from './add-friend-dialog/add-friend-dialog.component';

@Component({
  selector: 'app-group-chat',
  templateUrl: './group-chat.component.html',
  styleUrls: ['./group-chat.component.scss']
})
export class GroupChatComponent implements OnInit {

  messageForm!: FormGroup;
  newMessage!: FormControl;
  currentUser!: string;
  parentMessageId!: number;
  editMessageId!: number;
  isEditMessage: boolean = false;
  isReplyMessage: boolean = false;
  userEmail = localStorage.getItem('email') as string;
  users!: Array<USER>;

  @Input() groupId!: number;
  @Input() groupName!: string;
  @Input() messages!: Array<MESSAGE> ;
  @Output() messageEvent = new EventEmitter();

  constructor(private socialMediaService: SocialMediaService,
              public dialog: MatDialog,
              private renderer: Renderer2) {}

  ngOnInit() {
    this.currentUser = localStorage.getItem('userName') as string;
    this.createFormControls();
    this.createForm();
    this.getUsers();
  }

  createFormControls(){
    this.newMessage = new FormControl('', [Validators.required]);
  }

  createForm() {
    this.messageForm = new FormGroup({
      newMessage: this.newMessage
    });
  }

  submit() {
    if(this.messageForm.valid)
    {
      if(this.isEditMessage){
        const message: MESSAGE = this.messages.find(m => m.id === this.editMessageId) as MESSAGE;
        const messageToEdit: MESSAGE_TO_UPDATE =
        {
          id: message.id,
          text: this.newMessage.value
        } ;
        this.editMessage(messageToEdit);
        this.isEditMessage = false;
        return;
      }
      const messageToAdd: MESSAGE_TO_ADD = {
      text: this.newMessage.value,
      createdAt: new Date(),
      groupId: this.groupId,
      userEmail: localStorage.getItem('email') as string
      }
      if(this.isReplyMessage)
      {
        messageToAdd.parentMessageId = this.parentMessageId;
        this.replyMessage(messageToAdd);
        this.isReplyMessage = false;
        return;
      }
      this.addMessage(messageToAdd);
    }
  }

  addMessage(messageToAdd: MESSAGE_TO_ADD){
    this.socialMediaService.addMessage(messageToAdd).subscribe(
      () => { this.messageEvent.emit();
              this.newMessage.reset();
      },
      err => { alert(err.error)}
    )
  }

  deleteMessage(messageId: number){
    this.socialMediaService.deleteMessage(messageId).subscribe(
      () => this.messageEvent.emit()
    );
  }

  deleteForUserMessage(messageId: number){
    this.socialMediaService.deleteForUserMessage(messageId).subscribe(
      () => this.messageEvent.emit()
    );
  }

  replyMessageHandler(messageId: number){
    this.renderer.selectRootElement('#newMessage').focus();
    this.parentMessageId = messageId;
    this.isReplyMessage = true;
  }

  editMessageHandler(messageId: number){
    this.editMessageId = messageId;
    this.newMessage.setValue(this.messages.find(m => m.id === messageId)?.text);
    this.isEditMessage = true;
  }

  replyMessage(messageToAdd: MESSAGE_TO_ADD){
    this.socialMediaService.replyMessage(messageToAdd).subscribe(
      () => { this.messageEvent.emit();
              this.newMessage.reset();
      },
      err => { alert(err.error)}
    )
  }

  editMessage(message: MESSAGE_TO_UPDATE){
    this.socialMediaService.editMessage(message).subscribe(
      () => { this.messageEvent.emit();
              this.newMessage.reset();
      },
      err => { alert(err.error)}
    )
  }

  openDialog() {
    const dialogRef = this.dialog.open(AddFriendDialog);
  }

  getUsers()
  {
    this.socialMediaService.getGroupUsers(this.groupId).subscribe(
      users=>{
        this.users= users;
      })
  }

  openInfo(){
    this.dialog.open(InfoDialog,
      {data:{
        users: this.users
      }});
  }
}

@Component({
  selector: 'info-dialog',
  templateUrl: 'info-dialog.html',
  styles: ['.users { padding: 10px; }']
})
export class InfoDialog{

  constructor(@Inject(MAT_DIALOG_DATA) public data: any){}
}
