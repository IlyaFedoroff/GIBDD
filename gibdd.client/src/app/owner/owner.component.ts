import { Component } from '@angular/core';
import { Owner } from '../models/Owner';
import { OwnerService } from '../services/owner/owner.service';
import { MatDialog } from '@angular/material/dialog';
import { OwnerDialogComponent } from '../add-dialog/owner-dialog/owner-dialog.component';

@Component({
  selector: 'app-owner',
  templateUrl: './owner.component.html',
  styleUrl: './owner.component.css'
})
export class OwnerComponent {
  owners: Owner[] = [];

  constructor(private ownerService: OwnerService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadOwners();
  }

  loadOwners(): void {
    this.ownerService.getOwners().subscribe(data => {
      this.owners = data;
    })
  }

  addOwner(): void {
    const dialogRef = this.dialog.open(OwnerDialogComponent, {
      width: '400px',
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.ownerService.addOwner(result).subscribe(() => this.loadOwners());
      }
    });
  }

  editOwner(owner: Owner): void {
    const dialogRef = this.dialog.open(OwnerDialogComponent, {
      width: '400px',
      data: { ...owner }
    });

    dialogRef.afterClosed().subscribe((result: Owner | undefined) => {
      if (result) {
        this.ownerService.updateOwner(result).subscribe(() => {
          this.loadOwners();
        });
      }
    });
  }

  deleteOwner(ownerId: number): void {
    if (confirm('Вы уверены, что хотите удалить этого владельца?')) {
      this.ownerService.deleteOwner(ownerId).subscribe(() => {
        this.loadOwners();
      });
    }
  }


}

