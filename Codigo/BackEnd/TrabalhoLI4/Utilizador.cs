using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public abstract class Utilizador
    {

        private abstract String nome;
        private abstract String email;
        private abstract String password;
        private abstract String telefone;
        private abstract int id_utilizador;

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
            this.nome = u.getNome();
            this.email = u.getEmail();
            this.password = u.getPassword();
            this.telefone = u.getTelefone();
            this.id_utilizador = u.getId_utilizador();
        }

        public abstract String getNome();
        public abstract String getEmail();
        public abstract String getPassword();
        public abstract String getTelefone();
        public abstract int getId_utilizador();

        public abstract void setNome(String nome);
        public abstract void setEmail(String email);
        public abstract void setPassword(String password);
        public abstract void setTelefone(String telefone);
        public abstract void setId_utilizador(int id);
    }
}
