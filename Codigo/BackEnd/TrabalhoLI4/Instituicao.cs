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
        private ColaboradorDAO colaboradores;
        private AdministradorDAO administradores;
        private DepartamentoDAO departamentos;
        private VisitasDAO visitas;
        private PedidoVisitaDAO pedidosVisita;


        public Instituicao(int i)
        {
            cod_instituicao = i;
            nome = "";
            email = "";
            contactos = new ArrayList();
            localizacao = "";
            colaboradores = new ColaboradorDAO();
            administradores = new AdministradorDAO();
            departamentos = new DepartamentoDAO(i);
            visitas = new VisitasDAO();
            pedidosVisita = new PedidoVisitaDAO();
        }

        public Instituicao(int cod_instituicao, string nome, string email, ArrayList contactos, string localizacao)
        {
            this.cod_instituicao = cod_instituicao;
            this.nome = nome;
            this.email = email;
            this.contactos = new ArrayList(contactos);
            this.localizacao = localizacao;
            this.colaboradores = new ColaboradorDAO();
            this.administradores = new AdministradorDAO();
            this.departamentos = new DepartamentoDAO(cod_instituicao);
            this.visitas = new VisitasDAO();
            this.pedidosVisita = new PedidoVisitaDAO();
        }

        public Instituicao(Instituicao i)
        {
            this.cod_instituicao = i.GetCod_instituicao();
            this.nome = i.GetNome();
            this.email = i.GetEmail();
            this.contactos = new ArrayList(i.GetContactos());
            this.localizacao = i.GetLocalizacao();
            this.colaboradores = new ColaboradorDAO();
            this.administradores = new AdministradorDAO();
            this.departamentos = new DepartamentoDAO(i.GetCod_instituicao());
            this.visitas = new VisitasDAO();
            this.pedidosVisita = new PedidoVisitaDAO();
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

        public ColaboradorDAO GetColaboradores(){
          return this.colaboradores;
        }

        public AdministradorDAO GetAdministradores(){
          return new AdministradorDAO(this.administradores);
        }

        public DepartamentoDAO GetDepartamentos(){
          return new DepartamentoDAO(this.departamentos); ;
        }

        public void SetColaboradores(ColaboradorDAO colaboradores)
        {
            this.colaboradores = colaboradores;
        }

        public void SetAdministrador(AdministradorDAO admin)
        {
            this.administradores = admin;
        }

        public void SetDepartamento(DepartamentoDAO dep)
        {
            this.departamentos = dep;
        }

        public void SetCod_instituicao(int cod)
        {
            this.cod_instituicao = cod;
        }
    }
}
