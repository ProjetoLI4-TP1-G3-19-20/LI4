using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class ColaboradorDAO
{

    private string connection;

    public ColaboradorDAO(String con)
    {
        connection = con;
    }

    public void Put(Colaborador col, int id_inst, int admin)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`trabalhadores`(`Nome`,`Telemovel`,`email`,`id_inst`,`admin`,`id`) VALUES (@Nome, @tele, @mail, @id_inst, @admin, @id)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@tele", col.GetTelefone());
            mc.Parameters.AddWithValue("@nome", col.GetNome());
            mc.Parameters.AddWithValue("@email", col.GetEmail());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@admin", admin);
            mc.Parameters.AddWithValue("@id", col.GetId_utilizador());
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

    public Colaborador Get(int id_inst)
    {
        Colaborador col = new Colaborador();
        MySqlConnection msc = new MySqlConnection(connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM Departamento d, Trabalhadores t WHERE t.id_inst=@id and d.id_inst=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id_inst);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                col.SetTelefone(mr.GetString("t.Telemovel"));
                col.SetNome(mr.GetString("t.Nome"));
                col.SetEmail(mr.GetString("t.email"));
                col.SetId_utilizador(mr.GetInt32("t.id"));
                col.SetDepartamento(mr.GetInt32("d.id"));

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
        return col;
    }

    public void Update(Colaborador col, int id_inst, int admin)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "UPDATE Trabalhadores SET Nome=@nome, Telemovel=@tele, e-mail=@mail, id_inst=@id_inst, admin=@admin WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@nome", col.GetNome());
            mc.Parameters.AddWithValue("@tele", col.GetTelefone());
            mc.Parameters.AddWithValue("@email", col.GetEmail());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@admin", admin);
            mc.Parameters.AddWithValue("@id", col.GetId_utilizador());
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

    public ICollection<Colaborador> GetAll(int id_inst)
    {
        ICollection<Colaborador> col = new HashSet<Colaborador>();
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "SELECT id FROM Trabalhadores";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("id");
                col.Add(this.Get(id_inst));
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
        return col;
    }
}