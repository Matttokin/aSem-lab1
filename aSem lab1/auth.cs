using Oracle.ManagedDataAccess.Client;
using OracleInternal.Secure.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace aSem_lab1
{
    public partial class auth : Form
    {
        private static int countTryMax = 5; //максимальное кол-во попыток входа

        private static int countTry = 0; //попыток использовано на данный момент
        private string idUser;
        private string passwordFromDb;

        private static DateTime lockTime = new DateTime(); //время, до которого дейтсвует блокировка || скручено в 0, чтобы потом использовать
        private bool authVar = false;
        public auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now > lockTime)
                {
                    string login = textBox1.Text.ToString();
                    string password = textBox2.Text.ToString();

                    OracleDataReader d = send2db.send("SELECT lab1.findIdUserByName('" + login + "') as rez FROM DUAL");

                    d.Read();

                    idUser = d["rez"].ToString();

                    if (!idUser.Equals("-1")) //если -1, то не найден
                    {
                        countTry = 0; //сбрасываем количество попыток

                        d = send2db.send("SELECT lab1.getPasswordById('" + idUser + "') as rez FROM DUAL");

                        d.Read();

                        passwordFromDb = d["rez"].ToString();

                        if (VerifyMd5Hash(password,passwordFromDb))
                        {
                            MessageBox.Show("Успешный вход");
                            authVar = true;
                        } else
                        {
                            countTry++;
                            authVar = false;
                            if (countTry >= countTryMax) //если привышено кол-во попыток
                            {
                                DateTime timeNow = DateTime.Now;
                                lockTime = timeNow.AddMinutes(1); //ставим блокировку в +1 минуту
                                countTry = 0; //сбрасываем счетчик
                            }
                            MessageBox.Show("Неверный логин или пароль");
                        }
                    }
                    else
                    {
                        countTry++;
                        authVar = false;
                        if (countTry >= countTryMax) //если привышено кол-во попыток
                        {
                            DateTime timeNow = DateTime.Now;
                            lockTime = timeNow.AddMinutes(1); //ставим блокировку в +1 минуту
                            countTry = 0; //сбрасываем счетчик
                        }
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Доступ временно ограничен");
                }                
            }
            catch
            {
                MessageBox.Show("Неизвестная ошибка");
            }
        }
        static string GetMd5Hash(string input)
        {

            System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(string input, string hash)
        {
            System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            string hashOfInput = GetMd5Hash( input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
