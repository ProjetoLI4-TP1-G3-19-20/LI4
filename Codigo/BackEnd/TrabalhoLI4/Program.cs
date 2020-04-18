using System;
using MySql.Data.MySqlClient;
using LI4;
namespace Version
{
    class Program
    {

        static void Main()
        {
            
            string cs = @"server=localhost;user id=root;database=trabalholi4; password=PmMf1999.,,.";
            var bd = new MySqlConnection(cs);
            bd.Open();

            Console.WriteLine("A iniciar servidor");
            HTTPServer server = new HTTPServer("C:", 8080, cs);

            /*
            string cond = ""; 
             
            while (cond != "quit") {
                Console.WriteLine("ola");

            DepartamentoDAO vd = new DepartamentoDAO(cs);
            Departamento v2 = new Departamento(vd.Get(1));
                Console.WriteLine("ola");
              Departamento v = new Departamento();
              v.SetID(2);
              v.SetNome("Cardiologia");
              vd.Put(v,1);
           // Console.WriteLine(v2.GetNome());
                cond = Console.ReadLine();
            }

            */
            bd.Close();


        }
    }
}