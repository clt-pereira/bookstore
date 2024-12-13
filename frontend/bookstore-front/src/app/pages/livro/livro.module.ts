import { NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { ReadComponent } from './read/read.component';
import { DeleteComponent } from './delete/delete.component';
import { UpdateComponent } from './update/update.component';
import { CreateComponent } from './create/create.component';
import { LivroAppComponent } from './livro.app.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LivroService } from './services/livro.service';
import { LivroResolve } from './services/livro.resolve';
import { provideHttpClient } from '@angular/common/http';
import { LivroRoutingModule } from './livro.route';
import { CurrencyMaskModule } from 'ng2-currency-mask';

@NgModule({
  declarations: [
    LivroAppComponent,
    CreateComponent,
    ReadComponent,
    ReadComponent,
    UpdateComponent,
    DeleteComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    LivroRoutingModule,
    ReactiveFormsModule,
    CurrencyMaskModule
  ],
  providers: [
    LivroService,
    LivroResolve,
    provideHttpClient(),
  ]
})
export class LivroModule { }
