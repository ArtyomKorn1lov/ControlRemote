import { Component, OnInit } from '@angular/core';
import { EmployerModel } from '../Dto/EmployerModel';
import { EmployerService } from '../Services/employer.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employer-info',
  templateUrl: './employer-info.component.html',
  styleUrls: ['./employer-info.component.css']
})
export class EmployerInfoComponent implements OnInit {

  private employerListRoute: string = "/employer-list";
  public employer: EmployerModel = new EmployerModel(0, 0, "", "");

  constructor(private employerService: EmployerService, private router: Router) { }

  public RemoveEmployer(): void {
    this.employerService.RemoveEmployer(this.employer.id).subscribe(data => {
      if(data == "error") {
        alert("Ошибка удаления сотрудника");
        console.log(data);
        return;
      }
      alert(data);
      console.log(data);
      this.router.navigateByUrl(this.employerListRoute);
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.employerService.GetEmployerById(this.employerService.GetEmployerId()).subscribe(data => {
      this.employer = data;
    });
  }

}
