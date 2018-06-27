using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StokTakip
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\VT.mdb");
        OleDbCommand command;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                connection.Open();
                if (KullanıcıAdı.Text == "" || Sifre.Text == "" || Ad.Text == "" || Soyısım.Text == "")
                {
                    MessageBox.Show("BOŞ YER KALAMAZ!!");
                }
                else
                {
                    command = new OleDbCommand("Select * From Kullanıcılar where KullanıcıAdı='" + KullanıcıAdı.Text + "'", connection);
                    OleDbDataReader read = command.ExecuteReader();

                    while (read.Read())
                    {
                        count = count + 1;
                    }

                    if (count > 0)
                    {
                        MessageBox.Show("BU KULLANICI İSMİ ZATEN MEVCUT!!");

                    }
                    else
                    {
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        command.CommandText = "Insert into Kullanıcılar (KullanıcıAdı, Sifre, Ad ,Soyad) values('" + KullanıcıAdı.Text + "','" + Sifre.Text + "','" + Ad.Text + "','" + Soyısım.Text+"')";
                        command.ExecuteNonQuery();
                        command.Dispose();
                        MessageBox.Show("KAYIT BASARILI");

                        LoginForm loginFrorm = new LoginForm();
                        loginFrorm.Show();
                        this.Hide();
                    }
                   
                }
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }

        }
    
    }
}
