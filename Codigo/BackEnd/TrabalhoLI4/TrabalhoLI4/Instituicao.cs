using System;
using System.Collections;
using System.Collections.Generic;

namespace LI4
{
    public class Instituicao
    {
        private int cod_instituicao;
        private string email;
        private string nome;
        private ArrayList contactos;
        private String localizacao;
        private Dictionary<string, Colaborador> colaboradores;
        private Dictionary<string, Administrador> administradores;
        private Dictionary<int, Departamento> departamentos;
        private Dictionary<string, PessoaDeInteresse> pessoasDeInteresse;


        public Instituicao()
        {
            cod_instituicao = -1;
            nome = "";
            email = "";
            contactos = new ArrayList();
            localizacao = "";
            colaboradores = new Dictionary<string, Colaborador>();
            administradores = new Dictionary<string, Administrador>();
            departamentos = new Dictionary<int, Departamento>();
            pessoasDeInteresse = new Dictionary<string, PessoaDeInteresse>();
        }

        public Instituicao(int cod_instituicao, string nome, string email, ArrayList contactos, string localizacao, Dictionary<int, Departamento> departamentos, Dictionary<string, PessoaDeInteresse> pessoasDeInteresse, Dictionary<string, Administrador> administradores, Dictionary<string, Colaborador> colaboradores)
        {
            this.cod_instituicao = cod_instituicao;
            this.nome = nome;
            this.email = email;
            this.contactos = new ArrayList(contactos);
            this.localizacao = localizacao;
            this.colaboradores = new Dictionary<string, Colaborador>(colaboradores);
            this.administradores = new Dictionary<string, Administrador>(administradores);
            this.pessoasDeInteresse = new Dictionary<string, PessoaDeInteresse>(pessoasDeInteresse);
            this.departamentos = new Dictionary<int, Departamento>(departamentos);
        }

        public Instituicao(Instituicao i)
        {
            this.cod_instituicao = i.GetCod_instituicao();
            this.nome = i.GetNome();
            this.email = i.GetEmail();
            this.contactos = new ArrayList(i.GetContactos());
            this.localizacao = i.GetLocalizacao();
            this.colaboradores = i.GetColaboradores();
            this.administradores = i.GetAdministradores();
            this.pessoasDeInteresse = i.GetPessoasDeInteresse();
            this.departamentos = i.GetDepartamentos();
        }

        public string GetLocalizacao()
        {
            return localizacao;
        }

        public void SetLocalizacao(string loc)
        {
            this.localizacao = loc;
        }

        public ICollection GetContactos()
        {
            ArrayList r = new ArrayList(this.contactos);
            return r;
        }

        public string GetEmail(){
          return this.email;
        }

        public void SetEmail(string mail){
          this.email = mail;
        }

        public string GetNome()
        {
            return nome;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public int GetCod_instituicao()
        {
            return cod_instituicao;
        }

        public Dictionary<string, Colaborador> GetColaboradores(){
          return new Dictionary<string, Colaborador>(this.colaboradores);
        }

        public Dictionary<string, Administrador> GetAdministradores(){
          return new Dictionary<string, Administrador>(this.administradores);
        }

        public Dictionary<string, PessoaDeInteresse> GetPessoasDeInteresse(){
            return new Dictionary<string, PessoaDeInteresse>(this.pessoasDeInteresse);
        }

        public Dictionary<int, Departamento> GetDepartamentos(){
          return new Dictionary<int, Departamento>(this.departamentos); ;
        }

        public void SetColaboradores(Dictionary<string,Colaborador> colaboradores)
        {
            this.colaboradores = new Dictionary<string, Colaborador>(colaboradores);
        }

        public void SetAdministrador(Dictionary<string, Administrador> admin)
        {
            this.administradores = new Dictionary<string, Administrador>(admin);
        }

        public void SetPessoaDeInteresse(Dictionary<string, PessoaDeInteresse> pessoa)
        {
            this.pessoasDeInteresse = new Dictionary<string, PessoaDeInteresse>(pessoa);
        }

        public void SetDepartamento(Dictionary<int, Departamento> dep)
        {
            this.departamentos = new Dictionary<int, Departamento>(dep);
        }

        public void SetCod_instituicao(int cod)
        {
            this.cod_instituicao = cod;
        }
    }
}
