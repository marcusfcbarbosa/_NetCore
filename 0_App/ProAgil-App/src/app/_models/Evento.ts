import { Lote } from './Lote';

export interface Evento {

    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    imgUrl: string;
    telefone: string;
    identifyer: string;

    lotes: Lote[];
    palestranteEventos: Palestrante[];
    redesSociais: RedeSocial[];
}
