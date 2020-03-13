using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Colaborador : Utilizador
    {

        private String departamento;

        public Colaborador()
        {
            super();
            this.departamento = "";
        }

        public Colaborador(String departamento)
        {
            this.departamento = departamento;
        }

        public Colaborador(Colaborador c)
        {
            this.departamento = c.getDepartamento();
        }

        public String getDepartamento()
        {
            return departamento;
        }

        public void setDepartamento(String departamento)
        {
            this.departamento = departamento;
        }

        public override getNome()
        {
          return this.nome;
        }

        public override getEmail()
        {
          return this.email;
        }

        public override getPassword()
        {
          return this.password;
        }

        public override getTelefone()
        {
          return this.telefone;
        }

        public override getId_utilizador()
        {
          return this.id_utilizador;
        }
    }
}
