import { Component } from '@angular/core';
import { Autor } from '../models/autor';
import { AutorService } from '../services/autor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
})
export class DeleteComponent {
  autor: Autor;
  errors: any[] = [];

  constructor(
    private autorService: AutorService,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService) {

    this.autor = this.route.snapshot.data['autor'];
  }

  delete(): void {
    this.autorService.delete(this.autor.id).subscribe({
      next: success => this.handlerSuccess(success),
      error: fail => this.handlerFail(fail),
      complete: () => console.log('Operação concluída.')
    });
  }

  handlerSuccess(response: any) {
    let toastResponse = this.toastrService.success('Autor excluído com sucesso!', "Exclusão");
    if (toastResponse) {
      toastResponse.onHidden.subscribe(() => {
        this.router.navigate(['/autor/read']);
      });
    }
  }
  
  handlerFail(fail: any) {
    // Adiciona a resposta com falha na coleção de erros.
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro durante exclusão do autor', 'Opa :(');
  }
}


