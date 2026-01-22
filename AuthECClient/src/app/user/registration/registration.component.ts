import { NgIf, NgSwitch, NgSwitchCase } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { FirstKeyPipe } from '../../shared/pipes/first-key.pipe';
import { AuthService } from '../../shared/service/auth.service';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, NgSwitch, NgSwitchCase, FirstKeyPipe],
  templateUrl: './registration.component.html',
  styles: [``]
})
export class RegistrationComponent {
  form: FormGroup;
  formBuilder = inject(FormBuilder);
 private service = inject(AuthService);

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
        if (response.success) {
          this.form.reset();
          this.isSubmitted = false;
          console.log('User created successfully',response);
        }
      }, error: err =>console.log('error',err) });
      
    } else {
      console.log('Form is invalid');
    }
  }
  

  hasDisplayError(controlName: string, errorName: string): boolean {
    const control = this.form.get(controlName);
    return control ? control.touched && control.hasError(errorName) : false;
  }
  
}

