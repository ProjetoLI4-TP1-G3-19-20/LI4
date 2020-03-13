using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Departamento
    {
        public String nome;
        public int id;
        private Dictionary<int, Vaga> horas_disponiveis;

        public Departamento()
        {
            this.nome = "";
            this.id = -1;
            this.horas_disponiviveis;
        }

        public Departamento(String nome, int id, Dictionary<int, Vaga> horas_disponiveis)
        {
            this.nome = nome;
            this.id = id;
            this.horas_disponiveis = horas_disponiveis;
        }

        public Departamento(Departamento departamento)
        {
            this.nome = departamento.nome();
            this.id = departamento.id();
            this.horas_disponiveis = departamento.horas_disponiveis;
        }
    }
}
