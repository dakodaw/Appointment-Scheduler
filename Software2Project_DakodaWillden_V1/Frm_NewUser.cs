using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Software2Project_DakodaWillden_V1.Classes;

namespace Software2Project_DakodaWillden_V1
{
    public partial class Frm_NewUser : Form
    {
        //Class Variables
        public string connecterString { get; set; }
        public string loggedInUser { get; set; }

        //Constructor
        /// <summary>
        /// General Constructor
        /// </summary>
        public Frm_NewUser()
        {
            InitializeComponent();
        }

        //Event Handlers
        /// <summary>
        /// Adds a New User when pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                try
                {
                    User newUser = new User(Txt_NewUserName.Text, Txt_Password.Text, loggedInUser, conn);
                    MessageBox.Show("User Added Successfully");
                    Close();
                }
                catch(Exception ee)
                {
                    MessageBox.Show(ee.InnerException.ToString());
                }
            }
        }

        /// <summary>
        /// Close the New User Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
