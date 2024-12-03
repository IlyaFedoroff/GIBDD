import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './main/app.component';
import { OwnerComponent } from './owner/owner.component';
import { OwnerDialogComponent } from './add-dialog/owner-dialog/owner-dialog.component';

import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';

import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { CarsComponent } from './cars/cars.component';
import { CarDialogComponent } from './add-dialog/car-dialog/car-dialog.component';
import { OfficerComponent } from './officer/officer.component';
import { EditOfficerDialogComponent } from './add-dialog/edit-officer-dialog/edit-officer-dialog.component';
import { InspectionsComponent } from './inspections/inspections.component';
import { AddInspectionComponent } from './add-dialog/add-inspection/add-inspection.component';
import { InspectionHistoryComponent } from './inspection-history/inspection-history.component';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [
    AppComponent,
    OwnerComponent,
    OwnerDialogComponent,
    CarsComponent,
    CarDialogComponent,
    OfficerComponent,
    EditOfficerDialogComponent,
    InspectionsComponent,
    AddInspectionComponent,
    InspectionHistoryComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    MatTableModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    FormsModule,
    NoopAnimationsModule,
    CommonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatToolbarModule,
    MatTooltipModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
