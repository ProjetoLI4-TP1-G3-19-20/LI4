using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class VisitanteDAO
{

    private string Connection;

    public VisitanteDAO(String con)
    {
        Connection = con;
    }

    public void Put(Visitante visit)
    {
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`visitante`(`id`,`Telemóvel`,`Nome`,`email`,`morada`,`cod_postal`) VALUES (@id, @tele, @nome, @mail, @morada, @cp)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", visit.GetId_utilizador());
            mc.Parameters.AddWithValue("@tele", visit.GetTelefone());
            mc.Parameters.AddWithValue("@nome", visit.GetNome());
            mc.Parameters.AddWithValue("@mail", visit.GetEmail());
            mc.Parameters.AddWithValue("@morada", visit.GetMorada());
            mc.Parameters.AddWithValue("@cp", visit.GetCod_postal());
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

    public Boolean emailExiste(string emailEntrada, int id)
    {
        MySqlConnection msc = new MySqlConnection(Connection);
        bool teste = false;
        try
        {
            msc.Open();
            string query = "SELECT email FROM Visitante WHERE Visitante.id=@id";

            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Prepare();
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            while (mr.Read() && teste==false)
            {
                string testemail = mr.GetString("email");
                if (testemail == emailEntrada)
                {
                    teste = true;
                }
            }
            mr.Close();
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
        return teste;
    }

    public Visitante Get(int id)
    {
        Visitante visit = new Visitante();
        MySqlConnection msc = new MySqlConnection(Connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM Visitante WHERE Visitante.id=@id";

            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Prepare();
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                visit.SetId_utilizador(id);
                visit.SetTelefone(mr.GetString("Telemóvel"));
                visit.SetNome(mr.GetString("Nome"));
                visit.SetEmail(mr.GetString("email"));
                visit.SetMorada(mr.GetString("morada"));
                visit.SetCod_postal(mr.GetString("cod_postal"));

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
        return visit;
    }

    public void Update(Visitante visit)
    {
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`visitante` SET `id` = @id,`Telemóvel` = @tele,`Nome` = @nome,`email` = @email,`morada` = @morada,`cod_postal` = @cp WHERE `id` = @id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", visit.GetId_utilizador());
            mc.Parameters.AddWithValue("@tele", visit.GetTelefone());
            mc.Parameters.AddWithValue("@nome", visit.GetNome());
            mc.Parameters.AddWithValue("@email", visit.GetEmail());
            mc.Parameters.AddWithValue("@morada", visit.GetMorada());
            mc.Parameters.AddWithValue("@cp", visit.GetCod_postal());
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

    public ICollection<Visitante> GetAll()
    {
        ICollection<Visitante> visits = new HashSet<Visitante>();
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "SELECT id FROM Visitante";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("id");
                visits.Add(this.Get(id));
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
        return visits;
    }
}