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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;

namespace LibraryManagementSystem
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public SqlConnection connection()       //Polymorphic Refactoring reason :because all class will need this object to intereact with sql so declare in main class that make this class object and used in sub classes
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-HFH22KR\\MSSQLSERVER01;Integrated Security=True;Initial Catalog=LibraryManagementSystem");
            return con;                         //Delcared SQl Connection one time so it will not repeat in sub classes, just use this by making object of this class
        }
        public SqlCommand cmd(SqlConnection con, string query)      //Polymorphic Refactoring reason :because all class will need this object to intereact with sql so declare in main class that make this class object and used in sub classes
        {
            SqlCommand com = new SqlCommand(query, con);
            return com;                         //Delcared SQl Command one time so it will not repeat in sub classes, just use this by making object of this class
        }
        public void reset_textfields()          //after entering user and pass fields will be reset
        {
            username.Text = "";
            pass.Text = "";
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)      //this method will check that user and pass entered is correct or not if correct that it will redirect in next page and else notify 
        {
            int a = 0;
            test t1 = new test(); //For Black Box Testing declare an object of test class
            bool check = t1.admincheck(username.Text, pass.Text); 
            if (check) { //Black Box testing here if condition true so this method will start other wise method don't have to start
                try                                             //try catch so if there is any connection error so program will not terminate it just notify us
                {
                    var con = connection();
                    var com = cmd(con, "select pass from Admin where Username='" + username.Text + "'"); //sql command sending to sql with parameter whcih we want to do //we can sya that there is testuibng or pre condtion
                    SqlDataReader reader;
                    con.Open();
                    reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader["pass"].ToString() == pass.Text) //post condtion
                        {
                            home h1 = new home();
                            h1.Show();              //redirect on nect page
                            reset_textfields();
                            ++a;                //checking that if username and pass is corrrect so a will be increament
                            break;
                        }
                    }
                    if (a == 0)
                    {
                        reset_textfields();
                        MessageBox.Show("invalid users");
                    }
                
                    con.Close();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
            else //if test method return false so this method will exit 
            {
                MessageBox.Show("please fill all fields!!");
            }


        }
    }
}
