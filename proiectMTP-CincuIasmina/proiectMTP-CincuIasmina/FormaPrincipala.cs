using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proiectMTP_CincuIasmina
{
    public partial class FormaPrincipala : Form
    {
       
        public FormaPrincipala()
        {
            InitializeComponent();
        }

        private void FormaPrincipala_Load(object sender, EventArgs e)
        {
            string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            string tabel = "select * from Produse";
            SqlDataAdapter adapter = new SqlDataAdapter(tabel, connect);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Produse");
            dataGridView1.DataSource = ds.Tables["Produse"].DefaultView;
            conn.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
            try
            {
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        string tabel_date = "select * from Produse";
                        SqlDataAdapter da = new SqlDataAdapter(tabel_date, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Produse");
                        if (ds.Tables["Produse"] != null && ds.Tables["Produse"].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables["Produse"].DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("Nu există date de afișat.", "Informație", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nu s-a putut realiza conexiunea cu baza de date.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la încărcarea datelor: " + ex.Message, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdaugare_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty)
            {
                string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
                SqlConnection conn = new SqlConnection(connect);
                string query = "insert into Produse (id,nume, descriere) values(@id, @nume, @descr)";
                SqlCommand sc = new SqlCommand(query, conn);
                sc.CommandTimeout = 60;
                conn.Open();
                sc.Parameters.AddWithValue("@id", Convert.ToInt16(textBox1.Text));
                sc.Parameters.AddWithValue("@nume", textBox2.Text);
                sc.Parameters.AddWithValue("@descr", textBox3.Text);

                SqlDataReader myReader = sc.ExecuteReader();
                conn.Close();
                MessageBox.Show("Adaugare reusita!");
            }
            else
            {
                MessageBox.Show("Trebuie sa completati toate campurile!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty)
            {
                string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    string query = "UPDATE Produse SET Nume = @Nume, Descriere = @Descriere WHERE id = @id";
                    using (SqlCommand sc = new SqlCommand(query, conn))
                    {
                        sc.Parameters.AddWithValue("@Nume", textBox2.Text);
                        sc.Parameters.AddWithValue("@Descriere", textBox3.Text);
                        sc.Parameters.AddWithValue("@id", textBox1.Text);

                        conn.Open();
                        sc.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                MessageBox.Show("Update reusit!");
            }
            else
            {
                MessageBox.Show("Trebuie sa complectati toate campurile!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
                SqlConnection conn = new SqlConnection(connect);
                string query = "DELETE FROM Produse WHERE id = @id";
                SqlCommand sc = new SqlCommand(query, conn);
                sc.CommandTimeout = 60;
                sc.Parameters.AddWithValue("@id", textBox1.Text);

                try
                {
                    conn.Open();
                    int rowsAffected = sc.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Ștergere reușită!");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Nu există nicio înregistrare cu acest ID în baza de date!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la ștergere: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să introduceți ID-ul produsului pe care doriți să-l ștergeți!");
            }
        }

        private void btnCautare_Click(object sender, EventArgs e)
        {
            FormCautare formCautare = new FormCautare();
            formCautare.ShowDialog();
        }

        private void btnCategorii_Click(object sender, EventArgs e)
        {
            int id= Convert.ToInt16(dataGridView1.SelectedCells[0].Value.ToString());
            AdaugareInformatii ai= new AdaugareInformatii(id);
            ai.ShowDialog();
        }

        private void labelX_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
