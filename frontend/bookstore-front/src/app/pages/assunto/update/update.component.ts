import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { Assunto } from '../models/assunto';
import { DisplayMessage, GenericValidator, ValidationMessages } from '../../../shared/generic-form-validation';
import { AssuntoService } from '../services/assunto.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { fromEvent, merge, Observable } from 'rxjs';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
})

export class UpdateComponent implements OnInit, AfterViewInit {

  // Selector que varre o DOM com cada elemento  do formulário
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  
  //Coleção de erros de comunicação com a API
  errors: any[] = [];
  
    updateForm: FormGroup;
    assunto: Assunto;
  
    //Objetos para controle de validação do formulário
    validationMessages: ValidationMessages;
    genericValidator: GenericValidator;
    displayMessage: DisplayMessage = {};
  
    constructor(
      private fb: FormBuilder,
      private assuntoService: AssuntoService,
      private router: Router,
      private activatedRoute: ActivatedRoute,
      private toastrService: ToastrService) {
  
      this.validationMessages = {
        descricao: {
          required: 'Informe uma descrição para o assunto',
          nome: 'Descrição do assunto inválida'
        }
      };
  
      this.genericValidator = new GenericValidator(this.validationMessages);
      this.assunto = this.activatedRoute.snapshot.data['assunto'];
    }
  
    ngAfterViewInit(): void {
      let controlBlurs: Observable<any>[] = this.formInputElements
        .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
  
      merge(...controlBlurs).subscribe(() => {
        this.displayMessage = this.genericValidator.processarMensagens(this.updateForm);
      });
    }
  
    ngOnInit(): void {
      this.updateForm = this.fb.group({
        descricao: ['', [Validators.required, Validators.minLength(5)]]
      });
  
      this.fillForm();
    }
  
    update(): void {
      if (this.updateForm.dirty && this.updateForm.valid) {
        this.assunto = Object.assign({}, this.assunto, this.updateForm.value)
  
        this.assuntoService.update(this.assunto).subscribe({
          next: success => this.handlerSuccess(success),
          error: fail => this.handlerFail(fail),
          complete: () => console.log('Operação concluída.')
        });
      }
    }
  
    fillForm() {
      this.updateForm.patchValue({
        descricao: this.assunto.descricao
      });
    }
  
    handlerSuccess(response: any) {
      let toastResponse = this.toastrService.success('Assunto atualizado com sucesso!', "Edição");
  
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
