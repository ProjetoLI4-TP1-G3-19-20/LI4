using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class DepartamentoDAO
{

    private string connection;

    public DepartamentoDAO(String con)
    {
        connection = con;
    }

    public void Put(Departamento dep, int id_inst) {
        MySqlConnection msc = new MySqlConnection(connection);

        try {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`departamentos`(`id`,`id_inst`,`nome`) VALUES (@id, @id_inst, @nome)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            mc.ExecuteNonQuery();

            foreach (PessoaDeInteresse pi in dep.GetPessoaDeInteresses()) {
                query = "INSERT INTO `trabalholi4`.`pessoadeinteresse`(`nome`,`email`,`departamentos_id`,`departamentos_id_inst`)VALUES(@n,@email,@inst,@id)";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                mc1.Parameters.AddWithValue("@id", dep.GetID());
                mc1.Parameters.AddWithValue("@n", pi.getNome());
                mc1.Parameters.AddWithValue("@e", pi.getEmail());
                mc1.Parameters.AddWithValue("@inst", id_inst);
                mc1.ExecuteNonQuery();
                foreach (Vaga v in pi.GetHorasDisponiveis()) {
                    query = "INSERT INTO `trabalholi4`.`vagas`(`HorarioInicio`,`HorarioFinal`,`NrDeVagas`)VALUES(@hi,@hf,@nrv)";
                    MySqlCommand mc2 = new MySqlCommand(query, msc);
                    mc2.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                    mc2.Parameters.AddWithValue("@hf", v.GetHora_fim());
                    mc2.Parameters.AddWithValue("@nrv", v.GetNr_pessoas());
                    mc2.ExecuteNonQuery();


                }
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
    }

    public Departamento Get(int id) {
        Departamento dep = new Departamento();
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT * FROM departamentos WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read()) {
                dep.SetID(id);
                dep.SetNome(mr.GetString("nome"));

                mr.Close();

                List<Vaga> vagas = new List<Vaga>();

                query = "SELECT * FROM PessoaDeInteresse WHERE departamentos_id=@id";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                mc1.Parameters.AddWithValue("@id", id);
                MySqlDataReader mr1 = mc1.ExecuteReader();

                while (mr1.Read()) {
                    PessoaDeInteresse pi = new PessoaDeInteresse();
                    pi.setEmail(mr1.GetString("email"));
                    pi.setNome(mr1.GetString("nome"));

                    query = "SELECT * FROM HorariosOcupados WHERE nome=@n";
                    MySqlCommand mc3 = new MySqlCommand(query, msc);
                    mc3.Parameters.AddWithValue("@n", mr1.GetString("nome"));
                    MySqlDataReader mr3 = mc3.ExecuteReader();
                    List<HoraOcupada> hos = new List<HoraOcupada>();
                    while (mr3.Read()) {
                        HoraOcupada ho = new HoraOcupada();
                        ho.setHora_inicio(mr3.GetDateTime("data_inicio"));
                        ho.setHora_fim(mr3.GetDateTime("data_fim"));
                        hos.Add(ho);

                        List<Vaga> vagaslist = new List<Vaga>();

                        query = "SELECT * FROM Vagas v, pessoadeinteresse_has_vagas pv, pessoadeinteresse pi WHERE pi.nome=pv.nome and v.HorarioInicio=pv.hora_inicio and v.HorarioFinal=pv.hora_fim";
                        MySqlCommand mc2 = new MySqlCommand(query, msc);
                        mc2.Parameters.AddWithValue("@id", id);
                        MySqlDataReader mr2 = mc2.ExecuteReader();

                        while (mr2.Read()) {
                            Vaga v = new Vaga();
                            DateTime hi = mr2.GetDateTime("HorarioInicio");
                            DateTime hf = mr2.GetDateTime("HorarioFinal");
                            v.SetHora_inicio(hi);
                            v.SetHora_fim(hf);

                        }
                        mr2.Close();
                        pi.SetHorasDisponiveis(vagaslist);
                    }
                    mr3.Close();
                    pi.setHorasOcupadas(hos);



                }
                mr1.Close();
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
        return dep;
    }

    public void Update(Departamento dep, int id_inst) {
        MySqlConnection msc = new MySqlConnection(connection);

        try {
            msc.Open();
            string query = "UPDATE `trabalholi4`.`departamentos` SET `id` = @id,`id_inst` = @id_inst,`nome` = @nome WHERE `id` =@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            mc.ExecuteNonQuery();

            foreach (PessoaDeInteresse pi in dep.GetPessoaDeInteresses()) {
                query = "UPDATE PessoaDeInteresse set nome=@n, email=@e, departamentos_id=@id departamentos_id_inst=@inst";
                MySqlCommand mc2 = new MySqlCommand(query, msc);
                mc2.Parameters.AddWithValue("@id", dep.GetID());
                mc2.Parameters.AddWithValue("@n", pi.getNome());
                mc2.Parameters.AddWithValue("@e", pi.getEmail());
                mc2.Parameters.AddWithValue("@inst", id_inst);
                mc2.ExecuteNonQuery();




                foreach (Vaga v in pi.GetHorasDisponiveis()) {
                    query = "UPDATE Vagas SET HorarioInicio=@hi, HorarioFinal=@hf, NrDeVagas=@nrv";
                    MySqlCommand mc1 = new MySqlCommand(query, msc);
                    mc1.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                    mc1.Parameters.AddWithValue("@hf", v.GetHora_fim());
                    mc1.Parameters.AddWithValue("@nrv", v.GetNr_pessoas());
                    mc1.ExecuteNonQuery();

                    query = "UPDATE PessoaDeInteresse_has_Vagas SET nome=@n, hora_inicio=@hi, hora_fim=@hf";
                    MySqlCommand mc4 = new MySqlCommand(query, msc);
                    mc4.Parameters.AddWithValue("@n", pi.getNome());
                    mc4.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                    mc4.Parameters.AddWithValue("@hf", v.GetHora_fim());
                    mc4.ExecuteNonQuery();
                    foreach (HoraOcupada ho in pi.getHorasOcupadas()) {
                        query = "UPDATE HorariosOcupados SET data_inicio=@di, data_fim=@df, nome=@n";
                        MySqlCommand mc3 = new MySqlCommand(query, msc);
                        mc3.Parameters.AddWithValue("@di", ho.getHora_inicio());
                        mc3.Parameters.AddWithValue("@df", ho.getHora_fim());
                        mc3.Parameters.AddWithValue("@n", pi.getNome());
                        mc3.ExecuteNonQuery();
                    }
                }
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
    }

    public ICollection<Departamento> GetAll()
    {
        ICollection<Departamento> dep = new HashSet<Departamento>();
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "SELECT id FROM departamentos";
            MySqlCommand mc = new MySqlCommand(query, msc);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read())
            {
                int id = mr.GetInt32("id");
                dep.Add(this.Get(id));
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
        return dep;
    }

    public string getNameById(int id_inst, int id_dep) {
        string dep = "departamentoDefault";
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT nome FROM departamentos WHERE id = @id_dep AND id_inst = @id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@id_dep", id_dep);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                dep = mr.GetString("nome");
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
        return dep;
    }


    public int getIdByName(string name, int id_inst) {
        int dep = -1;
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT id FROM departamentos WHERE nome = @name AND id_inst = @id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id_inst", id_inst);
            mc.Parameters.AddWithValue("@name", name);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                dep = mr.GetInt32("id");
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
        return dep;
    }

    public List<string> DepByInst(string inst) {
        List<string> deps = new List<string>();
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT d.nome FROM departamentos d, instituicao i WHERE i.Nome = @inst AND d.id_inst = i.id_inst";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@inst", inst);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                deps.Add(mr.GetString("nome"));
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
        return deps;
    }

    public List<String> getPessoasByDepartamento(string inst, string dep){
        List<string> pessoas = new List<string>();
        MySqlConnection msc = new MySqlConnection(connection);
        try {
            msc.Open();
            string query = "SELECT p.nome FROM departamentos d, instituicao i, pessoadeinteresse p WHERE i.Nome = @inst AND d.nome = @dep AND i.id_inst = p.departamentos_id_inst AND d.id = p.departamentos_id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@inst", inst);
            mc.Parameters.AddWithValue("@dep", dep);
            MySqlDataReader mr = mc.ExecuteReader();
            while (mr.Read()) {
                pessoas.Add(mr.GetString("nome"));
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
        return pessoas;
    }
}
