using System;
using System.Collections.Generic;
using System.Text;
using Visita;
using PedidoVisita;

namespace LI4
{
    public class Visitante
    {

        private String morada;
        private String cod_postal;
        private PedidoVisitaDAO pedidosVisita;
        private VisitasDAO visitas;

        public Visitante()
        {
            super();
            this.morada = "";
            this.cod_postal = "";
            this.visitas = new VisitasDAO();
            this.pedidosVisita = new PedidoVisitaDAO();
        }

        public Visitante(String nome, String email, String password, String telefone, int id, String morada, String cod_postal, List<Visita> visitas, List<PedidoVisita> pedidosVisita)
        {
            super(nome, email, password, telefone, id);
            this.morada = morada;
            this.cod_postal = cod_postal;
            this.visitas = new VisitasDAO();
            this.pedidosVisita = new PedidoVisitaDAO();
        }

        public Visitante(Visitante v)
        {
            super(v.getNome, v.getEmail, v.getPassword, v.getTelefone, v.getId_utilizador);
            this.morada = v.getMorada();
            this.cod_postal = v.getCod_postal();
            this.visitas = new VisitasDAO();
            this.pedidosVisita = new PedidoVisitaDAO();
        }

        public override string getNome()
        {
          return this.nome;
        }

        public override string getEmail()
        {
          return this.email;
        }

        public override string getTelefone()
        {
          return this.telefone;
        }

        public override string getPassword()
        {
          return this.password;
        }

        public override int getId_utilizador()
        {
          return this.id_utilizador;
        }

        public override void setId_utilizador(int i)
        {
          return this.id_utilizador;
        }

        public String getMorada()
        {
            return morada;
        }

        public void setMorada(String morada)
        {
            this.morada = morada;
        }

        public String getCod_postal()
        {
            return cod_postal;
        }

        public void setCod_postal(String cod_postal)
        {
            this.cod_postal = cod_postal;
        }
    }
}
