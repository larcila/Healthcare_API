import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthService } from '../auth/auth.service';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';


@Component({
  selector: 'app-login',
  imports: [CommonModule,
    MatCardModule, //<mat-card>
    MatFormFieldModule, //<mat-form-field
    FormsModule, //[(ngModel)]="password"
    MatInputModule,
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';


  constructor(private authService: AuthService) {}

  onSubmit() {
    // this.authService.login(this.username, this.password).subscribe(
    //   (response) => {
    //     // Si la respuesta es exitosa, guardar el token y redirigir
    //     const token = response.token;
    //     this.authService.saveToken(token);
    //     this.authService.isAuthenticated = true;
    //     this.router.navigate(['/home']); // Redirigir a la página principal
    //   },
    //   (error) => {
    //     // Si hay un error, mostrar un mensaje
    //     this.errorMessage = 'Incorrect login or password.';
    //   }
    // );

    const success = this.authService.login(this.username, this.password);
    if (!success) {
      //alert('Invalid credentials. Please try again.');
      this.errorMessage = 'Incorrect login or password.';
    }
  }
}
