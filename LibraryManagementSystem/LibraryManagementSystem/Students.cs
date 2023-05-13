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
using System.Linq.Expressions;

namespace LibraryManagementSystem
{
    public partial class Students : Form
    {
        int[] books = new int[3];
        Admin ad = new Admin();
        int b1;
        int b2;
        int b3;
        public Students()
        {
            InitializeComponent();
        }
        public void updatestocks()   //stocks update of book
        {
            var con = ad.connection();

            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] != 0)
                {
                    var query = "update Books set Book_Quantity = Book_Quantity - " + 1 + " where Book_ID=" + books[i] + "";
                    var cmd = ad.cmd(con, query);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try {
               // if(sname.Text != "" && semail.Text != "" && scontact.Text != "") {          //Code smell detected!!
                bool fields = sname.Text != "" && semail.Text != "" && scontact.Text != ""; //Explaining variable Refactoring
                if (fields)                                                                 //Explaining Variable Refactoring 
                {
                    books[0] = Convert.ToInt32(idbook1.Value);
                    books[1] = Convert.ToInt32(idbook2.Value); 
                    books[2] = Convert.ToInt32(idbook3.Value);
                    var con = ad.connection();
                    var query = "insert into Students values('" + sname.Text + "','" + semail.Text + "', '" + scontact.Text + "'," + idbook1.Value + "," + idbook2.Value + "," + idbook3.Value + ")";
                    var cmd = ad.cmd(con, query);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    updatestocks();
                    sname.Text = "";
                    semail.Text = "";
                    scontact.Text = "";
                    idbook1.Value = 0;
                    idbook2.Value = 0;
                    idbook3.Value = 0;
                    MessageBox.Show("added student successfully!!");
                }
                else
                {
                    MessageBox.Show("please fill all students info..");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            searching(Convert.ToInt32(stuid.Text));

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var con = ad.connection();
                var cmd = ad.cmd(con, "update Students set s_name='" + stuname.Text + "', s_email='" + stuemail.Text + "', s_number='" + stucontact.Text + "' where s_id=" + Convert.ToInt32(stuid.Text) + "");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("data updated!!");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        
        /*private void stubooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }*/

        private void label13_Click(object sender, EventArgs e)
        {

        }

        public void searching(int id)       //Extract method reason this method was repeating two time in this class then declared in one time and call them many time in button2_click and button4_click
        {
            try                                 //THIS METHOD WORKS THAT IN SEARCH FIELD WHICH ID ENTERED SO CHECK DATABASE AND GET THESE ID DATA AND FILL FIELDS
            {
                var con = ad.connection();
                var cmd = ad.cmd(con, "select * from Students where s_id=" + id + "");
                SqlDataReader reader;
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stuname.Text = reader["s_name"].ToString();
                    stuemail.Text = reader["s_email"].ToString();
                    stucontact.Text = reader["s_number"].ToString();
                    textBox1.Text = reader["s_book1_id"].ToString();
                    textBox2.Text = reader["s_book2_id"].ToString();
                    textBox3.Text = reader["s_book3_id"].ToString();
                    b1 = Convert.ToInt32(reader["s_book1_id"].ToString());
                    b2 = Convert.ToInt32(reader["s_book2_id"].ToString());
                    b3 = Convert.ToInt32(reader["s_book3_id"].ToString());


                }
                con.Close();

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        
        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                var x = Convert.ToInt32(textBox1.Text);         //updating student book1 of student 
                if (x != b1)
                {
                    var con = ad.connection();
                    var cmd = ad.cmd(con, "update Students set s_book1_id=" + x + " where s_id=" + Convert.ToInt32(stuid.Text) + "");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    var con1 = ad.connection();
                    var cmd2 = ad.cmd(con1, "update Books set Book_Quantity=Book_Quantity - " + 1 + " where Book_ID=" + x + "");
                    var cmd1 = ad.cmd(con1, "update Books set Book_Quantity=Book_Quantity + " + 1 + " where Book_ID=" + b1 + "");
                    con1.Open();
                    cmd2.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con1.Close();


                    searching(Convert.ToInt32(stuid.Text));
                }
            }catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {                               //updating student book2 also stock
                var x = Convert.ToInt32(textBox2.Text);
                if (x != b2)
                {
                    var con = ad.connection();
                    var cmd = ad.cmd(con, "update Students set s_book2_id=" + x + " where s_id=" + Convert.ToInt32(stuid.Text) + "");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    var con1 = ad.connection();
                    var cmd2 = ad.cmd(con1, "update Books set Book_Quantity=Book_Quantity - " + 1 + " where Book_ID=" + x + "");
                    var cmd1 = ad.cmd(con1, "update Books set Book_Quantity=Book_Quantity + " + 1 + " where Book_ID=" + b2 + "");
                    con1.Open();
                    cmd2.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con1.Close();


                    searching(Convert.ToInt32(stuid.Text));
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try                                             //updating student book3 also stock
            {
                var x = Convert.ToInt32(textBox3.Text);
                if (x != b3)
                {
                    var con = ad.connection();
                    var cmd = ad.cmd(con, "update Students set s_book3_id=" + x + " where s_id=" + Convert.ToInt32(stuid.Text) + "");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    var con1 = ad.connection();
                    var cmd2 = ad.cmd(con1, "update Books set Book_Quantity=Book_Quantity - " + 1 + " where Book_ID=" + x + "");
                    var cmd1 = ad.cmd(con1, "update Books set Book_Quantity=Book_Quantity + " + 1 + " where Book_ID=" + b3 + "");
                    con1.Open();
                    cmd2.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    con1.Close();


                    searching(Convert.ToInt32(stuid.Text));
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            //variable explaining
            //methods extract/decompose
            //polymorphic

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {


        }
    }
}
