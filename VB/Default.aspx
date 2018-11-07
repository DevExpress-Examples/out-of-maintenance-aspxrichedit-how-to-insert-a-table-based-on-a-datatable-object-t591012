<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Web.ASPxRichEdit.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRichEdit" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>  
    <script type="text/javascript">
        var insertedFieldIntervalStartPos = null;

        //document variable approach
        function OnClickInsertDocv(s, e) {
            MyRichEdit.selection.collapsed = true;
            MyRichEdit.commands.createField.execute();
            MyRichEdit.commands.insertText.execute("docvariable MyCustomInsertTableField");
            insertedFieldIntervalStartPos = MyRichEdit.selection.intervals[0].start;
            MyRichEdit.commands.updateField.execute(afterUpdate);
        }
        function afterUpdate() {
            var field = MyRichEdit.document.activeSubDocument.findFields(insertedFieldIntervalStartPos)[0];
            MyRichEdit.selection.intervals = [field.resultInterval];
            MyRichEdit.commands.copyContent.execute(field.interval.start + field.interval.length);
            MyRichEdit.selection.intervals = [field.interval];
            MyRichEdit.commands.backspace.execute();
        }

        //client-side table inserting approach
        var myUrl = '@Url.Action("GetJsonData", "Home")';
        function OnClickInsertJSON(s, e) {
            MyRichEdit.selection.collapsed = true;
            var selectionPos = MyRichEdit.selection.intervals[0].start;
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetJSONdata", contentType: 'application/json; charset=utf-8',
                dataType: 'json', success: function (result) {
                    var table = JSON.parse(result.d);
                    var rowsCount = table.length;
                    if (rowsCount > 0) {
                        var properties = Object.keys(table[0]);
                        var colCount = properties.length;
                        MyRichEdit.commands.insertTable.execute(colCount, rowsCount + 1);
                        var tableIndex = MyRichEdit.document.activeSubDocument.findTables(selectionPos+1)[0].index;
                        //add columns data
                        for (var i = 1; i < rowsCount + 1; i++) {
                            for (var j = 0; j < colCount; j++) {
                                var t = MyRichEdit.document.activeSubDocument.tablesInfo[tableIndex];
                                var startPos = t.rows[i].cells[j].start;
                                MyRichEdit.selection.intervals = [new ASPx.Interval(startPos, 0)];
                                MyRichEdit.commands.insertText.execute(table[i - 1][properties[j]] + "");
                            }
                        }
                        //add columns captions
                        for (var j = 0; j < colCount; j++) {
                            var t = MyRichEdit.document.activeSubDocument.tablesInfo[tableIndex];
                            var startPos = t.rows[0].cells[j].start;
                            MyRichEdit.selection.intervals = [new ASPx.Interval(startPos, 0)];
                            MyRichEdit.commands.insertText.execute([properties[j]] + "");
                        }
                    }

                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxRichEdit ID="ASPxRichEdit1" ClientInstanceName="MyRichEdit" runat="server" OnCalculateDocumentVariable="ASPxRichEdit1_CalculateDocumentVariable" WorkDirectory="~\App_Data\WorkDirectory">
            <Settings>
                <Behavior Save="Hidden" SaveAs="Hidden" Open="Hidden" />
            </Settings>
        </dx:ASPxRichEdit>

        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="InsertByDocvariable" AutoPostBack="false">
            <ClientSideEvents Click="OnClickInsertDocv" />
        </dx:ASPxButton>
        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="InsertUsingJSON" AutoPostBack="false">
            <ClientSideEvents Click="OnClickInsertJSON" />
        </dx:ASPxButton>
    </form>
</body>
</html>