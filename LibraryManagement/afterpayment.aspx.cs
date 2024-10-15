using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using System.Configuration;
namespace LibraryManagement
{
    public partial class afterpayment : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null || Session["username"].ToString() == "")
                {
                    Response.Write("<script>alert('Session Expired. Please Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }

            if (!IsPostBack)
            {
                BindTableData();
                bookRequest();
            }
            // Retrieve the payment ID from the query string
            string paymentId = Request.QueryString["payment_id"];
            string orderId = Session["razorpay_order_id"].ToString();
            // You can use the paymentId to verify payment or update the database
            // Example: Store payment details in your database
            // Implement the verification logic here (optional)
            
        }
        private void BindTableData()
        {
            string paymentId = Request.QueryString["payment_id"];
            string orderId = Session["razorpay_order_id"].ToString();

            TableRow row = new TableRow();
            TableCell cell1 = new TableCell { Text = "Transaction ID" };
            TableCell cell2 = new TableCell { Text = paymentId.Trim() };
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            StyledTable.Rows.Add(row);

            row = new TableRow();
            cell1 = new TableCell { Text = "Order ID" };
            cell2 = new TableCell { Text = orderId.Trim() };
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            StyledTable.Rows.Add(row);
                       
        }

        void bookRequest()
        {

            string memId = Session["username"].ToString().Trim();
            string bookId = Session["bookId"].ToString().Trim();
            string startDate = Session["startDate"].ToString().Trim();
            string endDate = Session["endDate"].ToString().Trim();
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("select * from book_master_tbl where book_id=@bookId", con);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()) > 0)
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd = new SqlCommand("insert into req_issue_tbl (req_mem_id,req_book_id,issue_date,due_date,issue_status) values(@req_mem_id,@req_book_id,@issue_date,@due_date,@issue_status)", con);
                        cmd.Parameters.AddWithValue("@req_mem_id", memId);
                        cmd.Parameters.AddWithValue("@req_book_id", bookId);
                        cmd.Parameters.AddWithValue("@issue_date", startDate.Trim());
                        cmd.Parameters.AddWithValue("@due_date", endDate.Trim());
                        cmd.Parameters.AddWithValue("@issue_status", "pending");

                        cmd.ExecuteNonQuery();
                        con.Close();
                       // Response.Write("<script>alert('Admin will be notified.');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Currently out of stock.');</script>");
                    }
                }
                else
                {

                    Response.Write("<script>alert('Wrong Book ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}