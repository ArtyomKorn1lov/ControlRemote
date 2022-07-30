import { Component, OnInit } from '@angular/core';
import { FileService } from '../Services/file.service';
import { Router } from '@angular/router';
import { UploadFileModel } from '../Dto/UploadFileModel';

@Component({
  selector: 'app-file-create',
  templateUrl: './file-create.component.html',
  styleUrls: ['./file-create.component.css']
})
export class FileCreateComponent implements OnInit {

  private targetRoute: string = "/file-list";
  public file: File | undefined;

  constructor(private fileService: FileService, private router: Router) { }

  public UploadFile(): void {
    document.getElementById("SelectImage")?.click();
  }

  public Download(event: any): void {
    this.file = event.target.files[0];
    console.log(this.file);
  }

  public DownloadOnServer(): void {
    if(this.file == null || this.file == undefined)
    {
      alert("Не выбран файл");
      return;
    }
    var formData = new FormData();
    formData.append('file', this.file);
    console.log(formData);
    this.fileService.CreateFile(formData).subscribe(data => {
      if(data == "success") {
        alert("Файл загружен успешно");
        console.log(data);
        this.router.navigateByUrl(this.targetRoute);
        return;
      }
      alert("Ошибка загрузки файла");
      console.log(data);
      return;
    });
  }

  ngOnInit(): void {
  }

}
