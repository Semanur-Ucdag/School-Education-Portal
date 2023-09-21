using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;


namespace OgrenciOtomasyon
{
    public partial class Form2 : Form
    {
        public Form2() 
        {
            InitializeComponent();
            IsMdiContainer = true;

        }
        static string constring = ("Data Source=SEMANUR-PC\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);
        private void Form2_Load(object sender, EventArgs e)
        {
            ogrenci();     
        }

        private void ogretmen()
        {

            listView1.Items.Clear();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From Ogretmenn", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["AdSoyad"].ToString();
                ekle.SubItems.Add(oku["KullanıcıAdı"].ToString());
                ekle.SubItems.Add(oku["Sifre"].ToString());
                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
              baglan.Open();
              SqlCommand komut = new SqlCommand("Insert into Ogretmenn (AdSoyad, KullanıcıAdı, Sifre) Values ('" + textBox1.Text.ToString() + " ' , '" + textBox2.Text.ToString() + "' , '" + textBox3.Text.ToString() + "' )", baglan);
              komut.ExecuteNonQuery();
              baglan.Close();
           ogretmen();
            
        }

        private void button2_Click(object sender, EventArgs e)//Sil tuşu öğretmen
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];
                string AdSoyad = selected.SubItems[1].Text;

                baglan.Open();
                SqlCommand komut = new SqlCommand("DELETE FROM Ogretmenn WHERE AdSoyad = @AdSoyad", baglan);
                komut.Parameters.AddWithValue("@AdSoyad", AdSoyad);
                komut.ExecuteNonQuery();
                baglan.Close();
                listView1.Items.Remove(selected);
                ogretmen();
            }   
        }  

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void ogrenci()
        {

            listView2.Items.Clear();
            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            baglan.Open();
            SqlCommand komutt = new SqlCommand("Select * From Ogrenci", baglan);
            SqlDataReader oku = komutt.ExecuteReader();

            while (oku.Read()) 
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["TC"].ToString();
                ekle.SubItems.Add(oku["AdSoyad"].ToString());
                ekle.SubItems.Add(oku["KullanlıcıAdı"].ToString());
                ekle.SubItems.Add(oku["Sifre"].ToString());
                ekle.SubItems.Add(oku["Vize"].ToString());
                ekle.SubItems.Add(oku["Final"].ToString());
                listView2.Items.Add(ekle);
            }
            baglan.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Vize = "00";
            string Final = "00";
            baglan.Open();
            SqlCommand komutt = new SqlCommand("Insert into Ogrenci (TC, AdSoyad, KullanlıcıAdı, Sifre,Vize, Final) Values ('" + textBox4.Text.ToString() + " ' , '" + textBox5.Text.ToString() + "' , '" + textBox6.Text.ToString() + " ', '" + textBox7.Text.ToString() + " ' , '" + Vize + "', '" + Final + "')", baglan);
            komutt.ExecuteNonQuery();
            baglan.Close();
            ogrenci();



        }

        private void button4_Click(object sender, EventArgs e)//silme tuşu öğrenci
        {
            if (listView2.SelectedItems.Count > 0)
            {

                ListViewItem selected = listView2.SelectedItems[0];
                
                string adSoyad = selected.SubItems[1].Text;
                baglan.Open();
                SqlCommand komutt = new SqlCommand("DELETE FROM Ogrenci WHERE AdSoyad = @AdSoyad", baglan);
                komutt.Parameters.AddWithValue("@AdSoyad", adSoyad);
                komutt.ExecuteNonQuery();
                baglan.Close();

                listView2.Items.Remove(selected);
                ogrenci();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
