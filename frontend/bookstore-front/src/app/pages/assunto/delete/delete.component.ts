import { Component } from '@angular/core';
import { Assunto } from '../models/assunto';
import { AssuntoService } from '../services/assunto.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-delete',
  templateUrl: './delete.component.html',
})
export class DeleteComponent {
  assunto: Assunto;
  errors: any[] = [];

  constructor(
    private assuntoService: AssuntoService,
    private route: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService) {

    this.assunto = this.route.snapshot.data['assunto'];
  }

  delete(): void {
    this.assuntoService.delete(this.assunto.id).subscribe({
      next: success => this.handlerSuccess(success),
      error: fail => this.handlerFail(fail),
      complete: () => console.log('Operação concluída.')
    });
  }

  handlerSuccess(response: any) {
    let toastResponse = this.toastrService.success('Assunto excluído com sucesso!', "Exclusão");
    if (toastResponse) {
      toastResponse.onHidden.subscribe(() => {
        this.router.navigate(['/assunto/read']);
      });
    }
  }
  
  handlerFail(fail: any) {
    // Adiciona a resposta com falha na coleção de erros.
    this.errors = fail.error.errors;
    this.toastrService.error('Ocorreu um erro durante exclusão do assunto', 'Opa :(');
  }
}
