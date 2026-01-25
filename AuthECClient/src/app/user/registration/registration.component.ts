import { NgIf, NgSwitch, NgSwitchCase } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { FirstKeyPipe } from '../../shared/pipes/first-key.pipe';
import { AuthService } from '../../shared/service/auth.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, NgSwitch, NgSwitchCase, FirstKeyPipe,RouterLink],
  templateUrl: './registration.component.html',
  styles: [``]
})
export class RegistrationComponent {
  form: FormGroup;
  formBuilder = inject(FormBuilder);
  private service = inject(AuthService);
  private toastr = inject(ToastrService);


  isSubmitted:boolean = false;

  passwordMatchValidator:ValidatorFn = (control: AbstractControl) => {
    const password =control.get('password')?.value;
    const confirmPassword = control.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { mismatch: true };
  }

  constructor(){
    this.form = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [ Validators.required,
            Validators.minLength(6),
            Validators.pattern(/(?=.*[!@#$%^&*])/)]],
      confirmPassword: ['']
    },{ validators: this.passwordMatchValidator});
  }


  onSubmit() {
    if (this.form.valid) {
      this.isSubmitted = true;
      //console.log('Form Submitted', this.form.value);
      this.service.createUser(this.form.value).subscribe({ next: (response:any) => {
        if (response.succeeded) {
          this.form.reset();
          this.isSubmitted = false;
          this.toastr.success('New user created','Registration successful!');
        } 
      }, error: err => {
          if (err.error && err.error.errors) {
            err.error.errors.forEach((x: any) => {
                //console.log('error', error);
                switch (x.code) {
                  case 'DuplicateUserName':
                    // this.toastr.error('Password must be at least 6 characters long','Registration failed');
                    break;
                  case 'DuplicateEmail':
                    this.toastr.error('Email is already taken','Registration failed');
                    break;
                  default:
                    this.toastr.error(x.description,'Registration failed');
                    console.log('error',x);
                    break;
                }
              });
          }else{
                console.log('error',err);
            // this.toastr.error('An unexpected error occurred','Registration failed');
          }
        }
      });
      
    } else {
      console.log('Form is invalid');
    }
  }
  

  hasDisplayError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return control ? this.isSubmitted || (control.touched || control.hasError(errorName)) : false;
  }
  
}

