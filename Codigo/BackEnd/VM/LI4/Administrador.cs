using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Administrador : Utilizador
    {

        private int inst;

        public Administrador()
        {
            this.inst = -1;
            this.nome = "";
            this.email = "";
            this.telefone = "";
            this.password = "";
            this.id_utilizador = -2;
        }

        public Administrador(int inst, int id, string nome, string email, string telefone, string pass)
        {
            this.inst = inst;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.password = pass;
            this.id_utilizador = id;
        }

        public Administrador(Administrador a)
        {
            this.inst = a.Getinst();
            this.nome = a.GetNome();
            this.email = a.GetEmail();
            this.telefone = a.GetTelefone();
            this.password = a.GetPassword();
            this.id_utilizador = a.GetId_utilizador();
        }

        public int Getinst()
        {
            return inst;
        }

        public void Setinst(int inst)
        {
            this.inst = inst;
        }


        public override void SetNome(string nome)
        {
            this.nome=nome;
        }

        public override void SetEmail(string email)
        {
            this.email=email;
        }

        public override void SetPassword(string password)
        {
           this.password=password;
        }

        public override void SetTelefone(string telefone)
        {
            this.telefone=telefone;
        }

        public override void SetId_utilizador(int id)
        {
            this.id_utilizador=id;
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
    }
}
