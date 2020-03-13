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

        }

        public Instituicao(int cod_instituicao, string nome, ArrayList contactos, string localizacao)
        {
            this.cod_instituicao = cod_instituicao;
            this.nome = nome;
            this.contactos = new ArrayList(contactos);
            this.localizacao = localizacao;
        }

        public Instituicao(Instituicao i)
        {
            this.cod_instituicao = i.getCod_instituicao();
            this.nome = i.getNome();
            this.contactos = new ArrayList(i.getContactos());
            this.localizacao = i.getLocalizacao();
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

    }
}
