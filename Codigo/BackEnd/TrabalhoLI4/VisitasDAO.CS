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

    public void Put(Visitas visit, int id_inst)
    {
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "INSERT INTO Visitas (idInstituicao, idUser, dataInicio, dataSaida, estado, avaliacao, comentarios) VALUES (@idi, @idu, @di, @ds, @estado, @aval, @coment)";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@idi", id_inst);
        mc.Parameters.AddWithValue("@idu", visit.getVisitante().getId_utilizador());
        mc.Parameters.AddWithValue("@di", visit.getData_inicio());
        mc.Parameters.AddWithValue("@ds", visit.getData_saida());
        mc.Parameters.AddWithValue("@estado", visit.getEstado());
        mc.Parameters.AddWithValue("@aval", visit.getAva());
        mc.Parameters.AddWithValue("@coment", visit.getCod_postal());
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
      Visitas visit = new Visitas();
      MySqlConnection msc = new MySqlConnection(Connection);
      try{
        msc.Open();
        string query = "SELECT * FROM Visitas WHERE id=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", id);
        MySqlDataReader mr = mc.ExecuteReader();

        if(mr.Read()){
          visit.setId_utilizador(id);
          visit.SetTelemovel(mr.GetString("Telemóvel"));
          visit.SetNome(mr.GetString("Nome"));
          visit.SetEmail(mr.GetString("e-mail"));
          visit.SetMorada(mr.GetString("morada"));
          visit.setCod_postal(mr.GetString("cod_postal"));

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

    public void Update(Visitas visit){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "UPDATE Visitas SET Nome=@nome, e-mail=@mail, morada=@morada, Telemóvel=@tele, cod_postal=@cp WHERE id=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@idi", id_inst);
        mc.Parameters.AddWithValue("@idu", visit.getVisitante().getId_utilizador());
        mc.Parameters.AddWithValue("@di", visit.getData_inicio());
        mc.Parameters.AddWithValue("@ds", visit.getData_saida());
        mc.Parameters.AddWithValue("@estado", visit.getEstado());
        mc.Parameters.AddWithValue("@aval", visit.getAva());
        mc.Parameters.AddWithValue("@coment", visit.getCod_postal());
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

    public ICollection<Visitas> GetAll(int id_inst){
      ICollection<Visitas> visits = new HashSet<Visitas>();
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
