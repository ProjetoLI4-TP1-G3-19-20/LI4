﻿using System;
using MySql.Data.MySqlClient;

namespace Version
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = @"server=localhost;user id=root;database=trabalho; password=ZUhiza73#";

            var bd = new MySqlConnection(cs);
            bd.Open();

            bd.Close();

            Console.WriteLine($"ola");
        }
    }
}