using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class    Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // 1 Player option
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(1);
            this.Hide();
            f1.Show();
        }

        //Exit option
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 2 Players option
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(2);
            this.Hide();
            f1.Show();
        }

        //control option
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog(this);
        }
    }
}
