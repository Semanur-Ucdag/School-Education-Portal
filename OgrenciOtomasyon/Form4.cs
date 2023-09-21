using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OgrenciOtomasyon
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            Not();
        }
        static string constring = ("Data Source=SEMANUR-PC\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);
        private void Not()
        {

            listView2.Items.Clear();
            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From Ogrenci where KullanlıcıAdı = '" + label5.Text + "'", baglan);
                
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["AdSoyad"].ToString();
                ekle.SubItems.Add(oku["Vize"].ToString());
                ekle.SubItems.Add(oku["Final"].ToString());
                listView2.Items.Add(ekle);

                double vize = Convert.ToDouble(oku["Vize"]);
                double final = Convert.ToDouble(oku["Final"]);
                double ortalama = vize * 0.4 + final * 0.6;
                textBox1.Text = ortalama.ToString("F2");

                //Durum Bilgisi
                if (ortalama < 50)
                {
                    textBox2.Text = "Kaldı";
                }
                else
                {
                    textBox2.Text = "Geçti";
                }

                //textBox1.Text= (Convert.ToDouble(oku["Vize"].ToString()))*0.4 + (Convert.ToDouble(oku["Final"].ToString()) * 0.6 );
            }
            baglan.Close();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Not();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Not();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    } }
