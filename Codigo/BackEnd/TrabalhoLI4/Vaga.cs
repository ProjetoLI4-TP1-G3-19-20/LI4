using System;
using System.Collections.Generic;
using System.Text;
using LI4;

namespace LI4
{

    public class Vaga
    {
        private DateTime hora_fim;
        private DateTime hora_inicio;



        public Vaga()
        {
            this.hora_fim = new DateTime();
            this.hora_inicio = new DateTime();

        }

        public Vaga(int nr_pessoas, DateTime hora_fim, DateTime hora_inicio, List<PessoaDeInteresse> pessoasVisitaveis)
        {
            this.hora_fim = hora_fim;
            this.hora_inicio = hora_inicio;

        }

        public Vaga(Vaga vaga)
        {
            this.hora_fim = vaga.GetHora_fim();
            this.hora_inicio = vaga.GetHora_inicio();

        }

        public DateTime GetHora_fim()
        {
            return hora_fim;
        }

        public DateTime GetHora_inicio()
        {
            return hora_inicio;
        }

        public void SetHora_fim(DateTime hora_fim)
        {
            this.hora_fim = hora_fim;
        }

        public void SetHora_inicio(DateTime hora_inicio)
        {
            this.hora_inicio = hora_inicio;
        }

        public string getJson() {
            string json = "";

            json += "{\"inicio\":\"" + this.GetHora_inicio().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\", ";
            json += "\"fim\":\"" + this.GetHora_fim().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\"} ";

            return json;
        }


    }
}
