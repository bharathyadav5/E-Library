using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManagement
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty((string)Session["role"]))
                {
                    LinkButton1.Visible = true; //login
                    LinkButton2.Visible = true; //sign up
                    LinkButton3.Visible = false; //logout
                    LinkButton11.Visible = false;  //hello user
                    LinkButton6.Visible = true;//adminlogin
                    LinkButton5.Visible = false; //author management
                    LinkButton7.Visible = false;//publisher management
                    LinkButton8.Visible = false;//book inventory
                    LinkButton12.Visible = false;//issue requests
                    LinkButton9.Visible = false;//book issuing
                    LinkButton10.Visible = false;//member management
                }
                else if (Session["role"].Equals("user")){
                    LinkButton1.Visible = false;
                    LinkButton2.Visible = false;
                    LinkButton3.Visible = true;
                    LinkButton11.Visible = true;
                    LinkButton11.Text = "Hello " + Session["username"].ToString();
                    LinkButton6.Visible = false;
                    LinkButton5.Visible = false;
                    LinkButton7.Visible = false;
                    LinkButton8.Visible = false;
                    LinkButton9.Visible = false;
                    LinkButton10.Visible = false;
                    LinkButton12.Visible = false;
                }
                else if (Session["role"].Equals("admin")){
                    LinkButton1.Visible = false;
                    LinkButton2.Visible = false;
                    LinkButton3.Visible = true;
                    LinkButton11.Visible = true;
                    LinkButton11.Text = "Hello Admin";
                    LinkButton6.Visible = false;
                    LinkButton5.Visible = true;
                    LinkButton7.Visible = true;
                    LinkButton8.Visible = true;
                    LinkButton9.Visible = true;
                    LinkButton10.Visible = true;
                    LinkButton12.Visible = true;
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');");
            }
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminlogin.aspx");
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminauthor.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            LinkButton1.Visible = true; //login
            LinkButton2.Visible = true; //sign up
            LinkButton3.Visible = false; //logout
            LinkButton11.Visible = false;  //hello user
            LinkButton11.Text = "";
            LinkButton6.Visible = true;//adminlogin
            LinkButton5.Visible = false; //author management
            LinkButton7.Visible = false;//publisher management
            LinkButton8.Visible = false;//book inventory
            LinkButton9.Visible = false;//book issuing
            LinkButton10.Visible = false;//member management
            Response.Redirect("homepage.aspx");
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminpublisher.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("viewuserbooks.aspx");
        }
       

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminbookissue.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminmember.aspx");
        }

        protected void LinkButton12_Click(object sender, EventArgs e)
        {
            Response.Redirect("issuerequests.aspx");
        }
    }
}