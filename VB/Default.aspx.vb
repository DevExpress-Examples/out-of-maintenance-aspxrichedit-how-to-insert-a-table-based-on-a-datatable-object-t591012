Option Infer On

Imports DevExpress.Web.ASPxRichEdit
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Web.Services
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

	End Sub
	Protected Sub ASPxRichEdit1_CalculateDocumentVariable(ByVal sender As Object, ByVal e As DevExpress.XtraRichEdit.CalculateDocumentVariableEventArgs)
		Dim rich = DirectCast(sender, ASPxRichEdit)
		If e.VariableName = "MyCustomInsertTableField" Then
			Dim dataTable As System.Data.DataTable = GetDT1()
			Dim docServer As New DevExpress.XtraRichEdit.RichEditDocumentServer()
			Dim dataTableRows As Integer = dataTable.Rows.Count
			Dim dataTableColumns As Integer = dataTable.Columns.Count

			Dim table As DevExpress.XtraRichEdit.API.Native.Table = docServer.Document.Tables.Create(docServer.Document.Range.End, dataTableRows + 1, dataTableColumns)
			For i As Integer = 0 To dataTableColumns - 1
				docServer.Document.InsertText(table(0, i).Range.Start, dataTable.Columns(i).ColumnName)
			Next i

			table.ForEachCell(Sub(cell As DevExpress.XtraRichEdit.API.Native.TableCell, rowIndex As Integer, cellIndex As Integer)
				If rowIndex > 0 Then
					docServer.Document.InsertText(cell.Range.Start, dataTable.Rows(rowIndex - 1)(dataTable.Columns(cellIndex).ColumnName).ToString())
				End If
			End Sub)
			e.Value = docServer.Document
			e.Handled = True
		End If
	End Sub
	Public Shared Function GetDT1() As DataTable
		Dim dt As New DataTable()
		dt.Columns.Add("Name", GetType(String))
		dt.Columns.Add("Age", GetType(Integer))
		dt.Columns.Add("Position", GetType(String))

		dt.Rows.Add("Sumit", 21, "Manager")
		dt.Rows.Add("Amit", 23, "Developer")
		dt.Rows.Add("Sumit2", 31, "Manager")
		dt.Rows.Add("Amit2", 33, "Developer")
		Return dt
	End Function
	Public Shared Function GetDT2() As DataTable
		Dim dt As New DataTable()
		dt.Columns.Add("Product", GetType(String))
		dt.Columns.Add("Price", GetType(Integer))
		dt.Rows.Add("Product1", 21)
		dt.Rows.Add("Product2", 23)
		dt.Rows.Add("Product3", 25)
		Return dt
	End Function

	<WebMethod>
	Public Shared Function GetJSONdata() As String
		Dim jsSerializer As New JavaScriptSerializer()
		Dim parentRow As New List(Of Dictionary(Of String, Object))()
		Dim childRow As Dictionary(Of String, Object)
		Dim table As DataTable = GetDT2()
		For Each row As DataRow In table.Rows
			childRow = New Dictionary(Of String, Object)()
			For Each col As DataColumn In table.Columns
				childRow.Add(col.ColumnName, row(col))
			Next col
			parentRow.Add(childRow)
		Next row
		Return jsSerializer.Serialize(parentRow)
	End Function
End Class