import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Officer } from '../../models/Officer';

@Component({
  selector: 'app-edit-officer-dialog',
  templateUrl: './edit-officer-dialog.component.html',
  styleUrls: ['./edit-officer-dialog.component.css']
})
export class EditOfficerDialogComponent {
  availablePositions: string[] = [
    'Инспектор ДПС',
    'Старший инспектор',
    'Инженер по безопасности дорожного движения',
    'Начальник отдела ГИБДД',
    'Заместитель начальника ГИБДД',
    'Инспектор административной практики',
    'Инспектор технического надзора',
    'Специалист по регистрации транспортных средств'
  ];
  constructor(
    public dialogRef: MatDialogRef<EditOfficerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Officer
  ) { }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    this.dialogRef.close(this.data);
  }
}
