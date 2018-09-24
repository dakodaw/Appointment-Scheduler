using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Software2Project_DakodaWillden_V1.Classes;

namespace Software2Project_DakodaWillden_V1
{
    public partial class Frm_AddCustomer : Form
    {
        // Class Properties
        public string loggedInUser { get; set; }
        public string connecterString { get; set; }
        public int selectedCustomer { get; set; } = 0;
        List<Address> Addresses;
        public Customer myCustomer;
        public bool customerAdded { get; set; } = false;

        // Constructors/Loaders
        /// <summary>
        /// Main Constructor
        /// </summary>
        public Frm_AddCustomer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles what Loads with the Add/Update Customer Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCustomer_Load(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                List<Address> addresses = new List<Address>();
                fillAddressDropdown(addresses);
                Txt_Address1.Focus();

                //Check to see if a selected Customer is set from the main form. If it was, 
                //  we are updating the selected record
                if (selectedCustomer != 0)
                {
                    this.Text = "Update Customer";
                    Btn_Add.Text = "Update";
                    Txt_CustId.Text = selectedCustomer.ToString();
                    Customer updateCustomer = new Customer(selectedCustomer, conn);
                    Txt_CustName.Text = updateCustomer.customerName;
                    Txt_Address1.Text = updateCustomer.addressObject.address;
                    Txt_Address2.Text = updateCustomer.addressObject.address2;
                    Txt_City.Text = updateCustomer.addressObject.cityObject.cityName;
                    Txt_Country.Text = updateCustomer.addressObject.cityObject.countryObject.country;
                    Txt_PhoneNumber.Text = updateCustomer.addressObject.phone;
                    Txt_PostalCode.Text = updateCustomer.addressObject.postalCode;
                }

            }
            
        }


        //Event Handlers
        /// <summary>
        /// Closes the Add Customer Form when cancel is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    
        /// <summary>
        /// Attempts to Update the object and Database when Update button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            string successMessage = "";
            using (var conn = new MySqlConnection(connecterString))
            {
                try
                {
                    //Update information with Information with Textboxes, the user and dates updated
                    if (selectedCustomer != 0)
                    {
                        myCustomer = new Customer(selectedCustomer, conn);
                        // If the Address is changing, create a new address, and link the new address to the customer
                        string textBoxCheckAddress = Txt_Address1.Text + Txt_Address2.Text + Txt_City.Text;
                        string databaseCheckAddress = myCustomer.addressObject.address + myCustomer.addressObject.address2 + myCustomer.addressObject.cityObject.cityName;
                        int updatingAddressId = (int)myCustomer.addressID;

                        //Set the values to the customer Object based on the 
                        myCustomer.customerName = Txt_CustName.Text;
                        myCustomer.lastUpdate = DateTime.Now;
                        myCustomer.lastUpdateBy = loggedInUser;


                        if (textBoxCheckAddress != databaseCheckAddress)
                        {
                            Address updatedAddress = new Address(Txt_Address1.Text, Txt_Address2.Text, Txt_City.Text, Txt_Country.Text, Txt_PostalCode.Text, Txt_PhoneNumber.Text, loggedInUser, conn);
                            myCustomer.addressID = updatedAddress.addressId;
                            updatingAddressId = myCustomer.addressID;
                        }



                        string myUpdateCustomersQuery = "UPDATE customer c JOIN address a ON c.addressId = a.addressId "
                            + "SET c.customerName = '" + Txt_CustName.Text.ToString() + "', c.addressId = " 
                            + updatingAddressId.ToString() + ", c.lastUpdate = '" + DateTime.Now.ToString("yyyy-MM-dd")
                            + "', c.lastUpdateBy = '" + loggedInUser + "', a.phone = '" + Txt_PhoneNumber.Text 
                            + "', a.lastUpdate = '" + DateTime.Now.ToString("yyyy-MM-dd")
                            + "', a.lastUpdateBy = '" + loggedInUser + "' "
                            + "WHERE c.customerId = " + myCustomer.customerID.ToString();
                        //DataChanges dt = new DataChanges();
                        DataChanges.ExecuteMySqlCommand(myUpdateCustomersQuery, conn);
                        customerAdded = true;
                        successMessage = "Customer Information successfully Updated";
                    }
                    else
                    {
                        //int id = int.Parse(Txt_CustId.Text);
                        myCustomer = new Customer(Txt_CustName.Text, Txt_Address1.Text, Txt_Address2.Text, Txt_City.Text, Txt_Country.Text, Txt_PostalCode.Text, Txt_PhoneNumber.Text, loggedInUser, conn);
                        Address myAddress = myCustomer.addressObject;
                        Txt_CustId.Text = myCustomer.customerID.ToString();
                        Txt_CustName.Text = myCustomer.customerName;
                        Txt_Address1.Text = myAddress.address;
                        Txt_Address2.Text = myAddress.address2;
                        Txt_City.Text = myAddress.cityObject.cityName;
                        Txt_Country.Text = myAddress.cityObject.countryObject.country;
                        Txt_PhoneNumber.Text = myAddress.phone;
                        Txt_PostalCode.Text = myAddress.postalCode;
                        successMessage = "Customer Successfully Added";
                    }
                    MessageBox.Show(successMessage);

                    //If customer is created, set customerAdded = true to use if creation is cancelled
                    customerAdded = true;
                    Close();

                }
                catch(Exception innerException)
                {
                    MessageBox.Show("Add/Update Failed \n", innerException.ToString());
                }
            }
        }

        /// <summary>
        /// Populates the form with Address information if previously entered address is present
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_AddressPopulate_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int newIndex = comboBox_AddressPopulate.SelectedIndex;
                Address a = Addresses.ElementAt(newIndex);
                int selectedAddressId = a.addressId;

                Txt_Address1.Text = a.address;
                Txt_Address2.Text = a.address2;
                Txt_City.Text = a.cityObject.cityName;
                Txt_Country.Text = a.cityObject.countryObject.country;
                Txt_PhoneNumber.Text = a.phone;
                Txt_PostalCode.Text = a.postalCode;
            }
            catch
            {

            }
        }

        // Class Methods
        /// <summary>
        /// Gets a list of Addresses, and updates the list with Database info
        /// </summary>
        /// <param name="addresses"></param>
        public void fillAddressDropdown(List<Address> addresses)
        {
            using (var conn = new MySqlConnection(connecterString))
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM address";
                MySqlDataReader reader1 = DataChanges.GetMySqlReader(conn, cmd.CommandText);
                //Use this to see if the country exists, and to count the number of countries
                while (reader1.Read())
                {
                    var AddressId = (int)reader1["addressId"];
                    var Address1 = reader1["address"].ToString();
                    var Address2 = reader1["address2"].ToString();
                    var CityId = (int)reader1["cityId"];
                    var PostalCode = reader1["postalCode"].ToString();
                    var Phone = reader1["phone"].ToString();
                    var CreatedBy = reader1["createdBy"].ToString();
                    var LastUpdateBy = reader1["lastUpdateBy"].ToString();
                    Address myAddress = new Address(AddressId, Address1, Address2, CityId, PostalCode, Phone, CreatedBy, LastUpdateBy);
                    addresses.Add(myAddress);
                }
                conn.Close();
                cmd.Connection.Close();

                foreach (var addr in addresses)
                {
                    // Populate a new row on the combobox
                    comboBox_AddressPopulate.Items.Add(addr.address.ToString() + ", " + addr.address2.ToString());
                    // fill the city object for the address object
                    addr.cityObject = new City(addr.cityId, loggedInUser, conn);
                }
                //Set the list equal to the class list
                Addresses = addresses;
            }
        }
    }
}
