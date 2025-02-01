import { Component } from '@angular/core';

import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [
    MatIconModule,
    RouterModule, //Is necesary to [routerLink]
    RouterOutlet,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: []
})
export class AppComponent {
  title = 'HealthApp';
}
