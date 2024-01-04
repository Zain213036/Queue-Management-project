using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QM_Part_2_
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            InitializeTimer();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-CBQO5S5\\SQLEXPRESS;Initial Catalog=Queuing;Integrated Security=True");

        private void InitializeTimer()
        {
            Timer timer = new Timer();
            timer.Interval = (1 * 1000);
            timer.Tick += new EventHandler(Timer1_Tick);
            timer.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Refreshbtn.PerformClick();
        }

        public void DisplayCashierQueue()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-CBQO5S5\\SQLEXPRESS;Initial Catalog=SMMSPro;Integrated Security=True"))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TicketNumber FROM CashierQueue", conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("TicketNumber", typeof(string));
                    dt.Load(rdr);

                    Ticket_list.Items.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (you can log it, display an error message, etc.)
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            // Remove the ticket from the queue
            string query = "DELETE FROM CashierQueue WHERE Id = (SELECT MIN(Id) FROM CashierQueue)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();

            DisplayCashierQueue();
        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            DisplayCashierQueue();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 85, 100, 85);
        }
    }
}
