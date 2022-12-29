import { Component, OnChanges, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/features/authorization/authorization.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  userName!: string;
  localStorage = localStorage;

  constructor(private as: AuthorizationService){}

  ngOnInit(): void {
    this.userName = localStorage.getItem('userName') as string;
  }

  public get isLoggedIn(): boolean{
    return this.as.isAuthenticated();
  }

  public get getName(): string{
    return localStorage.getItem('userName') as string;
  }

  logout() {
    this.as.logout().subscribe(
      () => alert('You sign out successful')
    );
    this.userName = '';
  }
}
