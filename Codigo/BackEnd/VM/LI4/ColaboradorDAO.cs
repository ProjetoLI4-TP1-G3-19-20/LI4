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
            mc.Parameters.AddWithValue("@mail", col.GetEmail());
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

    public Boolean emailExiste(string emailEntrada)
    {
        MySqlConnection msc = new MySqlConnection(this.connection);
        bool teste = false;
        try
        {
            msc.Open();
            string query = "SELECT id FROM trabalhadores WHERE trabalhadores.email=@emailEntrada";

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


    public Colaborador Get(int id_inst, int id_col)
    {
        Colaborador col = new Colaborador();
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
                col.SetTelefone(mr.GetString("Telemovel"));
                col.SetNome(mr.GetString("Nome"));
                col.SetEmail(mr.GetString("email"));
                col.SetId_utilizador(mr.GetInt32("id_col"));
                mr.Close();

                string query2 = "SELECT * FROM departamentos WHERE id_inst=`id_inst` ";
                MySqlCommand mc2 = new MySqlCommand(query2, msc);
                MySqlDataReader mr2 = mc2.ExecuteReader();

                if (mr2.Read())
                {

                    col.SetDepartamento(mr2.GetInt32("id"));

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
        return col;
    }

    public void Update(Colaborador col, int id_inst, int admin)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`trabalhadores`SET`Nome` = @nome,`Telemovel` = @tele,`email` = @email,`id_inst` = @id_inst,`admin` = @admin,`id_col` = @id  WHERE `id_col` = @id";
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

    public ICollection<Colaborador> GetAll(int id_inst, int id_col, int admin)
    {
        ICollection<Colaborador> col = new HashSet<Colaborador>();
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
                col.Add(this.Get(id_inst, id_col));
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