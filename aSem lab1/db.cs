using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSem_lab1
{
    static class db
    {
        public static OracleConnection conn;
        public static void connect()
        {
            //открываем соединение и сохраняем дескриптор
            string oradb = "Data Source=(DESCRIPTION =" + "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" + "(CONNECT_DATA =" + "(SERVER = DEDICATED)" + "(SERVICE_NAME = XE)));" + "User Id= C##aSem;Password=aSem;";

            conn = new OracleConnection(oradb);
            conn.Open();
        }
    }
}
