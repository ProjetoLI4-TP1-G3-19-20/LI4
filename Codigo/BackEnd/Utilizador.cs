using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Utilizador
    {

        private String nome;
        private String email;
        private String password;
        private String telefone;
        private int id_utilizador;

        public Utilizador()
        {
            this.nome = "";
            this.email = "";
            this.password = "";
            this.telefone = "";
            this.id_utilizador = -1;
        }

        public Utilizador(String nome, String email, String password, String telefone, int id_utilizador)
        {
            this.nome = nome;
            this.email = email;
            this.password = password;
            this.telefone = telefone;
            this.id_utilizador = id_utilizador;
        }

        public Utilizador(Utilizador u)
        {
            this.nome = u.getNome();
            this.email = u.getEmail();
            this.password = u.getPassword();
            this.telefone = u.getTelefone();
            this.id_utilizador = u.getId_utilizador();
        }

        public String getNome()
        {
            return nome;
        }

        public void setNome(String nome)
        {
            this.nome = nome;
        }

        public String getEmail()
        {
            return email;
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

        public String getPassword()
        {
            return password;
        }

        public void setPassword(String password)
        {
            this.password = password;
        }

        public String getTelefone()
        {
            return telefone;
        }

        public void setTelefone(String telefone)
        {
            this.telefone = telefone;
        }

        public int getId_utilizador()
        {
            return id_utilizador;
        }

        public void setId_utilizador(int id_utilizador)
        {
            this.id_utilizador = id_utilizador;
        }
    }
}
