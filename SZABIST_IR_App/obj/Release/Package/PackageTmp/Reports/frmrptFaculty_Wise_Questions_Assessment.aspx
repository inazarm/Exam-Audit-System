<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmrptFaculty_Wise_Questions_Assessment.aspx.cs" Inherits="SZABIST_IR_App.Reports.frmrptFaculty_Wise_Questions_Assessment" %>

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
        <rsweb:ReportViewer ID="rvFaculty_Wise_Questions_Assessment" style="margin-left:50px" Height="700px" Width="800px" runat="server"></rsweb:ReportViewer>
    </div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
