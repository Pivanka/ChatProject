import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { NotfoundComponent } from './components/notfound/notfound.component';

import { SignInComponent, SignUpComponent} from './features/authorization/index';
import { CreateGroupComponent } from './features/profile/components/create-group/create-group.component';
import { ListFriendsComponent } from './features/profile/components/list-friends/list-friends.component';
import { ListGroupsComponent } from './features/profile/components/list-groups/list-groups.component';
import { ProfileComponent } from './features/profile/profile.component';
import { GroupChatComponent } from './features/social-media/components/group-chat/group-chat.component';
import { SocialMediaComponent } from './features/social-media/social-media/social-media.component';

const routes: Routes = [
  {path: '', component: HomePageComponent},
  {path: 'signin', component: SignInComponent},
  {path: 'signup', component: SignUpComponent},
  {path: 'social-media',
  component: SocialMediaComponent,
  children: [
    {
      path: 'chat/:id',
      component: GroupChatComponent
    }
  ]},
  {path: 'profile', component: ProfileComponent,
  children: [
    {
      path: 'groups',
      component: ListGroupsComponent
    },
    {
      path: 'friends',
      component: ListFriendsComponent
    }
  ]},
  { path: 'create-group', component: CreateGroupComponent},
  {path: '**', component: NotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
