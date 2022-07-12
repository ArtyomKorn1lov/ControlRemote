import { Component, OnInit } from '@angular/core';
import { ActionSortByUserLoginModel } from '../Dto/ActionSortByUserLoginModel';
import { ReportService } from '../Services/report.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent implements OnInit {

  public start: Date | undefined;
  public final: Date | undefined;
  public actions: ActionSortByUserLoginModel[] = [];

  constructor(private reportService: ReportService) { }

  public CompleteRequest(): void {
    if(this.start !=  undefined && this.final != undefined) {
      this.reportService.GetAllForTime(this.start, this.final).subscribe(data => {
        this.actions = data;
      });
    }
  }

  ngOnInit(): void {
  }

}
