using System;
using System.Collections.Generic;
using System.Text;

namespace LI4
{
    public class PessoaDeInteresse
    {
        private String nome;
        private String email;
        private String password;
        private String phone;
        private int inst;
        private int dep;

        public PessoaDeInteresse()
        {
            this.nome = "";
            this.email = "";
            this.password = "";
        }

        public PessoaDeInteresse(String nome, String email, String password, String phone, int inst, int dep)
        {
            this.nome = nome;
            this.email = email;
            this.password = password;
            this.phone = phone;
            this.inst = inst;
            this.dep = dep; 

        }

        public PessoaDeInteresse(PessoaDeInteresse pessoa)
        {
            this.nome = pessoa.getNome();
            this.email = pessoa.getEmail();
        }


        public void setInst(int inst) {
            this.inst = inst;
        }

        public void setDep(int dep) {
            this.dep = dep;
        }

        public string getPhone() {
            return this.phone;
        }

        public void setPhone(string phone) {
            this.phone = phone;
        }

        public int getInst() {
            return inst;
        }

        public int getDep() {
            return this.dep;
        }

        public String getNome()
        {
            return nome;
        }

        public void setNome(String nome)
        {
            this.nome = nome;
        }

        public String getEmail()
        {
            return email;
        }

        public void setEmail(String email)
        {
            this.email = email;
        }

        public String getPassword() {
            return password;
        }

        public void setPassword(String password) {
            this.password = password;
        }

        public string getJson(string inst, string dep) {
            string r = "";

            r += "{";
            r += "\"nome\" : \"" + this.getNome() + "\",";
            r += "\"email\" : \"" + this.getEmail() + "\",";
            r += "\"telefone\" : \"" + this.getPhone() + "\",";
            r += "\"inst\" : \"" + inst + "\",";
            r += "\"dep\" : \"" + dep + "\",";
            r += "\"password\" : \"" + this.getPassword() + "\"}";

            return r;
        }

    }
}
