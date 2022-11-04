<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StandardWise.aspx.cs" Inherits="SZABIST_IR_App.Reports.StandardWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:scriptmanager runat="server"></asp:scriptmanager>
    <div> 
     <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server"></rsweb:ReportViewer>--%>
    <rsweb:ReportViewer ID="ReportViewer1"  style="margin-left:150px" Height="700px" Width="800px" runat="server"></rsweb:ReportViewer>
    </div>
        
       
    </form>
</body>
</html>
