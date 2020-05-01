using System;
using System.Collections.Generic;
using System.Text;

namespace LI4 {
    public class PedidoVisita {

        private DateTime hora_inicio;
        private DateTime hora_fim;
        private String comentario;
        private String visitado;
        private int departamento;
        private int instituicao;
        private int visitante;

        public PedidoVisita() {
            this.hora_inicio = new DateTime();
            this.hora_fim = new DateTime();
            this.comentario = "";
            this.visitado = "";
            this.departamento = -1;
            this.instituicao = -1;
            this.visitante = -1;
        }

        public PedidoVisita(DateTime hora_inicio, DateTime hora_fim, String comentario, String visitado, int departamento, int instituicao, int visitante) {
            this.hora_inicio = hora_inicio;
            this.hora_fim = hora_fim;
            this.comentario = comentario;
            this.visitado = visitado;
            this.departamento = departamento;
            this.instituicao = instituicao;
            this.visitante = visitante;
        }

        public DateTime getHoraInicio() {
            return this.hora_inicio;
        }

        public DateTime getHoraFim() {
            return this.hora_fim;
        }

        public String getComentario() {
            return this.comentario;
        }

        public String getVisitado() {
            return this.visitado;
        }

        public int getDepartamento() {
            return this.departamento;
        }

        public int getInstituicao() {
            return this.instituicao;
        }

        public int getVisitante() {
            return this.visitante;
        }

        public void setHoraInicio(DateTime hora_inicio) {
            this.hora_inicio = hora_inicio;
        }

        public void setHoraFim(DateTime hora_fim) {
            this.hora_fim = hora_fim;
        }

        public void setComentario(String comentario) {
            this.comentario = comentario;
        }

        public void setVisitado(String visitado) {
            this.visitado = visitado;
        }

        public void setDepartamento(int departamento) {
            this.departamento = departamento;
        }

        public void setInstituicao(int instituicao) {
            this.instituicao = instituicao;
        }

        public void setVisitante(int visitante) {
            this.visitante = visitante;
        }

        public String getJson() {
            string j = "{";

            j += "\"inicio\": \"" + this.getHoraInicio().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\",";
            j += "\"fim\": \"" + this.getHoraFim().Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\",";
            j += "\"comentario\": \"" + this.getComentario() + "\",";
            j += "\"visitante\": \"" + this.getVisitante() + "\",";
            j += "\"departamento\": \"" + this.getDepartamento() + "\",";
            j += "\"instituicao\": \"" + this.getInstituicao() + "\"";

            j += "}";

            return j;
        }


    }
}
