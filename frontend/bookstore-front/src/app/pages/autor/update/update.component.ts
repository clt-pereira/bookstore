import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { Autor } from '../models/autor';
import { DisplayMessage, GenericValidator, ValidationMessages } from '../../../shared/generic-form-validation';
import { AutorService } from '../services/autor.service';
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
  autor: Autor;

  //Objetos para controle de validação do formulário
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private fb: FormBuilder,
    private autorService: AutorService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastrService: ToastrService) {

    this.validationMessages = {
      nome: {
        required: 'Informe um nome para o autor',
        nome: 'Nome do autor inválido'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.autor = this.activatedRoute.snapshot.data['autor'];
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
      nome: ['', [Validators.required, Validators.minLength(5)]]
    });

    this.fillForm();
  }

  update(): void {
    if (this.updateForm.dirty && this.updateForm.valid) {
      this.autor = Object.assign({}, this.autor, this.updateForm.value)

      this.autorService.update(this.autor).subscribe({
        next: success => this.handlerSuccess(success),
        error: fail => this.handlerFail(fail),
        complete: () => console.log('Operação concluída.')
      });
    }
  }

  fillForm() {
    this.updateForm.patchValue({
      nome: this.autor.nome
    });
  }

  handlerSuccess(response: any) {
    let toastResponse = this.toastrService.success('Autor atualizado com sucesso!', "Sucesso!");

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
