﻿using System;
using System.Collections.Generic;
using System.Text;
using PessoaDeInteresse;

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
            this.nr_pessoas = vaga.getNr_pessoas();
            this.hora_fim = vaga.hora_fim;
            this.hora_inicio = vaga.hora_inicio;
            this.pessoasVisitaveis = vaga.getPessoasVisitaveis;
        }

        public int getNr_pessoas()
        {

            return nr_pessoas;
        }

        public DateTime getHora_fim()
        {
            return hora_fim;
        }

        public DateTime getHora_inicio()
        {
            return hora_inicio;
        }

        public void setNr_pessoas(int nr_pessoas)
        {
            this.nr_pessoas = nr_pessoas;
        }

        public void setHora_fim(DateTime hora_fim)
        {
            this.hora_fim = hora_fim;
        }

        public void setHora_inicio(DateTime hora_inicio)
        {
            this.hora_inicio = hora_inicio;
        }

        public List<PessoaDeInteresse> getPessoasVisitaveis()
        {
            return pessoasVisitaveis;
        }

        public void setPessoasVisitaveis(List<PessoaDeInteresse> pessoasVisitaveis)
        {
            this.pessoasVisitaveis = pessoasVisitaveis;
        }
    }
}
