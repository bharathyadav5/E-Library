<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewuserbooks.aspx.cs" Inherits="LibraryManagement.viewuserbooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-color: gainsboro" class="container">
        <div class="row">
            <div class="col-sm-12">
                <center>
                    <h3>Book Inventory List</h3>
                </center>
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Panel CssClass="alert alert-success" role="alert" ID="Panel1" runat="server" Visible="false">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <img src="Images/books.jpeg" width="350" height="130" style="border-radius: 10px" alt="image" />

                                    </center>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col">
                                    <hr />
                                </div>
                            </div>
                            <div class="row">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:elibraryDBConnectionString6 %>" ProviderName="<%$ ConnectionStrings:elibraryDBConnectionString6.ProviderName %>" SelectCommand="SELECT * FROM [book_master_tbl]"></asp:SqlDataSource>

                                <div class="col">
                                    <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="book_id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="book_id" HeaderText="Book ID" ReadOnly="True" SortExpression="book_id">

                                                <ItemStyle Font-Bold="True" />
                                            </asp:BoundField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <div class="col-lg-10">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("book_name")%>' Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        Author -
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("author_name")%>' Font-Overline="False" Font-Bold="True"></asp:Label>
                                                                        | Genre -
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("genre")%>' Font-Bold="True"></asp:Label>
                                                                        | Language -
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("language")%>' Font-Bold="True"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        Publisher -
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("publisher_name")%>' Font-Overline="False" Font-Bold="True"></asp:Label>
                                                                        | Publish-Date -
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("publish_date")%>' Font-Bold="True"></asp:Label>
                                                                        | Pages -
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("no_of_pages")%>' Font-Bold="True"></asp:Label>
                                                                        | Edition -
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("edition")%>' Font-Bold="True"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        Book Cost - <b>Rs.</b>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("book_cost")%>' Font-Overline="False" Font-Bold="True"></asp:Label>
                                                                        per day | Actual Stock -
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("actual_stock")%>' Font-Bold="True"></asp:Label>
                                                                        | Current Stock -
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("current_stock")%>' Font-Bold="True"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        Description -
                                                                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("book_description")%>' Font-Italic="True" Font-Size="Small"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <br />
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label13" runat="server" Text="From"></asp:Label>
                                                                        <div class="form-group">
                                                                            <asp:TextBox Width="150" CssClass="form-control" ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label14" runat="server" Text="To"></asp:Label>
                                                                        <div class="form-group">
                                                                            <asp:TextBox Width="150" CssClass="form-control" ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <br />
                                                                        <div class="form-group">
                                                                            <asp:LinkButton CssClass="btn btn-warning" ID="LinkButton1" OnClick="LinkButton1_Click" runat="server">Pay Now</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <br />
                                                                        <div class="form-group">
                                                                            <asp:Button CssClass="btn btn-secondary" OnClick="Button1_Click" ID="Button1" Visible="false" runat="server" Text="Borrow" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:Image class="img-fluid p-2" ID="Image1" runat="server" ImageUrl='<%# Eval("book_img_link") %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <center>
                <a href="homepage.aspx"><< Back to Home</a><span class="clearfix"></span><br />
            </center>
        </div>
    </div>
</asp:Content>
