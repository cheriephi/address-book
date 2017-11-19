<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddressGrid.aspx.cs" Inherits="AddressBookWeb.AddressGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Address Book</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ObjectDataSource ID="ObjectDataSource1" 
                runat="server" 
                DataObjectTypeName="ConsoleAddress.Address"
                TypeName="AddressBookWeb.AddressDataAccess" 
                SelectMethod="GetAll" 
                InsertMethod="Insert"
                UpdateMethod="Update"
                DeleteMethod="Delete">
            </asp:ObjectDataSource>

            <asp:GridView ID="GridView1" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" 
                DataSourceID="ObjectDataSource1"
                DataKeyNames="Name"
                >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="AddressKey" runat="server" value='<%#DataBinder.Eval(Container.DataItem, "AddressKey")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Full Name" />
                    <asp:BoundField DataField="Street" HeaderText="Street" />
                    <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:BoundField DataField="State" HeaderText="State" />
                    <asp:BoundField DataField="Zip" HeaderText="Zip" />
                    <asp:BoundField DataField="Country" HeaderText="Country" />

                    <asp:CommandField ShowInsertButton="true" />
                    <asp:CommandField ShowEditButton="true" />
                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>

                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>         
        </div>
    </form>
</body>
</html>
