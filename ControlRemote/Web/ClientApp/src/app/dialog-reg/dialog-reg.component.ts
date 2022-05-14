import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { RegisterModel } from '../dto/RegisterModel';
//import { AuthServiceService } from '../services/auth-service.service';

@Component({
  selector: 'app-dialog-reg',
  templateUrl: './dialog-reg.component.html',
  styleUrls: ['./dialog-reg.component.css']
})
export class DialogRegComponent implements OnInit {

  public name: string | undefined;
  public login: string | undefined;
  public password: string | undefined;
  public confirm_password: string | undefined;

  constructor(public dialogRef: MatDialogRef<DialogRegComponent>) { }

  public async Registration(): Promise<void> {
    /*if (this.name == undefined || this.name.trim() == '') {
      alert("Введите имя пользователя");
      this.name = '';
      return;
    }
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
    if (this.confirm_password == undefined || this.password.trim() == '') {
      alert("Подтвердите пароль");
      this.confirm_password = '';
      return;
    }
    if (this.confirm_password != this.password) {
      alert("Пароли не совпадают, проверьте пароли");
      this.password = '';
      this.confirm_password = '';
      return;
    }
    var model = new RegisterModel(this.name, this.login, this.password);
    let result = await this.authService.Registration(model);
    if(result == true) {
      console.log(result);
      this.dialogRef.close();
      return;
    }
    console.log(result);*/
  }

  ngOnInit(): void {
  }

}
