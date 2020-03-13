using System;
using System.Collections.Generic;
using System.Text;
using Visitante;

namespace LI4
{
    public class Visita
    {

        private DateTime data;
        private String comentario;
        private bool aceite;
        private int estado;
        private DateTime data_inicio;
        private DateTime data_saida;
        private Visitante visitante;


        public Visita()
        {
            this.data = new DateTime();
            this.comentario = "";
            this.aceite = false;
            this.estado = -1;
            this.data_inicio = new DateTime();
            this.data_saida = new DateTime();
            this.visitante = new Visitante();
            this.visitado = "";
        }


        public Visita(DateTime data, String comentario, bool aceite, int estado, DateTime data_inicio, DateTime data_saida, String attribute, Visitante v, String vis)
        {
            this.data = data;
            this.comentario = comentario;
            this.aceite = aceite;
            this.estado = estado;
            this.data_inicio = data_inicio;
            this.data_saida = data_saida;
            this.visitante = v;
            this.visitado = vis;
        }

        public Visita(Visita v)
        {
            this.data = v.getData();
            this.comentario = v.getComentario();
            this.aceite = v.isAceite();
            this.estado = v.getEstado();
            this.data_inicio = v.getData_inicio();
            this.data_saida = v.getData_saida();
            this.visitante = v.getVisitante();
            this.visitado = v.getVisitado();
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

        public bool isAceite()
        {
            return aceite;
        }

        public void setAceite(bool aceite)
        {
            this.aceite = aceite;
        }

        public int getEstado()
        {
            return estado;
        }

        public void setEstado(int estado)
        {
            this.estado = estado;
        }

        public DateTime getData_inicio()
        {
            return data_inicio;
        }

        public void setData_inicio(DateTime data_inicio)
        {
            this.data_inicio = data_inicio;
        }

        public DateTime getData_saida()
        {
            return data_saida;
        }

        public void setData_saida(DateTime data_saida)
        {
            this.data_saida = data_saida;
        }

        public String getVisitado()
        {
            return visitado;
        }

        public void setVisitado(String visitado)
        {
            this.visitado = visitado;
        }

        public Visitante getVisitante()
        {
          return visitante;
        }

        public void setVisitante(Visitante v)
        {
          this.visitante = visitante;
        }
    }
}
