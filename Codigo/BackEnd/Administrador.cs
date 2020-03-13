using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class Administrador : Utilizador
    {

        private int departamento;

        public Administrador()
        {
            this.departamento = -1;
        }

        public Administrador(int departamento)
        {
            this.departamento = departamento;
        }

        public Administrador(Administrador a)
        {
            this.departamento = a.getDepartamento();
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
