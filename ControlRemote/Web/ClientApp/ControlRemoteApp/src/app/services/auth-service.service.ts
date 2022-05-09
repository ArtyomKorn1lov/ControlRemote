import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { firstValueFrom } from "rxjs";
import { LoginModel } from '../dto/LoginModel';
import { RegisterModel } from '../dto/RegisterModel';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http: HttpClient) { }

  public async Registration(user: RegisterModel): Promise<boolean> {
    let registrationResult: Promise<string> = firstValueFrom(await this.http.post<string>(`api/account/register`, user));
    if(registrationResult.toString() == "success") {
      return true;
    }
    return false;
  }

  public async Login(user: LoginModel): Promise<boolean> {
    let loginResult: Promise<string> = firstValueFrom(await this.http.post<string>(`api/account/login`, user));
    if(loginResult.toString() == "success") {
      return true;
    }
    return false;
  }

  public async IsUserAuthorized(): Promise<boolean> {
    let authType: Promise<string> = firstValueFrom(await this.http.get<string>(`api/account/is-authorized`));
    if(authType != null) {
      return true;
    }
    return false;
  } 

  public async LogOut(): Promise<string> {
    return firstValueFrom(await this.http.get<string>(`api/account/logout`))
  }

}
