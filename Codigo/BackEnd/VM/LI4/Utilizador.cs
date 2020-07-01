using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public abstract class Utilizador
    {

        protected String nome;
        protected String email;
        protected String password;
        protected String telefone;
        protected int id_utilizador;

        protected Utilizador()
        {
            this.nome = "";
            this.email = "";
            this.password = "";
            this.telefone = "";
            this.id_utilizador = -1;
        }

        protected Utilizador(String nome, String email, String password, String telefone, int id_utilizador)
        {
            this.nome = nome;
            this.email = email;
            this.password = password;
            this.telefone = telefone;
            this.id_utilizador = id_utilizador;
        }

        protected Utilizador(Utilizador u)
        {
            this.nome = u.GetNome();
            this.email = u.GetEmail();
            this.password = u.GetPassword();
            this.telefone = u.GetTelefone();
            this.id_utilizador = u.GetId_utilizador();
        }

        public abstract String GetNome();
        public abstract String GetEmail();
        public abstract String GetPassword();
        public abstract String GetTelefone();
        public abstract int GetId_utilizador();

        public abstract void SetNome(String nome);
        public abstract void SetEmail(String email);
        public abstract void SetPassword(String password);
        public abstract void SetTelefone(String telefone);
        public abstract void SetId_utilizador(int id);
    }
}
