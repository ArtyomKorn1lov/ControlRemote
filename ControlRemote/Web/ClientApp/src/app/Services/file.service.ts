import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileInfoModel } from '../Dto/FileInfoModel';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) { }

  public CreateFile(uploadFile: FormData): Observable<string> {
    return this.http.post(`api/file/create`, uploadFile, { responseType: 'text' });
  }

  public GetFileNames(): Observable<FileInfoModel[]> {
    return this.http.get<FileInfoModel[]>(`api/file/file-list`);
  } 

  public RemoveFile(id: number): Observable<string> {
    return this.http.delete(`api/file/remove/${id}`, { responseType: 'text' })
  }

  public DownloadFile(id: number): Observable<Blob> {
    return this.http.get(`api/file/download/${id}`, { responseType: 'blob' });
  }
}
