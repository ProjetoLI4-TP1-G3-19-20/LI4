using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Administrador : Utilizador
    {

        private int departamento;

        public Administrador()
        {
            super();
            this.departamento = -1;
        }

        public Administrador(String nome, String email, String pass, String telefone, int id, int departamento)
        {
            super(nome, email, pass, telefone, id);
            this.departamento = departamento;
        }

        public Administrador(Administrador a)
        {
            super(a.getNome(), a.getEmail(), a.getPassword(), a.getTelefone(), a.getId_utilizador());
            this.departamento = a.getDepartamento();
        }

        public int getDepartamento()
        {
            return departamento;
        }

        public void setDepartamento(int departamento)
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

        public override getTelefone()
        {
          return this.telefone;
        }

        public override getPassword()
        {
          return this.password;
        }

        public override getId_utilizador()
        {
          return this.id_utilizador;
        }
    }
}
