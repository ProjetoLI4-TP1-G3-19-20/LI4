using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;

public class VisitasDAO
{

    private string Connection;

    public VisitasDAO(String con)
    {
        Connection = con;
    }

    public void Put(Visita visit)
    {
        MySqlConnection msc = new MySqlConnection(Connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`visitas`(`idInstituicao`,`idUser`,`dataInicio`,`dataSaida`,`estado`,`avaliacao`,`comentarios`, `visitado`, `departamentos_id`) VALUES (@idi, @idu, @di, @ds, @estado, @aval, @coment, @visitado, @dep)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@idi", visit.GetId_inst());
            mc.Parameters.AddWithValue("@idu", visit.GetVisitante());
            mc.Parameters.AddWithValue("@di", visit.GetData_inicio());
            mc.Parameters.AddWithValue("@ds", visit.GetData_saida());
            mc.Parameters.AddWithValue("@estado", visit.GetEstado());
            mc.Parameters.AddWithValue("@aval", visit.GetAvaliacao());
            mc.Parameters.AddWithValue("@coment", visit.GetComentario());
            mc.Parameters.AddWithValue("@dep", visit.GetDepartamentoID());
            mc.Parameters.AddWithValue("@visitado", visit.GetVisitado());
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
                visit.SetData_saida(mr.GetDateTime("dataSaida"));
                visit.SetComentario(mr.GetString("comentarios"));
                visit.SetAvaliacao(mr.GetString("avaliacao"));
                visit.SetEstado(mr.GetInt32("estado"));
                visit.setId_inst(mr.GetInt32("idInstituicao"));
                visit.SetDepartamentoID(mr.GetInt32("departamentos_id"));

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


    public void finishVisita(string user, DateTime begin, string aval) {
        MySqlConnection msc = new MySqlConnection(Connection);

        try {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`visitas` SET `estado` = @estado ,`avaliacao` = @ava WHERE`dataInicio` = @datai AND `visitado` = @visitado";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@estado", 1);
            mc.Parameters.AddWithValue("@ava", aval);
            mc.Parameters.AddWithValue("@datai", begin);
            mc.Parameters.AddWithValue("@visitado", user);
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

    public void initVisita(string user, DateTime begin) {
        MySqlConnection msc = new MySqlConnection(Connection);

        try {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`visitas` SET `estado` = @estado WHERE`dataInicio` = @datai AND `visitado` = @visitado";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@estado", 2);
            mc.Parameters.AddWithValue("@datai", begin);
            mc.Parameters.AddWithValue("@visitado", user);
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

    public List<Vaga> getVagas(string nome) {
        List<Vaga> va = new List<Vaga>();

        MySqlConnection msc = new MySqlConnection(Connection);
        try {
            msc.Open();
            string query = "SELECT * FROM pessoadeinteresse_has_vagas piv " +
                "           WHERE piv.nome = @n";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@n", nome);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                Vaga v = new Vaga();
                v.SetHora_fim(mr.GetDateTime("hora_fim"));
                v.SetHora_inicio(mr.GetDateTime("hora_inicio"));
                va.Add(v);
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
        return va;
    }

    public void deleteVaga(string nome, DateTime inicio) {

        MySqlConnection msc = new MySqlConnection(Connection);
        try {
            msc.Open();
            string query = "DELETE FROM pessoadeinteresse_has_vagas piv " +
                           "WHERE piv.nome = @n AND piv.hora_inicio = @d";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@n", nome);
            mc.Parameters.AddWithValue("@d", inicio);
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

    public void putVaga(DateTime inicio, DateTime fim, string nome) {
        MySqlConnection msc = new MySqlConnection(Connection);
        try {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`pessoadeinteresse_has_vagas`(`nome`,`hora_inicio`,`hora_fim`)VALUES(@nome, @inicio, @fim); ";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@nome", nome);
            mc.Parameters.AddWithValue("@inicio", inicio);
            mc.Parameters.AddWithValue("@fim", fim);
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

    public List<Visita> getFutureVisits(String visitado) {
        List<Visita> list = new List<Visita>();

        MySqlConnection msc = new MySqlConnection(Connection);
        try {
            msc.Open();
            string query = "SELECT * FROM Visitas v " +
                "           WHERE (v.estado = 0 OR v.estado = 2) AND v.visitado = @name";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@name", visitado);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                Visita v = new Visita();
                v = this.Get(mr.GetInt32("idUser"), mr.GetInt32("idInstituicao"), mr.GetDateTime("dataInicio"));
                list.Add(v);
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


        return list;
    }
}
