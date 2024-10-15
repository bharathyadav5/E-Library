using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagement
{
    public partial class userprofile : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        string bookId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null || Session["username"].ToString() == "")
                {
                    Response.Write("<script>alert('Session Expired. Please Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                else 
                {
                    if (!Page.IsPostBack)
                    {
                        getUserData();
                        getUserPersonalDetails();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
        }

        //update
        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime dob;
            try
            {
                if (Session["username"].ToString() == "" || Session["username"] == null)
                {
                    Response.Write("<script>alert('Session Expired. Please Login Again');</script>");
                    Response.Redirect("userlogin.aspx");
                }
                // Check if all fields are filled
                else if (string.IsNullOrEmpty(TextBox3.Text) ||
                    string.IsNullOrEmpty(TextBox1.Text) ||
                    string.IsNullOrEmpty(TextBox4.Text) ||
                    string.IsNullOrEmpty(TextBox5.Text) ||
                    DropDownList1.SelectedValue == "select" ||
                    string.IsNullOrEmpty(TextBox7.Text) ||
                    string.IsNullOrEmpty(TextBox8.Text) ||
                    string.IsNullOrEmpty(TextBox2.Text))
                {
                    // Show an error message or handle accordingly
                    Response.Write("<script>alert('Please fill in all the required fields');</script>");
                    return;
                }
                else if (DateTime.TryParse(TextBox1.Text, out dob))
                {
                    // Check if the DOB is within the allowed range
                    DateTime minDate = new DateTime(1920, 1, 1);
                    DateTime maxDate = DateTime.Today;

                    if (dob < minDate || dob > maxDate)
                    {
                        Response.Write("<script>alert('Date of Birth must be between 1920-01-01 and today.');</script>");
                        return;
                    }

                    // Proceed with updating user details if DOB is valid
                    updateUserPersonalDetails();
                    getUserData();
                    TextBox10.Text = "";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink hyperLink = e.Row.Cells[e.Row.Cells.Count - 1].Controls[0] as HyperLink;

                    if (hyperLink != null)
                    {
                        // Set inline CSS for text-decoration: none;
                        hyperLink.Attributes.Add("style", "text-decoration: none; color: #20B2AA; font-weight: bold;");
                    }
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[2].Text);
                    DateTime today = DateTime.Today;
                    if (today>dt)
                    {
                        e.Row.Cells[0].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[1].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[2].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[3].BackColor = Color.PaleVioletRed;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        void AddHyperLinkColumn()
        {
            // Create a new HyperLinkField
            HyperLinkField hyperlinkField = new HyperLinkField();

            // Set the properties for the HyperLinkField
            hyperlinkField.HeaderText = "View Book";
            hyperlinkField.DataTextField = "book_name";  // The text that will be displayed as the hyperlink
            hyperlinkField.DataNavigateUrlFields = new string[] { "book_id" };  // The field used for the query string
            hyperlinkField.DataNavigateUrlFormatString = "bookdetails.aspx?bookId={0}";  // URL format for the hyperlink

            // Add the HyperLinkField to the GridView
            GridView1.Columns.Add(hyperlinkField);
        }
        void updateUserPersonalDetails()
        {
            string password = "";
            if (TextBox10.Text.Trim() == "")
            {
                password = TextBox9.Text.Trim();
            }
            else
            {
                password = TextBox10.Text.Trim();
            }
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("update member_master_tbl set full_name=@full_name,dob=@dob,contact_no=@contact_no,email=@email,state=@state,city=@city,pincode=@pincode,full_address=@full_address,password=@password,account_status=@account_status where member_id='" + Session["username"].ToString().Trim() + "'", con);
                cmd.Parameters.AddWithValue("@full_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@account_status", "pending");

                int result = cmd.ExecuteNonQuery();
               con.Close();
                if (result > 0)
                {
                    Response.Write("<script>alert('Your Details Updated Successfully');</script>");
                    getUserPersonalDetails();
                    getUserData();
                    
                }
                else
                {
                    Response.Write("<script>alert('Invalid Entry');</script>");
                }
                
        }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void AddBoundFields()
        {
            // Adding BoundField for Book ID
            BoundField bookIdField = new BoundField();
            bookIdField.DataField = "book_id";
            bookIdField.HeaderText = "Book ID";
            GridView1.Columns.Add(bookIdField);

            // Add more BoundFields as needed, e.g., Issue Date, Return Date
            BoundField issueDateField = new BoundField();
            issueDateField.DataField = "issue_date";
            issueDateField.HeaderText = "Issue Date";
            GridView1.Columns.Add(issueDateField);

            BoundField returnDateField = new BoundField();
            returnDateField.DataField = "due_date";
            returnDateField.HeaderText = "Due Date";
            GridView1.Columns.Add(returnDateField);
        }
        void getUserPersonalDetails()
        {
            if (Session["role"].Equals("admin"))
            {
                return;
            }
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from member_master_tbl where member_id='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                TextBox3.Text = dt.Rows[0]["full_name"].ToString();
                TextBox1.Text = dt.Rows[0]["dob"].ToString();
                TextBox4.Text = dt.Rows[0]["contact_no"].ToString();
                TextBox5.Text = dt.Rows[0]["email"].ToString();
                DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
                TextBox7.Text = dt.Rows[0]["city"].ToString();
                TextBox8.Text = dt.Rows[0]["pincode"].ToString();
                TextBox2.Text = dt.Rows[0]["full_address"].ToString();
                TextBox6.Text = dt.Rows[0]["member_id"].ToString();
                TextBox9.Text = dt.Rows[0]["password"].ToString();

                Label1.Text = dt.Rows[0]["account_status"].ToString().Trim();
                if(dt.Rows[0]["account_status"].ToString().Trim() == "active")
                {
                    Label1.Attributes.Add("class", "badge text-bg-success");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                {
                    Label1.Attributes.Add("class", "badge text-bg-warning");
                }
                else if (dt.Rows[0]["account_status"].ToString().Trim() == "deactive")
                {
                    Label1.Attributes.Add("class", "badge text-bg-danger");
                }
                else
                {
                     Label1.Attributes.Add("class", "badge text-bg-primary");
            
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        void getUserData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from book_issue_tbl where member_id='" + Session["username"].ToString() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    bookId = dt.Rows[0]["book_id"].ToString().Trim();
                    if (!IsPostBack)  // Prevent adding columns multiple times
                    {
                        AddBoundFields();
                        AddHyperLinkColumn();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }

        //go
        protected void Button2_Click(object sender, EventArgs e)
        {
            getNames();
        }

        //return 
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfIssueExists())
            {
                requestReturnBook();
            }
            else
            {
                Response.Write("<script>alert('This Member does not have this book.');</script>");
            }
        }
        void getNames()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select book_name from book_master_tbl where book_id='" + TextBox14.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    TextBox12.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {

                    Response.Write("<script>alert('Wrong Book ID');</script>");
                }
                cmd = new SqlCommand("select full_name from member_master_tbl where member_id='" + TextBox13.Text.Trim() + "'", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox11.Text = dt.Rows[0]["full_name"].ToString();
                }
                else
                {

                    Response.Write("<script>alert('Wrong User ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
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
                SqlCommand cmd = new SqlCommand("select * from book_issue_tbl where member_id='" + TextBox13.Text.Trim() + "' and book_id= '" + TextBox14.Text.Trim() + "'", con);
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
        void requestReturnBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("insert into req_return_tbl (ret_member_id,ret_book_id) values(@ret_member_id,@ret_book_id)", con);
                cmd.Parameters.AddWithValue("@ret_member_id", TextBox13.Text.Trim());
                cmd.Parameters.AddWithValue("@ret_book_id", TextBox14.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Admin will be notified.');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}