<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
                <asp:TextBox ID="txtCourse" runat="server"></asp:TextBox>
                <asp:Button ID="btnSave" runat="server" Text="Button" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:testDBConnectionString %>" SelectCommand="SELECT [Id], [Name] FROM [Departments]"></asp:SqlDataSource>
            <asp:EntityDataSource ID="EntityDataSource1" runat="server">
            </asp:EntityDataSource>
        </div>
    </div>
</asp:Content>
