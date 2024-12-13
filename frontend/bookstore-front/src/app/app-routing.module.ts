import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './template/home/home.component';
import { NotFoundComponent } from './template/not-found/not-found.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'autor', loadChildren: () => import('./pages/autor/autor.module').then(m => m.AutorModule) },
  { path: 'assunto', loadChildren: () => import('./pages/assunto/assunto.module').then(m => m.AssuntoModule) },  
  { path: 'livro', loadChildren: () => import('./pages/livro/livro.module').then(m => m.LivroModule) },  
  { path: 'not-found', component: NotFoundComponent},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
