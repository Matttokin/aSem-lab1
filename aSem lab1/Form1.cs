using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aSem_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            db.connect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new register().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new auth().Show();
        }
    }
}
