import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { environment } from '../../environments/environment';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root',
})
export class MedicalFollowUpService {
  //private apiUrl = 'http://localhost:29654/api/Follow_Up/patient'; // Endpoint de usuarios
  //private apiUrl = `${environment.apiBaseUrl}/Follow_Up`;
  private apiUrl = '';

  constructor(private http: HttpClient,
    private configService: ConfigService
  ) {   
    this.apiUrl= `${this.configService.apiBaseUrl}/Follow_Up`;
   }

  getAllFollowUp(patientId: number): Observable<any> {
    //console.error('Medical-follow-up.service.ts, patient_ID:', patientId);
    const token = localStorage.getItem('token'); // Obtener el token del localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/patient/${patientId}`; // Construir la URL dinámica

    //console.error('Medical-follow-up.service.ts, url:', url);
    return this.http.get<any>(url, { headers });
  }

  getFollowUpById(id: number): Observable<any>{
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`; // Construir la URL dinámica
    return this.http.get<any>(url, { headers });
  }

  createFollowUp(followUp: any): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post(this.apiUrl, followUp, { headers });
  }

  updateFollowUp(followUp: any): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.put(this.apiUrl, followUp, { headers });
  }

  deleteFollowUp(id: number): Observable<any> {
    const token = localStorage.getItem('token'); // get token from localStorage
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`; // Construir la URL dinámica
    return this.http.delete(url, { headers });
  }
}
