using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proiectMTP_CincuIasmina
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Inregistrare i=new Inregistrare();
            i.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Autentificare_Load(object sender, EventArgs e)
        {
            string[] utilizatori = File.ReadAllLines("utilizatori.txt");
            foreach (var linie in utilizatori)
            {
                string[] inregistrare = linie.Split(',');
                cmbUtilizator.Items.Add(inregistrare[0]);
            }
        }

       

        private int incercari = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbUtilizator.Text) || string.IsNullOrWhiteSpace(txtParola.Text))
            {
                MessageBox.Show("Vă rugăm să introduceți un nume de utilizator și o parolă.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] utilizatori = File.ReadAllLines("utilizatori.txt");
            foreach (var linie in utilizatori)
            {
                string[] inregistrare = linie.Split(',');
                if ((cmbUtilizator.Text).Equals(inregistrare[0]))
                {
                    if ((txtParola.Text.Trim()).Equals(inregistrare[1].Trim()))
                    {
                        Menu f = new Menu();
                        f.ShowDialog();
                        return; 
                    }
                    else
                    {
                        incercari++;
                        MessageBox.Show("Parolă incorectă! Mai aveți " + (3 - incercari).ToString() + " încercări!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (incercari == 3)
                        {
                            MessageBox.Show("Prea multe încercări eșuate. Aplicația se va închide.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }
            }
        }

            
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtParola.PasswordChar = '\0';


            }
            else

                txtParola.PasswordChar = '♥';
        }
    }
}
