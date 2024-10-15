<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="afterpayment.aspx.cs" Inherits="LibraryManagement.afterpayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .styled-table {
            width: 80%;
            margin: auto;
            border-collapse: collapse;
            border: 1px solid #ddd;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

            .styled-table th,
            .styled-table td {
                padding: 12px 15px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .styled-table th {
                background-color: #4CAF50;
                color: white;
            }

            .styled-table tr:hover {
                background-color: #f1f1f1;
            }

            .styled-table tr:nth-child(even) {
                background-color: #f9f9f9;
            }

            .styled-table tr:nth-child(odd) {
                background-color: #fff;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: gainsboro" class="container">
        <center>
            <h2 style="color: green">Payment Successful!</h2>
        </center>
        <br />
        <div class="table-container">
            <asp:Table ID="StyledTable" runat="server" CssClass="styled-table">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Payment Details</asp:TableHeaderCell>
                    <asp:TableHeaderCell></asp:TableHeaderCell>
                </asp:TableHeaderRow>
            </asp:Table>
        </div>
        <br />
        <br />
        <br />
        <center>
            <span style="background-color:aqua;border-radius:10px; padding:15px 20px; font-size:16px; color:black">Admin will be Notified</span>
            <br />
            <br />
            <br />
            <span style="background-color:aquamarine;padding:15px 20px; color:red">Do not try to go back! Click on View Books 
            </span>
        </center>
        <br />
    </div>
</asp:Content>
