using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aSem_lab1
{
    static class send2db
    {
        static public OracleDataReader send(string s)
        {
            Console.WriteLine(s);
            OracleCommand cmd = new OracleCommand(); //инициализируем новый запрос к бд

            cmd.Connection = db.conn; //получаем дескриптор соединения

            cmd.CommandText =
                s; //запрос
            cmd.CommandType = CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();  //получаем ответ

            return dr; //считываем строку с ответом
        }
    }
}
