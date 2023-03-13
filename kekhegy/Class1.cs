using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace kekhegy
{
    internal class Room
    {
        public int Emelet { get; set; }
        public int Szobaszam { get; set; }
        public int Ferohelyek { get; set; }
        public decimal Ar { get; set; }

        public string Megjegyzes { get; set; }


        public Room(MySqlDataReader reader)
        {
            
            
            Emelet = reader.GetInt32(0);
            Szobaszam = reader.GetInt32(1);
            Ferohelyek = reader.GetInt32(2);
            Ar = reader.GetDecimal(3);
            Megjegyzes = reader.GetString(4);
        }

        public Room()
        {
        }
    }
}
