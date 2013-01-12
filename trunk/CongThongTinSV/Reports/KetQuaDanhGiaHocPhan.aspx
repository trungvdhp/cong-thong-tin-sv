<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KetQuaDanhGiaHocPhan.aspx.cs" Inherits="CongThongTinSV.Reports.KetQuaDanhGiaHocPhan" EnableSessionState="True" EnableViewState="True" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>  
        <rsweb:reportviewer id="rpvKetQuaDanhGiaHocPhan" runat="server" height="100%" width="100%" borderstyle="None" clientidmode="AutoID" internalborderstyle="Ridge" waitcontroldisplayafter="0" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" SizeToReportContent="True" style="margin-bottom:30px" ZoomMode="PageWidth" ViewStateMode="Enabled">
            <LocalReport ReportPath="./Reports/rptKetQuaDanhGiaHocPhan.rdlc">
            </LocalReport>
        </rsweb:reportviewer>  
    </div>
    </form>
</body>
</html>
