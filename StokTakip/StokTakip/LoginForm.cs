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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            passwordInit();
          
        }

        public void passwordInit()
        {
            textBox2.PasswordChar = '*';
        }

        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\VT.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                connection.Open();
                OleDbCommand command;                
                command= new OleDbCommand("Select * From Kullanıcılar where KullanıcıAdı='"+textBox1.Text+"'and Sifre='"+textBox2.Text+"'",connection);
                OleDbDataReader read = command.ExecuteReader();
                while (read.Read())
                {             
                    count = count + 1;
                }

                if(count==1)
                {
                    
                    AnaForm anaform = new AnaForm();
                    anaform.userName = textBox1.Text;
                    anaform.ShowDialog(); 
                    this.Hide(); 

                }
                else
                {
                    MessageBox.Show("LOGIN BASARISIZ");
                }
                connection.Close();
            }


            catch(Exception ex)
            {               
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }
    }
}
