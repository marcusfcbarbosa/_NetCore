import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';
import { Constants } from './../_util/Constants';
@Injectable({
  providedIn: 'root'
})

export class EventoService {
 // Constants.EVENTO_BASE_URL
  baseURL = Constants.EVENTO_BASE_URL;

  constructor(private http: HttpClient) { }

  getAllEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }

  getEventoByIdentifier(identifier: string): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${identifier}`);
  }
}
