import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { DeleteComponent } from './delete/delete.component';
import { ReadComponent } from './read/read.component';
import { UpdateComponent } from './update/update.component';
import { AssuntoAppComponent } from './assunto.app.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AssuntoRoutingModule } from './assunto.route';
import { AssuntoService } from './services/assunto.service';
import { AssuntoResolve } from './services/assunto.resolve';
import { provideHttpClient } from '@angular/common/http';

@NgModule({
  declarations: [
    AssuntoAppComponent,
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
    AssuntoRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    AssuntoService,
    AssuntoResolve,
    provideHttpClient()
  ]
})
export class AssuntoModule { }
