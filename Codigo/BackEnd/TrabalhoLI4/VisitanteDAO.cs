using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;

public class VisitanteDAO
{

    private string Connection;

    public VisitanteDAO(String con)
    {
        Connection = con;
    }

    public bool Put(Visitante visit)
    {
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`visitante`(`Telemóvel`,`Nome`,`email`,`morada`,`cod_postal`, `password`) VALUES (@tele, @nome, @mail, @morada, @cp, @password)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@tele", visit.GetTelefone());
            mc.Parameters.AddWithValue("@nome", visit.GetNome());
            mc.Parameters.AddWithValue("@mail", visit.GetEmail());
            mc.Parameters.AddWithValue("@morada", visit.GetMorada());
            mc.Parameters.AddWithValue("@cp", visit.GetCod_postal());
            mc.Parameters.AddWithValue("@password", visit.GetPassword());
            mc.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return false;
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
        return true;
    }

    public Boolean emailExiste(string emailEntrada)
    {
        MySqlConnection msc = new MySqlConnection(Connection);
        bool teste = false;
        try
        {
            msc.Open();
            string query = "SELECT id FROM Visitante WHERE Visitante.email=@emailEntrada";

            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Prepare();
            mc.Parameters.AddWithValue("@emailEntrada", emailEntrada);
            MySqlDataReader mr = mc.ExecuteReader();

            while (mr.Read())
            {
                    teste = true;
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

    public Visitante Get(string email)
    {
        Visitante visit = new Visitante();
        MySqlConnection msc = new MySqlConnection(Connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM Visitante WHERE Visitante.email=@email";

            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Prepare();
            mc.Parameters.AddWithValue("@email", email);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                visit.SetEmail(email);
                visit.SetTelefone(mr.GetString("Telemóvel"));
                visit.SetNome(mr.GetString("Nome"));
                visit.SetId_utilizador(mr.GetInt32("id"));
                visit.SetMorada(mr.GetString("morada"));
                visit.SetCod_postal(mr.GetString("cod_postal"));
                visit.SetPassword(mr.GetString("password"));

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


    public Visitante Get(int id) {
        Visitante visit = new Visitante();
        MySqlConnection msc = new MySqlConnection(Connection);
        try {
            msc.Open();
            string query = "SELECT * FROM Visitante WHERE Visitante.id=@id";

            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Prepare();
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read()) {
                visit.SetId_utilizador(id);
                visit.SetTelefone(mr.GetString("Telemóvel"));
                visit.SetNome(mr.GetString("Nome"));
                visit.SetEmail(mr.GetString("Email"));
                visit.SetMorada(mr.GetString("morada"));
                visit.SetCod_postal(mr.GetString("cod_postal"));
                visit.SetPassword(mr.GetString("password"));

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
            string query = "SELECT email FROM Visitante";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                string email = mr.GetString("email");
                visits.Add(this.Get(email));
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

    public string getName(int id) {
        MySqlConnection msc = new MySqlConnection(Connection);
        string name = "";
        try {
            msc.Open();
            string query = "SELECT Nome FROM visitante WHERE id = @id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read()) {
                name = mr.GetString("Nome");
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

        return name;
    }
}