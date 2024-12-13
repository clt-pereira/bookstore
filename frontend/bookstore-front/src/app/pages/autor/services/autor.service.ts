import { Injectable } from '@angular/core';
import { Autor } from '../models/autor';
import { HttpClient } from '@angular/common/http';
import { catchError, map, Observable } from 'rxjs';
import { BaseService } from '../../../shared/base.service';

@Injectable({
  providedIn: 'root'
})
export class AutorService extends BaseService {

  constructor(private httpClient: HttpClient) { super(); }

  create(autor: Autor): Observable<Autor> {
    let response = this.httpClient
      .post(`${this.urlServiceV1}autor`, autor, this.getHeaderJson())
      .pipe(
        map(super.extractResponseData),
        catchError(super.handleResponseError));

    return response;
  }

  update(autor: Autor): Observable<void> {
    return this.httpClient
      .put<void>(`${this.urlServiceV1}autor/${autor.id}`, autor, this.getHeaderJson())
      .pipe(
        catchError(super.handleResponseError));
  }

  delete(id: number): Observable<void> {
    return this.httpClient
      .delete<void>(`${this.urlServiceV1}autor/${id}`, this.getHeaderJson())
      .pipe(
        catchError(super.handleResponseError)
      );
  }

  findAll(): Observable<Autor[]> {
    return this.httpClient
      .get<Autor[]>(`${this.urlServiceV1}autor`)
      .pipe(catchError(super.handleResponseError));
  }

  findById(id: number): Observable<Autor> {
    return this.httpClient
      .get<Autor>(`${this.urlServiceV1}autor/${id}`, this.getHeaderJson())
      .pipe(catchError(super.handleResponseError));
  }
}
