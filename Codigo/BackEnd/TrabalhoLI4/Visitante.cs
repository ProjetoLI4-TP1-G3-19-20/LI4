using System;
using System.Collections.Generic;
using System.Text;
using LI4;

namespace LI4
{
    public class Visitante : Utilizador
    {

        private String morada;
        private String cod_postal;
        private List<PedidoVisita> pedidosVisita;
        private List<Visita> visitas;

        public Visitante()
        {
            this.morada = "";
            this.cod_postal = "";
            this.nome = "";
            this.email = "";
            this.telefone = "";
            this.password = "";
            this.id_utilizador = -2;
        }

        public string getJson() {
            string r = "";

            r += "{";
            r += "\"morada\" : \"" + this.GetMorada() + "\",";
            r += "\"cod_postal\" : \"" + this.GetCod_postal() + "\",";
            r += "\"nome\" : \"" + this.GetNome() + "\",";
            r += "\"email\" : \"" + this.GetEmail() + "\",";
            r += "\"telefone\" : \"" + this.GetTelefone() + "\",";
            r += "\"password\" : \"" + this.GetPassword() + "\"}";

            return r;
        }

        public Visitante(String morada, String cod_postal, List<Visita> visitas, List<PedidoVisita> pedidosVisita, int id, string nome, string email, string telefone, string pass)
        {
            this.morada = morada;
            this.cod_postal = cod_postal;
            this.visitas = visitas;
            this.pedidosVisita = pedidosVisita;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.password = pass;
            this.id_utilizador = id;
        }


        public Visitante(Visitante v)
        {
            this.morada = v.GetMorada();
            this.cod_postal = v.GetCod_postal();
            this.nome = v.GetNome();
            this.email = v.GetEmail();
            this.password = v.GetPassword();
            this.id_utilizador = v.GetId_utilizador();
            this.telefone = v.GetTelefone();
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

        public String GetMorada()
        {
            return morada;
        }

        public void SetMorada(String morada)
        {
            this.morada = morada;
        }

        public String GetCod_postal()
        {
            return cod_postal;
        }

        public void SetCod_postal(String cod_postal)
        {
            this.cod_postal = cod_postal;
        }

        public override void SetNome(string nome)
        {
            this.nome = nome;
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
    }
}
