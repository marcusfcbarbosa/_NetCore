
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})

export class EventosComponent implements OnInit {
  _filtroLista: string;
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  eventosFiltrados: any = [];
  eventos: any;
  imagemLargura = 100;
  imagemMargem = 2;
  mostrarImagem = false;


  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.getEventos();
  }

  filtrarEvento(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );

  }
  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }
  getRetornoPadrao() {
    return [
      {
        id: 1,
        local: 'Sao Paulo',
        dataEvento: '29/05/1986',
        tema: 'Banco Rendimento',
        qtdPessoas: 200,
        imgUrl: 'img1.jpg',
        telefone: '11487578758',
        email: 'marcus@teste.com'
      },
      {
        id: 2,
        local: 'Sao Paulo',
        dataEvento: '29/05/1986',
        tema: 'Banco Rendimento',
        qtdPessoas: 200,
        imgUrl: 'img2.jpg',
        telefone: '11487578758',
        email: 'marcus@teste.com'
      }, {
        id: 3,
        local: 'Sao Paulo',
        dataEvento: '29/05/1986',
        tema: 'Banco Rendimento',
        qtdPessoas: 200,
        imgUrl: 'img3.jpg',
        telefone: '11487578758',
        email: 'marcus@teste.com'
      }, {
        id: 4,
        local: 'Sao Paulo',
        dataEvento: '29/05/1986',
        tema: 'Banco Rendimento',
        qtdPessoas: 200,
        imgUrl: 'img4.jpg',
        telefone: '11487578758',
        email: 'marcus@teste.com'
      }
    ];
  }

  getEventos() {
    this.eventos = this.http.get('http://localhost:5000/api/evento').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = response;
        if (this.eventos.length === 0) {
          this.eventos = this.getRetornoPadrao();
        }
      }, error => {
        console.log(error);
      }
    );
  }
}
