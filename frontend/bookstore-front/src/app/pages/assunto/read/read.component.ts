import { Component, OnInit } from '@angular/core';
import { Assunto } from '../models/assunto';
import { AssuntoService } from '../services/assunto.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
})
export class ReadComponent implements OnInit {

  public assuntos: Assunto[];
  errors: any[] = [];

  constructor(
    private assuntoService: AssuntoService,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    this.assuntoService.findAll().subscribe({
      next: assuntos => this.assuntos = assuntos,
      error: fail => this.handlerFail(fail)
    });
  }

  handlerFail(fail: any) {
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro inesperado ao listar os assuntos', 'Opa :(');
  }
}
