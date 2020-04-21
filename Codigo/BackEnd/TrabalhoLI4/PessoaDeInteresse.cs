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
        private List<Vaga> horas_disponiveis;

        public PessoaDeInteresse()
        {
            this.nome = "";
            this.email = "";
            this.horasOcupadas = new List<HoraOcupada>();
            this.horas_disponiveis = new List<Vaga>();
        }

        public PessoaDeInteresse(String nome, String email, List<HoraOcupada> horasOcupadas, List<Vaga> horas_disponiveis)
        {
            this.nome = nome;
            this.email = email;
            this.horasOcupadas = horasOcupadas;
            this.horas_disponiveis = horas_disponiveis;
        }

        public PessoaDeInteresse(PessoaDeInteresse pessoa)
        {
            this.nome = pessoa.getNome();
            this.email = pessoa.getEmail();
            this.horasOcupadas = pessoa.getHorasOcupadas();
            this.horas_disponiveis = pessoa.GetHorasDisponiveis();
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

        public List<Vaga> GetHorasDisponiveis()
        {
            return new List<Vaga>(this.horas_disponiveis); ;
        }

        public void SetHorasDisponiveis(List<Vaga> horas)
        {
            this.horas_disponiveis = new List<Vaga>(horas);
        }
    }
}
