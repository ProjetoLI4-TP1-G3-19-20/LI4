using System;
using System.Collections.Generic;
using System.Text;
using LI4;

namespace LI4
{

    public class Vaga
    {
        private int nr_pessoas;
        private DateTime hora_fim;
        private DateTime hora_inicio;
        private List<PessoaDeInteresse> pessoasVisitaveis;


        public Vaga()
        {
            this.nr_pessoas = -1;
            this.hora_fim = new DateTime();
            this.hora_inicio = new DateTime();
            this.pessoasVisitaveis = new List<PessoaDeInteresse>();
        }

        public Vaga(int nr_pessoas, DateTime hora_fim, DateTime hora_inicio, List<PessoaDeInteresse> pessoasVisitaveis)
        {
            this.nr_pessoas = nr_pessoas;
            this.hora_fim = hora_fim;
            this.hora_inicio = hora_inicio;
            this.pessoasVisitaveis = pessoasVisitaveis;
        }

        public Vaga(Vaga vaga)
        {
            this.nr_pessoas = vaga.GetNr_pessoas();
            this.hora_fim = vaga.GetHora_fim();
            this.hora_inicio = vaga.GetHora_inicio();
            this.pessoasVisitaveis = vaga.GetPessoasVisitaveis();
        }

        public int GetNr_pessoas()
        {

            return nr_pessoas;
        }

        public DateTime GetHora_fim()
        {
            return hora_fim;
        }

        public DateTime GetHora_inicio()
        {
            return hora_inicio;
        }

        public void SetNr_pessoas(int nr_pessoas)
        {
            this.nr_pessoas = nr_pessoas;
        }

        public void SetHora_fim(DateTime hora_fim)
        {
            this.hora_fim = hora_fim;
        }

        public void SetHora_inicio(DateTime hora_inicio)
        {
            this.hora_inicio = hora_inicio;
        }

        public List<PessoaDeInteresse> GetPessoasVisitaveis()
        {
            return new List<PessoaDeInteresse>(this.pessoasVisitaveis);
        }

        public void SetPessoasVisitaveis(List<PessoaDeInteresse> pessoasVisitaveis)
        {
            this.pessoasVisitaveis = new List<PessoaDeInteresse>(pessoasVisitaveis);
        }
    }
}
