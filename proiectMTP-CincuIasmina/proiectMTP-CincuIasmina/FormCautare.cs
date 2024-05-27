using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proiectMTP_CincuIasmina
{
    public partial class FormCautare : Form
    {
        public FormCautare()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && int.TryParse(textBox1.Text, out int id))
            {
                string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        conn.Open();
                        string query = "SELECT * FROM Produse WHERE id = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "Produse");

                        if (ds.Tables["Produse"].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables["Produse"].DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("Nu s-au găsit înregistrări pentru ID-ul specificat.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la efectuarea căutării: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți un ID valid.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
                SqlConnection conn = new SqlConnection(connect);
                try
                {
                    conn.Open();
                    string tabel = "SELECT * FROM Produse WHERE nume = @nume";
                    SqlDataAdapter adapter = new SqlDataAdapter(tabel, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@nume", textBox2.Text);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Produse");

                    dataGridView1.DataSource = ds.Tables["Produse"].DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți un nume în câmpul de căutare.", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void labelX_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
