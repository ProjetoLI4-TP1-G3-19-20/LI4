using System;
using System.Collections.Generic;
using System.Text;
using LI4;
using Newtonsoft.Json;

namespace LI4
{
    public class Visita
    {
        private string comentario;
        private int estado;
        private DateTime data_inicio;
        private DateTime data_saida;
        private int visitante;
        private string visitado;
        private string avaliacao;
        private int id_inst;
        private int departamentoID;


        public Visita()
        {
            this.comentario = "";
            this.estado = -1;
            this.data_inicio = new DateTime();
            this.data_saida = new DateTime();
            this.visitante = -1;
            this.visitado = "";
            this.avaliacao = "";
            this.id_inst = -1;
            this.departamentoID = -1;
        }


        public Visita(string comentario, int estado, DateTime data_inicio, DateTime data_saida, int v, string vis, string aval, int id_inst, int dep)
        {
            this.comentario = comentario;
            this.estado = estado;
            this.data_inicio = data_inicio;
            this.data_saida = data_saida;
            this.visitante = v;
            this.visitado = vis;
            this.avaliacao = aval;
            this.estado = estado;
            this.id_inst = id_inst;
            this.departamentoID = dep;
        }

        public int GetDepartamentoID() {
            return departamentoID;
        }

        public void SetDepartamentoID(int id) {
            this.departamentoID = id;
        }


        public string GetAvaliacao()
        {
            return this.avaliacao;
        }

        public int GetId_inst() {
            return id_inst;
        }

        public void setId_inst(int id_inst) {
            this.id_inst = id_inst;
        }

        public void SetComentario(string c)
        {
            this.comentario = c;
        }

        public void SetAvaliacao(string c)
        {
            this.avaliacao = c;
        }


        public string GetComentario()
        {
            return comentario;
        }

        public int GetEstado()
        {
            return estado;
        }

        public void SetEstado(int estado)
        {
            this.estado = estado;
        }

        public DateTime GetData_inicio()
        {
            return data_inicio;
        }

        public void SetData_inicio(DateTime data_inicio)
        {
            this.data_inicio = data_inicio;
        }

        public DateTime GetData_saida()
        {
            return data_saida;
        }

        public void SetData_saida(DateTime data_saida)
        {
            this.data_saida = data_saida;
        }

        public string GetVisitado()
        {
            return visitado;
        }

        public void SetVisitado(string visitado)
        {
            this.visitado = visitado;
        }

        public int GetVisitante()
        {
            return visitante;
        }

        public void SetVisitante(int v)
        {
            this.visitante = v;
        }

        public string getJson(string nameInst, string nameDep) {
            string r = "";

            r += "{";
            r += "\"data_inicio\" : \"" + this.GetData_inicio() + "\",";
            r += "\"comentario\" : \"" + this.GetComentario() + "\",";
            r += "\"data_saida\" : \"" + this.GetData_saida() + "\",";
            r += "\"visitante\" : \"" + this.GetVisitante() + "\",";
            r += "\"visitado\" : \"" + this.GetVisitado() + "\",";
            r += "\"id_inst\" : \"" + nameInst + "\",";
            r += "\"departamentosID\" : \"" + nameDep + "\",";
            r += "\"avaliacao\" : \"" + this.GetAvaliacao() + "\"}";

            return r;
        }

        public string getJsonTimeStamp(string nameInst, string nameDep) {
            string r = "";

            r += "{";
            r += "\"data_inicio\" : \"" + this.GetData_inicio().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\",";
            r += "\"comentario\" : \"" + this.GetComentario() + "\",";
            r += "\"data_saida\" : \"" + this.GetData_saida().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\",";
            r += "\"visitante\" : \"" + this.GetVisitante() + "\",";
            r += "\"visitado\" : \"" + this.GetVisitado() + "\",";
            r += "\"id_inst\" : \"" + nameInst + "\",";
            r += "\"departamentosID\" : \"" + nameDep + "\",";
            r += "\"avaliacao\" : \"" + this.GetAvaliacao() + "\"}";

            return r;
        }

    }
}