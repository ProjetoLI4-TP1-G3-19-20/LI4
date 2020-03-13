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
        private List<PedidoVisita> pedidosVisita;
        private List<Visita> visitas;

        public Visitante()
        {
            super();
            this.morada = "";
            this.cod_postal = "";
            this.visitas = new List<Visita>;
            this.pedidosVisita = new List<PedidoVisita>;
        }

        public Visitante(String nome, String email, String password, String telefone, int id, String morada, String cod_postal, List<Visita> visitas, List<PedidoVisita> pedidosVisita)
        {
            super(nome, email, password, telefone, id);
            this.morada = morada;
            this.cod_postal = cod_postal;
            this.visitas = visitas;
            this.pedidosVisita = pedidosVisita
        }

        public Visitante(Visitante v)
        {
            super(v.getNome, v.getEmail, v.getPassword, v.getTelefone, v.getId_utilizador);
            this.morada = v.getMorada();
            this.cod_postal = v.getCod_postal();
            this.visitas = v.getVisitas();
            this.pedidosVisita = v.getPedidosVisita();
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

        public List<PedidoVisita> getPedidosVisita()
        {
          return pedidosVisita;
        }

        public void setPedidosVisita(List<PedidoVisita> pv)
        {
          this.pedidosVisita = pv;
        }

        public List<Visita> getVisitas()
        {
          return visitas;
        }

        public void setPedidosVisita(List<Visita> v)
        {
          this.visitas = v;
        }

    }
}
