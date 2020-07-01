using System;
using System.Collections;
using System.Collections.Generic;

namespace LI4
{
    public class Instituicao
    {
        private int cod_instituicao;
        private string email;
        private string nome;
        private String localizacao;


        public Instituicao()
        {
            cod_instituicao = -1;
            nome = "";
            email = "";
            localizacao = "";
        }

        public Instituicao(int cod_instituicao, string nome, string email, ArrayList contactos, string localizacao)
        {
            this.cod_instituicao = cod_instituicao;
            this.nome = nome;
            this.email = email;
            this.localizacao = localizacao;
        }

        public Instituicao(Instituicao i)
        {
            this.cod_instituicao = i.GetCod_instituicao();
            this.nome = i.GetNome();
            this.email = i.GetEmail();
            this.localizacao = i.GetLocalizacao();
        }

        public string GetLocalizacao()
        {
            return localizacao;
        }

        public void SetLocalizacao(string loc)
        {
            this.localizacao = loc;
        }


        public string GetEmail(){
          return this.email;
        }

        public void SetEmail(string mail){
          this.email = mail;
        }

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public int GetCod_instituicao()
        {
            return cod_instituicao;
        }

        public void SetCod_instituicao(int cod)
        {
            this.cod_instituicao = cod;
        }
    }
}
