using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LI4;
using System.Collections;

public class DepartamentoDAO
{

    private string connection;
    private int id_instituicao;

    public DepartamentoDAO(String con, int id)
    {
        connection = con;
        id_instituicao = id;
    }

    public void Put(Departamento dep)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "INSERT INTO `trabalholi4`.`departamentos`(`id`,`id_inst`,`nome`) VALUES (@id, @id_inst, @nome)";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", this.id_instituicao);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            mc.ExecuteNonQuery();

            foreach (Vaga v in dep.GetHorasDisponiveis())
            {
                query = "INSERT INTO Vagas (idDepartamento, HorarioInicio, HorarioFinal, NrDeVagas) VALUES (@id, @hi, @hf, @nrv)";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                mc1.Parameters.AddWithValue("@id", dep.GetID());
                mc1.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                mc1.Parameters.AddWithValue("@hf", v.GetHora_fim());
                mc1.Parameters.AddWithValue("@nrv", v.GetNr_pessoas());
                mc1.ExecuteNonQuery();

                foreach (PessoaDeInteresse pi in v.GetPessoasVisitaveis())
                {
                    query = "INSERT INTO PessoaDeInteresse (nome, idInst, email) VALUES (@n, @id, @e) WHERE NOT EXISTS (SELECT * FROM PessoaDeInteresse WHERE nome=@n)";
                    MySqlCommand mc2 = new MySqlCommand(query, msc);
                    mc2.Parameters.AddWithValue("@id", this.id_instituicao);
                    mc2.Parameters.AddWithValue("@n", pi.getNome());
                    mc2.Parameters.AddWithValue("@e", pi.getEmail());
                    mc2.ExecuteNonQuery();

                    query = "INSERT INTO PessoaDeInteresse_has_Vagas (nome, hora_inicio, hora_fim) VALUES (@n, @hi, @hf)";
                    MySqlCommand mc4 = new MySqlCommand(query, msc);
                    mc4.Parameters.AddWithValue("@n", pi.getNome());
                    mc4.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                    mc4.Parameters.AddWithValue("@hf", v.GetHora_fim());
                    mc4.ExecuteNonQuery();

                    foreach (HoraOcupada ho in pi.getHorasOcupadas())
                    {
                        query = "INSERT INTO HorariosOcupados (data_inicio, data_fim, nome) VALUES (@di, @df, @n)";
                        MySqlCommand mc3 = new MySqlCommand(query, msc);
                        mc3.Parameters.AddWithValue("@di", ho.getHora_inicio());
                        mc3.Parameters.AddWithValue("@df", ho.getHora_fim());
                        mc3.Parameters.AddWithValue("@n", pi.getNome());
                        mc3.ExecuteNonQuery();
                    }
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
    }

    public Departamento Get(int id)
    {
        Departamento dep = new Departamento();
        MySqlConnection msc = new MySqlConnection(connection);
        try
        {
            msc.Open();
            string query = "SELECT * FROM departamentos WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", id);
            MySqlDataReader mr = mc.ExecuteReader();

            if (mr.Read())
            {
                dep.SetID(id);
                dep.SetNome(mr.GetString("nome"));

                mr.Close();

                List<Vaga> vagas = new List<Vaga>();

                query = "SELECT * FROM Vagas WHERE idDepartamento=@id";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                mc1.Parameters.AddWithValue("@id", id);
                MySqlDataReader mr1 = mc1.ExecuteReader();

                while (mr1.Read())
                {
                    Vaga v = new Vaga();
                    DateTime hi = mr1.GetDateTime("HorarioInicio");
                    DateTime hf = mr1.GetDateTime("HorarioFinal");
                    v.SetHora_inicio(hi);
                    v.SetHora_fim(hf);
                    v.SetNr_pessoas(mr1.GetInt32("NrDeVagas"));

                    List<PessoaDeInteresse> pis = new List<PessoaDeInteresse>();

                    query = "SELECT * FROM PessoaDeInteresse AS PI INNER JOIN PessoaDeInteresse_has_Vagas AS PIHV ON PI.nome=PIHV.nome WHERE PIHV.hora_inicio=@hi AND PIHV.hora_fim=@hf";
                    MySqlCommand mc2 = new MySqlCommand(query, msc);
                    mc2.Parameters.AddWithValue("@hi", hi);
                    mc2.Parameters.AddWithValue("@hf", hf);
                    MySqlDataReader mr2 = mc2.ExecuteReader();


                    while (mr2.Read())
                    {
                        PessoaDeInteresse pi = new PessoaDeInteresse();
                        string nome_pi = mr2.GetString("nome");
                        pi.setNome(nome_pi);
                        pi.setEmail(mr2.GetString("email"));
                        List<HoraOcupada> hos = new List<HoraOcupada>();

                        query = "SELECT * FROM HorariosOcupados WHERE nome=@n";
                      MySqlCommand mc3 = new MySqlCommand(query, msc);
                        mc3.Parameters.AddWithValue("@n", nome_pi);
                        MySqlDataReader mr3 = mc3.ExecuteReader();

                        while (mr3.Read())
                        {
                            HoraOcupada ho = new HoraOcupada();
                            ho.setHora_inicio(mr3.GetDateTime("data_inicio"));
                            ho.setHora_fim(mr3.GetDateTime("data_fim"));
                            hos.Add(ho);
                        }
                        mr3.Close();
                        pi.setHorasOcupadas(hos);
                        pis.Add(pi);
                    }
                    mr2.Close();
                    v.SetPessoasVisitaveis(pis);
                    vagas.Add(v);
                }
                mr1.Close();
                dep.SetHorasDisponiveis(vagas);
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
        return dep;
    }

    public void Update(Departamento dep)
    {
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "UPDATE departamentos SET id_inst=@id_inst, nome=@nome WHERE id=@id";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@id", dep.GetID());
            mc.Parameters.AddWithValue("@id_inst", this.id_instituicao);
            mc.Parameters.AddWithValue("@nome", dep.GetNome());
            mc.ExecuteNonQuery();

            foreach (Vaga v in dep.GetHorasDisponiveis())
            {
                query = "UPDATE Vagas SET idDepartamento=@id, HorarioInicio=@hi, HorarioFinal=@hf, NrDeVagas=@nrv";
                MySqlCommand mc1 = new MySqlCommand(query, msc);
                mc1.Parameters.AddWithValue("@id", dep.GetID());
                mc1.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                mc1.Parameters.AddWithValue("@hf", v.GetHora_fim());
                mc1.Parameters.AddWithValue("@nrv", v.GetNr_pessoas());
                mc1.ExecuteNonQuery();

                foreach (PessoaDeInteresse pi in v.GetPessoasVisitaveis())
                {
                    query = "UPDATE PessoaDeInteresse set nome=@n, idInst=@id, email=@e";
                    MySqlCommand mc2 = new MySqlCommand(query, msc);
                    mc2.Parameters.AddWithValue("@id", this.id_instituicao);
                    mc2.Parameters.AddWithValue("@n", pi.getNome());
                    mc2.Parameters.AddWithValue("@e", pi.getEmail());
                    mc2.ExecuteNonQuery();

                    query = "UPDATE PessoaDeInteresse_has_Vagas SET nome=@n, hora_inicio=@hi, hora_fim=@hf";
                    MySqlCommand mc4 = new MySqlCommand(query, msc);
                    mc4.Parameters.AddWithValue("@n", pi.getNome());
                    mc4.Parameters.AddWithValue("@hi", v.GetHora_inicio());
                    mc4.Parameters.AddWithValue("@hf", v.GetHora_fim());
                    mc4.ExecuteNonQuery();

                    foreach (HoraOcupada ho in pi.getHorasOcupadas())
                    {
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

    public ICollection<Departamento> GetAll()
    {
        ICollection<Departamento> dep = new HashSet<Departamento>();
        MySqlConnection msc = new MySqlConnection(connection);

        try
        {
            msc.Open();
            string query = "SELECT id FROM departamentos WHERE idInstituicao=@idi";
            MySqlCommand mc = new MySqlCommand(query, msc);
            mc.Parameters.AddWithValue("@idi", this.id_instituicao);
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
}
