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
using System.Windows.Forms.VisualStyles;

namespace LibraryManagementSystem
{
    public partial class Books : Form
    {
        
        public Books()
        {
            InitializeComponent();
        }
        public void loading()   //when page is loading so listview will filled and fetch dtaa from books table
        {
            Admin ad = new Admin();
            var con = ad.connection();
            var cmd = ad.cmd(con, "select * from Books");
            SqlDataReader reader;
            con.Open();
           
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listView1.Items.Add(reader["Book_name"].ToString());
                
                listView1.Items.Add(reader["Book_ID"].ToString());

            }
            con.Close();
        }
        private void Books_Load(object sender, EventArgs e)         //when page is loading
        {
            loading();
        }

        private void button1_Click(object sender, EventArgs e)      //on clickbutton 1 will submit all data which are in fields
        {
            
            Admin ad = new Admin();
            var con=ad.connection();
            //if (bookname.Text != "" && bookauthor.Text != "" && bookquantity.Value != 0)      //code smell detected
                bool tfieldsname = bookname.Text != "" && bookauthor.Text != "" && bookquantity.Value != 0;     //Explaining variable refactoring
            if(tfieldsname) //we can say that this is pre condtion
            {
                var cmd = ad.cmd(con, "insert into Books values('" + bookname.Text + "','" + bookauthor.Text + "','" + bookquantity.Value + "')");
                con.Open();
                MessageBox.Show("Book Added Successfully!!");
                cmd.ExecuteNonQuery();
                clear();
                con.Close();
            }
            else
            {
                MessageBox.Show("please fill all fields properly...");
            }


        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        public Boolean checking(string a)
        {
            try
            {
                int b = Convert.ToInt32(a);
                return true;
            }
            catch(Exception x)
            {
                return false;
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e) //when a item from listview selected so this method will call
        {
            try {
                bool a = checking(listView1.FocusedItem.Text);  // Explaining Variable 
                if (a)                                          // Explaining Variable
                //if (checking(listView1.FocusedItem.Text))     //Code smell detected!
                {
                    Admin ad = new Admin();
                    var con = ad.connection();
                    var cmd = ad.cmd(con, "select * from Books Where Book_ID='" + Convert.ToInt32(listView1.FocusedItem.Text) + "'");
                    SqlDataReader reader;
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //filling text fields and fetch data from sql
                        BID.Value = Convert.ToInt64(reader["Book_ID"].ToString());
                        name.Text = reader["Book_name"].ToString();
                        author.Text = reader["Book_Author"].ToString();
                        quantity.Value = Convert.ToInt32(reader["Book_Quantity"].ToString());
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("please select id's of books..");
                    loading();
                }
                }
                
            
            catch (Exception x )
            {
                MessageBox.Show(x.Message);
            }
            }
        

        private void button2_Click(object sender, EventArgs e)   //onlick data will update
        {
            try
            {
                Admin ad = new Admin();
                var con = ad.connection();
                var cmd = ad.cmd(con, "update Books set Book_name='" + name.Text + "',Book_author='" + author.Text + "',Book_quantity=" + quantity.Value + " where Book_ID=" + BID.Value + "");
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                clear();
                MessageBox.Show("Book Updated Sucessfully!");
            }
            catch(Exception j)
            {
                MessageBox.Show(j.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e) //refereshing list
        {
            listView1.Items.Clear(); 
            loading();
        }
        public void clear()
        {
            BID.Value = 0;
            name.Text = "";
            author.Text = "";
            quantity.Value = 0; 
        }
        private void button3_Click(object sender, EventArgs e) //updating books when a book is deleted so from students allocated book will 0
        {
            Admin ad = new Admin();
            var con = ad.connection();
            var cmd1 = ad.cmd(con, "update students set s_book1_id=" + 0 + " where s_book1_id=" + BID.Value + "");
            var cmd2 = ad.cmd(con, "update students set s_book2_id=" + 0 + " where s_book2_id=" + BID.Value + "");
            var cmd3 = ad.cmd(con, "update students set s_book3_id=" + 0 + " where s_book3_id=" + BID.Value + "");
            var cmd = ad.cmd(con, "delete Books where Book_ID='"+BID.Value+"'");
            con.Open();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Deleted Book Successfully!");
            listView1.Items.Clear();
            clear();
            loading();

        }
    }
}
