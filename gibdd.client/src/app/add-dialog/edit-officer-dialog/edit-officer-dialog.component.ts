import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Officer } from '../../models/Officer';

@Component({
  selector: 'app-edit-officer-dialog',
  templateUrl: './edit-officer-dialog.component.html',
  styleUrls: ['./edit-officer-dialog.component.css']
})
export class EditOfficerDialogComponent {
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
