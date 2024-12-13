import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ReadComponent } from "./read/read.component";
import { CreateComponent } from "./create/create.component";
import { UpdateComponent } from "./update/update.component";
import { DeleteComponent } from "./delete/delete.component";
import { AssuntoAppComponent } from "./assunto.app.component";
import { AssuntoResolve } from "./services/assunto.resolve";

const assuntoRouterConfig: Routes = [
    {
        path: '', component: AssuntoAppComponent,
        children: [
            { path: 'read', component: ReadComponent },
            { path: 'create', component: CreateComponent, },
            {
                path: 'update/:id', component: UpdateComponent,
                resolve: {
                    assunto: AssuntoResolve
                }
            },
            {
                path: 'delete/:id', component: DeleteComponent,
                resolve: {
                    assunto: AssuntoResolve
                }
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(assuntoRouterConfig)
    ],
    exports: [RouterModule]
})

export class AssuntoRoutingModule { }