import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { BaseService } from '../../../shared/base.service';
import { Livro } from '../models/livro';

@Injectable({
  providedIn: 'root'
})
export class LivroService extends BaseService {

  constructor(private httpClient: HttpClient) { super(); }

  create(livro: Livro): Observable<Livro> {
    let response = this.httpClient
      .post(`${this.urlServiceV1}livro`, livro, this.getHeaderJson())
      .pipe(
        map(super.extractResponseData),
        catchError(super.handleResponseError));

    return response;
  }

  update(livro: Livro): Observable<void> {
    return this.httpClient
      .put<void>(`${this.urlServiceV1}livro/${livro.id}`, livro, this.getHeaderJson())
      .pipe(
        catchError(super.handleResponseError));
  }

  delete(id: number): Observable<void> {
    return this.httpClient
      .delete<void>(`${this.urlServiceV1}livro/${id}`, this.getHeaderJson())
      .pipe(
        catchError(super.handleResponseError)
      );
  }

  findAll(): Observable<Livro[]> {
    return this.httpClient
      .get<Livro[]>(`${this.urlServiceV1}livro`)
      .pipe(catchError(super.handleResponseError));
  }

  findById(id: number): Observable<Livro> {
    return this.httpClient
      .get<Livro>(`${this.urlServiceV1}livro/${id}`, this.getHeaderJson())
      .pipe(catchError(super.handleResponseError));
  }
}
