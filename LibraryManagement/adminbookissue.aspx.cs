using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace LibraryManagement
{
    public partial class adminbookissue : System.Web.UI.Page
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
            GridView2.DataBind();
        }


        //go
        protected void Button2_Click(object sender, EventArgs e)
        {
            getNames();
        }


        //return
        protected void Button4_Click(object sender, EventArgs e)
        {
           
     
                if (checkIfIssueExists() && checkIfRequested())
                {
                    returnBook();
                }
                else
                {
                    Response.Write("<script>alert('This Entry Does not Exist.');</script>");
                }
            
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;
                    if (today>dt)
                    {
                        e.Row.Cells[0].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[1].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[2].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[3].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[4].BackColor = Color.PaleVioletRed;
                        e.Row.Cells[5].BackColor = Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
        }

        void returnBook()
        {
            
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("delete from book_issue_tbl where book_id='" + TextBox3.Text.Trim() + "' and member_id='" + TextBox1.Text.Trim() + "'", con);
                int result = cmd.ExecuteNonQuery();

                con.Close();
                if (result > 0)
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    cmd = new SqlCommand("update book_master_tbl set current_stock = current_stock+1 where book_id='" + TextBox3.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from req_issue_tbl where req_book_id='" + TextBox3.Text.Trim() + "' and req_mem_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from req_return_tbl where ret_book_id='" + TextBox3.Text.Trim() + "' and ret_member_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    Response.Write("<script>alert('Book Returned Successfully !');</script>");

                    GridView1.DataBind();
                    GridView2.DataBind();

                }
                else
                {
                    Response.Write("<script>alert('Invalid Details');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        bool checkIfMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select full_name from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "' and current_stock >0", con);
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

        bool checkIfIssueExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from book_issue_tbl where member_id='" + TextBox1.Text.Trim() + "' and book_id= '" + TextBox3.Text.Trim() + "'", con);
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

        bool checkIfRequested()
        {

            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("select * from req_return_tbl where ret_member_id='" + TextBox1.Text.Trim() + "' and ret_book_id= '" + TextBox3.Text.Trim() + "'", con);
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
        bool checkIfBookExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from book_master_tbl where book_id='" + TextBox3.Text.Trim() + "' and current_stock >0", con);
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
        void getNames()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select book_name from book_master_tbl where book_id='" + TextBox3.Text.Trim() + "'",con);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    TextBox4.Text = dt.Rows[0]["book_name"].ToString();
                }
                else
                {

                    Response.Write("<script>alert('Wrong Book ID');</script>");
                }
                cmd = new SqlCommand("select full_name from member_master_tbl where member_id='" + TextBox1.Text.Trim() + "'", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0]["full_name"].ToString();
                }
                else
                {

                    Response.Write("<script>alert('Wrong User ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
        }
    }
}