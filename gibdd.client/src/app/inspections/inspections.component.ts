import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Inspection, AddInspection } from '../models/Inspection';
import { InspectionService } from '../services/inspection/inspection.service';
import { AddInspectionComponent } from '../add-dialog/add-inspection/add-inspection.component';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-inspections',
  templateUrl: './inspections.component.html',
  styleUrls: ['./inspections.component.css'],
})
export class InspectionsComponent implements OnInit {
  inspections: Inspection[] = [];
  displayedColumns: string[] = ['officerName', 'ownerFullName', 'carBrand', 'licensePlate', 'inspectionDate', 'result', 'actions']; // Колонки таблицы




  constructor(
    private inspectionService: InspectionService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  openAddInspection(): void {
    const dialogRef = this.dialog.open(AddInspectionComponent, {
      width: '600px'
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadInspections();
      }
    });
  }


  ngOnInit(): void {
    this.loadInspections(); // Загрузка данных при инициализации компонента
  }

  loadInspections(): void {
    this.inspectionService.getInspections().subscribe({
      next: (data) => (this.inspections = data),
      error: (err) => console.error('Ошибка при загрузке осмотров:', err)
    });
  }



  // Метод для удаления осмотра
  deleteInspection(id: number): void {
    if (confirm('Вы уверены, что хотите удалить этот осмотр?')) {
      this.inspectionService.deleteInspection(id).subscribe({
        next: () => {
          this.snackBar.open('Осмотр успешно удален', 'Закрыть', { duration: 2000 });
          this.loadInspections(); // Обновляем список осмотров после удаления
        },
        error: (err) => {
          console.error(err);
          this.snackBar.open('Ошибка при удалении осмотра', 'Закрыть', { duration: 2000 });
        }
      });
    }
  }

}
