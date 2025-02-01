import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';

//import { environment } from '../../environments/environment';
import { ConfigService } from '../services/config.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAuthenticated = false;
  private userRole: string | null = null; //user rol
  private idUser: string| null = null; //
  //private apiUrl = 'http://localhost:29654/api/Auth/Login';  // URL of API
  //private apiUrl = `${environment.apiBaseUrl}/Auth/Login`;
  private apiUrl = '';

  constructor(private router: Router,  
    private http: HttpClient,
    private configService: ConfigService
  ) { 
    this.apiUrl= `${this.configService.apiBaseUrl}/Auth/Login`;
  }
  
  getData() {
    const url = `${this.configService.apiBaseUrl}/Auth/Login`;
    return this.http.get(url);
  }

  login(username: string, password: string): boolean {
    const loginData = {
      user_Name: username,
      password: password
    };

    this.http.post<any>(this.apiUrl, loginData).subscribe({
      next: (response) => {
        // If the response is successful, save the token and redirect
        
        const token = response.token;
        const idUser = response.id;
        this.userRole = response.rol;
        this.saveToken(token);
        this.saveCurrentUser(idUser);


        this.isAuthenticated = true;
        this.router.navigate(['/home']); 
        return this.isAuthenticated;
      },
      error: (response) => {
        return this.isAuthenticated;
      },
      //complete: () => {}
  });
    
  return this.isAuthenticated;

  }

  logout(): void {
    this.isAuthenticated = false;
    this.userRole = null;
    localStorage.removeItem('token');  // Eliminar el token del almacenamiento local
    this.router.navigate(['/login']);
  }


  isLoggedIn(): boolean {
    return this.isAuthenticated;
  }

  getUserRole(): string | null {
    return this.userRole;
  }

  saveToken(token: string): void {
    localStorage.setItem('token', token);  // Guardar el token en el almacenamiento local
  }
  saveCurrentUser(idUser: string): void {
    localStorage.setItem('User_Id', idUser);  // Guardar el token en el almacenamiento local
  }

}
