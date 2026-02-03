import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/service/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styles: ``,
})
export class DashboardComponent {
  constructor(
    private router: Router,
    private authService: AuthService,

  ) { }
  onLogout() {
    this.authService.deleteToken();
   // localStorage.removeItem('token');
    this.router.navigateByUrl('/signin');
  }
}
