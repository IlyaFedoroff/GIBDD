import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Car } from '../../models/Car';

@Component({
  selector: 'app-car-dialog',
  templateUrl: './car-dialog.component.html',
  styleUrl: './car-dialog.component.css'
})
export class CarDialogComponent {
  availableBrands: string[] = [
    'АвтоВАЗ',
    'ГАЗ',
    'УАЗ',
    'КамАЗ',
    'Лада',
    'Волга',
    'ЗАЗ',
    'Чери',
    'Москвич',
    'ИжАвто',
    'Toyota',
    'BMW',
    'Mercedes-Benz',
    'Audi',
    'Volkswagen',
    'Honda',
    'Ford',
    'Nissan',
    'Hyundai',
  ];
  availableColors: string[] = [
    'Красный',
    'Синий',
    'Зеленый',
    'Черный',
    'Белый',
    'Серый',
    'Желтый',
    'Оранжевый'
  ];


  constructor(
    public dialogRef: MatDialogRef<CarDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Car
  ) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    this.dialogRef.close(this.data);
  }
}
