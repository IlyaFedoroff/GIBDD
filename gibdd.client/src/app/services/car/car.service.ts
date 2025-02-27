import { Injectable } from '@angular/core';
import { Car } from '../../models/Car';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InspectionHistory } from '../../models/InspectionHistory';
import {environment} from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = `${environment.apiUrl}/cars`;

  constructor(private http: HttpClient) { }

  getCars(): Observable<Car[]> {
    return this.http.get<Car[]>(this.apiUrl);
  }

  getCar(id: number): Observable<Car> {
    return this.http.get<Car>(this.apiUrl);
  }

  addCar(car: Car): Observable<Car> {
    return this.http.post<Car>(this.apiUrl, car, this.httpOptions());
  }

  updateCar(car: Car): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${car.carId}`, car, this.httpOptions());
  }

  deleteCar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getCarInspectionHistory(carId: number): Observable<InspectionHistory[]> {
    return this.http.get<InspectionHistory[]>(`${this.apiUrl}/${carId}/history`);
  }

  private httpOptions() {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application / json' }),
    };
  }

}
