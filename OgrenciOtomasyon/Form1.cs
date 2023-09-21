using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OgrenciOtomasyon;
using System.Diagnostics.Eventing.Reader;

namespace OgrenciOtomasyon
{

    public partial class Form1 : Form
    {
        static string constring = ("Data Source=SEMANUR-PC\\SQLEXPRESS;Initial Catalog=Otomasyon;Integrated Security=True");
        SqlConnection baglan = new SqlConnection(constring);
        Form2 form2;
        Form3 form3;
        Form4 form4;
        public Form1()
        {

            InitializeComponent();
            IsMdiContainer = true; //form içinde form açabiliyor hale getirdim
            form2 = new Form2();
            form3 = new Form3();
            form4 = new Form4();

        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //ADMİN GİRİŞİ
            string Kullanici = textBox1.Text;
            string sifre = textBox2.Text;

            if (Kullanici == "admin" && sifre == "12345")
            {
                // Kullanıcı adı ve şifre doğruysa Form2'yi açar

                form2.Show();
               

                MessageBox.Show("Giriş Başarılı");
            }
            else
            {
                // Öğretmen doğrulama
                form2.Close();
                SqlCommand komut = new SqlCommand("Select * From Ogretmenn", baglan);
                komut.Connection = baglan;
                komut.CommandText = "Select * from Ogretmenn where KullanıcıAdı='" + textBox1.Text + "' And Sifre='" + textBox2.Text + "'";//burada sorun olabilir mi sonradan bak boş kayıt ataması için

                komut.Parameters.AddWithValue("@KullanıcıAdı", textBox1.Text);
                komut.Parameters.AddWithValue("@Sifre", textBox2.Text);

                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    baglan.Close();
                    form3.Show();
                    this.Hide();
                    MessageBox.Show("Giriş Başarılı");
                }
                else//ÖĞRENCİ Doğrulama
                {
                    form3.Close();
                    baglan.Close();

                    SqlCommand komutt = new SqlCommand("Select * From Ogrenci", baglan);
                    komutt.Connection = baglan;
                    komutt.CommandText = "Select * from Ogrenci WHERE KullanlıcıAdı= '" + textBox1.Text + "'And Sifre='" + textBox2.Text + "'";

                    komutt.Parameters.AddWithValue("@KullanlıcıAdı", textBox1.Text);
                    komutt.Parameters.AddWithValue("@Sifre", textBox2.Text);

                    baglan.Open();
                    SqlDataReader oku2 = komutt.ExecuteReader();
                    if (oku2.Read())
                    {
                        this.Hide();
                        MessageBox.Show("Giriş Başarılı");
                        form4.label5.Text = textBox1.Text;
                        
                        while (oku2.Read())
                        {
                            string AdSoyad = oku2["AdSoyad"].ToString();
                            double Vize = Convert.ToDouble(oku2["Vize"]);
                            double Final = Convert.ToDouble(oku2["Final"]);
                            double ort = HesaplaOrtalama(Vize, Final);
                            form4.textBox1.Text = ort.ToString();
                            //form4.listView1.Items.Add(new ListViewItem(new string[] { AdSoyad, Vize.ToString(), Final.ToString() }));

                        } 
                    baglan.Close();
                    form4.ShowDialog();
                    
                   
                    }



                    else
                    {
                        form4.Close();
                        MessageBox.Show("Hatalı Giriş. Kullanıcı Adı veya Şifre Yanlış");
                    }
                }

                baglan.Close();


            }  

        }
       private double HesaplaOrtalama(double vizeNotu, double finalNotu)
        {
            return vizeNotu * 0.4 + finalNotu * 0.6;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Font yeniFont = new Font("Arial", 10, FontStyle.Bold);
            textBox1.Font = yeniFont;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox2.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Font yeniFont = new Font("Arial", 10, FontStyle.Bold);
            textBox2.Font = yeniFont;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 32 || e.KeyChar > 126) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Özel karakterleri ve silme tuşunu engellesin istedim
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 32 || e.KeyChar > 126) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Özel karakterleri ve silme tuşunu engellesin istedim
            }

        }
    }
}


 