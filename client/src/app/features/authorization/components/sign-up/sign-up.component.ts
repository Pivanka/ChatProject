import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import {PASSWORD_VALIDATION_RULES, SIGN_UP_REQUEST_BODY } from '../../../../models/authorization.models';
import { passwordPattern, PasswordRulesStartValue, match } from 'src/app/app.config';
import { AuthorizationService } from '../../authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit{

  registrationForm!: FormGroup;

  isPasswordValid: PASSWORD_VALIDATION_RULES = PasswordRulesStartValue;

  email!: FormControl;
  name!: FormControl;
  password!: FormControl;
  confirmPassword!: FormControl;

  constructor(private as: AuthorizationService,
    private router: Router) {
  }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
  }

  createFormControls(){
    this.name = new FormControl('', [Validators.required]);
    this.email = new FormControl('', [Validators.email, Validators.required]);
    this.password = new FormControl('', [
        Validators.required,
        Validators.pattern(passwordPattern)
      ]);
    this.confirmPassword = new FormControl('', [Validators.required]);
  }

  createForm() {
    this.registrationForm = new FormGroup({
        name: this.name,
        email: this.email,
        password: this.password,
        confirmPassword: this.confirmPassword
    }, {validators: match(this.password, this.confirmPassword)});
  }

  getClass(control: FormControl) {
    if (control.touched && control.invalid)
    {
        this.setErrorFor(control);
        return "form-control error";
    } else if(control.valid && control.dirty)
    {
        this.formErrors = {
            nameErrors: '',
            emailErrors: '',
            passErrors: '',
            confirmPassErrors: ''
          };
        return "form-control success";
    } else{
        return "form-control";
    }
  }

  formErrors = {
    nameErrors: '',
    emailErrors: '',
    passErrors: '',
    confirmPassErrors: ''
  }

  setErrorFor(control: FormControl){
    if(control.errors)
    {
        if(control.errors['required'])
        {
           if(control == this.name)
           {
            this.formErrors.nameErrors = "This value is required. ";
           } else if(control == this.email)
           {
            this.formErrors.emailErrors = "This value is required. ";
           } else if(control == this.password)
           {
            this.formErrors.passErrors = "This value is required. ";
           } else if(control == this.confirmPassword)
           {
            this.formErrors.confirmPassErrors = "This value is required. ";
           }
        } else if(control.errors['email'])
        {
            this.formErrors.emailErrors = "Input correct input value. ";
        } else if (control.errors['pattern'])
        {
            this.formErrors.passErrors = "Password need one digit, one big letter, small letter and length more 4. "
        }
        if(control.errors['match'])
        {
            this.formErrors.confirmPassErrors = "Password doesn't match. ";
        }
    }
  }

  signup(){
    if(this.registrationForm.valid)
    {
      const body: SIGN_UP_REQUEST_BODY = {
        userName: this.name.value,
        email: this.email.value,
        password: this.password.value,
        confirmPassword: this.confirmPassword.value
      }
      this.as.signup(body)
        .subscribe(res => {
          if(res.isSuccess)
          {
            this.router.navigate(['/signin']);
          } else {
            alert(res.message);
          }
        }, err => { alert(err.error.message)})
    }
  }

}
