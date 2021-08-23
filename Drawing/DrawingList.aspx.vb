Public Class DrawingList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		If Not IsPostBack Then
		End If
		LoadPage()
	End Sub

	Sub LoadPage()
		Dim SqlList As String = "
            DECLARE @Search Varchar(20);
			SET @Search = '" & TxtSearch.Text.ToUpper & "%';

			SELECT 
				MAX([Active_Date]) as [Active_Date]
				,[PartName]
				,[PartNo]
				,[Dash]
				,CASE
					WHEN [Dash] = '-' THEN [Dwg_no]
					ELSE REPLACE([Dwg_no],'XX',[Dash])
				END AS [Drawing No.]
				,[Dwg_rev]
				,[Process_Name]
				,[Upload_By]

			  FROM [Drawing_Mecha2].[dbo].[Drawing_Process] as t1

			LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t2
			ON t1.[Process_id] = t2.[Process_id]

			LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Master] as t3
			ON t1.[Dwg_id] = t3.[Dwg_id]

			LEFT JOIN [Drawing_Mecha2].[dbo].[Active_Log] as t4
			ON t1.[Dwg_id] = t4.[Dwg_id]

			WHERE
				[Dwg_no] like '%'+@Search
			OR
				[PartNo] like '%'+@Search

			GROUP BY [PartName]
				,[PartNo]
				,[Dwg_no]
				,[Dwg_rev]
				,[Dash]
				,[Process_Name]
				,[Upload_By]
			ORDER BY MAX([Active_Date]) DESC
        "

		Dim dy = StandardFunction.GetDataTable(SqlList)

		If dy.Rows.Count = 0 Then
			Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('ไม่พบ Drawing ที่ค้นหา');", True)
		Else
			StandardFunction.fillDataTableToDataGrid(GrdDrawingList, SqlList, "")
		End If

	End Sub

	Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
		LoadPage()
	End Sub

	Sub Clearform()
		TxtSearch.Text = Nothing
		Response.Redirect(Me.Request.Url.ToString())
	End Sub

	Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
		Clearform()
	End Sub

	Private Sub GrdDrawingList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdDrawingList.PageIndexChanging
		GrdDrawingList.PageIndex = e.NewPageIndex
		GrdDrawingList.DataBind()
	End Sub
End Class