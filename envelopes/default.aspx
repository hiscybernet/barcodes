<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>COSC 4100</title>
    <meta charset="utf-8" />
    <script src="https://unpkg.com/pdf-lib@1.4.0"></script>
    <script src="https://unpkg.com/downloadjs@1.4.7"></script>
    <script type="text/javascript">
        async function TO_ME() {
            if (document.getElementById("CB2ME").checked == true) {
                document.getElementById("TBADDRESSEE").value = "Izzy Islander\n6300 Ocean Drive\nCorpus Christi, Texas 78412";
            }
            else {
                if (document.getElementById("CB2ME").checked == false) {
                    document.getElementById("TBADDRESSEE").value = "";
                }
            }
            return;
        }
    </script>
</head>
<body style="background-color: #0094ff;">
    <h2>THE CAREER MEN</h2>
    <h3>INTELIGENT MAIIL BARCODE GENERATOR</h3>
    <form id="form1" runat="server">
        <input type="checkbox" id="CB2ME" name="CB2ME" value="CB2ME" onchange="TO_ME();" />
        <label for="CB2ME">DELIVER TO ME</label>
        <asp:Button ID="BTNPRINT" runat="server" OnClick="BTNPRINT_Click" Text="Calculate Intelligent Mail® barcode." />
        <br />
        <asp:TextBox ID="TBRETURNADDRESS" runat="server" Font-Names="Tahoma" Font-Size="12pt" Height="100px" Rows="4" TextMode="MultiLine" Width="300px">Izzy Islander
6300 Ocean Drive
Corpus Christi, Texas 78412</asp:TextBox>
        <asp:TextBox ID="TBADDRESSEE" runat="server" Font-Names="Tahoma" Font-Size="14pt" Height="100px" Rows="4" TextMode="MultiLine" Width="300px"></asp:TextBox>
        <br />
        <iframe id="pdf" runat="server" style="width: 800px; height: 400px;"></iframe>
        <hr />
    </form>
</body>
</html>
