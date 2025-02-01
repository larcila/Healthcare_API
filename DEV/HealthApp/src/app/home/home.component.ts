import { Component } from '@angular/core';
import { AuthService } from '../auth/auth.service';

import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MenuComponent } from '../shared/menu/menu.component';

@Component({
  selector: 'app-home',
  standalone: true, // Define standalone componente
  imports: [
    MatCardModule,
    MatButtonModule,
    MenuComponent, 
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private authService: AuthService) {}

  logout() {
    this.authService.logout();
  }
}
