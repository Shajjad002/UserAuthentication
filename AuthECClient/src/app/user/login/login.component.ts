import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';

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

  constructor(public formBuilder: FormBuilder) {
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
    console.log(this.form.value);
  }


}

