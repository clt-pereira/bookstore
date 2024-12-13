import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { LivroService } from "./livro.service";
import { Livro } from "../models/livro";

@Injectable()
export class LivroResolve implements Resolve<Livro> {

    constructor(private livroService: LivroService) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.livroService.findById(route.params['id']);
    }
}