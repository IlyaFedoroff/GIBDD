import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOfficerDialogComponent } from './edit-officer-dialog.component';

describe('EditOfficerDialogComponent', () => {
  let component: EditOfficerDialogComponent;
  let fixture: ComponentFixture<EditOfficerDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditOfficerDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditOfficerDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
