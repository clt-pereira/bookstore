import { Component, OnInit } from '@angular/core';
import { Livro } from '../models/livro';
import { LivroService } from '../services/livro.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
})
export class ReadComponent implements OnInit {

  public livros: Livro[];
  errors: any[] = [];

  constructor(
    private livroService: LivroService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.livroService.findAll().subscribe({
      next: livros => this.livros = livros,
      error: fail => this.handlerFail(fail)
    });
  }

  handlerFail(fail: any) {
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro inesperado ao listar os livros', 'Opa :(');
  }    
}