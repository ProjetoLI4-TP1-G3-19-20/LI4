using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Departamento
    {
        private String nome;
        private int id;
        private List<PessoaDeInteresse> pess;
        

        public Departamento()
        {
            this.nome = "";
            this.id = -1;
            this.pess = new List<PessoaDeInteresse>();
        }

        public Departamento(String nome, int id, List<PessoaDeInteresse> pess)
        {
            this.nome = nome;
            this.id = id;
            this.pess = pess;
        }

        public Departamento(Departamento departamento)
        {
            this.nome = departamento.GetNome();
            this.id = departamento.GetID();
            this.pess = departamento.GetPessoaDeInteresses();
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


        public List<PessoaDeInteresse> GetPessoaDeInteresses()
        {
            return new List<PessoaDeInteresse>(this.pess); ;
        }

        public void SetPessoasDeInteresse(List<PessoaDeInteresse> pess)
        {
            this.pess = new List<PessoaDeInteresse>(pess);
        }

    }
}