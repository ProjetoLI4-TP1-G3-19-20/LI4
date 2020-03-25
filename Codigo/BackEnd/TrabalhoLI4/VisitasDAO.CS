using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class VisitasDAO{

    private string Connection;

    public VisitasDAO(String con){
      Connection = con;
    }

    public void Put(Visita visit, int id_inst)
    {
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "INSERT INTO Visitas (idInstituicao, idUser, dataInicio, dataSaida, estado, avaliacao, comentarios) VALUES (@idi, @idu, @di, @ds, @estado, @aval, @coment)";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@idi", id_inst);
        mc.Parameters.AddWithValue("@idu", visit.getVisitante());
        mc.Parameters.AddWithValue("@di", visit.getData_inicio());
        mc.Parameters.AddWithValue("@ds", visit.getData_saida());
        mc.Parameters.AddWithValue("@estado", visit.getEstado());
        mc.Parameters.AddWithValue("@aval", visit.getAva());
        mc.Parameters.AddWithValue("@coment", visit.getComentario());
        mc.ExecuteNonQuery();
      }
      catch(Exception e){
        Console.WriteLine(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          Console.WriteLine(e.ToString());
        }
      }
    }

    public Visitas Get(int id_visit, int id_inst, DateTime data_inicio)
    {
      Visita visit = new Visita();
      MySqlConnection msc = new MySqlConnection(Connection);
      try{
        msc.Open();
        string query = "SELECT * FROM Visitas WHERE idInstituicao=@id_inst, idUser=@id_user, dataInicio=@di";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id_inst", id_inst);
        mc.Parameters.AddWithValue("@id_user", id_visit);
        mc.Parameters.AddWithValue("@di", data_inicio);
        MySqlDataReader mr = mc.ExecuteReader();

        if(mr.Read()){
          visit.setVisitante(id_visit);
          visit.setData_inicio(data_inicio);
          visit.setData(data_inicio);
          visit.setData_fim(mr.GetString("dataSaida"));
          visit.setComentario(mr.GetString("comentarios"));
          visit.setAvaliacao(mr.GetString("avaliacao"));
          visit.setEstado(mr.GetInt32("estado"));

          mr.Close();
        }
      }
      catch(Exception e){
        Console.WriteLine(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          Console.WriteLine(e.ToString());
        }
      }
      return visit;
    }

    public void Update(Visita visit){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "UPDATE Visitas SET Nome=@nome, e-mail=@mail, morada=@morada, Telemóvel=@tele, cod_postal=@cp WHERE id=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@idi", id_inst);
        mc.Parameters.AddWithValue("@idu", visit.getVisitante());
        mc.Parameters.AddWithValue("@di", visit.getData_inicio());
        mc.Parameters.AddWithValue("@ds", visit.getData_saida());
        mc.Parameters.AddWithValue("@estado", visit.getEstado());
        mc.Parameters.AddWithValue("@aval", visit.getAva());
        mc.Parameters.AddWithValue("@coment", visit.getComentario());
        mc.ExecuteNonQuery();
      }
      catch(Exception e){
        Console.WriteLine(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          Console.WriteLine(e.ToString());
        }
      }}

    public ICollection<Visita> GetAllByUser(int id_user){
      ICollection<Visita> visits = new HashSet<Visita>();
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "SELECT * FROM Visitas WHERE idUser=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", id_user);
        MySqlDataReader mr = mc.ExecuteReader();
        while(mr.Read()){
          int id = mr.GetInt32("id");
          visits.Add(this.Get(id));
        }

      }
      catch(Exception e){
        Console.WriteLine(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          Console.WriteLine(e.ToString());
        }
      }
        return visits;
    }
    }

    public ICollection<Visita> GetAll(int id_inst){
      ICollection<Visita> visits = new HashSet<Visita>();
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "SELECT * FROM Visitas WHERE idInstituicao=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", id);
        MySqlDataReader mr = mc.ExecuteReader();
        while(mr.Read()){
          int id = mr.GetInt32("id");
          visits.Add(this.Get(id));
        }

      }
      catch(Exception e){
        Console.WriteLine(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          Console.WriteLine(e.ToString());
        }
      }
        return visits;
    }
  }
