using System;
using System.Collections.Generic;
using LI4;
using MySql.Data.MySqlClient;

namespace LI4
{
    class PedidoVisitaDAO
    {

        private string Connection;

        public PedidoVisitaDAO(String con)
        {
            Connection = con;
        }

        public void Put(PedidoVisita visit)
        {
            MySqlConnection msc = new MySqlConnection(Connection);

            try
            {
                msc.Open();
                string query = "INSERT INTO `trabalholi4`.`pedidovisita` (`hora_inicio`,`hora_fim`,`comentarios`,`idInst`,`idDepartamento`,`visitado`,`idUser`)" +
                               "VALUES(@hora_inicio, @hora_fim, @comentario, @inst, @dep, @visitado, @idUser)";

                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@hora_inicio", visit.getHoraInicio());
                mc.Parameters.AddWithValue("@hora_fim", visit.getHoraFim());
                mc.Parameters.AddWithValue("@comentario", visit.getComentario());
                mc.Parameters.AddWithValue("@inst", visit.getInstituicao());
                mc.Parameters.AddWithValue("@dep", visit.getDepartamento());
                mc.Parameters.AddWithValue("@visitado", visit.getVisitado());
                mc.Parameters.AddWithValue("@idUser", visit.getVisitante());
                mc.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
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

        public PedidoVisita Get(int idVisit)
        {
            PedidoVisita visit = new PedidoVisita();
            MySqlConnection msc = new MySqlConnection(Connection);
            try
            {
                msc.Open();
                string query = "SELECT * FROM pedidovisita WHERE idVisita=@id";


                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Prepare();
                mc.Parameters.AddWithValue("@id", idVisit);
                MySqlDataReader mr = mc.ExecuteReader();
                VisitanteDAO vdao = new VisitanteDAO(this.Connection);

                if (mr.Read())
                {
                    visit.setHoraInicio(mr.GetDateTime("hora_inicio"));
                    visit.setHoraFim(mr.GetDateTime("hora_fim"));
                    visit.setComentario(mr.GetString("comentarios"));
                    visit.setInstituicao(mr.GetInt32("idInst"));
                    visit.setDepartamento(mr.GetInt32("idDepartamento"));
                    visit.setVisitado(mr.GetString("visitado"));
                    visit.setVisitante(mr.GetInt32("idUser"));
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


        public ICollection<PedidoVisita> GetAll()
        {
            ICollection<PedidoVisita> visits = new HashSet<PedidoVisita>();
            MySqlConnection msc = new MySqlConnection(Connection);

            try
            {
                msc.Open();
                string query = "SELECT id FROM pedidovisita";
                MySqlCommand mc = new MySqlCommand(query, msc);
                MySqlDataReader mr = mc.ExecuteReader();
                while (mr.Read())
                {
                    int id = mr.GetInt32("id");
                    visits.Add(this.Get(id));
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

        public List<Vaga> getHorasDisponiveis(string visitado)
        {
            List<Vaga> v = new List<Vaga>();
            MySqlConnection msc = new MySqlConnection(Connection);


            try
            {
                msc.Open();
                string query = "SELECT * FROM pessoadeinteresse_has_vagas where nome=@n";
                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Prepare();
                mc.Parameters.AddWithValue("@n", visitado);
                MySqlDataReader mr = mc.ExecuteReader();
                Vaga vag = new Vaga();
                while (mr.Read())
                {
                    vag.SetHora_inicio(mr.GetDateTime("hora_inicio"));
                    vag.SetHora_fim(mr.GetDateTime("hora_fim"));
                    v.Add(vag);
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

            return v;
        }


        public List<PedidoVisita> getAllPedidos(String nome) {
            List<PedidoVisita> va = new List<PedidoVisita>();

            MySqlConnection msc = new MySqlConnection(Connection);
            try {
                msc.Open();
                string query = "SELECT * FROM pedidovisita pv " +
                               "WHERE pv.visitado = @n";
                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@n", nome);
                MySqlDataReader mr = mc.ExecuteReader();
                while (mr.Read()) {
                    PedidoVisita pv = new PedidoVisita();
                    pv = this.Get(mr.GetInt32("idVisita"));
                    va.Add(pv);
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

    }
}
