import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-auth',
  templateUrl: './dialog-auth.component.html',
  styleUrls: ['./dialog-auth.component.css']
})
export class DialogAuthComponent implements OnInit {

  public login: string | undefined;
  public password: string | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
