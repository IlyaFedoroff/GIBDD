import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Owner } from '../../models/Owner';


@Component({
  selector: 'app-owner-dialog',
  templateUrl: './owner-dialog.component.html',
  styleUrl: './owner-dialog.component.css'
})
export class OwnerDialogComponent {
  availableAddresses: string[] = [
    'ул. Советская, 1',
    'ул. Чкалова, 5',
    'ул. Пролетарская, 14',
    'ул. Мира, 23',
    'ул. Гагарина, 7',
    'ул. Терешковой, 50',
    'ул. Набережная, 12',
    'ул. Туркестанская, 8',
    'ул. 8 Марта, 34',
    'ул. Пушкина, 25',
    'ул. Орджоникидзе, 16',
    'ул. Лесозащитная, 44',
    'ул. Степная, 19',
    'ул. Речная, 2',
    'ул. Деповская, 29',
    'ул. Салмышская, 15',
    'ул. Донецкая, 31',
    'ул. Дзержинского, 6',
    'ул. Культурная, 10',
    'ул. Конституции, 22',
  ];





  constructor(
    public dialogRef: MatDialogRef<OwnerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Owner
  ) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    this.dialogRef.close(this.data);
  }
}
