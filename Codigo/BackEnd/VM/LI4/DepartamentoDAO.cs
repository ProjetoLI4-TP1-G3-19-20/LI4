using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;


public class DepartamentoDAO
{

    private string connection;

    public DepartamentoDAO(String con)
    {
        connection = con;
    }

    public bool Put(Departamento dep, int id_inst) {
        MySqlConnection msc = new MySqlConnection(connection);

        try {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`departamentos`(`id_inst`,`nome`) VALUES (@id_inst, @nome)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
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

    public Departamento Get(int id) {
        Departamento dep = new Departamento();
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT * FROM departamentos WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read()) {
                dep.SetID(id);
                dep.SetNome(mr.GetString("nome"));
                mr.Close();
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
        return dep;
    }

    public void Update(Departamento dep, int id_inst) {
        MySqlConnection msc = new MySqlConnection(connection);

        try {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`departamentos` SET `id` = @id,`id_inst` = @id_inst,`nome` = @nome WHERE `id` =@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            mc.ExecuteNonQuery();     
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

    public ICollection<Departamento> GetAll()
    {
        ICollection<Departamento> dep = new HashSet<Departamento>();
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "SELECT id FROM departamentos";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("id");
                dep.Add(this.Get(id));
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            try
            {
                msc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        return dep;
    }

    public string getNameById(int id_inst, int id_dep) {
        string dep = "departamentoDefault";
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT nome FROM departamentos WHERE id = @id_dep AND id_inst = @id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@id_dep", id_dep);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                dep = mr.GetString("nome");
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
        return dep;
    }


    public int getIdByName(string name, int id_inst) {
        int dep = -1;
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT id FROM departamentos WHERE nome = @name AND id_inst = @id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@name", name);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                dep = mr.GetInt32("id");
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
        return dep;
    }

    public List<string> DepByInst(string inst) {
        List<string> deps = new List<string>();
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT d.nome FROM departamentos d, instituicao i WHERE i.Nome = @inst AND d.id_inst = i.id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@inst", inst);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                deps.Add(mr.GetString("nome"));
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
        return deps;
    }

    public List<String> getPessoasByDepartamento(string inst, string dep){
        List<string> pessoas = new List<string>();
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT p.nome FROM departamentos d, instituicao i, pessoadeinteresse p WHERE i.Nome = @inst AND d.nome = @dep AND i.id_inst = p.departamentos_id_inst AND d.id = p.departamentos_id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@inst", inst);
            mc.Parameters.AddWithValue("@dep", dep);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                pessoas.Add(mr.GetString("nome"));
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
        return pessoas;
    }

    public bool existeNome(string nome, int inst) {
        bool reply = false;

        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT id FROM departamentos WHERE nome = @name AND id_inst = @id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", inst);
            mc.Parameters.AddWithValue("@name", nome);
            MySqlDataReader mr = mc.ExecuteReader();
            if (mr.Read()) {
                reply = true;
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

        return reply;
    }
}
