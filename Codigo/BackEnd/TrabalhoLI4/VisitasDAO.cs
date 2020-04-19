using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class VisitasDAO
{

    private string Connection;

    public VisitasDAO(String con)
    {
        Connection = con;
    }

    public void Put(Visita visit, int id_inst)
    {
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`visitas`(`idInstituicao`,`idUser`,`dataInicio`,`dataSaida`,`estado`,`avaliacao`,`comentarios`) VALUES (@idi, @idu, @di, @ds, @estado, @aval, @coment)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@idi", id_inst);
            mc.Parameters.AddWithValue("@idu", visit.GetVisitante());
            mc.Parameters.AddWithValue("@di", visit.GetData_inicio());
            mc.Parameters.AddWithValue("@ds", visit.GetData_saida());
            mc.Parameters.AddWithValue("@estado", visit.GetEstado());
            mc.Parameters.AddWithValue("@aval", visit.GetAvaliacao());
            mc.Parameters.AddWithValue("@coment", visit.GetComentario());
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

    public Visita Get(int id_visit, int id_inst, DateTime data_inicio)
    {
        Visita visit = new Visita();
        MySqlConnection msc = new MySqlConnection(Connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM Visitas WHERE idInstituicao = @id_inst and idUser= @id_user and dataInicio= @di";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@id_user", id_visit);
            mc.Parameters.AddWithValue("@di", data_inicio);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                visit.SetVisitado(mr.GetString("visitado"));
                visit.SetVisitante(id_visit);
                visit.SetData_inicio(data_inicio);
                visit.SetData(data_inicio);
                visit.SetData_saida(mr.GetDateTime("dataSaida"));
                visit.SetComentario(mr.GetString("comentarios"));
                visit.SetAvaliacao(mr.GetString("avaliacao"));
                visit.SetEstado(mr.GetInt32("estado"));
                visit.setId_inst(mr.GetInt32("idInstituicao"));
                visit.SetDepartamentoID(mr.GetInt32("departamentosID"));

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

    public void Update(Visita visit, int id_inst,int iduser)
    {
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`visitas` SET `idInstituicao` = @id_inst, `idUser` = @idu ,`dataInicio` = @datai ,`dataSaida` = @datas ,`estado` = @estado ,`avaliacao` = @ava ,`comentarios` = @comentarios WHERE `idInstituicao` = @id_inst AND `idUser` = @idu AND `dataInicio` = @datai";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@idu", visit.GetVisitante());
            mc.Parameters.AddWithValue("@datai", visit.GetData_inicio());
            mc.Parameters.AddWithValue("@datas", visit.GetData_saida());
            mc.Parameters.AddWithValue("@estado", visit.GetEstado());
            mc.Parameters.AddWithValue("@ava", visit.GetAvaliacao());
            mc.Parameters.AddWithValue("@comentarios", visit.GetComentario());
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

    public List<Visita> GetAllByUser(int id_user)
    {
        List<Visita> visits = new List<Visita>();
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "SELECT * FROM Visitas WHERE idUser=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id_user);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("idUser");
                int id_inst = mr.GetInt32("idInstituicao");
                DateTime data_inicio = mr.GetDateTime("dataInicio");
                visits.Add(this.Get(id, id_inst, data_inicio));
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


    public ICollection<Visita> GetAll(int id_user, int id_inst, DateTime data_inicio)
    {
        ICollection<Visita> visits = new HashSet<Visita>();
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "SELECT * FROM Visitas WHERE idInstituicao=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id_user);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("id");
                visits.Add(this.Get(id, id_inst, data_inicio));
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