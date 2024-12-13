import { Component } from '@angular/core';
import { Livro } from '../models/livro';
import { LivroService } from '../services/livro.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
})
export class DeleteComponent {
  livro: Livro;
  errors: any[] = [];

  constructor(
    private livroService: LivroService,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService) {

    this.livro = this.route.snapshot.data['livro'];
  }

  delete(): void {
    this.livroService.delete(this.livro.id).subscribe({
      next: success => this.handlerSuccess(success),
      error: fail => this.handlerFail(fail),
      complete: () => console.log('Operação concluída.')
    });
  }

  handlerSuccess(response: any) {
    let toastResponse = this.toastrService.success('Livro excluído com sucesso!', "Exclusão");
    if (toastResponse) {
      toastResponse.onHidden.subscribe(() => {
        this.router.navigate(['/livro/read']);
      });
    }
  }
  
  handlerFail(fail: any) {
    // Adiciona a resposta com falha na coleção de erros.
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro durante exclusão do livro', 'Opa :(');
  }
}