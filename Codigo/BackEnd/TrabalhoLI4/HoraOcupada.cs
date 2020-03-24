using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{

    public class HoraOcupada
    {
        private DateTime hora_inicio;
        private DateTime hora_fim;

        public HoraOcupada()
        {
            this.hora_inicio = new DateTime();
            this.hora_fim = new DateTime();
        }

        public HoraOcupada(DateTime hora_inicio, DateTime hora_fim)
        {
            this.hora_inicio = hora_inicio;
            this.hora_fim = hora_fim;
        }

        public HoraOcupada(HoraOcupada ocupada)
        {
            this.hora_inicio = ocupada.getHora_inicio();
            this.hora_fim = ocupada.getHora_fim();
        }

        public DateTime getHora_inicio()
        {
            return hora_inicio;
        }

        public void setHora_inicio(DateTime hora_inicio)
        {
            this.hora_inicio = hora_inicio;
        }

        public DateTime getHora_fim()
        {
            return hora_fim;
        }

        public void setHora_fim(DateTime hora_fim)
        {
            this.hora_fim = hora_fim;
        }
    }
}
