<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Training.aspx.cs" Inherits="TrainingApp.Training" %>

<%@ Register Src="~/Product.ascx" TagPrefix="uc1" TagName="Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ECTP Training</title>
    <style>
        .message {
            background-color: black;
            color: white;
        }

        .productDetailsContainer {
            width: 50%;
            border: solid 2px black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="welcomeMessage" class="message" runat="server" Text="Welcome to the ECTP Training WebForm Page!"></asp:Label>
            <br />
            <br />
            <asp:DropDownList ID="ddlCategories" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" AutoPostBack="true" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSubCategories" OnSelectedIndexChanged="ddlSubCategories_SelectedIndexChanged" AutoPostBack="true" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:GridView ID="gvProducts" 
                AutoGenerateColumns="False" 
                DataKeyNames="ProductId" 
                OnRowCommand="gvProducts_RowCommand" EmptyDataText="No Products for the selected SubCategory"
                runat="server" >
                <Columns>
                    <asp:ButtonField ButtonType="Button" Text="Select" CommandName="Select" />
                    <asp:BoundField DataField="ProductName" HeaderText="Products" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:c}" />
                </Columns>
            </asp:GridView>
            <br />
            <div id="productDetailsContainer" class="productDetailsContainer" runat="server" visible="false" >
                <uc1:Product runat="server" ID="ProductDetails" />
            </div>
            <br />
        </div>
    </form>
</body>
</html>