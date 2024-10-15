using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagement
{
    public partial class usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime dob;
            if (checkMemberExists())
            {
                Label1.Text = "Member with this ID already Exists.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (emailExists())
            {
                Label1.Text = "This email ID is already registered.";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (string.IsNullOrEmpty(TextBox3.Text) ||
                    string.IsNullOrEmpty(TextBox1.Text) ||
                    string.IsNullOrEmpty(TextBox4.Text) ||
                    string.IsNullOrEmpty(TextBox5.Text) ||
                    DropDownList1.SelectedValue == "select" ||
                    string.IsNullOrEmpty(TextBox7.Text) ||
                    string.IsNullOrEmpty(TextBox8.Text) ||
                    string.IsNullOrEmpty(TextBox2.Text) ||
                    string.IsNullOrEmpty(TextBox6.Text) ||
                    string.IsNullOrEmpty(TextBox9.Text))
            {
                // Show an error message or handle accordingly
                Label1.Text = "Please enter all the fields";
                Label1.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (DateTime.TryParse(TextBox1.Text, out dob))
            {
                // Check if the DOB is within the allowed range
                DateTime minDate = new DateTime(1920, 1, 1);
                DateTime maxDate = DateTime.Today;

                if (dob < minDate || dob > maxDate)
                {
                    Label2.Text = "Enter a valid DOB";
                    Label2.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                Label2.Text = "";
                signUpNewMember();
            }
        }

        bool emailExists()
        {
                    try
                    {
                        SqlConnection con = new SqlConnection(strcon);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlCommand cmd = new SqlCommand("select email from member_master_tbl where email='" + TextBox5.Text.Trim() + "'", con);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count >= 1) return true;
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
        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from member_master_tbl where member_id='" + TextBox6.Text.Trim() + "';",con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1) return true;
                else return false;
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }

        void signUpNewMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("insert into member_master_tbl (full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) values(@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@member_id,@password,@account_status)", con);
                cmd.Parameters.AddWithValue("@full_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@pincode", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Text = "Sign Up Successful! Go to the User Login";
                Label1.ForeColor = System.Drawing.Color.DarkGreen;
              
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}