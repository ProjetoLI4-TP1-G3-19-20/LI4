using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Colaborador
    {

        private String departamento;

        public Colaborador()
        {
            this.departamento = "";
        }

        public Colaborador(String departamento)
        {
            this.departamento = departamento;
        }

        public Colaborador(Colaborador c)
        {
            this.departamento = c.getDepartamento();
        }

        public String getDepartamento()
        {
            return departamento;
        }

        public void setDepartamento(String departamento)
        {
            this.departamento = departamento;
        }
    }
}
