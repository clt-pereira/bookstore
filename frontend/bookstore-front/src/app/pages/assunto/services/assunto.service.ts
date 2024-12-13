import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { BaseService } from '../../../shared/base.service';
import { Assunto } from '../models/assunto';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService extends BaseService {

  constructor(private httpClient: HttpClient) { super(); }

  create(assunto: Assunto): Observable<Assunto> {
    let response = this.httpClient
      .post(`${this.urlServiceV1}assunto`, assunto, this.getHeaderJson())
      .pipe(
        map(super.extractResponseData),
        catchError(super.handleResponseError));

    return response;
  }

  update(assunto: Assunto): Observable<void> {
    return this.httpClient
      .put<void>(`${this.urlServiceV1}assunto/${assunto.id}`, assunto, this.getHeaderJson())
      .pipe(
        catchError(super.handleResponseError));
  }

  delete(id: number): Observable<void> {
    return this.httpClient
      .delete<void>(`${this.urlServiceV1}assunto/${id}`, this.getHeaderJson())
      .pipe(
        catchError(super.handleResponseError)
      );
  }

  findAll(): Observable<Assunto[]> {
    return this.httpClient
      .get<Assunto[]>(`${this.urlServiceV1}assunto`)
      .pipe(catchError(super.handleResponseError));
  }

  findById(id: number): Observable<Assunto> {
    return this.httpClient
      .get<Assunto>(`${this.urlServiceV1}assunto/${id}`, this.getHeaderJson())
      .pipe(catchError(super.handleResponseError));
  }
}
