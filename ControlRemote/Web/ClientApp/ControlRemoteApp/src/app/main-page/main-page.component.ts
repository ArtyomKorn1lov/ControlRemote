import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogRegComponent } from '../dialog-reg/dialog-reg.component';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  public openRegDialog(): void {
    const dialogRef = this.dialog.open(DialogRegComponent);
  }

  ngOnInit(): void {
  }

}
