using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class PessoaDeInteresse
    {
        private String nome;
        private String email;
        private List<HoraOcupada> horasOcupadas;

        public PessoaDeInteresse()
        {
            this.nome = "";
            this.email = "";
            this.horasOcupadas = new List<HoraOcupada>();
        }

        public PessoaDeInteresse(String nome, String email, List<HoraOcupada> horasOcupadas)
        {
            this.nome = nome;
            this.email = email;
            this.horasOcupadas = horasOcupadas;
        }

        public PessoaDeInteresse(PessoaDeInteresse pessoa)
        {
            this.nome = pessoa.getNome();
            this.email = pessoa.getEmail();
            this.horasOcupadas = pessoa.getHorasOcupadas();
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

        public List<HoraOcupada> getHorasOcupadas()
        {
            return horasOcupadas;
        }

        public void setHorasOcupadas(List<HoraOcupada> horasOcupadas)
        {
            this.horasOcupadas = horasOcupadas;
        }
    }
}
