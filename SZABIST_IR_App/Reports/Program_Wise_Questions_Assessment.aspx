<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Program_Wise_Questions_Assessment.aspx.cs" Inherits="SZABIST_IR_App.Reports.Program_Wise_Questions_Assessment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <asp:scriptmanager runat="server"></asp:scriptmanager>
   
   <div>
     <rsweb:ReportViewer ID="ReportViewer1" Height="700px" Width="800px" runat="server"></rsweb:ReportViewer>
    </div>

        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
