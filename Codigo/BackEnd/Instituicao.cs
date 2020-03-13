using System;
using System.Collections;

namespace LI4
{
    public class Instituicao
    {
        private int cod_instituicao;
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
            contactos = new ArrayList();
            localizacao = "";
            colaboradores = new Dictionary<string, Colaborador>();
            administradores = new Dictionary<string, Administrador>();
            departamentos = new Dictionary<int, Departamento>();
            pessoasDeInteresse = new Dictionary<string, PessoaDeInteresse>();
        }

        public Instituicao(int cod_instituicao, string nome, ArrayList contactos, string localizacao, Dictionary<int, Departamento> departamentos, Dictionary<string, PessoaDeInteresse> pessoasDeInteresse, Dictionary<string, Administrador> administradores, Dictionary<string, Colaborador> colaboradores)
        {
            this.cod_instituicao = cod_instituicao;
            this.nome = nome;
            this.contactos = new ArrayList(contactos);
            this.localizacao = localizacao;
            this.colaboradores = new Dictionary<string, Colaborador>(colaboradores);
            this.administradores = new Dictionary<string, Administrador>(administradores);
            this.pessoasDeInteresse = new Dictionary<string, PessoaDeInteresse>(pessoasDeInteresse);
            this.departamentos = new Dictionary<int, Departamento>(departamentos);
        }

        public Instituicao(Instituicao i)
        {
            this.cod_instituicao = i.getCod_instituicao();
            this.nome = i.getNome();
            this.contactos = new ArrayList(i.getContactos());
            this.localizacao = i.getLocalizacao();
            this.colaboradores = i.getColaboradores();
            this.administradores = i.getAdministradores();
            this.pessoasDeInteresse = i.getPessoasDeInteresse();
            this.departamentos = i.getDepartamentos();
        }

        private string getLocalizacao()
        {
            return localizacao;
        }

        private ICollection getContactos()
        {
            ArrayList r = new ArrayList(this.contactos);
            return r;
        }

        private string getNome()
        {
            return nome;
        }

        private int getCod_instituicao()
        {
            return cod_instituicao;
        }

        private Dictionary<string, Colaborador> getColaboradores(){
          return this.colaboradores;
        }

        private Dictionary<string, Administrador> getAdministradores(){
          return this.administradores;
        }

        private Dictionary<string, PessoaDeInteresse> getPessoasDeInteresse(){
          return this.pessoasDeInteresse;
        }

        private Dictionary<int, Departamento> getDepartamentos(){
          return this.departamentos;
        }

    }
}
