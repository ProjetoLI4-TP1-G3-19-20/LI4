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

        public bool Update(PessoaDeInteresse pdi, string nomeOriginal) {
            bool worked = false;

            MySqlConnection msc = new MySqlConnection(Connection);

            if(pdi.getPassword().CompareTo("") != 0) {
                try {
                    msc.Open();
                    string query = "UPDATE `trabalholi4`.`pessoadeinteresse` SET `nome` = @nome, `email` = @email,`departamentos_id` = @dep, `departamentos_id_inst` = @inst, `password` = @pass, `phone` = @phone WHERE `nome` = @nome2";

                    MySqlCommand mc = new MySqlCommand(query, msc);
                    mc.Parameters.AddWithValue("@nome2", nomeOriginal);
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
            else {
                try {
                    msc.Open();
                    string query = "UPDATE `trabalholi4`.`pessoadeinteresse` SET `nome` = @nome, `email` = @email,`departamentos_id` = @dep, `departamentos_id_inst` = @inst, `phone` = @phone WHERE `nome` = @nome2";
                    MySqlCommand mc = new MySqlCommand(query, msc);
                    mc.Parameters.AddWithValue("@nome2", nomeOriginal);
                    mc.Parameters.AddWithValue("@nome", pdi.getNome());
                    mc.Parameters.AddWithValue("@email", pdi.getEmail());
                    mc.Parameters.AddWithValue("@dep", pdi.getDep());
                    mc.Parameters.AddWithValue("@inst", pdi.getInst());
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

            
        }

        public PessoaDeInteresse Get(string nome) {

            PessoaDeInteresse p = new PessoaDeInteresse();
            MySqlConnection msc = new MySqlConnection(Connection);
            try {
                msc.Open();
                string query = "SELECT * FROM pessoadeinteresse WHERE nome = @nome";

                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Prepare();
                mc.Parameters.AddWithValue("@nome", nome);
                MySqlDataReader mr = mc.ExecuteReader();

                if (mr.Read()) {
                    p.setNome(mr.GetString("nome"));
                    p.setEmail(mr.GetString("email"));
                    p.setPassword(mr.GetString("password"));
                    p.setInst(mr.GetInt32("departamentos_id_inst"));
                    p.setDep(mr.GetInt32("departamentos_id"));
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


        public string getNextVisit(string nome) {
            MySqlConnection msc = new MySqlConnection(Connection);
            string str = "";
            try {
                msc.Open();
                string query = "SELECT * FROM visitas WHERE visitado = @nome AND estado = 0 ORDER BY dataInicio;";
                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@nome", nome);
                MySqlDataReader mr = mc.ExecuteReader();

                if (mr.Read()) {
                    str = "{\"visitante\" : \"" + mr.GetString("idUser") + "\", \"data\" : \"" + mr.GetDateTime("dataInicio").Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "\"}";
                }
                else {
                    str = "{\"visitante\" : \"-1\"}";
                }
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

            return str;
        }

        public string getNumberOfRequests(string nome) {
            MySqlConnection msc = new MySqlConnection(Connection);
            string str = "";
            try {
                msc.Open();
                string query = "SELECT COUNT(*) FROM pedidovisita WHERE visitado = @nome";
                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@nome", nome);
                MySqlDataReader mr = mc.ExecuteReader();

                if (mr.Read()) {
                    str = mr.GetInt32("COUNT(*)").ToString();
                }
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

            return str;
        }

    }
}
