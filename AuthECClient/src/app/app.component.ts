import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserComponent } from './user/user.component'; // ✅ ADD

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserComponent], // ✅ ADD
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'AuthECClient';
}