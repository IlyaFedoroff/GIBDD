import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OfficerService } from '../services/officer/officer.service';
import { Officer } from '../models/Officer';
import { EditOfficerDialogComponent } from '../add-dialog/edit-officer-dialog/edit-officer-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-officer',
  templateUrl: './officer.component.html',
  styleUrls: ['./officer.component.css']
})
export class OfficerComponent implements OnInit {
  officers: Officer[] = [];
  errorMessage: string | null = null;
  displayedColumns: string[] = ['lastName', 'firstName', 'middleName', 'position', 'actions'];

  constructor(private officerService: OfficerService, private dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.loadOfficers();
  }

  loadOfficers(): void {
    this.officerService.getOfficers().subscribe((data: Officer[]) => {
      this.officers = data;
    });
  }

  addOfficer(): void {
    const dialogRef = this.dialog.open(EditOfficerDialogComponent, {
      width: '400px',
      data: {}
    });

    dialogRef.afterClosed().subscribe((result: Officer | undefined) => {
      if (result) {
        this.officerService.addOfficer(result).subscribe(() => {
          this.loadOfficers();
        });
      }
    });
  }

  editOfficer(officer: Officer): void {
    const dialogRef = this.dialog.open(EditOfficerDialogComponent, {
      width: '400px',
      data: { ...officer }
    });

    dialogRef.afterClosed().subscribe((result: Officer | undefined) => {
      if (result) {
        this.officerService.updateOfficer(result).subscribe(() => {
          this.loadOfficers();
          this.snackBar.open('Успешно', 'Закрыть', { duration: 2000 });
        });
      }
    });
  }

  //deleteOfficer(officerId: number): void {
  //  if (confirm('Вы уверены, что хотите удалить этого сотрудника?')) {
  //    this.officerService.deleteOfficer(officerId).subscribe(() => {
  //      this.loadOfficers();
  //    });
  //  }
  //}

  // Метод для удаления осмотра
  deleteOfficer(officerId: number): void {
    if (confirm('Вы уверены, что хотите удалить этого сотрудника?')) {
      this.officerService.deleteOfficer(officerId).subscribe({
        next: () => {
          this.snackBar.open('Сотрудник успешно удален', 'Закрыть', { duration: 2000 });
          this.loadOfficers(); // Обновляем список осмотров после удаления
        },
        error: (err) => {
          console.error(err);
          this.snackBar.open('Ошибка при удалении сотрудника', 'Закрыть', { duration: 2000 });
        }
      });
    }
  }
}
