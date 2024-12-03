import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Owner } from '../../models/Owner';


@Component({
  selector: 'app-owner-dialog',
  templateUrl: './owner-dialog.component.html',
  styleUrl: './owner-dialog.component.css'
})
export class OwnerDialogComponent {
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
