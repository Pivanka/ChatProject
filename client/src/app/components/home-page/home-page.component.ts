import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizationService } from 'src/app/features/authorization/authorization.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {

  constructor(private as: AuthorizationService,
    private router: Router){}

  ngOnInit(): void {
  }

  public get isLoggedIn(): boolean{
    return this.as.isAuthenticated();
  }

  goToChat(){
    this.router.navigate(['/social-media']);
  }

  goToLogin(){
    this.router.navigate(['/signin']);
  }

  goToRegister(){
    this.router.navigate(['/signup']);
  }
}
