using ProAgil.Domain.ValueObjects;
using ProAgil.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAgil.Domain.ProAgilContext.Entities
{
    public class Palestrante : Entity
    {
        protected Palestrante(){}
        private readonly IList<RedeSocial> _redesSociais;
        
        public Palestrante(
         string nome,
         string miniCurriculo,
         string imgUrl, 
         string telefone,
          Email email)
        {
            Nome = nome;
            MiniCurriculo = miniCurriculo;
            ImgUrl = imgUrl;
            Telefone = telefone;
            Email = email.Address;
            _redesSociais = new List<RedeSocial>();
         
        }
        public string Nome { get; private set; }
        public string MiniCurriculo { get; private set; }
        public string ImgUrl { get; private set; }
        public string Telefone { get; private set; }
        public String Email { get; private set; }
        public int EventoId {get; private set;}
        //Associativa
        public List<PalestranteEvento>  PalestranteEventos {get;set;}
        public IReadOnlyCollection<RedeSocial> RedesSociais
        {
            get
            {
                if (_redesSociais != null)
                    return _redesSociais.ToArray();
                else
                    return default(List<RedeSocial>);
            }
        }
        
        public void AdicionaRedeSocial(RedeSocial redeSocial){
            this._redesSociais.Add(redeSocial);
        }
        
    }
}