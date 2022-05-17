import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { LoginModel } from '../Dto/LoginModel';
import { AuthServiceService } from '../Services/auth-service.service';

@Component({
  selector: 'app-dialog-auth',
  templateUrl: './dialog-auth.component.html',
  styleUrls: ['./dialog-auth.component.css']
})
export class DialogAuthComponent implements OnInit {

  public login: string | undefined;
  public password: string | undefined;

  constructor(public dialogRef: MatDialogRef<DialogAuthComponent>, private authService: AuthServiceService) { }

  public Login(): void {
    if (this.login == undefined || this.login.trim() == '') {
      alert("Введите логин");
      this.login = '';
      return;
    }
    if (this.password == undefined || this.password.trim() == '') {
      alert("Введите пароль");
      this.password = '';
      return;
    }
    var model = new LoginModel(this.login, this.password);
    this.authService.Login(model).subscribe(data => {
      if(data == "success") {
        console.log(data);
        alert(data);
        this.dialogRef.close();
        location.reload();
        return;
      }
      alert("Некорректные логин и(или) пароль");
      console.log(data);
      this.login = '';
      this.password = '';
      return;
    });
  }

  ngOnInit(): void {
  }

}
