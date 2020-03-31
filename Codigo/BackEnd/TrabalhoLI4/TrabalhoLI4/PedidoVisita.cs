using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class PedidoVisita
    {

        private DateTime data;
        private String comentario;
        private String visitado;
        private int departamento;
        private Visitante visitante;

        public PedidoVisita()
        {
            this.data = new DateTime();
            this.comentario = "";
            this.visitado = "";
            this.departamento = -1;
            this.visitante = new Visitante();
        }

        public PedidoVisita(DateTime data, String comentario, String visitado, int departamento, Visitante v)
        {
            this.data = data;
            this.comentario = comentario;
            this.visitado = visitado;
            this.departamento = departamento;
            this.visitante = v;
        }

        public PedidoVisita(PedidoVisita pv)
        {
            this.data = pv.GetData();
            this.comentario = pv.GetComentario();
            this.visitado = pv.GetVisitado();
            this.departamento = pv.GetDepartamento();
            this.visitante = pv.GetVisitante();
        }


        public DateTime GetData()
        {
            return data;
        }

        public void SetData(DateTime data)
        {
            this.data = data;
        }

        public String GetComentario()
        {
            return comentario;
        }

        public void SetComentario(String comentario)
        {
            this.comentario = comentario;
        }

        public String GetVisitado()
        {
            return visitado;
        }

        public void SetVisitado(String visitado)
        {
            this.visitado = visitado;
        }

        public int GetDepartamento()
        {
            return departamento;
        }

        public void SetDepartamento(int departamento)
        {
            this.departamento = departamento;
        }

        public Visitante GetVisitante()
        {
          return visitante;
        }

        public void SetVisitante(Visitante v)
        {
          this.visitante = v;
        }
    }
}
