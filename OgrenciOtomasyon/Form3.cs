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

namespace OgrenciOtomasyon
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=SEMANUR-PC\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);
        private void listele()
        {

            listView1.Items.Clear();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From Ogrenci", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["TC"].ToString();
                ekle.SubItems.Add(oku["AdSoyad"].ToString());
                ekle.SubItems.Add(oku["Vize"].ToString());
                ekle.SubItems.Add(oku["Final"].ToString());
                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)//öğrenci not ekleme bölümü
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];

                string TC = selected.SubItems[0].Text; // Öğrenci Id'sini almak için 1. hücreyi alıyoruz.
                string vize = textBox3.Text;
                string final = textBox4.Text;

                baglan.Open();
                SqlCommand komut = new SqlCommand("UPDATE Ogrenci SET Vize = @vize, Final = @final WHERE TC = @tc", baglan);
                komut.Parameters.AddWithValue("@vize", vize);
                komut.Parameters.AddWithValue("@final", final);
                komut.Parameters.AddWithValue("@TC", TC);
               
                int affectedRows = komut.ExecuteNonQuery();
                baglan.Close();
                listele();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Notlar güncellendi.");
                }
                else
                {
                    MessageBox.Show("Güncellenecek öğrenci bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek öğrenciyi seçin.");
            }





            /* baglan.Open();
              SqlCommand komut = new SqlCommand("UPDATE Ogrenci SET ( Vize, Final) Values ('" + textBox3.Text.ToString() + " '  , '" + textBox4.Text.ToString() + "' )", baglan);
              komut.ExecuteNonQuery();
              baglan.Close();*/

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

       
        private void button2_Click(object sender, EventArgs e)//SİLME TUŞU
        {
            /* if (listView1.SelectedItems.Count > 0)
               {
                   ListViewItem selected = listView1.SelectedItems[0];
                   string adSoyad = selected.SubItems[0].Text;

                   baglan.Open();
                   SqlCommand komut = new SqlCommand("DELETE FROM Ogrenci WHERE AdSoyad = @AdSoyad", baglan);
                   komut.Parameters.AddWithValue("@AdSoyad", adSoyad);
                   komut.ExecuteNonQuery();
                   baglan.Close();
                   listView1.Items.Remove(selected);

              }*/
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selected = listView1.SelectedItems[0];
                string adSoyad = selected.SubItems[1].Text; // Sütun indeksi 1'e düzeltildi.

                baglan.Open();
                SqlCommand komut = new SqlCommand("DELETE FROM Ogrenci WHERE AdSoyad = @AdSoyad", baglan);
                komut.Parameters.AddWithValue("@AdSoyad", adSoyad);
                komut.ExecuteNonQuery();
                baglan.Close();
                listView1.Items.Remove(selected);
            }








        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
