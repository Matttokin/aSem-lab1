using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aSem_lab1
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text.ToString();
                string password = textBox2.Text.ToString();
                OracleDataReader d = send2db.send("SELECT lab1.registerUser('"+login+"','"+ GetMd5Hash(password)+ "') as rez FROM DUAL");

                d.Read();

                if (d.HasRows)
                {
                    if (d["rez"].ToString().Equals("0"))
                    {
                        MessageBox.Show("Успешно!");
                    } else
                    {
                        MessageBox.Show("Регистрация не удалась. Логин занят!");
                    }
                }
            } catch {
                MessageBox.Show("Неизвестная ошибка");
            }
        }
        static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }    
}
