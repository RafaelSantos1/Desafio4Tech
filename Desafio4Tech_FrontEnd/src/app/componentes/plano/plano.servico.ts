import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Plano } from './plano.model';

@Injectable({
  providedIn: 'root'
})
export class PlanoServico {
  private apiUrl = 'https://localhost:44307/api/plano';

  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  getById(id: number): Observable<any> {
    return this.http.get<Plano>(`${this.apiUrl}/${id}`);
  }

  create(plano: Plano): Observable<any> {
    return this.http.post<Plano>(this.apiUrl, plano);
  }

  update(id: number, plano: Plano): Observable<any> {
    return this.http.put<Plano>(`${this.apiUrl}/${id}`, plano);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
