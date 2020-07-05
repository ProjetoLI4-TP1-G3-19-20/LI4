using System;
using MySql.Data.MySqlClient;
using LI4;

    class Program
    {

        static void Main()
        {
            
            string cs = @"server=ezvisits.mysql.database.azure.com;user id=uminho_admin@ezvisits;database=trabalholi4; password=PmMf1999.,,.";
            var bd = new MySqlConnection(cs);
            bd.Open();

            Console.WriteLine("A iniciar servidor");
            HTTPServer server = new HTTPServer("C:", 443, cs);

           
            bd.Close();


        }
}