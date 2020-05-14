using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class InstituicaoDAO {

    private string Connection;

    public InstituicaoDAO(String con) {
        Connection = con;
    }

    public bool Put(Instituicao inst) {
        MySqlConnection msc = new MySqlConnection(Connection);

        try {
            msc.Open();
            string query = "SELECT * FROM Instituicao WHERE Nome=@nome";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@nome", inst.GetNome());
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read()) {
                mr.Close();
                msc.Close();
                return false;
            }

            query = "INSERT INTO `trabalholi4`.`instituicao`(`Nome`,`email`,`localizacao`) VALUES (@nome, @mail, @local)";
            mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@nome", inst.GetNome());
            mc.Parameters.AddWithValue("@mail", inst.GetEmail());
            mc.Parameters.AddWithValue("@local", inst.GetLocalizacao());
            mc.ExecuteNonQuery();
        }
        catch (Exception e) {
            Console.WriteLine(e.ToString());
            return false;
        }
        finally {
            try {
                msc.Close();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        return true;
    }

    public Instituicao Get(int id_inst) {
        Instituicao inst = new Instituicao();
        MySqlConnection msc = new MySqlConnection(Connection);
        try {
            msc.Open();
            string query = "SELECT * FROM Instituicao WHERE id_inst=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id_inst);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read()) {
                inst.SetCod_instituicao(id_inst);
                inst.SetNome(mr.GetString("Nome"));
                inst.SetLocalizacao(mr.GetString("localizacao"));
                inst.SetEmail(mr.GetString("email"));

                mr.Close();

                query = "SELECT telemovel FROM Contacto WHERE id_inst = + `id_inst`";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                MySqlDataReader mr1 = mc1.ExecuteReader();

                ArrayList conts = new ArrayList();

                while (mr1.Read()) {
                    conts.Add(mr1.GetString("telemovel"));
                }
                mr1.Close();
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
        return inst;
    }

    public void Update(Instituicao inst) {
        MySqlConnection msc = new MySqlConnection(Connection);

        try {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`instituicao` SET `id_inst` = @id,`Nome` = @nome,`email` = @email,`localizacao` = @local WHERE `id_inst` = @id ";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", inst.GetCod_instituicao());
            mc.Parameters.AddWithValue("@nome", inst.GetNome());
            mc.Parameters.AddWithValue("@email", inst.GetEmail());
            mc.Parameters.AddWithValue("@local", inst.GetLocalizacao());
            mc.ExecuteNonQuery();
            foreach (string contacto in inst.GetContactos()) {
                query = "UPDATE Contactos SET telemovel=@tele WHERE id_inst=@id, @tele";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                mc1.Parameters.AddWithValue("@id", inst.GetCod_instituicao());
                mc1.Parameters.AddWithValue("@tele", contacto);
                mc1.ExecuteNonQuery();
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
    }

    public ICollection<Instituicao> GetAll() {
        ICollection<Instituicao> insts = new HashSet<Instituicao>();
        MySqlConnection msc = new MySqlConnection(Connection);

        try {
            msc.Open();
            string query = "SELECT id_inst FROM Instituicao";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                insts.Add(this.Get(mr.GetInt32("id_inst")));
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
        return insts;
    }


    public List<String> getAllNames() {
        List<String> res = new List<String>();

        MySqlConnection msc = new MySqlConnection(Connection);

        try {
            msc.Open();
            string query = "SELECT Nome FROM instituicao";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                res.Add(mr.GetString("Nome"));
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
        return res;

    }

    public int getIdByName(string nome) {
        MySqlConnection msc = new MySqlConnection(Connection);
        int id = -1;
        try {
            msc.Open();
            string query = "SELECT id_inst FROM instituicao WHERE nome = @nome";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@nome", nome);

            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                id = mr.GetInt32("id_inst");
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
        return id;
    }


    public string getNamebyId(int id) {
        MySqlConnection msc = new MySqlConnection(Connection);
        string r = "";
        try {
            msc.Open();
            string query = "SELECT Nome FROM instituicao WHERE id_inst = @id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id);

            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                r = mr.GetString("Nome");
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
        return r;
    }

}
