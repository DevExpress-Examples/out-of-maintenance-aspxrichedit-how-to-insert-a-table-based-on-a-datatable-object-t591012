using DevExpress.Web.ASPxRichEdit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxRichEdit1_CalculateDocumentVariable(object sender, DevExpress.XtraRichEdit.CalculateDocumentVariableEventArgs e)
    {
        var rich = (ASPxRichEdit)sender;
        if (e.VariableName == "MyCustomInsertTableField")
        {
            System.Data.DataTable dataTable = GetDT1();
            DevExpress.XtraRichEdit.RichEditDocumentServer docServer = new DevExpress.XtraRichEdit.RichEditDocumentServer();
            int dataTableRows = dataTable.Rows.Count;
            int dataTableColumns = dataTable.Columns.Count;

            DevExpress.XtraRichEdit.API.Native.Table table = docServer.Document.Tables.Create(docServer.Document.Range.End, dataTableRows + 1, dataTableColumns);
            for (int i = 0; i < dataTableColumns; i++)
            {
                docServer.Document.InsertText(table[0, i].Range.Start, dataTable.Columns[i].ColumnName);
            }

            table.ForEachCell(delegate(DevExpress.XtraRichEdit.API.Native.TableCell cell, int rowIndex, int cellIndex)
            {
                if (rowIndex > 0)
                {
                    docServer.Document.InsertText(cell.Range.Start, dataTable.Rows[rowIndex - 1][dataTable.Columns[cellIndex].ColumnName].ToString());
                }
            });
            e.Value = docServer.Document;
            e.Handled = true;
        }
    }
    public static DataTable GetDT1()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Age", typeof(int));
        dt.Columns.Add("Position", typeof(string));

        dt.Rows.Add("Sumit", 21, "Manager");
        dt.Rows.Add("Amit", 23, "Developer");
        dt.Rows.Add("Sumit2", 31, "Manager");
        dt.Rows.Add("Amit2", 33, "Developer");
        return dt;
    }
    public static DataTable GetDT2()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Product", typeof(string));
        dt.Columns.Add("Price", typeof(int));
        dt.Rows.Add("Product1", 21);
        dt.Rows.Add("Product2", 23);
        dt.Rows.Add("Product3", 25);
        return dt;
    }

    [WebMethod]
    public static string GetJSONdata()
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        DataTable table = GetDT2();
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }
}