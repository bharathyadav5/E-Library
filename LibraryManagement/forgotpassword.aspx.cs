using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace LibraryManagement
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("update member_master_tbl set password=@password where email='"+TextBox1.Text.Trim()+"'",con);
                cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());
                int result=cmd.ExecuteNonQuery();
                con.Close();
                if (result > 0)
                {
                    Response.Redirect("userlogin.aspx");
                }
                else
                {
                    Response.Write("something is wrong");
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        //get otp
        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text.Trim();

            // Validate if the email exists in the database
            if (isValidEmail(email))
            {
                
                string otp = GenerateOTP();

                Session["OTP"] = otp;
                Session["UserEmail"] = email;

                
                string subject = "Your OTP for Password Reset";
                string body = $"Your OTP is {otp}. Please use this to reset your password.";

                // Use EmailHelper to send the email
                EmailHelper.SendEmail(email, subject, body);

                Label1.Text = "OTP has been sent to your email.";
                Label1.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label1.Text = "Email not found.";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Method to generate OTP (6-digit random number)
        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
       

        protected void Button2_Click(object sender, EventArgs e)
        {
            string enteredOTP = TextBox3.Text.Trim();
            string sessionOTP = Session["OTP"]?.ToString();

            if (!string.IsNullOrEmpty(sessionOTP) && enteredOTP == sessionOTP)
            {
                Label2.Text = "OTP verified.";
                Label2.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Label2.Text = "Invalid OTP. Please try again.";
                Label2.ForeColor = System.Drawing.Color.Red;
            }
        }

        bool isValidEmail(string email)
        {
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("select email from member_master_tbl where email='" + email.Trim() + "'", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count >= 1) return true;
                    else {
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
}