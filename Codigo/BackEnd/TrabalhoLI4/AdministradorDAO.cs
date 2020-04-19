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

    public bool Put(Administrador ad, int id_inst, int admin)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`trabalhadores`(`Nome`,`Telemovel`,`email`,`id_inst`,`admin`, `password`) VALUES (@Nome, @tele, @mail, @id_inst, @admin, @password)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@tele", ad.GetTelefone());
            mc.Parameters.AddWithValue("@nome", ad.GetNome());
            mc.Parameters.AddWithValue("@mail", ad.GetEmail());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@admin", admin);
            mc.Parameters.AddWithValue("@password", ad.GetPassword());
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
        MySqlConnection msc = new MySqlConnection(this.connection);
        bool teste = false;
        try
        {
            msc.Open();
            string query = "SELECT id_col FROM trabalhadores WHERE trabalhadores.email=@emailEntrada";

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


    public Administrador Get(int id_inst, int id_col)
    {
        Administrador ad = new Administrador();
        MySqlConnection msc = new MySqlConnection(connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM trabalhadores WHERE id_inst=@id and id_col = @id2 ";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id_inst);
            mc.Parameters.AddWithValue("@id2", id_col);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                ad.SetTelefone(mr.GetString("Telemovel"));
                ad.SetNome(mr.GetString("Nome"));
                ad.SetEmail(mr.GetString("email"));
                ad.SetId_utilizador(mr.GetInt32("id_col"));
                mr.Close();

                string query2 = "SELECT * FROM departamentos WHERE id_inst=`id_inst` ";
                MySqlCommand mc2 = new MySqlCommand(query2, msc);
                MySqlDataReader mr2 = mc2.ExecuteReader();

                if (mr2.Read())
                {

                    ad.SetDepartamento(mr2.GetInt32("id"));

                    mr2.Close();
                }
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
            string query = "UPDATE `trabalholi4`.`trabalhadores`SET`Nome` = @nome,`Telemovel` = @tele,`email` = @email,`id_inst` = @id_inst,`admin` = @admin,`id_col` = @id  WHERE `id_col` = @id";
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

    public ICollection<Administrador> GetAll(int id_inst, int id_col,int admin)
    {
        ICollection<Administrador> ad = new HashSet<Administrador>();
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "SELECT id FROM Trabalhadores WHERE admin = @valor";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@valor", admin);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("id");
                ad.Add(this.Get(id_inst,id_col));
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