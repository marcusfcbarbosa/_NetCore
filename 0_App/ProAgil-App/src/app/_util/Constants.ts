export class Constants {
    static readonly DATE_FMT = 'dd/MM/yyyy';
    static readonly DATE_TIME_FMT = `${Constants.DATE_FMT} hh:mm:ss`;

    // ROTAS
    static readonly EVENTO_BASE_URL = 'http://localhost:5000/';
    static readonly EVENTO_BASE_EVENTO = 'http://localhost:5000/api/evento';
    static readonly URL_BASE_AUTHENTICATION = 'http://localhost:5000/api/authentication/';

    static readonly CONTEXT = 'proAgil';
}