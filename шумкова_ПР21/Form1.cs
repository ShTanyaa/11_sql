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

namespace шумкова_ПР21
{
    public partial class Form1 :Form
    {
        SqlDataAdapter adptr;
        SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
        DataTable table;
        public Form1 ()
        {
            InitializeComponent();
            tbl_view("select * from dbo.Корабль");
            dataGridView1.DataSource = table;
            tbl_view("select * from dbo.Порт");
            dataGridView2.DataSource = table;
            tbl_view("select * from dbo.Посещение");
            dataGridView3.DataSource = table;
        }
        private void tbl_view (string mes)
        {
            connect.Open();
            adptr = new SqlDataAdapter(mes, connect);
            table = new DataTable();
            adptr.Fill(table);
            connect.Close();
        }
        public void korbl ()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Корабль", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView1.DataSource = table;
            connect.Close();
        }
        public void port ()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Порт", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView2.DataSource = table;
            connect.Close();
        }
        public void posechenie ()
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter("select * from Посещение", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView3.DataSource = table;
            connect.Close();
        }
        public DataTable Zapros (string zapros)
        {
            //запрос в базу
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter(zapros, connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            connect.Close();
            return table;
        }
        public void combobox ()
        {
            DataTable k = Zapros("select название from Корабль");
            for (int i = 0; i < k.Rows.Count; i++)
            {
                comboBox1.Items.Add(k.Rows[i][0]);
            }
            DataTable pr = Zapros("select название from Порт");
            for (int i = 0; i < pr.Rows.Count; i++)
            {
                comboBox3.Items.Add(pr.Rows[i][0]);
            }
            DataTable ps = Zapros("select капитан from Корабль");
            for (int i = 0; i < ps.Rows.Count; i++)
            {
                comboBox2.Items.Add(ps.Rows[i][0]);
            }

        }
        private void Form1_Load (object sender, EventArgs e)
        {
            combobox();
        }

        private void button1_Click (object sender, EventArgs e)
        {
            string name, portpr, displ, captn, kod;
            name = textBox1.Text;
            portpr = textBox2.Text;
            displ = numericUpDown1.Text;
            captn = textBox3.Text;
            kod = numericUpDown3.Text;

            if (name == "" || portpr == "" || displ == "0" || captn == "" || kod == "0")
            {
                MessageBox.Show("Не все поля ввода данных заполнены", "error");
            } else
            {
                tbl_view($"insert into dbo.Корабль(код,название,водоизмещение,портприписки,капитан) values ('{kod}','{name}','{displ}','{portpr}','{captn}')");
                tbl_view("select * from dbo.Корабль");
                dataGridView1.DataSource = table;
            }
        }

        private void button4_Click (object sender, EventArgs e)
        {
            int stroca = dataGridView1.CurrentCell.RowIndex;
            string number = Convert.ToString(dataGridView1[0, stroca].Value);
            Zapros($"DELETE FROM [Корабль]WHERE [код]='{number}'");
            Zapros($"DELETE FROM [Посещение]WHERE [корабльID]='{number}'");
            korbl();
            port();
            posechenie();
            combobox();
        }

        private void button2_Click (object sender, EventArgs e)
        {
            string name, country, category, kod;
            name = textBox5.Text;
            country = textBox4.Text;
            category = numericUpDown5.Text;
            kod = numericUpDown4.Text;

            if (name == "" || country == "" || category == "" || kod == "")
            {
                MessageBox.Show("Не все поля ввода данных заполнены", "error");
            } else
            {
                tbl_view($"insert into dbo.Порт(код,название,страна,категория) values ('{kod}','{name}','{country}','{category}')");
                tbl_view("select * from dbo.Порт");
                dataGridView2.DataSource = table;
            }
        }

        private void button5_Click (object sender, EventArgs e)
        {
            int stroca = dataGridView1.CurrentCell.RowIndex;
            string number = Convert.ToString(dataGridView1[0, stroca].Value);
            Zapros($"DELETE FROM [Порт]WHERE [код]='{number}'");
            Zapros($"DELETE FROM [Посещение]WHERE [портID]='{number}'");
            korbl();
            port();
            posechenie();
            combobox();
        }

        private void button3_Click (object sender, EventArgs e)
        {
            string date1, date2, num, purpose, portId, korblId, kod;
            date1 = textBox9.Text;
            date2 = textBox8.Text;
            num = numericUpDown2.Text;
            purpose = textBox7.Text;
            portId = textBox11.Text;
            korblId = textBox10.Text;
            kod = numericUpDown6.Text;

            if (date1 == "" || date2 == "" || num == "0" || purpose == "" || portId == "" || korblId == "" || kod == "0")
            {
                MessageBox.Show("Не все поля ввода данных заполнены", "error");
            } else
            {
                tbl_view($"insert into dbo.Посещение(код,дата прибытия,дата убытия,номер причала,портID,корабльID) values ('{kod}','{date1}','{date2}','{num}','{purpose}','{portId}','{korblId}')");
                tbl_view("select * from dbo.Посещение");
                dataGridView3.DataSource = table;
            }
        }

        private void button6_Click (object sender, EventArgs e)
        {
            int stroca = dataGridView1.CurrentCell.RowIndex;
            string number = Convert.ToString(dataGridView1[0, stroca].Value);
            Zapros($"DELETE FROM [Порт]WHERE [код]='{number}'");
            Zapros($"DELETE FROM [Корабль]WHERE [корабльID]='{number}'");
            korbl();
            port();
            posechenie();
            combobox();
        }

        private void tabPage4_Click (object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged (object sender, EventArgs e)
        {
        }

        private void button7_Click (object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter($"select * from Корабль where название='{comboBox1.Text}'", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView4.DataSource = table;
            connect.Close();
        }

        private void comboBox3_SelectedIndexChanged (object sender, EventArgs e)
        {
           
        }

        private void button9_Click (object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter($"select * from Порт where название='{comboBox3.Text}'", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView4.DataSource = table;
            connect.Close();
        }

        private void button8_Click (object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=PC325L01;Initial Catalog=shumkova_11var;Integrated Security=True");
            connect.Open();
            SqlDataAdapter adptr = new SqlDataAdapter($"select * from Корабль where капитан='{comboBox2.Text}'", connect);
            DataTable table = new DataTable();
            adptr.Fill(table);
            dataGridView4.DataSource = table;
            connect.Close();
        }
    }
}
