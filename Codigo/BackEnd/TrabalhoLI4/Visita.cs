using System;
using System.Collections.Generic;
using System.Text;
using Visitante;

namespace LI4
{
    public class Visita
    {
        private DateTime data;
        private string comentario;
        private bool aceite;
        private int estado;
        private DateTime data_inicio;
        private DateTime data_saida;
        private int visitante;
        private string avaliacao;
        private string comentarios;


        public Visita()
        {
            this.data = new DateTime();
            this.comentario = "";
            this.aceite = false;
            this.estado = -1;
            this.data_inicio = new DateTime();
            this.data_saida = new DateTime();
            this.visitante = -1;
            this.visitado = "";
            this.comentarios = "";
            this.avaliacao = "";
        }


        public Visita(DateTime data, string comentario, bool aceite, int estado, DateTime data_inicio, DateTime data_saida, string attribute, int v, string vis, string coment, string aval)
        {
            this.data = data;
            this.comentario = comentario;
            this.aceite = aceite;
            this.estado = estado;
            this.data_inicio = data_inicio;
            this.data_saida = data_saida;
            this.visitante = v;
            this.visitado = vis;
            this.comentarios = coment;
            this.avaliacao = aval;
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
            this.avaliacao = v.getAvaliacao();
            this.comentarios = v.getComentario();
        }

        public string getAvaliacao()
        {
          return this.avaliacao;
        }

        public string getComentarios()
        {
          return this.comentarios
        }

        public void setComentario(string c)
        {
          this.comentarios = c;
        }

        public void setAvaliacao(string c)
        {
          this.avaliacao = c;
        }


        public DateTime getData()
        {
            return data;
        }

        public void setData(DateTime data)
        {
            this.data = data;
        }

        public string getComentario()
        {
            return comentario;
        }

        public void setComentario(string comentario)
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

        public string getVisitado()
        {
            return visitado;
        }

        public void setVisitado(string visitado)
        {
            this.visitado = visitado;
        }

        public int getVisitante()
        {
          return visitante;
        }

        public void setVisitante(int v)
        {
          this.visitante = visitante;
        }
    }
}
