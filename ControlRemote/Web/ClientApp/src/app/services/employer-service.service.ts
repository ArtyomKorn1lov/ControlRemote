import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployerDto } from '../Dto/EmployerDto';

@Injectable({
  providedIn: 'root'
})
export class EmployerServiceService {

  constructor(private http: HttpClient) { }

  public GetEmployer(): Observable<EmployerDto> {
    return this.http.get<EmployerDto>(`api/employer/id`);
  }
}
