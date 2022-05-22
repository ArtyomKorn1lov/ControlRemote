import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";
import { firstValueFrom, Observable } from "rxjs";
import { LoginModel } from '../Dto/LoginModel';
import { RegisterModel } from '../Dto/RegisterModel';
import { AuthoriseModel } from '../Dto/AuthoriseModel';
import { UserModel } from '../Dto/UserModel';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: HttpClient) { }

  public Registration(user: RegisterModel): Observable<string> {
    return this.http.post(`api/account/register`, user, { responseType: 'text' });
  }

  public Login(user: LoginModel): Observable<string> {
    return this.http.post(`api/account/login`, user, { responseType: 'text' });
  }

  public IsUserAuthorized(): Observable<AuthoriseModel> {
    return this.http.get<AuthoriseModel>(`api/account/is-authorized`);
  }

  public LogOut(): Observable<string> {
    var user = new UserModel(0, "", "", "");
    return this.http.post(`api/account/logout`, user, { responseType: 'text' });
  }

  public GetUsers(): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`api/account/user-list`);
  }
}
