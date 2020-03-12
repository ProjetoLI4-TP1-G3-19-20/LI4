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

        public PedidoVisita()
        {
            this.data = new DateTime();
            this.comentario = "";
            this.visitado = "";
            this.departamento = -1;
        }

        public PedidoVisita(DateTime data, String comentario, String visitado, int departamento)
        {
            this.data = data;
            this.comentario = comentario;
            this.visitado = visitado;
            this.departamento = departamento;
        }

        public PedidoVisita(PedidoVisita pv)
        {
            this.data = pv.getData();
            this.comentario = pv.getComentario();
            this.visitado = pv.getVisitado();
            this.departamento = pv.getDepartamento();
        }


        public DateTime getData()
        {
            return data;
        }

        public void setData(DateTime data)
        {
            this.data = data;
        }

        public String getComentario()
        {
            return comentario;
        }

        public void setComentario(String comentario)
        {
            this.comentario = comentario;
        }

        public String getVisitado()
        {
            return visitado;
        }

        public void setVisitado(String visitado)
        {
            this.visitado = visitado;
        }

        public int getDepartamento()
        {
            return departamento;
        }

        public void setDepartamento(int departamento)
        {
            this.departamento = departamento;
        }
    }
}
