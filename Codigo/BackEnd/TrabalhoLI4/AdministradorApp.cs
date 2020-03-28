using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class AdministradorApp : Utilizador
    {

        private int departamento;

        public AdministradorApp()
        {
            this.departamento = -1;
        }

        public AdministradorApp(int dep)
        {
            this.departamento = dep;
        }

        public AdministradorApp(Administrador a)
        {
            this.departamento = a.GetDepartamento();
        }

        public override String GetNome()
        {
          return this.nome;
        }

        public override String GetEmail()
        {
          return this.email;
        }

        public override String GetTelefone()
        {
          return this.telefone;
        }

        public override String GetPassword()
        {
          return this.password;
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
