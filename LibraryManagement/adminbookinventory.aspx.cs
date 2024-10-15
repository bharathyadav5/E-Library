using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace LibraryManagement
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        static string global_filepath;
        static int global_actual_stock, global_current_stock, global_issued_books;
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
            if (!IsPostBack)
            {
               // fillAuthorPublisherValues();
            }
            
            GridView1.DataBind();
        }

        //go
        protected void Button2_Click(object sender, EventArgs e)
        {
            getBookByID();
        }

        //add
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (    string.IsNullOrEmpty(TextBox1.Text) ||
                    string.IsNullOrEmpty(TextBox4.Text) ||
                    string.IsNullOrEmpty(TextBox5.Text) ||
                    string.IsNullOrEmpty(TextBox10.Text) ||
                    string.IsNullOrEmpty(TextBox6.Text) ||
                    string.IsNullOrEmpty(TextBox9.Text) ||
                    string.IsNullOrEmpty(TextBox2.Text) ||
                    string.IsNullOrEmpty(TextBox12.Text) ||
                    string.IsNullOrEmpty(TextBox11.Text))
            {
                // Show an error message or handle accordingly
                Response.Write("<script>alert('Please fill in all the required fields');</script>");
                return;
            }

            if (checkIfBookExists())
            {
                Response.Write("<script>alert('Book with this ID Already Exists');</script>");
            }
            else
            {
                addNewBook();
            }
        }

        //update
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text) ||
                    string.IsNullOrEmpty(TextBox4.Text) ||
                    string.IsNullOrEmpty(TextBox5.Text) ||
                    string.IsNullOrEmpty(TextBox10.Text) ||
                    string.IsNullOrEmpty(TextBox6.Text) ||
                    string.IsNullOrEmpty(TextBox9.Text) ||
                    string.IsNullOrEmpty(TextBox2.Text) || 
                    string.IsNullOrEmpty(TextBox12.Text) ||
                    string.IsNullOrEmpty(TextBox11.Text))
            {
                // Show an error message or handle accordingly
                Response.Write("<script>alert('Please fill in all the required fields');</script>");
                return;
            }
            updateBookByID();
        }

        //delete
        protected void Button4_Click(object sender, EventArgs e)
        {
            deleteBookByID();
        }

        void deleteBookByID()

        {
            if (checkIfBookExists())
            {
                try
                {
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("delete from book_master_tbl where book_id='" + TextBox3.Text.Trim() + "'", con);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Book deleted Successfully !');</script>");
                    
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID !');</script>");
            }
        }
        void updateBookByID()
        {       
               if (checkIfBookExists())

                {
                    try
                    {
                    int actual_stock = Convert.ToInt32(TextBox4.Text.Trim());
                    int current_stock = Convert.ToInt32(TextBox7.Text.Trim());
                    if(global_actual_stock == actual_stock)
                    {

                    }
                    else
                    {
                        if(actual_stock < global_actual_stock)
                        {
                            Response.Write("<script>alert('Actual stock cannot be less than current stock.');</script>");
                            return;
                        }
                        else
                        {
                            current_stock = actual_stock - global_issued_books;
                            TextBox7.Text = "" + current_stock;
                        }
                    }
                    string genres = "";
                    foreach(int i in ListBox1.GetSelectedIndices())
                    {
                        genres = genres + ListBox1.Items[i] + ",";
                    }
                    genres=genres.Remove(genres.Length - 1);

                    string filepath = "~/BookInventory/book.jpg";
                    string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    if (filename == "" || filename == null)
                    {
                        filepath = global_filepath;
                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath("BookInventory/" + filename));
                        filepath = "~/BookInventory/" + filename;
                    }

                        SqlConnection con = new SqlConnection(strcon);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        SqlCommand cmd = new SqlCommand("update book_master_tbl set book_name=@book_name, genre=@genre,author_name=@author_name,publisher_name=@publisher_name,publish_date=@publish_date,language=@language,edition=@edition,book_cost=@book_cost,no_of_pages=@no_of_pages,book_description=@book_description,actual_stock=@actual_stock,current_stock=@current_stock,book_img_link=@book_img_link where book_id='" + TextBox3.Text.Trim() + "'", con);

                    cmd.Parameters.AddWithValue("@book_id", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_name", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@genre", genres);
                    cmd.Parameters.AddWithValue("@author_name", TextBox12.Text.Trim());
                    cmd.Parameters.AddWithValue("@publisher_name", TextBox11.Text.Trim());
                    cmd.Parameters.AddWithValue("@publish_date", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@language", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@edition", TextBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_cost", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@no_of_pages", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@book_description", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@actual_stock", actual_stock.ToString());
                    cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                    cmd.Parameters.AddWithValue("@book_img_link", filepath);

                    cmd.ExecuteNonQuery();
                        con.Close();
                        GridView1.DataBind();
                    Response.Write("<script>alert('Book Updated Successfully');</script>");
                }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID !');</script>");
                }
            
        }

        void getBookByID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from book_master_tbl where book_id = '" + TextBox3.Text.Trim() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                if(dt.Rows.Count >= 1)
                {
                    TextBox1.Text=dt.Rows[0]["book_name"].ToString();

                    TextBox12.Text = dt.Rows[0]["author_name"].ToString().Trim();
                    TextBox11.Text = dt.Rows[0]["publisher_name"].ToString().Trim();
                    TextBox2.Text = dt.Rows[0]["publish_date"].ToString().Trim();
                    TextBox5.Text = dt.Rows[0]["edition"].ToString().Trim();
                    TextBox6.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    TextBox9.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    TextBox10.Text = dt.Rows[0]["book_description"].ToString().Trim();
                    TextBox4.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    TextBox7.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    DropDownList1.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    TextBox8.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString()));

                    ListBox1.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for(int i = 0; i < genre.Length; i++)
                    {
                        for(int j = 0; j < ListBox1.Items.Count; j++)
                        {
                            if (ListBox1.Items[j].ToString() == genre[i])
                            {
                                ListBox1.Items[j].Selected = true;
                            }
                        }
                    }

                    global_actual_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_books = global_actual_stock - global_current_stock;
                    global_filepath = dt.Rows[0]["book_img_link"].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        //void fillAuthorPublisherValues()
        //{
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(strcon);
        //        if(con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        SqlCommand cmd = new SqlCommand("select author_name from author_master_tbl",con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        DropDownList3.DataSource = dt;
        //        DropDownList3.DataValueField = "author_name";
        //        DropDownList3.DataBind();

        //        cmd = new SqlCommand("select publisher_name from publisher_master_tbl", con);
        //        da = new SqlDataAdapter(cmd);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //        DropDownList2.DataSource = dt;
        //        DropDownList2.DataValueField = "publisher_name";
        //        DropDownList2.DataBind();

        //    }
        //    catch(Exception ex)
        //    {
        //        Response.Write("<script>alert('"+ ex.Message+"');</script>");
        //    }
        //}

        bool checkIfBookExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("select * from book_master_tbl where book_id='" + TextBox3.Text.Trim() + "' or book_name='" + TextBox1.Text.Trim() + "'", con);
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
        void addNewBook()
        {
            try
            {
                
                string genres = "";
                foreach(int i in ListBox1.GetSelectedIndices())
                {
                    genres = genres + ListBox1.Items[i] + ",";
                }
                genres=genres.Remove(genres.Length - 1);
                if (genres == "" || genres == null)
                {
                    Response.Write("<script>alert('Please choose the genre.');</script>");
                }
                string filepath = "~/BookInventory/book.jpg";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (filename == "" || filename == null)
                {
                    Response.Write("<script>alert('Please choose the file.');</script>");
                }
                FileUpload1.SaveAs(Server.MapPath("BookInventory/" + filename));
                filepath = "~/BookInventory/" + filename;
                SqlConnection con =new SqlConnection(strcon);
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("insert into book_master_tbl (book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link) values(@book_id,@book_name,@genre,@author_name,@publisher_name,@publish_date,@language,@edition,@book_cost,@no_of_pages,@book_description,@actual_stock,@current_stock,@book_img_link)",con);

                cmd.Parameters.AddWithValue("@book_id",TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name",TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@genre",genres);
                cmd.Parameters.AddWithValue("@author_name",TextBox12.Text.Trim());
                cmd.Parameters.AddWithValue("@publisher_name",TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@publish_date",TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@language",DropDownList1.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition",TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost",TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages",TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description",TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock",TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock",TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link",filepath);
                cmd.ExecuteNonQuery();
                con.Close();
                Response.Write("<script>alert('book added successfully');</script>");
                GridView1.DataBind();
            }
            catch( Exception ex )
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}