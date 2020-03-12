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

        public Departamento(String nome, int id)
        {
            this.nome = nome;
            this.id = id;
        }

        public Departamento(Departamento departamento)
        {
            this.nome = departamento.getNome();
            this.id = departamento.getId();
        }

        public String getNome()
        {
            return nome;
        }

        public void setNome(String nome)
        {
            this.nome = nome;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }
}
