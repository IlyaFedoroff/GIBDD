import { Component, OnInit } from '@angular/core';
import { InspectionService } from '../../services/inspection/inspection.service';
import { AddInspection } from '../../models/Inspection';
import { Car } from '../../models/Car';
import { Owner } from '../../models/Owner';
import { Officer } from '../../models/Officer';
import { OwnerService } from '../../services/owner/owner.service';
import { CarService } from '../../services/car/car.service';
import { OfficerService } from '../../services/officer/officer.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-inspection',
  templateUrl: './add-inspection.component.html',
  styleUrl: './add-inspection.component.css'
})
export class AddInspectionComponent implements OnInit {
  inspection: AddInspection = {
    carId: 1,
    ownerId: 1,
    officerId: 1,
    inspectionDate: '2077-07-07',
    result: 'Passed'
  };

  cars: Car[] = [];
  owners: Owner[] = [];
  officers: Officer[] = [];

  constructor(
    public dialogRef: MatDialogRef<AddInspectionComponent>,
    private inspectionService: InspectionService,
    private carService: CarService,
    private ownerService: OwnerService,
    private officerService: OfficerService
  ) { }

  ngOnInit(): void {
    this.loadCars();
    this.loadOwners();
    this.loadOfficers();
  }

  loadCars(): void {
    this.carService.getCars().subscribe((data) => (this.cars = data));
  }

  loadOwners(): void {
    this.ownerService.getOwners().subscribe((data) => (this.owners = data));
  }

  loadOfficers(): void {
    this.officerService.getOfficers().subscribe((data) => (this.officers = data));
  }


  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    this.inspectionService.addInspection(this.inspection).subscribe({
      next: () => {
        alert('Осмотр успешно добавлен!');
        this.dialogRef.close(true);
      },
      error: (err) => {
        console.error(err);
        alert('Ошибка при добавлении осмотра.');
      }
    });
  }



















  submitInspection(): void {
    console.log('Отправка осмотра:', this.inspection);
    this.inspectionService.addInspection(this.inspection).subscribe({
      next: () => {
        alert('Осмотр успешно добавлен!');
        this.resetForm();
      },
      error: (err) => {
        console.error(err);
        alert('Ошибка при добавлении осмотра.');
      }
    });
  }

  resetForm(): void {
    this.inspection = {
      carId: 1,
      ownerId: 1,
      officerId: 1,
      inspectionDate: '2077-07-07',
      result: 'Passed'
    };
  }
}
