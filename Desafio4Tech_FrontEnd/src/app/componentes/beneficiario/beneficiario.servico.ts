import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Beneficiario } from './beneficiario.model';

@Injectable({
  providedIn: 'root'
})
export class BeneficiarioServico {
  private apiUrl = 'https://localhost:44307/api/beneficiario';

  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  create(Beneficiario: Beneficiario): Observable<any> {
    return this.http.post<Beneficiario>(this.apiUrl, Beneficiario);
  }

  update(id: number, Beneficiario: Beneficiario): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, Beneficiario);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
