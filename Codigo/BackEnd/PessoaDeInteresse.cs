using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class PessoaDeInteresse
    {
        private String nome;
        private String email;

        public PessoaDeInteresse()
        {
            this.nome = "";
            this.email = "";
        }

        public PessoaDeInteresse(String nome, String email)
        {
            this.nome = nome;
            this.email = email;
        }

        public PessoaDeInteresse(PessoaDeInteresse pessoa)
        {
            this.nome = pessoa.getNome();
            this.email = pessoa.getEmail();
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
    }
}
