<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="razorpay.aspx.cs" Inherits="LibraryManagement.razorpay" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <form id="form2">
        <div>
            <!-- Razorpay Integration -->
            <%
                string orderId = Request.QueryString["orderId"];  // You can fetch it from Session or Database too
                string razorpayKey = "rzp_test_m4SNOy01AucT2E"; 
                int amount = Convert.ToInt32(Request.QueryString["amount"]);
                
            %>

            <!-- Razorpay Checkout Form -->
            <script type="text/javascript">
                // Ensure the server-side variables are injected into JavaScript correctly
                var options = {
                    "key": "<%= razorpayKey %>", // Razorpay API Key ID injected from server-side
                    "amount": "<%= amount %>", // Amount in paise (50000 paise = Rs 500)
                    "currency": "INR",
                    "name": "E-Library",
                    "description": "<%=Request.QueryString["bookName"]%>",
                    "order_id": "<%= orderId %>", // Razorpay order ID passed from the server
                    "handler": function (response) {
                        // After successful payment, redirect to payment success page
                        window.location.href = 'afterpayment.aspx?payment_id=' + response.razorpay_payment_id;
                    },
                    "prefill": {
                        "name": "<%=Request.QueryString["memName"]%>",
                        "email": "<%=Request.QueryString["email"]%>",
                        "contact": "<%=Request.QueryString["contactNo"]%>"
                    },
                    "theme": {
                        "color": "#F37254"
                    },
                    "modal": {
                        "ondismiss": function () {
                            // Handle payment failure scenario
                            alert("Payment process canceled or failed!");
                            // Redirect to viewuserbooks.aspx
                            window.location.href = "viewuserbooks.aspx";
                        }
                    }
                };

                // Initialize and open Razorpay payment popup
                var rzp1 = new Razorpay(options);
                rzp1.open();
            </script>
        </div>
    </form>

</asp:Content>