using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LI4;
using MySql.Data.MySqlClient;

namespace TrabalhoLI4
{
    class PedidoVisitaDAO
    {

        private string Connection;

        public PedidoVisitaDAO(String con)
        {
            Connection = con;
        }

        public void Put(PedidoVisita visit, int id_inst,int id_user, int idVisit)
        {
            MySqlConnection msc = new MySqlConnection(Connection);

            try
            {
                msc.Open();
                string query = "INSERT INTO `trabalholi4`.`pedidovisita`(`data`,`comentarios`,`idDepartament0`,`visitado`,`idInst`,`idVisita`,`idUser`) VALUES (@data, @comentarios, @idd, @visitado, @idinst, @idVisita, @idu)";
                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@data", visit.GetData());
                mc.Parameters.AddWithValue("@comentarios", visit.GetComentario());
                mc.Parameters.AddWithValue("@idd", visit.GetDepartamento());
                mc.Parameters.AddWithValue("@visitado", visit.GetVisitado());
                mc.Parameters.AddWithValue("@idInst", id_inst);
                mc.Parameters.AddWithValue("@idVisita", idVisit);
                mc.Parameters.AddWithValue("@idu", id_user);
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

        public PedidoVisita Get(int idVisit)
        {
            PedidoVisita visit = new PedidoVisita();
            MySqlConnection msc = new MySqlConnection(Connection);
            try
            {
                msc.Open();
                string query = "SELECT * FROM pedidovisita WHERE id=@id";

                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Prepare();
                mc.Parameters.AddWithValue("@id", idVisit);
                MySqlDataReader mr = mc.ExecuteReader();
                VisitanteDAO vdao = new VisitanteDAO(this.Connection);

                if (mr.Read())
                {
                    visit.SetComentario(mr.GetString("Comentario"));
                    visit.SetData(mr.GetDateTime("data"));
                    visit.SetDepartamento(mr.GetInt32("idDepartament0"));
                    visit.SetVisitado(mr.GetString("visitado"));
                    Visitante vis = new Visitante(vdao.Get(mr.GetInt32("idUser")));
                    visit.SetVisitante(vis);
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

        public void Update(PedidoVisita visit, int id_inst, int idVisit)
        {
            MySqlConnection msc = new MySqlConnection(Connection);
            Visitante v = new Visitante();
            v = visit.GetVisitante();
            try
            {
                msc.Open();
                string query = "UPDATE `trabalholi4`.`pedidovisita `SET` data` = @data,`comentarios` = @comentarios,`idDepartament0` = @idd,`visitado` = @visitado,`idInst` = @id_inst,`idVisita` = @idVisita,`idUser` = @idu WHERE id=@idVisita";
                MySqlCommand mc = new MySqlCommand(query, msc);
                mc.Parameters.AddWithValue("@idVisita", idVisit);
                mc.Parameters.AddWithValue("@data", visit.GetData());
                mc.Parameters.AddWithValue("@comentarios", visit.GetComentario());
                mc.Parameters.AddWithValue("@idd", visit.GetDepartamento());
                mc.Parameters.AddWithValue("@visitado", visit.GetVisitado());
                mc.Parameters.AddWithValue("@idInst", id_inst);
                mc.Parameters.AddWithValue("@idu", v.GetId_utilizador());
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
    }
}
