using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Departamento
    {
        private String nome;
        private int id;
        private Dictionary<int, Vaga> horas_disponiveis;

        public Departamento()
        {
            this.nome = "";
            this.id = -1;
            this.horas_disponiveis = new Dictionary<int, Vaga>();
        }

        public Departamento(String nome, int id, Dictionary<int, Vaga> horas_disponiveis)
        {
            this.nome = nome;
            this.id = id;
            this.horas_disponiveis = horas_disponiveis;
        }

        public Departamento(Departamento departamento)
        {
            this.nome = departamento.GetNome();
            this.id = departamento.GetID();
            this.horas_disponiveis = departamento.GetHorasDisponiveis();
        }

        public String GetNome()
        {
            return this.nome;
        }

        public int GetID()
        {
            return this.id;
        }



        public void SetNome(String nome)
        {
            this.nome = nome; ;
        }

        public void SetID(int id)
        {
           this.id=id;
        }


        public Dictionary<int, Vaga> GetHorasDisponiveis()
        {
            return new Dictionary<int, Vaga>(this.horas_disponiveis); ;
        }

        public void SetHorasDisponiveis(Dictionary<int, Vaga> horas)
        {
            this.horas_disponiveis = new Dictionary<int, Vaga>(horas);
        }

    }
}
