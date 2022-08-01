import { Component, OnInit } from '@angular/core';
import { FileService } from '../Services/file.service';
import { FileInfoModel } from '../Dto/FileInfoModel';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {

  public files: FileInfoModel[] = [];

  constructor(private fileService: FileService) { }

  public RemoveFile(id: number): void {
    this.fileService.RemoveFile(id).subscribe(data => {
      if(data == "success") {
        alert("Успешное удаление файла");
        console.log(data);
        this.ngOnInit();
        return;
      }
      alert("Ошибка удаления файла");
      console.log(data);
      return;
    })
  }

  public DownloadFile(id: number, name: string): void {
    this.fileService.DownloadFile(id).subscribe(data => {
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(data);
      a.href = objectUrl;
      a.download = name;
      a.click();
      URL.revokeObjectURL(objectUrl);
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.fileService.GetFileNames().subscribe(data => {
      this.files = data;
    });
  }
}
