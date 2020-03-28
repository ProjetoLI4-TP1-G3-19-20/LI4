using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class InstituicaoDAO{

    private string Connection;

    public InstituicaoDAO(String con){
      Connection = con;
    }

    public void Put(Instituicao inst){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "INSERT INTO `trabalholi4`.`instituicao`(`id_inst`,`Nome`,`email`,`localizacao`) VALUES (@id, @nome, @mail, @local)";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", inst.GetCod_instituicao());
        mc.Parameters.AddWithValue("@nome", inst.GetNome());
        mc.Parameters.AddWithValue("@mail", inst.GetEmail());
        mc.Parameters.AddWithValue("@local", inst.GetLocalizacao());
        mc.ExecuteNonQuery();
        foreach (string contacto in inst.GetContactos()){
          query = "INSERT INTO Contactos (id_inst, telemovel) VALUES (@id, @tele)";
          MySqlCommand mc1 = new MySqlCommand(query, msc);
          mc1.Parameters.AddWithValue("@id", inst.GetCod_instituicao());
          mc1.Parameters.AddWithValue("@tele", contacto);
          mc1.ExecuteNonQuery();
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
      }}

    public Instituicao Get(int id_inst){
      Instituicao inst = new Instituicao();
      MySqlConnection msc = new MySqlConnection(Connection);
      try{
        msc.Open();
        string query = "SELECT * FROM Instituicao WHERE id_inst=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", id_inst);
        MySqlDataReader mr = mc.ExecuteReader();

        if(mr.Read()){
          inst.SetCod_instituicao(id_inst);
          inst.SetNome(mr.GetString("Nome"));
          inst.SetLocalizacao(mr.GetString("localizacao"));
          inst.SetEmail(mr.GetString("email"));

          mr.Close();

          query = "SELECT telemovel FROM Contacto WHERE id_inst=@id";
          MySqlCommand mc1 = new MySqlCommand(query, msc);
          mc.Parameters.AddWithValue("@id", id_inst);
          MySqlDataReader mr1 = mc1.ExecuteReader();

          ArrayList conts = new ArrayList();

          while(mr1.Read()){
            conts.Add(mr1.GetString("telemovel"));
                }
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
        return inst;
    }

    public void Update(Instituicao inst){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "UPDATE Instituicao SET Nome=@nome, e-mail=@mail, localizacao=@local WHERE id_inst=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", inst.GetCod_instituicao());
        mc.Parameters.AddWithValue("@nome", inst.GetNome());
        mc.Parameters.AddWithValue("@email", inst.GetEmail());
        mc.Parameters.AddWithValue("@local", inst.GetLocalizacao());
        mc.ExecuteNonQuery();
        foreach (string contacto in inst.GetContactos()){
          query = "UPDATE Contactos SET telemovel=@tele WHERE id_inst=@id, @tele";
          MySqlCommand mc1 = new MySqlCommand(query, msc);
          mc1.Parameters.AddWithValue("@id", inst.GetCod_instituicao());
          mc1.Parameters.AddWithValue("@tele", contacto);
          mc1.ExecuteNonQuery();
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
      }}

    public ICollection<Instituicao> GetAll(){
      ICollection<Instituicao> insts = new HashSet<Instituicao>();
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "SELECT id_inst FROM Instituicao";
        MySqlCommand mc = new MySqlCommand(query, msc);
        MySqlDataReader mr = mc.ExecuteReader();
        while(mr.Read()){
          insts.Add(this.Get(mr.GetInt32("id_inst")));
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
        return insts;
    }
  }
