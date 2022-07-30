import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  public CreateFile(uploadFile: FormData): Observable<string> {
    return this.http.post(`api/file/create`, uploadFile, { responseType: 'text' });
  }
}
