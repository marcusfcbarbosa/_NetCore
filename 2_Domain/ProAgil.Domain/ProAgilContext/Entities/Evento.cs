using ProAgil.Domain.ValueObjects;
using ProAgil.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProAgil.Domain.ProAgilContext.Entities
{
    public class Evento : Entity
    {
        private readonly IList<Lote> _lotes;
        private readonly IList<RedeSocial> _redesSociais;

        protected Evento() { }
        public Evento(
                string local,
                DateTime dataEvento,
                string tema, int qtdPessoas,
                string imgUrl,
                string telefone,
                Email email)
        {

            AddNotifications(email);

            this.Local = local;
            this.DataEvento = dataEvento;
            this.Tema = tema;
            this.QtdPessoas = qtdPessoas;
            this.ImgUrl = imgUrl;
            this.Telefone = telefone;
            this.Email = email.Address;
            _lotes = new List<Lote>();
            _redesSociais = new List<RedeSocial>();
        }
        public string Local { get; private set; }
        public DateTime DataEvento { get; private set; }
        public string Tema { get; private set; }
        public int QtdPessoas { get; private set; }
        public string ImgUrl { get; private set; }
        public string Telefone { get; private set; }
        public String Email { get; private set; }
        public IReadOnlyCollection<Lote> Lotes
        {
            get
            {
                if (_lotes != null)
                    return _lotes.ToArray();
                else
                    return default(List<Lote>);
            }
        }
        public IReadOnlyCollection<RedeSocial> RedesSociais { get
            {
                if (_redesSociais != null)
                    return _redesSociais.ToArray();
                else
                    return default(List<RedeSocial>);
            }
        }

        public List<PalestranteEvento> PalestranteEventos { get; set; }
        public void AdicionaRedeSocial(RedeSocial redeSocial)
        {
            this._redesSociais.Add(redeSocial);
        }
        public void AdicionaLote(Lote lote)
        {
            this._lotes.Add(lote);
        }
    }
}