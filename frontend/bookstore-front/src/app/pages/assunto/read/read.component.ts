import { Component, OnInit } from '@angular/core';
import { Assunto } from '../models/assunto';
import { AssuntoService } from '../services/assunto.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
})
export class ReadComponent implements OnInit {

  public assuntos: Assunto[];
  errors: any[] = [];

  constructor(
    private assuntoService: AssuntoService,
    private toastrService: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show();
    this.assuntoService.findAll().subscribe({
      next: assuntos => this.assuntos = assuntos,
      error: fail => this.handlerFail(fail)
    });
    setTimeout(() => {
      this.spinner.hide();
    }, 1000);
  }

  handlerFail(fail: any) {
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro inesperado ao listar os assuntos', 'Opa :(');
  }
}
