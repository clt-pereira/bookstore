import { Component, OnInit } from '@angular/core';
import { Autor } from '../models/autor';
import { AutorService } from '../services/autor.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
})
export class ReadComponent implements OnInit {

  public autores: Autor[];
  errors: any[] = [];

  constructor(
    private autorService: AutorService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.autorService.findAll().subscribe({
      next: autores => this.autores = autores,
      error: fail => this.handlerFail(fail)
    });
  }
  
  handlerFail(fail: any) {
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro inesperado ao listar os autores', 'Opa :(');
  }  
}
