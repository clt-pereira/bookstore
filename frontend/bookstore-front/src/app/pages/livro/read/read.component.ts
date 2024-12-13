import { Component, OnInit } from '@angular/core';
import { Livro } from '../models/livro';
import { LivroService } from '../services/livro.service';
import { ToastrService } from 'ngx-toastr';
import { ExportAsConfig, ExportAsService } from 'ngx-export-as';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
})
export class ReadComponent implements OnInit {

  public livros: Livro[];
  exportAsConfig: ExportAsConfig = {
    type: 'xlsx',
    elementIdOrContent: 'livroTable'
  };

  errors: any[] = [];

  constructor(
    private livroService: LivroService,
    private toastrService: ToastrService,
    private exportAsService: ExportAsService
  ) {}

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

  exportarParaExcel(): void {
    this.exportAsService.save(this.exportAsConfig, 'livros').subscribe(() => {
      console.log("Exportando excel");
    });
  }
}