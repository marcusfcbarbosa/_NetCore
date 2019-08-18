import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { ToastrService } from 'ngx-toastr';

import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  titulo = 'Eventos';
  _filtroLista: string;
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 100;
  imagemMargem = 2;
  mostrarImagem = false;
  modoSalvar = 'post';
  bodyDeletarEvento ='';
  registerForm: FormGroup;
  file: File;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista
      ? this.filtrarEvento(this.filtroLista)
      : this.eventos;
  }
  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  FileChange(event) {
    
    const reader = new FileReader();
    if(event.target.files && event.target.files.length){
      this.file = event.target.files;
    }
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {

      if(this.modoSalvar === 'post'){
        this.evento = Object.assign({}, this.registerForm.value);

        this.eventoService.postUpload(this.file).subscribe();
        const nomeArquivo = this.evento.imgUrl.split('\\', 3);
        this.evento.imgUrl = nomeArquivo[2];

        this.eventoService.postEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Criado com Sucesso!', ' \o/ ');
          },
          error => {
            this.toastr.error(`Erro de criaçao ${error}`);
          }
        );
      } else {
        this.evento = Object.assign({identifyer: this.evento.identifyer}, this.registerForm.value);
        this.eventoService.putEvento(this.evento).subscribe(
          (novoEvento: Evento) => {
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com Sucesso!', ' \o/ ');
          },
          error => {
            this.toastr.error(`Erro de edição ${error}`);
          }
        );
      }
    }
  }

  excluirEvento(evento: Evento, template: any) {
    
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }
  
  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.evento.identifyer).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Deletado!', 'Deletado!');
        }, error => {
          this.toastr.error(`Erro ao tentar deletar ${error}`);
        }
    );
  }

  editarEvento(evento: Evento, template: any) {
    this.modoSalvar = 'put';
    this.openModal(template);
    this.evento = evento;
    this.registerForm.patchValue(evento);
  }

  novoEvento(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  validation() {
    this.registerForm = this.fb.group({
      tema: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(50)]
      ],
      local: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(50)]
      ],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(20000)]],
      imgUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  filtrarEvento(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos() {
    this.eventoService.getAllEventos().subscribe(
      (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error => {
        this.toastr.error(`Erro ao tentar carregar eventos ${error}`);
      }
    );
  }
}
