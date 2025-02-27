import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Inspection, AddInspection } from '../../models/Inspection';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InspectionService {
  private apiUrl = `${environment.apiUrl}/inspections`;

  constructor(private http: HttpClient) { }


  getInspections(): Observable<Inspection[]> {
    return this.http.get<Inspection[]>(this.apiUrl);
  }

  addInspection(inspection: AddInspection): Observable<void> {
    return this.http.post<void>(this.apiUrl, inspection, this.httpOptions);
  }


  deleteInspection(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }







  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
}
