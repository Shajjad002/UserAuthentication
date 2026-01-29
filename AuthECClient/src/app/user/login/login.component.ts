import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../shared/service/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterLink],
  templateUrl: './login.component.html',
  styles: ``
})
export class LoginComponent {
  form: FormGroup;  // Declare the form property
  isSubmitted:boolean = false;
  

  constructor(public formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {
    // Initialize the form property
    this.form = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  };


   hasDisplayError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return control ? this.isSubmitted || (control.touched || control.hasError(errorName)) : false;
  };

  onSubmit() {
    this.isSubmitted = true;
    if(this.form.valid){
      
      this.authService.signin(this.form.value).subscribe({ next: (response:any) => {
          localStorage.setItem('token',response.token);
          this.router.navigateByUrl('/dashboard');
      }, error: err => {
          // Handle login error, e.g., show error message
          if (err.status === 400) {
            this.toastr.error(err.error.message, 'Login failed');
          }
          else if (err.error && err.error.message) {
            this.toastr.error('Incorrect email or password', 'Login failed');
          }
      } });
     
    }
  }


}

