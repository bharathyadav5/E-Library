using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagement
{
    public partial class adminauthor : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"] == null || Session["username"].ToString() == "")
                {
                    Response.Write("<script>alert('Session Expired. Please Login Again');</script>");
                    Response.Redirect("adminlogin.aspx");
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            GridView1.DataBind();
        }

        //add
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists() && !(string.IsNullOrEmpty(TextBox3.Text)))
            {
                Response.Write("<script>alert('Author with this ID already exists.');</script>");
            }
            else if (string.IsNullOrEmpty(TextBox3.Text) ||
                   string.IsNullOrEmpty(TextBox1.Text))
            {
                // Show an error message or handle accordingly
                Response.Write("<script>alert('Please fill in all the required fields');</script>");
                return;
            }
            else
            {
                addNewAuthor();
            }
        }

        //update
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists() && !(string.IsNullOrEmpty(TextBox3.Text) ||
                   string.IsNullOrEmpty(TextBox1.Text)))
            {
                updateAuthor();
               
            }
            else if(string.IsNullOrEmpty(TextBox3.Text) ||
                   string.IsNullOrEmpty(TextBox1.Text))
            {
                Response.Write("<script>alert('Please fill in all the required fields');</script>");
            }
            else
            {
                Response.Write("<script>alert('Author does not exist.');</script>");
            }
            
        }

        //delete
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                deleteAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author deleted successfully');</script>");
            }
            
        }

        //go
        protected void Button2_Click(object sender, EventArgs e)
        {
            getAuthorById();
        }
        void getAuthorById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from author_master_tbl where author_id='" + TextBox3.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox1.Text = dt.Rows[0][1].ToString();
                }
                else Response.Write("<script>alert('Invalid Author ID');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
               
            }
        }


        void deleteAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("delete from author_master_tbl where author_id='" + TextBox3.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author deleted Successfully !');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        void updateAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("update author_master_tbl set author_name=@author_name where author_id='"+TextBox3.Text.Trim()+"'", con);
               
                cmd.Parameters.AddWithValue("@author_name", TextBox1.Text.Trim());


                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('Author updated Successfully !');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void addNewAuthor()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("insert into author_master_tbl (author_id,author_name) values(@author_id,@author_name)", con);
                cmd.Parameters.AddWithValue("@author_id", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@author_name", TextBox1.Text.Trim());
                

                cmd.ExecuteNonQuery();
                con.Close();
               // Response.Write("<script>alert('Author added Successfully !');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        bool checkIfAuthorExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from author_master_tbl where author_id='" + TextBox3.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1) return true;
                else return false;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
        void clearForm()
        {
            TextBox1.Text = "";
            TextBox3.Text = "";
        }
    }
}