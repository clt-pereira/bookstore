import { HttpErrorResponse, HttpHeaders } from "@angular/common/http"
import { throwError } from "rxjs";
import { environment } from "../../environments/environment";

export abstract class BaseService{
    
    protected urlServiceV1: string = environment.apiBaseUrl;

    protected getHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        }
    }

    protected extractResponseData(response: any){
        return response.data || {};
    }

    protected handleResponseError(response: Response | any)  {
        let customError: string[] = [];

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }
        }

        console.error(response);
        return throwError(() => response);
    }    
}