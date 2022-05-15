import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { firstValueFrom, Observable } from "rxjs";
import { LoginModel } from '../dto/LoginModel';
import { RegisterModel } from '../dto/RegisterModel';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http: HttpClient) { }

  public Registration(user: RegisterModel): Observable<string> {
    return this.http.post<string>(`api/account/register`, user);
  }

  public async Login(user: LoginModel): Promise<boolean> {
    let loginResult: Promise<string> = firstValueFrom(await this.http.post<string>(`api/account/login`, user));
    if(loginResult.toString() == "success") {
      return true;
    }
    return false;
  }

  public IsUserAuthorized(): Observable<string> {
    return this.http.get<string>(`api/account/is-authorized`);
  } 

  public async LogOut(): Promise<string> {
    return firstValueFrom(await this.http.get<string>(`api/account/logout`))
  }

}
