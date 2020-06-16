using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LI4 {
    class PessoaDeInteresseDAO {
        private string Connection;

        public PessoaDeInteresseDAO(String con) {
            Connection = con;
        }

        public bool Put(PessoaDeInteresse pdi) {
            bool worked = false;

            MySqlConnection msc = new MySqlConnection(Connection);

            try {
                msc.Open();
                string query = "INSERT INTO `trabalholi4`.`pessoadeinteresse` (`nome`,`email`,`departamentos_id`,`departamentos_id_inst`,`password`,`phone`)" +
                               "VALUES(@nome, @email, @dep, @inst, @pass, @phone)";

                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@nome", pdi.getNome());
                mc.Parameters.AddWithValue("@email", pdi.getEmail());
                mc.Parameters.AddWithValue("@dep", pdi.getDep());
                mc.Parameters.AddWithValue("@inst", pdi.getInst());
                mc.Parameters.AddWithValue("@pass", pdi.getPassword());
                mc.Parameters.AddWithValue("@phone", pdi.getPhone());
                mc.ExecuteNonQuery();
                worked = true;
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                throw e;
            }
            finally {
                try {
                    msc.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(e.ToString());
                }
            }

            return worked;
        }

        public PessoaDeInteresse GetByEmail(string email) {

            PessoaDeInteresse p = new PessoaDeInteresse();
            MySqlConnection msc = new MySqlConnection(Connection);
            try {
                msc.Open();
                string query = "SELECT * FROM pessoadeinteresse WHERE email = @email";

                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Prepare();
                mc.Parameters.AddWithValue("@email", email);
                MySqlDataReader mr = mc.ExecuteReader();

                if (mr.Read()) {
                    p.setNome(mr.GetString("nome"));
                    p.setEmail(mr.GetString("email"));
                    p.setPassword(mr.GetString("password"));
                }
                mr.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            finally {
                try {
                    msc.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(e.ToString());
                }
            }

            return p;
        }

        public Boolean emailExiste(string emailEntrada) {
            MySqlConnection msc = new MySqlConnection(Connection);
            bool teste = false;
            try {
                msc.Open();
                string query = "SELECT * FROM pessoadeinteresse pdi WHERE pdi.email=@emailEntrada";

                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Prepare();
                mc.Parameters.AddWithValue("@emailEntrada", emailEntrada);
                MySqlDataReader mr = mc.ExecuteReader();

                while (mr.Read()) {
                    teste = true;
                }
                mr.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            finally {
                try {
                    msc.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(e.ToString());
                }
            }
            return teste;
        }

    }
}
