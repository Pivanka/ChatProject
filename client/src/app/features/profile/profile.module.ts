import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SearchComponent } from 'src/app/components/search/search.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';

import { ProfileComponent, UserItemComponent, GroupItemComponent, ListGroupsComponent, ListFriendsComponent, CreateGroupComponent} from './index';

@NgModule({
    declarations: [
        ProfileComponent,
        ListGroupsComponent,
        ListFriendsComponent,
        SearchComponent,
        UserItemComponent,
        GroupItemComponent,
        CreateGroupComponent
    ],
    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        ReactiveFormsModule,
        MatListModule
    ]
})
export class ProfileModule { }
