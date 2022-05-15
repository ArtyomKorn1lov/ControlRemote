import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogRegComponent } from '../dialog-reg/dialog-reg.component';
import { EmployerServiceService } from '../services/employer-service.service';
import { AuthServiceService } from '../services/auth-service.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.css']
})
export class MainPageComponent implements OnInit {

  public id: number | undefined;
  public type: string | undefined;

  constructor(public dialog: MatDialog, private employerService: EmployerServiceService, private authService: AuthServiceService) { }

  public openRegDialog(): void {
    const dialogRef = this.dialog.open(DialogRegComponent);
  }

  public clickId(): void {
    this.employerService.GetEmployer().subscribe(data => this.id = data.id);
  }

  ngOnInit(): void {
    this.authService.IsUserAuthorized().subscribe(data => {
      this.type = data;
    });
  }

}
