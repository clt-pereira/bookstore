import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Assunto } from "../models/assunto";
import { AssuntoService } from "./assunto.service";

@Injectable()
export class AssuntoResolve implements Resolve<Assunto> {

    constructor(private assuntoService: AssuntoService) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.assuntoService.findById(route.params['id']);
    }
}