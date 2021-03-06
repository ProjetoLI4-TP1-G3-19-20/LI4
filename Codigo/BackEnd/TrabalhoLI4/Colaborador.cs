﻿using System;
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
            this.nome = "";
            this.email = "";
            this.telefone = "";
            this.password = "";
            this.id_utilizador = -2;
        }

        public Colaborador(int departamento, int id, string nome, string email, string telefone, string pass)
        {
            this.departamento = departamento;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.password = pass;
            this.id_utilizador = id;
        }

        public Colaborador(Colaborador c)
        {
            this.departamento = c.GetDepartamento();
            this.nome = c.GetNome();
            this.email = c.GetEmail();
            this.telefone = c.GetTelefone();
            this.password = c.GetPassword();
            this.id_utilizador = c.GetId_utilizador();
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
