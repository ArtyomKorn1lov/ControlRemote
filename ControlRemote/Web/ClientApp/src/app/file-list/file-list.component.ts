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
  public fileUpdate: File | undefined;

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

  public UploadUpdateFile(): void {
    document.getElementById("SelectImage")?.click();
  }

  public Download(event: any): void {
    this.fileUpdate = event.target.files[0];
    console.log(this.fileUpdate);
  }

  public UpdateFile(): void {
    if(this.fileUpdate == null || this.fileUpdate == undefined)
    {
      alert("Не выбран файл");
      return;
    }
    var formData = new FormData();
    formData.append('file', this.fileUpdate);
    console.log(formData);
    this.fileService.UpdateFile(formData).subscribe(data => {
      if(data == "success") {
        alert("Файл обновлён успешно");
        console.log(data);
        this.fileUpdate = undefined;
        return;
      }
      alert("Ошибка обновления файла");
      console.log(data);
      this.fileUpdate = undefined;
      return;
    });
  }

  public async ngOnInit(): Promise<void> {
    await this.fileService.GetFileNames().subscribe(data => {
      this.files = data;
    });
  }
}
