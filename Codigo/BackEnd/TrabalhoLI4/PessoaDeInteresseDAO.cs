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

    }
}
