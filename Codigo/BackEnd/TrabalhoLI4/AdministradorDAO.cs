using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class AdministradorDAO
{

    private string connection;

    public AdministradorDAO(String con)
    {
        connection = con;
    }

    public void Put(Administrador ad, int id_inst, int admin)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`trabalhadores`(`Nome`,`Telemovel`,`email`,`id_inst`,`admin`,`id`) VALUES (@Nome, @tele, @mail, @id_inst, @admin, @id)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@tele", ad.GetTelefone());
            mc.Parameters.AddWithValue("@nome", ad.GetNome());
            mc.Parameters.AddWithValue("@email", ad.GetEmail());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@admin", admin);
            mc.Parameters.AddWithValue("@id", ad.GetId_utilizador());
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

    public Administrador Get(int id_inst)
    {
        Administrador ad = new Administrador();
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
                ad.SetTelefone(mr.GetString("t.Telemovel"));
                ad.SetNome(mr.GetString("t.Nome"));
                ad.SetEmail(mr.GetString("t.email"));
                ad.SetId_utilizador(mr.GetInt32("t.id"));
                ad.SetDepartamento(mr.GetInt32("d.id"));

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
        return ad;
    }

    public void Update(Administrador ad, int id_inst, int admin)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "UPDATE Trabalhadores SET nome=@nome, Telemóvel=@tele, e-mail=@mail, id_inst=@id_inst, admin=@admin WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@nome", ad.GetNome());
            mc.Parameters.AddWithValue("@tele", ad.GetTelefone());
            mc.Parameters.AddWithValue("@email", ad.GetEmail());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@admin", admin);
            mc.Parameters.AddWithValue("@admin", ad.GetId_utilizador());
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

    public ICollection<Administrador> GetAll(int id_inst)
    {
        ICollection<Administrador> ad = new HashSet<Administrador>();
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
                ad.Add(this.Get(id_inst));
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
        return ad;
    }
}