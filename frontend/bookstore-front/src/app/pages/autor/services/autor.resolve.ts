import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { AutorService } from "./autor.service";
import { Autor } from "../models/autor";

@Injectable()
export class AutorResolve implements Resolve<Autor> {

    constructor(private autorService: AutorService) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.autorService.findById(route.params['id']);
    }
}