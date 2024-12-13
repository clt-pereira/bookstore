import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { fromEvent, merge, Observable } from 'rxjs';

import { Autor } from '../models/autor';
import { AutorService } from '../services/autor.service';
import { DisplayMessage, GenericValidator, ValidationMessages } from '../../../shared/generic-form-validation';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
})
export class CreateComponent implements OnInit, AfterViewInit {

  // Selector que varre o DOM com cada elemento  do formulário
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  //Coleção de erros de comunicação com a API
  errors: any[] = [];
  formResult: string = '';

  createForm: FormGroup;
  autor: Autor;

  //Objetos para controle de validação do formulário
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private fb: FormBuilder,
    private autorService: AutorService,
    private router: Router,
    private toastrService: ToastrService) {

    this.validationMessages = {
      nome: {
        required: 'Informe um nome para o autor',
        nome: 'Nome do autor inválido'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.createForm);
    });
  }

  ngOnInit(): void {
    this.createForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(5)]]
    })
  }

  create(): void {
    if (this.createForm.dirty && this.createForm.valid) {
      this.autor = Object.assign({}, this.autor, this.createForm.value)

      this.formResult = JSON.stringify(this.autor);

      this.autorService.create(this.autor).subscribe({
        next: success => this.handlerSuccess(success),
        error: fail => this.handlerFail(fail),
        complete: () => console.log('Operação concluída.')
      });
    }
  }

  handlerSuccess(response: any) {
    // Limpa o formulário e limpa a coleção de erros.
    this.createForm.reset();
    this.errors = [];

    let toastResponse = this.toastrService.success('Autor cadastrado com sucesso!', "Sucesso!");

    if (toastResponse) {
      toastResponse.onHidden.subscribe(() => {
        this.router.navigate(['/autor/read']);
      });
    }
  }

  handlerFail(fail: any) {
    // Adiciona a resposta com falha na coleção de erros.
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro durante a gravação dos dados do autor', 'Opa :(');
  }
}
