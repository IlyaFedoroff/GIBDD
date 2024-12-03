import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Owner } from '../../models/Owner';



@Injectable({
  providedIn: 'root'
})
export class OwnerService {

  private apiUrl = "https://localhost:7048/api/owners";

  constructor(private http: HttpClient) { }

  public getOwners(): Observable<Owner[]> {
    return this.http.get<Owner[]>(this.apiUrl);
  }

  getOwnerById(id: number): Observable<Owner> {
    return this.http.get<Owner>(`${this.apiUrl}/${id}`);
  }

  addOwner(owner: Owner): Observable<Owner> {
    return this.http.post<Owner>(this.apiUrl, owner);
  }

  updateOwner(owner: Owner): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${owner.ownerId}`, owner, this.httpOptions);
  }

  deleteOwner(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
}
