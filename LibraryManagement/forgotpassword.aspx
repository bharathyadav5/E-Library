<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="forgotpassword.aspx.cs" Inherits="LibraryManagement.forgotpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col">
                <center>
                    <br />
                    <br />
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="Images/password.png" width="160" height="160" alt="profile" />
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h3>Update Password</h3>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <center>
                                    <hr />
                                </center>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col">
                                <label>Email ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Email ID"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="Get OTP" OnClick="Button1_Click" />
                                </div>
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <br />
                                <label>Enter OTP</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="OTP"></asp:TextBox>
                                </div>
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                               
                                <div class="form-group">
                                    <asp:Button CssClass="btn btn-warning" ID="Button2" runat="server" Text="Verify OTP" OnClick="Button2_Click" />
                                </div>
                                 <br />
                                <label>New Password</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                                </div>


                                <div class="row">
                                    <div class="col">
                                        <center>
                                            <hr />
                                        </center>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div>
                                        <asp:Button class="btn btn-success btn-block btn-lg " Width="90%" ID="Button3" runat="server" Text="Update" OnClick="Button3_Click" />

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <a href="homepage.aspx"><< Back to Home</a>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
