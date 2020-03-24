using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class VisitanteDAO{

    private string Connection;

    public VisitanteDAO(String con){
      Connection = con;
    }

    public void Put(Visitante visit)
    {
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "INSERT INTO Visitante (id, Telemóvel, Nome, e-mail, morada, cod_postal) VALUES (@id, @tele, @nome, @mail, @morada, @cp)";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", visit.getId_utilizador());
        mc.Parameters.AddWithValue("@tele", visit.GetTelefone());
        mc.Parameters.AddWithValue("@nome", visit.GetNome());
        mc.Parameters.AddWithValue("@email", visit.GetEmail());
        mc.Parameters.AddWithValue("@local", visit.GetMorada());
        mc.Parameters.AddWithValue("@cp", visit.getCod_postal());
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

    public Visitante Get(int id)
    {
      Visitante visit = new Visitante();
      MySqlConnection msc = new MySqlConnection(Connection);
      try{
        msc.Open();
        string query = "SELECT * FROM Visitante WHERE id=@id";
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

    public void Update(Visitante visit){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "UPDATE Visitante SET Nome=@nome, e-mail=@mail, morada=@morada, Telemóvel=@tele, cod_postal=@cp WHERE id=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", visit.getId_utilizador());
        mc.Parameters.AddWithValue("@tele", visit.GetTelefone());
        mc.Parameters.AddWithValue("@nome", visit.GetNome());
        mc.Parameters.AddWithValue("@email", visit.GetEmail());
        mc.Parameters.AddWithValue("@morada", visit.GetMorada());
        mc.Parameters.AddWithValue("@cp", visit.getCod_postal());
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

    public ICollection<Visitante> GetAll(){
      ICollection<Visitante> visits = new HashSet<Visitante>();
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "SELECT id FROM Visitante";
        MySqlCommand mc = new MySqlCommand(query, msc);
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
