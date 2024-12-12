import { Component, OnInit } from '@angular/core';
import { CarService } from '../services/car/car.service';
import { Car } from '../models/Car';
import { MatDialog } from '@angular/material/dialog';
import { CarDialogComponent } from '../add-dialog/car-dialog/car-dialog.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrl: './cars.component.css'
})
export class CarsComponent implements OnInit {
  cars: Car[] = [];
  errorMessage: string | null = null;

  


  constructor(private carService: CarService, private dialog: MatDialog, private router: Router) { }

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars(): void {
    this.carService.getCars().subscribe({
      next: (data) => {
        this.cars = data;
      },
      error: (err) => {
        this.errorMessage = "Ошибка при загрузке автомобилей";
        console.error(err);
      }
    });
  }

  addCar(): void {
    const dialogRef = this.dialog.open(CarDialogComponent, {
      width: '400px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: Car | undefined) => {
      if (result) {
        this.carService.addCar(result).subscribe(() => {
          this.loadCars();
        });
      }
    });
  }

  editCar(car: Car): void {
    const dialogRef = this.dialog.open(CarDialogComponent, {
      width: '400px',
      data: { ...car }
    });

    dialogRef.afterClosed().subscribe((result: Car | undefined) => {
      if (result) {
        this.carService.updateCar(result).subscribe(() => {
          this.loadCars();
        });
      }
    });
  }



  

  deleteCar(carId: number): void {
    console.log("Удаляется автомобиль с carId:"), carId
    if (confirm('Вы уверены, что хотите удалить этот автомобиль?')) {
      this.carService.deleteCar(carId).subscribe(() => {
        this.loadCars();
      });
    }
  }

  viewHistory(carId: number): void {
    this.router.navigate([`/cars/${carId}/history`]);
  }

}
