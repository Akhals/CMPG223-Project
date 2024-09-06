using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InventoryManagementSystemCMPG223
{
    public partial class UpdateProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //DEPENDENCIES

        //string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\InventoryManagementSystemDB.mdf;Integrated Security=True";

        readonly string ConnString = @"Data Source=GIVEN\SQLEXPRESS;Initial Catalog=InventoryManagementSystemDB;Integrated Security=True;Trust Server Certificate=True";
        SqlConnection conn;
        SqlDataAdapter adapter;
        SqlCommand cmd;
        DataSet ds;



        //UPDATE DATABASE
        public void UpdateAProduct(string query, string keyword)
        {
            try
            {
                conn = new SqlConnection(ConnString);
                adapter = new SqlDataAdapter();
                conn.Open();

                // Get product details from the form
                int id = Int32.Parse(ProductId.Text);
                string name = Name.Text;
                string description = Descriptions.Text; // Access the Description textbox value
                double price = Double.Parse(Price.Text);
                double size = Double.Parse(Size.Text);

                cmd = new SqlCommand(query, conn);

                // Parameters
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                // Add description parameter
                cmd.Parameters.AddWithValue("@size", size);
                cmd.Parameters.AddWithValue("@price", price);

                adapter.UpdateCommand = cmd;

                int countUpdated = adapter.UpdateCommand.ExecuteNonQuery();
                if (countUpdated > 0)
                {
                    FeedbackLbl.Text = $"Successfully updated {keyword}";
                }
                else
                {
                    FeedbackLbl.Text = $"Failed to update {keyword}";
                }
            }
            catch (SqlException ex)
            {
                FeedbackLbl.Text = ex.ToString();
            }
            catch (Exception e)
            {
                FeedbackLbl.Text = e.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
    }   
}