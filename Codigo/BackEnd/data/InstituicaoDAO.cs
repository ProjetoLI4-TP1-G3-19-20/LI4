using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using namespace BackEnd.Data{
  public class InstituicaoDAO{

    private string Connection;

    public InstituicaoDAO(String con){
      Connection = con;
    }

    public void Put(Instituicao inst){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "INSERT INTO Instituicao (id_inst, Nome, e-mail, localizacao) VALUES (@id, @nome, @mail, @local)";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", inst.getCod_instituicao());
        mc.Parameters.AddWithValue("@nome", inst.getNome());
        mc.Parameters.AddWithValue("@email", inst.getEmail());
        mc.Parameters.AddWithValue("@local", inst.getLocalizacao());
        mc.ExecuteNonQuery();
        foreach (string contacto in inst.getContactos()){
          query = "INSERT INTO Contacto (id_inst, telemovel) VALUES (@id, @tele)";
          MySqlCommand mc1 = new MySqlCommand(query, msc);
          mc1.Parameters.AddWithValue("@id", inst.getCod_instituicao());
          mc1.Parameters.AddWithValue("@tele", contacto);
          mc1.ExecuteNonQuery();
        }
      }
      catch(Exception e){
        MessageBox.Show(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          MessageBox.Show(e.ToString());
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
          inst.setCod_instituicao(id_inst);
          inst.setNome(mr.GetString("Nome"));
          inst.setLocalizacao(mr.GetString("localizacao"));
          inst.setEmail(mr.GetString("e-mail"));

          mr.Close();

          query = "SELECT telemovel FROM Contacto WHERE id_inst=@id";
          MySqlCommand mc1 = new MySqlCommand(query, msc);
          mc.Parameters.AddWithValue("@id", id_inst);
          MySqlDataReader mr1 = mc1.ExecuteReader();

          ArrayList conts = new ArrayList();

          while(mr1.Read()){
            conts.add(mr1.GetString("telemovel"));
          }
        }
      }
      catch(Exception e){
        MessageBox.Show(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          MessageBox.Show(e.ToString());
        }
      }}

    public void Update(Instituicao inst){
      MySqlConnection msc = new MySqlConnection(Connection);

      try{
        msc.Open();
        string query = "UPDATE Instituicao SET Nome=@nome, e-mail=@mail, localizacao=@local WHERE id_inst=@id";
        MySqlCommand mc = new MySqlCommand(query, msc);
        mc.Parameters.AddWithValue("@id", inst.getCod_instituicao());
        mc.Parameters.AddWithValue("@nome", inst.getNome());
        mc.Parameters.AddWithValue("@email", inst.getEmail());
        mc.Parameters.AddWithValue("@local", inst.getLocalizacao());
        mc.ExecuteNonQuery();
        foreach (string contacto in inst.getContactos()){
          query = "UPDATE Contacto SET telemovel=@tele WHERE id_inst=@id, @tele";
          MySqlCommand mc1 = new MySqlCommand(query, msc);
          mc1.Parameters.AddWithValue("@id", inst.getCod_instituicao());
          mc1.Parameters.AddWithValue("@tele", contacto);
          mc1.ExecuteNonQuery();
        }
      }
      catch(Exception e){
        MessageBox.Show(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          MessageBox.Show(e.ToString());
        }
      }}

    public ICollection<Instituicao> GetAll(){
      ICollection<Instituicao> insts = new HashSet<Instituicao>;
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
        MessageBox.Show(e.ToString());
      }
      finally{
        try{
          msc.Close();
        }
        catch(Exception e){
          MessageBox.Show(e.ToString());
        }
      }




    }
  }
}
