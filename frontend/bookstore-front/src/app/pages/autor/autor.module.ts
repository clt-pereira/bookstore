import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AutorRoutingModule } from './autor.route';
import { AutorAppComponent } from './autor.app.component';
import { AutorService } from './services/autor.service';
import { provideHttpClient } from '@angular/common/http';
import { CreateComponent } from './create/create.component';
import { ReadComponent } from './read/read.component';
import { UpdateComponent } from './update/update.component';
import { AutorResolve } from './services/autor.resolve';
import { DeleteComponent } from './delete/delete.component';

@NgModule({
  declarations: [
    AutorAppComponent,
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
    AutorRoutingModule,
    ReactiveFormsModule
  ],
  providers: [
    AutorService,
    AutorResolve,
    provideHttpClient()
  ]
})
export class AutorModule { }
