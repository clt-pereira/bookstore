import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AutorAppComponent } from "./autor.app.component";
import { ReadComponent } from "./read/read.component";
import { CreateComponent } from "./create/create.component";
import { UpdateComponent } from "./update/update.component";
import { AutorResolve } from "./services/autor.resolve";
import { DeleteComponent } from "./delete/delete.component";

const autorRouterConfig: Routes = [
    {
        path: '', component: AutorAppComponent,
        children: [
            { path: 'read', component: ReadComponent },
            { path: 'create', component: CreateComponent, },
            {
                path: 'update/:id', component: UpdateComponent,
                resolve: {
                    autor: AutorResolve
                }
            },
            {
                path: 'delete/:id', component: DeleteComponent,
                resolve: {
                    autor: AutorResolve
                }
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(autorRouterConfig)
    ],
    exports: [RouterModule]
})

export class AutorRoutingModule { }