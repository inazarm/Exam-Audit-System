<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IRReport.aspx.cs" Inherits="SZABIST_IR_App.Reports.IRReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body style="margin: 0 auto">
   
    <form id="form1" runat="server">
         <asp:scriptmanager runat="server"></asp:scriptmanager>
    <div style="text-align:center">
        <rsweb:ReportViewer ID="ReportViewer1"   Width="100%" Height="800px" runat="server"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
