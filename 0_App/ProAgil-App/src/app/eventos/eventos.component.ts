
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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


  constructor(private http: HttpClient) { }
  ngOnInit() { 
    this.getEventos();
   }
  //metodo que preenche o objeto declarado 
  getEventos() {
    this.eventos = this.http.get('http://localhost:5000/api/eventos').subscribe(
        response =>  { 
          this.eventos = response;
         }, error =>{
           console.log(error);
         }
    );
  }
}
