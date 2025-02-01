import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Importa CommonModule (ngFro, ngIf)
import { AuthService } from '../../auth/auth.service';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu'

import { RouterModule } from '@angular/router';



@Component({
  selector: 'app-menu',
  standalone: true, // Define how standalone component
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    RouterModule, // Import RouterModule to [routerLink] and routerLinkActive
  ],
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent {
  menuItems = [
    { label: 'User', route: '/user', icon: 'person', roles: ['ADMIN']  },
    //{ label: 'Role', route: '/role', icon: 'security', roles: ['ADMIN']  },
    { label: 'Patient', route: '/patient', icon: 'healing', roles: ['ADMIN','USER']  },
    { label: 'Logout', route: '/login', icon: 'close', roles: ['ADMIN','USER']  },
  ];

  constructor(private authService: AuthService) {}

  get userRole(): string | null {
    return this.authService.getUserRole();
  }

  get filteredMenuItems() {
    // Get option by role
    return this.menuItems.filter((item) => item.roles.includes(this.userRole!));
  }

  logout(): void {
    this.authService.logout();
  }

}
