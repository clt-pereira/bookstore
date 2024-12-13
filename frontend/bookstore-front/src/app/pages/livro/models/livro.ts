import { Assunto } from "../../assunto/models/assunto";
import { Autor } from "../../autor/models/autor";

export interface Livro {
    id: number;
    titulo: string;
    editora: string;
    edicao: number;
    anoPublicacao: string;
    valor: number;
    assuntos: Assunto[];
    autores: Autor[];
}