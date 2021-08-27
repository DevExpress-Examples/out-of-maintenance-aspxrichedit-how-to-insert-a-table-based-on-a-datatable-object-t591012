<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128545397/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T591012)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))**
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
<!-- default file list end -->
# ASPxRichEdit - How to insert a table based on a DataTable object
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t591012/)**
<!-- run online end -->


This example demonstrates two possible approaches that allow you to insert a table inside the ASPxRichEdit document content based on data of a DataTable object.<br><br>The first approach adds a custom "DOCVARIABLE" field to the document content on the client side, fills it with a required table in the server-side event handler, and removes this field after inserting the table on the client side. The table is inserted into the document by using the <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressXtraRichEditRichEditDocumentServertopic">RichEditDocumentServer</a> component - our non-visual document processing engine. See the <a href="https://www.devexpress.com/Support/Center/p/E3664">Table API - How to display a DataTable</a> example for more information.<br><br>The second approach initiates an AJAX request to get the table data in the JSON format on the client side. Then, the required table is created and filled with data on the client by using the <a href="https://docs.devexpress.com/AspNet/js-ASPxClientRichEdit._members">ASPxRichEdit client-side API</a>.<br><br><strong>See also:</strong><br><a href="https://www.devexpress.com/Support/Center/p/T590876">RichEdit - How to insert a table based on a DataTable object</a>

<br/>


