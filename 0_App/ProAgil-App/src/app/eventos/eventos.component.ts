import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any = [
    {
      id: 1,
      local: 'Sao Paulo',
      dataEvento: Date.now,
      tema: 'Banco Rendimento',
      qtdPessoas: 200,
      imgUrl: 'teste',
      telefone: '11487578758',
      email: 'marcus@teste.com'
    },
    {
      id: 2,
      local: 'Sao Paulo',
      dataEvento: Date.now,
      tema: 'Banco Rendimento',
      qtdPessoas: 200,
      imgUrl: 'teste',
      telefone: '11487578758',
      email: 'marcus@teste.com'
    }
  ];


  constructor() { }

  ngOnInit() {
  }

}
