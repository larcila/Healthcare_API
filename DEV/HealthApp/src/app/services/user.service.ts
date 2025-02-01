import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { environment } from '../../environments/environment';
import { ConfigService } from './config.service';


@Injectable({
  providedIn: 'root',
})
export class UserService {
  //private apiUrl = 'http://localhost:29654/api/User'; // Endpoint de usuarios
  //private apiUrl = `${environment.apiBaseUrl}/User`;
  //private apiRoleByUserUrl = `${environment.apiBaseUrl}/Role_By_User`;
  private apiUrl = '';
  private apiRoleByUserUrl = '';

  constructor(private http: HttpClient,
    private configService: ConfigService
  ) {  
    this.apiUrl= `${this.configService.apiBaseUrl}/User`;
    this.apiRoleByUserUrl= `${this.configService.apiBaseUrl}/Role_By_User`;
  }

  getUsers(): Observable<any> {
    const token = localStorage.getItem('token'); // Get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any>(this.apiUrl, { headers });
  }

  getUserById(id: number): Observable<any>{
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`; // Construir la URL dinámica
    return this.http.get<any>(url, { headers });
  }

  createUser(user: any): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(this.apiUrl, user, { headers });
  }
  
  deleteUser(id: number): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`; // Construir la URL dinámica
    return this.http.delete(url, { headers });
  }

  updateUser(user: any): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put(this.apiUrl, user, { headers });
  }

  getRole_By_UserByIdUser(idUser: number): Observable<any>{
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiRoleByUserUrl}/user/${idUser}`; // Construir la URL dinámica
    return this.http.get<any>(url, { headers });
  }

}
