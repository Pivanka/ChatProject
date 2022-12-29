import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../authorization.service';
import { SIGN_IN_REQUEST_BODY } from 'src/app/models/authorization.models';
import { Router } from '@angular/router';

import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit{

  public get isLoggedIn(): boolean{
    return this.as.isAuthenticated();
  }

  constructor(private as: AuthorizationService,
              private router: Router){}

  loginForm!: FormGroup;

  email!: FormControl;
  password!: FormControl;

  ngOnInit() {
    this.createFormControls();
    this.createForm();
  }

  createFormControls(){
    this.email = new FormControl('', [Validators.email, Validators.required]);
    this.password = new FormControl('', [Validators.required]);
  }

  createForm() {
    this.loginForm = new FormGroup({
        email: this.email,
        password: this.password,
    });
  }

  login()
  {
    if(this.loginForm.valid)
    {
      const body: SIGN_IN_REQUEST_BODY = {
        email: this.email.value,
        password: this.password.value
      }
      this.as.login(body)
        .subscribe(
          res => {
          if(res.isSuccess)
          {
            this.router.navigate(['/social-media']);
          } else {
            alert(res.message);
          }
          },
          err => { alert(err.error.message)}
          )
    }
  }

  logout() {
    this.as.logout();
  }
}
