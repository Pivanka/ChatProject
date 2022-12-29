import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { environment } from 'src/environments/environments';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AuthorizationModule } from './features/authorization/authorization.module';
import { API_URL } from './app-injection-tokens';
import { JwtModule } from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from '../environments/environments';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavComponent } from './components/nav/nav.component';
import { SocialMediaModule } from './features/social-media/social.media.module';
import { ProfileModule } from './features/profile/profile.module';
import { MatDialogModule } from '@angular/material/dialog';
import { HomePageComponent } from './components/home-page/home-page.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    NotfoundComponent,
    FooterComponent,
    NavComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthorizationModule,
    HttpClientModule,
    SocialMediaModule,
    ProfileModule,
    ReactiveFormsModule,
    MatDialogModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhiteListedDomains
      }
    }),

  ],
  providers: [{
    provide: API_URL,
    useValue: environment.api
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
