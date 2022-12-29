import { NgModule,  } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatListModule } from '@angular/material/list';
import { FormsModule } from '@angular/forms';

import { GroupComponent, MessageComponent, GroupChatComponent, InfoDialog, SocialMediaComponent, AddFriendDialog } from './index';

@NgModule({
    declarations: [
        GroupComponent,
        MessageComponent,
        GroupChatComponent,
        SocialMediaComponent,
        AddFriendDialog,
        InfoDialog
    ],
    imports: [
        CommonModule,
        RouterModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatListModule,
        ReactiveFormsModule,
        FormsModule,
    ]
})
export class SocialMediaModule { }
