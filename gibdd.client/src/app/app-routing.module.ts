import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerComponent } from './owner/owner.component';
import { CarsComponent } from './cars/cars.component';
import { OfficerComponent } from './officer/officer.component';
import { InspectionsComponent } from './inspections/inspections.component';
import { InspectionHistoryComponent } from './inspection-history/inspection-history.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  //{ path: '', redirectTo: '/home', pathMatch: 'full' },
  //{ path: '**', redirectTo: '/home'},
  { path: 'owners', component: OwnerComponent },
  { path: 'cars', component: CarsComponent },
  { path: 'officers', component: OfficerComponent },
  { path: 'inspections', component: InspectionsComponent },
  { path: 'cars/:id/history', component: InspectionHistoryComponent },
  { path: 'home', component: HomeComponent },
  {path: '', component: HomeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
