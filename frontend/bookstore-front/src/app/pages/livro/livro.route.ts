import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ReadComponent } from "./read/read.component";
import { CreateComponent } from "./create/create.component";
import { UpdateComponent } from "./update/update.component";
import { DeleteComponent } from "./delete/delete.component";
import { LivroAppComponent } from "./livro.app.component";
import { LivroResolve } from "./services/livro.resolve";

const livroRouterConfig: Routes = [
    {
        path: '', component: LivroAppComponent,
        children: [
            { path: 'read', component: ReadComponent },
            { path: 'create', component: CreateComponent, },
            {
                path: 'update/:id', component: UpdateComponent,
                resolve: {
                    livro: LivroResolve
                }
            },
            {
                path: 'delete/:id', component: DeleteComponent,
                resolve: {
                    livro: LivroResolve
                }
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(livroRouterConfig)
    ],
    exports: [RouterModule]
})

export class LivroRoutingModule { }