using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Departamento
    {
        private String nome;
        private int id;
        

        public Departamento()
        {
            this.nome = "";
            this.id = -1;
        }

        public Departamento(String nome, int id, List<PessoaDeInteresse> pess)
        {
            this.nome = nome;
            this.id = id;
        }

        public Departamento(Departamento departamento)
        {
            this.nome = departamento.GetNome();
            this.id = departamento.GetID();
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

    }
}