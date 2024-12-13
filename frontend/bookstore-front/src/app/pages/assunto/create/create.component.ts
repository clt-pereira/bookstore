import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Assunto } from '../models/assunto';
import { DisplayMessage, GenericValidator, ValidationMessages } from '../../../shared/generic-form-validation';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { AssuntoService } from '../services/assunto.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, merge, Observable } from 'rxjs';

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
  assunto: Assunto;

  //Objetos para controle de validação do formulário
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private fb: FormBuilder,
    private assuntoService: AssuntoService,
    private router: Router,
    private toastrService: ToastrService) {

    this.validationMessages = {
      descricao: {
        required: 'Informe a descrição para o assunto',
        nome: 'Descrição do assunto inválido'
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
      descricao: ['', [Validators.required, Validators.minLength(5)]]
    })
  }

  create(): void {
    if (this.createForm.dirty && this.createForm.valid) {
      this.assunto = Object.assign({}, this.assunto, this.createForm.value)

      this.formResult = JSON.stringify(this.assunto);

      this.assuntoService.create(this.assunto).subscribe({
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

    let toastResponse = this.toastrService.success('Assunto cadastrado com sucesso!', "Sucesso!!!");

    if (toastResponse) {
      toastResponse.onHidden.subscribe(() => {
        this.router.navigate(['/assunto/read']);
      });
    }
  }

  handlerFail(fail: any) {
    // Adiciona a resposta com falha na coleção de erros.
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro durante a gravação dos dados do assunto', 'Opa :(');
  }
}
