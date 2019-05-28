using ProAgil.Domain.ValueObjects;
using ProAgil.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidator;
using FluentValidator.Validation;


namespace ProAgil.Domain.ProAgilContext.Entities
{
    public class Evento : Entity
    {
        private readonly IList<Lote> _lotes;
        private readonly IList<RedeSocial> _redesSociais;
       
        private Evento(){}
        public Evento(
                string local,
                DateTime dataEvento,
                string tema,int qtdPessoas,
                string imgUrl,
                string telefone,
                Email email){

                AddNotifications(email);

                this.Local = local;
                this.DataEvento = dataEvento;
                this.Tema = tema;
                this.QtdPessoas = qtdPessoas;
                this.ImgUrl =imgUrl;
                this.Telefone = telefone;

                _lotes = new List<Lote>();
                _redesSociais = new List<RedeSocial>();
        }
        public string Local { get; private set; }
        public DateTime DataEvento { get; private set; }
        public string Tema { get; private set; }
        public int QtdPessoas { get; private set; }
        public string ImgUrl { get; private set; }
        public string Telefone { get; private set; }
        public Email Email {get; private set;}
        public IReadOnlyCollection<Lote> Lotes { get { return _lotes.ToArray(); } }
        public IReadOnlyCollection<RedeSocial>  RedesSociais {get {return _redesSociais.ToArray(); }}
        public List<PalestranteEvento>  PalestranteEventos {get;set;}
        public void AdicionaRedeSocial(RedeSocial redeSocial){
            this._redesSociais.Add(redeSocial);
        }
        public void AdicionaLote(Lote lote){
                this._lotes.Add(lote);
        }
         
    }
}