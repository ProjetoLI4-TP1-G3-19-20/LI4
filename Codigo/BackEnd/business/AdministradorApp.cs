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
            super();
        }

        public AdministradorApp(String nome, String email, String pass, String telefone, int id)
        {
            super(nome, email, pass, telefone, id);
            this.departamento = departamento;
        }

        public AdministradorApp(Administrador a)
        {
            super(a.getNome(), a.getEmail(), a.getPassword(), a.getTelefone(), a.getId_utilizador());
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
