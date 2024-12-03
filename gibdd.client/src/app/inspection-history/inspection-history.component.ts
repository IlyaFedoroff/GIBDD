import { Component, OnInit, Input } from '@angular/core';
import { InspectionHistory } from '../models/InspectionHistory';
import { CarService } from '../services/car/car.service';
import { ActivatedRoute } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-inspection-history',
  templateUrl: './inspection-history.component.html',
  styleUrl: './inspection-history.component.css'
})
export class InspectionHistoryComponent {
  inspections: InspectionHistory[] = [];
  carId: number | null = null;;
  errorMessage: string | null = null;

  constructor(private carService: CarService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.carId = +params.get('id')!;
      if (this.carId) {
        this.loadInspectionHistory(this.carId);
      }
    });
  }

  loadInspectionHistory(carId: number): void {
    this.carService.getCarInspectionHistory(carId).pipe(
      catchError((error) => {
        if (error.status === 404) {
          this.errorMessage = 'Осмотры для данного автомобиля не найдены.';
        } else {
          this.errorMessage = 'Ошибка загрузки данных.';
        }
        return of([]); // Возвращаем пустой массив, чтобы избежать дальнейших ошибок
      })
    ).subscribe({
      next: (inspections) => {
        this.inspections = inspections;
      },
    });
  }
}
