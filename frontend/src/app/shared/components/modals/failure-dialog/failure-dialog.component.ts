import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-failure-dialog',
  standalone: false,
  templateUrl: './failure-dialog.component.html',
  styleUrls: ['./failure-dialog.component.css']
})
export class FailureDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {}
}
