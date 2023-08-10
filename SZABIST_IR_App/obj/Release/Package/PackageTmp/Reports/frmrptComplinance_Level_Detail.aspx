<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmrptComplinance_Level_Detail.aspx.cs" Inherits="SZABIST_IR_App.Reports.frmrptComplinance_Level_Detail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div> 
        <rsweb:ReportViewer ID="rvComplinance_Level_Details" style="margin-left:50px" Height="800" Width="800" runat="server"></rsweb:ReportViewer>
    </div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
