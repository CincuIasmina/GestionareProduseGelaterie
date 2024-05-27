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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proiectMTP_CincuIasmina
{
    public partial class AdaugareInformatii : Form
    {
        int id_citit;
        public AdaugareInformatii(int id)
        {
            this.id_citit = id;
            InitializeComponent();
        }

        private void AdaugareInformatii_Load(object sender, EventArgs e)
        {
            txtID.Text = id_citit.ToString();
            string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            string tabel = "select * from infoProduse";
            SqlDataAdapter adapter = new SqlDataAdapter(tabel, connect);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "infoProduse");
            dataGridView1.DataSource = ds.Tables["infoProduse"].DefaultView;
            conn.Close();
        }

        private void btnAdaugare_Click(object sender, EventArgs e)
        {
            if (txtID.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty)
            {
                //SqlConnection com = new SqlConnection("Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True");
                string connect = @"Data Source=DESKTOP-DNRPMM0\MSSQLSERVER01;Initial Catalog=ProduseGelaterie; Integrated Security=True";
                SqlConnection conn = new SqlConnection(connect);
                string query = "insert into infoProduse (id,nume,pret,cantitate) values(@id, @nume, @pret, @cant)";
                SqlCommand sc = new SqlCommand(query, conn);
                sc.CommandTimeout = 60;
                conn.Open();
                sc.Parameters.AddWithValue("@id", Convert.ToInt16(txtID.Text));
                sc.Parameters.AddWithValue("@nume", textBox2.Text);
                sc.Parameters.AddWithValue("@pret", Convert.ToInt16(textBox3.Text));
                sc.Parameters.AddWithValue("@cant", Convert.ToInt16(textBox4.Text));
                sc.ExecuteNonQuery();
                conn.Close();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Trebuie sa complecati toate campurile!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                if (selectedRow != null)
                {
                    if (selectedRow.Cells[0].Value != null && selectedRow.Cells[1].Value != null &&
                        selectedRow.Cells[2].Value != null && selectedRow.Cells[3].Value != null)
                    {
                        txtID.Text = selectedRow.Cells[0].Value.ToString();
                        textBox2.Text = selectedRow.Cells[1].Value.ToString();
                        textBox3.Text = selectedRow.Cells[2].Value.ToString();
                        textBox4.Text = selectedRow.Cells[3].Value.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Câmpurile selectate nu au valori valide!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void labelX_Click(object sender, EventArgs e)
        {
            Menu m = new Menu();
            m.ShowDialog();
        }
    }
}
