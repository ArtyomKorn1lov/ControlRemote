import { Component, OnInit } from '@angular/core';
import { AccountService } from '../Services/account.service';
import { UserModel } from '../Dto/UserModel';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  public users: UserModel[] = [];

  constructor(private accountService: AccountService) { }

  public async ngOnInit(): Promise<void> {
    await this.accountService.GetUsers().subscribe(data => {
      this.users = data;
    });
  }

}
