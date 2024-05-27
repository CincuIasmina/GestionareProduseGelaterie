using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proiectMTP_CincuIasmina
{
    public partial class Inregistrare : Form
    {
        public Inregistrare()
        {
            InitializeComponent();
        }

        private void btnSalvare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUtilizator.Text) || string.IsNullOrWhiteSpace(txtParola.Text))
            {
                MessageBox.Show("Te rog completează toate câmpurile.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            timer1.Start();
            using (StreamWriter w = File.AppendText("utilizatori.txt"))
            {
                w.WriteLine(txtUtilizator.Text + "," + txtParola.Text);
            }
            MessageBox.Show("Utilizatorul a fost salvat cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 10;
            progressBar1.Value -= 1;
            progressBar1.Value += 1;
            if (progressBar1.Value == progressBar1.Maximum) {
            timer1.Stop();
                MessageBox.Show("Inregistrarea a reusit!");
                Application.Restart();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtParola.PasswordChar = '\0';


            }
            else

                txtParola.PasswordChar = '♥';
        }

        private void labelX_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
