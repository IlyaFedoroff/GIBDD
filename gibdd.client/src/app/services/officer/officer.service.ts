import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Officer } from '../../models/Officer';

@Injectable({
  providedIn: 'root'
})
export class OfficerService {
  private apiUrl = 'https://localhost:7048/api/officers';

  constructor(private http: HttpClient) { }

  getOfficers(): Observable<Officer[]> {
    return this.http.get<Officer[]>(this.apiUrl);
  }

  addOfficer(officer: Officer): Observable<Officer> {
    return this.http.post<Officer>(this.apiUrl, officer, this.httpOptions);
  }

  updateOfficer(officer: Officer): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${officer.officerId}`, officer, this.httpOptions);
  }

  deleteOfficer(officerId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${officerId}`, this.httpOptions);
  }

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
}
