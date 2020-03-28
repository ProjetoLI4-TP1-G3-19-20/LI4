using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Departamento
    {
        private String nome;
        private int id;
        private List<Vaga> horas_disponiveis;

        public Departamento()
        {
            this.nome = "";
            this.id = -1;
            this.horas_disponiveis = new List<Vaga>();
        }

        public Departamento(String nome, int id, List<Vaga> horas_disponiveis)
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
            this.id = id;
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