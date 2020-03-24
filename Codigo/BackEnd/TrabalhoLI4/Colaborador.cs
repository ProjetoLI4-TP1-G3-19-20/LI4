using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Colaborador : Utilizador
    {

        private int departamento;

        public Colaborador()
        {
            this.departamento = -1;
        }

        public Colaborador(int departamento)
        {
            this.departamento = departamento;
        }

        public Colaborador(Colaborador c)
        {
            this.departamento = c.GetDepartamento();
        }

        public int GetDepartamento()
        {
            return departamento;
        }

        public void SetDepartamento(int departamento)
        {
            this.departamento = departamento;
        }

        public override String GetNome()
        {
          return this.nome;
        }

        public override String GetEmail()
        {
          return this.email;
        }

        public override String GetPassword()
        {
          return this.password;
        }

        public override String GetTelefone()
        {
          return this.telefone;
        }

        public override int GetId_utilizador()
        {
          return this.id_utilizador;
        }

        public override void SetNome(string nome)
        {
            this.nome = nome;
        }

        public override void SetEmail(string email)
        {
            this.email = email;
        }

        public override void SetPassword(string password)
        {
            this.password = password;
        }

        public override void SetTelefone(string telefone)
        {
            this.telefone = telefone;
        }

        public override void SetId_utilizador(int id)
        {
            this.id_utilizador = id;
        }
    }
}
