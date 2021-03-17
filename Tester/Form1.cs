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

namespace Tester
{
    public partial class Form1 : Form
    {
        static string mycnnstring = "Data Source = ULISSESPC; " +
                "Initial Catalog = test; Integrated Security = True; " +
                "Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; " +
                "ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
        SqlConnection cnn = new SqlConnection(mycnnstring);

        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            
            cnn.Open();
            MessageBox.Show("You Connected to your database Customers!");

        }

        private void DisplayButton_Click(object sender, EventArgs e)
        {
            
            string output = "";

            SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", cnn);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                output = output + reader.GetValue(0) + "-" + reader.GetValue(1) + " " + reader.GetValue(2) + " " + reader.GetValue(3) + "\n";
            }

            DialogResult dialogResult = MessageBox.Show(output);
            
            reader.Close();
            cmd.Dispose();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            
            string lastID = "";
            string lastname = inputLast.Text;
            string firstname = InputFirst.Text;
            string age = InputAge.Text;

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Customers ORDER BY ID DESC", cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            SqlDataAdapter adapter = new SqlDataAdapter();

            while (reader.Read())
            {
                lastID = reader.GetValue(0).ToString();
            }
            reader.Close();

            int nextId = Int32.Parse(lastID) + 1;
            lastID = nextId.ToString();

            string insertSQL = "INSERT INTO Customers VALUES ('" + lastID + "', '" + lastname + "', '" + firstname + "', '" + age + "');";

            cmd = new SqlCommand(insertSQL, cnn);

            adapter.InsertCommand = new SqlCommand(insertSQL, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            reader.Close();
            cmd.Dispose();
            

        }
    }
}
