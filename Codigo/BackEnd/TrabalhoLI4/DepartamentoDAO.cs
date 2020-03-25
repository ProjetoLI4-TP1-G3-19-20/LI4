using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class DepartamentoDAO
{

    private string connection;

    public DepartamentoDAO(String con)
    {
        connection = con;
    }

    public void Put(Departamento dep,int id_inst)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO Visitante (id, id_inst, nome) VALUES (@id, @id_inst, @nome)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            
            mc.ExecuteNonQuery();
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
    }

    public Departamento Get(int id)
    {
        Departamento dep = new Departamento();
        MySqlConnection msc = new MySqlConnection(connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM departamentos WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                dep.SetID(id);
                dep.SetNome(mr.GetString("nome"));
                mr.Close();
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

    public void Update(Departamento dep,int id_inst)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "UPDATE departamentos SET id_inst=@id_inst, nome=@nome WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            mc.ExecuteNonQuery();
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
}