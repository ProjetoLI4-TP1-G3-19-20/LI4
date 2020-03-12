using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Visitante
    {

        private String morada;
        private String cod_postal;

        public Visitante()
        {
            this.morada = "";
            this.cod_postal = "";
        }

        public Visitante(String morada, String cod_postal)
        {
            this.morada = morada;
            this.cod_postal = cod_postal;
        }

        public Visitante(Visitante v)
        {
            this.morada = v.getMorada();
            this.cod_postal = v.getCod_postal();
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
