import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-success-dialog',
  standalone: false,
  templateUrl: './success-dialog.component.html',
  styleUrls: ['./success-dialog.component.css']
})
export class SuccessDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
}
