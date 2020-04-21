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

           
            bd.Close();


        }
    }
}