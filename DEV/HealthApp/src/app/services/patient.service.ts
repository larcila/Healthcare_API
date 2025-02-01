import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { environment } from '../../environments/environment';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  //private apiUrl = 'http://localhost:29654/api/Patient'; // Endpoint de usuarios
  //private apiUrl = `${environment.apiBaseUrl}/Patient`;
  private apiUrl = '';
  
  constructor(private http: HttpClient, 
    private configService: ConfigService
  ) {
    this.apiUrl= `${this.configService.apiBaseUrl}/Patient`;
  }

  getPatients(): Observable<any> {
    const token = localStorage.getItem('token'); // Obtener el token del localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<any>(this.apiUrl, { headers });
  }

  getPatientById(id: number): Observable<any>{
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`; // Construir la URL dinámica
    return this.http.get<any>(url, { headers });
  }

  createPatient(patient: any): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(this.apiUrl, patient, { headers });
  }

  updatePatient(patient: any): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put(this.apiUrl, patient, { headers });
  }


  deletePatient(id: number): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`; // Construir la URL dinámica
    return this.http.delete(url, { headers });
  }
}
