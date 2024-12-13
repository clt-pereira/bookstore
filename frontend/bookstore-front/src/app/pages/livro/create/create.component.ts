import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, fromEvent, merge } from 'rxjs';
import { ValidationMessages, GenericValidator, DisplayMessage } from '../../../shared/generic-form-validation';
import { Livro } from '../models/livro';
import { LivroService } from '../services/livro.service';
import { AutorService } from '../../autor/services/autor.service';
import { AssuntoService } from '../../assunto/services/assunto.service';
import { Autor } from '../../autor/models/autor';
import { Assunto } from '../../assunto/models/assunto';

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
  errorMessage: string;

  createForm: FormGroup;
  livro: Livro;
  autores: Autor[] = [];
  assuntos: Assunto[] = [];

  //Objetos para controle de validação do formulário
  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

  constructor(
    private fb: FormBuilder,
    private livroService: LivroService,
    private autorService: AutorService,
    private assuntoService: AssuntoService,
    private router: Router,
    private toastrService: ToastrService) {

    this.validationMessages = {
      titulo: {
        required: 'Informe um título para o livro',
        nome: 'Título do livro inválido'
      },
      editora: {
        required: 'Informe uma editora para o livro',
        nome: 'Editora do livro inválido'
      },      
      edicao: {
        required: 'Informe uma edição para o livro',
        nome: 'Edição do livro inválido'
      },
      anoPublicacao: {
        required: 'Informe um ano de publicação para o livro',
        nome: 'Ano de publicação do livro inválido'
      },                  
      valor: {
        required: 'Informe um valor para o livro',
        nome: 'Valor do livro inválido'
      },
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
      titulo: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(40)]],
      editora: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]],
      edicao: ['', [Validators.required]],
      anoPublicacao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(4)]],
      valor: ['', [Validators.required]],
      autores: [[], Validators.required],
      assuntos: [[], Validators.required]
    })

    this.carregarAutores();
    this.carregarAssuntos();
  }

  create(): void {
    if (this.createForm.dirty && this.createForm.valid) {
      this.livro = Object.assign({}, this.livro, this.createForm.value)

      const autoresSelecionados = this.createForm.value.autores;
      const assuntosSelecionados = this.createForm.value.assuntos;

      this.livro.autores = this.autores.filter((autor) => 
        autoresSelecionados.includes(autor.id));

      this.livro.assuntos = this.assuntos.filter((assunto) => 
        assuntosSelecionados.includes(assunto.id));

      console.log('Enviando livro:', this.livro);

      this.formResult = JSON.stringify(this.livro);

      this.livroService.create(this.livro).subscribe({
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

    let toastResponse = this.toastrService.success('Livro cadastrado com sucesso!', "Sucesso!!!");

    if (toastResponse) {
      toastResponse.onHidden.subscribe(() => {
        this.router.navigate(['/livro/read']);
      });
    }
  }

  handlerFail(fail: any) {
    // Adiciona a resposta com falha na coleção de erros.
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro inesperado', 'Opa :(');
  }

  carregarAutores(): void {
    this.autorService.findAll().subscribe({
      next: autores => this.autores = autores,
      error: fail => this.handlerFail(fail)
    });
  }

  carregarAssuntos(): void {
    this.assuntoService.findAll().subscribe({
      next: assuntos => this.assuntos = assuntos,
      error: fail => this.handlerFail(fail)
    });
  }
}
