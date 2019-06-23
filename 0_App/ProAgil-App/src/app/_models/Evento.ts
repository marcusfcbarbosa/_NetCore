import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {
  local: string;
  dataEvento: Date;
  tema: string;
  qtdPessoas: number;
  imgUrl: string;
  telefone: string;
  identifyer: string;
  email: string;
  lotes: Lote[];
  palestranteEventos: Palestrante[];
  redesSociais: RedeSocial[];
}
