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
        private Dictionary<int, Colaborador> colaboradores;
        private Dictionary<int, Administrador> administradores;
        private Dictionary<int, Departamento> departamentos;
        private Dictionary<string, PessoaDeInteresse> pessoasDeInteresse;


        public Instituicao()
        {
            cod_instituicao = -1;
            nome = "";
            email = "";
            contactos = new ArrayList();
            localizacao = "";
            colaboradores = new Dictionary<int, Colaborador>();
            administradores = new Dictionary<int, Administrador>();
            departamentos = new Dictionary<int, Departamento>();
            pessoasDeInteresse = new Dictionary<string, PessoaDeInteresse>();
        }

        public Instituicao(int cod_instituicao, string nome, string email, ArrayList contactos, string localizacao, Dictionary<int, Departamento> departamentos, Dictionary<string, PessoaDeInteresse> pessoasDeInteresse, Dictionary<int, Administrador> administradores, Dictionary<int, Colaborador> colaboradores)
        {
            this.cod_instituicao = cod_instituicao;
            this.nome = nome;
            this.email = email;
            this.contactos = new ArrayList(contactos);
            this.localizacao = localizacao;
            this.colaboradores = new Dictionary<int, Colaborador>(colaboradores);
            this.administradores = new Dictionary<int, Administrador>(administradores);
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

        public void SetContactos(ArrayList conts){
          this.contactos = new ArrayList(conts);
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

        public Dictionary<int, Colaborador> GetColaboradores(){
          return new Dictionary<int, Colaborador>(this.colaboradores);
        }

        public Dictionary<int, Administrador> GetAdministradores(){
          return new Dictionary<int, Administrador>(this.administradores);
        }

        public Dictionary<string, PessoaDeInteresse> GetPessoasDeInteresse(){
            return new Dictionary<string, PessoaDeInteresse>(this.pessoasDeInteresse);
        }

        public Dictionary<int, Departamento> GetDepartamentos(){
          return new Dictionary<int, Departamento>(this.departamentos); ;
        }

        public void SetColaboradores(Dictionary<int,Colaborador> colaboradores)
        {
            this.colaboradores = new Dictionary<int, Colaborador>(colaboradores);
        }

        public void SetAdministrador(Dictionary<int, Administrador> admin)
        {
            this.administradores = new Dictionary<int, Administrador>(admin);
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
