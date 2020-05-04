using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace demo_northwind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            var connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Select * from Contacts", conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(dataReader);

                            this.dgCustomers.ItemsSource = dataTable.DefaultView;
                        }

                    } catch
                    {
                        MessageBox.Show("An error occured!");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}