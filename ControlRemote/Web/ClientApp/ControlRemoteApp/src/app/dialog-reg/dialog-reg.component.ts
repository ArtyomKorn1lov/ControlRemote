import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-reg',
  templateUrl: './dialog-reg.component.html',
  styleUrls: ['./dialog-reg.component.css']
})
export class DialogRegComponent implements OnInit {

  public login: string | undefined;
  public password: string | undefined;

  constructor() { }

  ngOnInit(): void {
  }

}
