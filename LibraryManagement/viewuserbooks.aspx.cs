using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Xml.Linq;
using Razorpay.Api;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
namespace LibraryManagement
{ 
    public partial class viewuserbooks : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        int daysDifference = 0;

        private static string key = "rzp_test_m4SNOy01AucT2E"; // API Key ID
        private static string secret = "2lsBWJWRa8NtuVGUXoCmLkpN";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            
            string bookName = "";
            string memName = "";
            string email = "";
            DateTime issuedate;
            DateTime returndate;
            string contactNo = "";
            int amount = 0;

            if (Session["role"].Equals("user"))
            {
                {
                    var closeLink = (Control)sender;
                    GridViewRow row = (GridViewRow)closeLink.NamingContainer;
                    Session["bookId"] = row.Cells[0].Text.Trim();
                    //bookId = row.Cells[0].Text.Trim();
                    TextBox TextBox1 = (TextBox)row.FindControl("TextBox1");
                    TextBox TextBox2 = (TextBox)row.FindControl("TextBox2");
                    Button Button1 = (Button)row.FindControl("Button1");
                    Label Label15 = (Label)row.FindControl("Label15");
                    Button1.Visible = false;

                    
                        if (checkIfIssueExists())
                        {
                            Label15.Text = "This member already has this book.";
                        }
                       
                        Session["startDate"] = TextBox1.Text.Trim();
                        Session["endDate"] = TextBox2.Text.Trim();

                        bool isStartDateValid = DateTime.TryParse(TextBox1.Text, out issuedate);
                        bool isEndDateValid = DateTime.TryParse(TextBox2.Text, out returndate);
                        TimeSpan difference = returndate - issuedate;
                        daysDifference = (int)difference.TotalDays;
                        try
                        {
                            SqlConnection con = new SqlConnection(strcon);
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("select book_cost,book_name from book_master_tbl where book_id='" + Session["bookId"].ToString().Trim() + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                amount = 100 * (Convert.ToInt32(dt.Rows[0]["book_cost"].ToString())) * daysDifference;
                                bookName = dt.Rows[0]["book_name"].ToString().Trim();
                            }
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                            }
                            cmd = new SqlCommand("select full_name,email,contact_no from member_master_tbl where member_id='" + Session["username"].ToString().Trim() + "'", con);
                            da = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                memName = dt.Rows[0]["full_name"].ToString().Trim();
                                email = dt.Rows[0]["email"].ToString().Trim();
                                contactNo = dt.Rows[0]["contact_no"].ToString().Trim();
                            }
                            // Initialize Razorpay client with API key and secret
                            Dictionary<string, object> input = new Dictionary<string, object>();

                            // Create an order to initiate the payment
                            input.Add("amount", amount); // Amount in paise (50000 paise = Rs 500)
                            input.Add("currency", "INR");
                            input.Add("receipt", "order_rcptid_11");
                            input.Add("payment_capture", 1); // Automatically capture payment

                            // Create Razorpay client and order
                            RazorpayClient client = new RazorpayClient(key, secret);
                            Order order = client.Order.Create(input);

                            // Get order ID and other details
                            string orderId = order["id"].ToString();

                            // Store the order ID in session to use in the callback after payment success
                            Session["razorpay_order_id"] = orderId;

                            // Redirect the user to Razorpay payment page (you will need to use Razorpay's checkout.js for this)
                            Response.Redirect("razorpay.aspx?orderId=" + orderId);
                        }
                        catch (Exception ex)
                        {
                            // Handle exception (log it or display a message)
                            Response.Write("Error: " + ex.Message);
                        }
                    
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty((string)Session["role"]))
            {
                Response.Redirect("userlogin.aspx");  
            }
           
        }

        
        protected void ValidateDate(object sender, ServerValidateEventArgs e)
        {
            DateTime tempDate;
            
            bool isValidDate = DateTime.TryParse(e.Value, out tempDate);
           

            // Check if the date is valid and within a certain range (optional)
            if (isValidDate && tempDate <= new DateTime(2025, 1, 1) && tempDate >= DateTime.Now )
            {
                e.IsValid = true; // Valid date
            }
            else
            {
                e.IsValid = false; // Invalid date or out of range
            }
        }

        protected void ValidateReturnDate(object source, ServerValidateEventArgs args)
        {
            TextBox TextBox1 = (TextBox)((CustomValidator)source).NamingContainer.FindControl("TextBox1");
            TextBox TextBox2 = (TextBox)((CustomValidator)source).NamingContainer.FindControl("TextBox2");

            DateTime startDate, endDate;

            // Check if both dates are valid
            bool isStartDateValid = DateTime.TryParse(TextBox1.Text, out startDate);
            bool isEndDateValid = DateTime.TryParse(TextBox2.Text, out endDate);

            // Validate that the return date is after the issue date
            args.IsValid = isStartDateValid && isEndDateValid && endDate > startDate;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button Button1 = (Button)e.Row.FindControl("Button1");
                TextBox TextBox1 = (TextBox)e.Row.FindControl("TextBox1");
                TextBox TextBox2 = (TextBox)e.Row.FindControl("TextBox2");
                LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("LinkButton1");
                Label Label13 = (Label)e.Row.FindControl("Label13");
                Label Label14 = (Label)e.Row.FindControl("Label14");
                if (Button1 != null && (Session["role"] == null || Session["role"].Equals("admin")))
                {
                    Button1.Visible = false;
                    TextBox1.Visible = false;
                    TextBox2.Visible = false;
                    Label13.Visible = false;
                    Label14.Visible = false;
                    LinkButton1.Visible = false;
                }
               
                if(TextBox1 != null && TextBox2 != null && string.IsNullOrEmpty((string)Session["role"]))
                {
                    Button1.Visible = true;
                    TextBox1.Visible = false;
                    TextBox2.Visible = false;
                    Label13.Visible = false;
                    Label14.Visible = false;
                    LinkButton1.Visible = false;
                }
            }
        }


        bool checkIfIssueExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from book_issue_tbl where member_id='" + Session["username"].ToString().Trim() + "' and book_id= '" + Session["bookId"].ToString().Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
    }
}