Imports System.Data.SqlClient

Public Class EditDrawing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("CanAccess") And Not Session("Position") = "ENGINEER" Then 'Test Phase Due To Mecha2 No Manpower DB
            Response.Redirect("~/Login")
        End If

        If Not IsPostBack Then
            Dim SqlDrpProcess = "SELECT [Process_Name] FROM [Drawing_Mecha2].[dbo].[Process_Master]"
            StandardFunction.setDropdownlist(DrpProcess, SqlDrpProcess, True)
        End If

        LoadPage()
    End Sub

    Sub LoadPage()
        Dim SqlDWGList = "
        DECLARE @Search Varchar(25)
        SET @Search = '%" & TxtSearch.Text & "%'

		SELECT
				[Upload_Date]
				--,t1.[Dwg_id]
				,[Dwg_no]
				,[Dwg_rev]
				,[PartName]
				,[PartNo]
                ,[Dash]
				,[Process_Name]
				,[Upload_By]
				--,[DwgProcess_id]
				--,t2.[Dwg_id]
				--,t2.[Process_id]
				--,t3.[Process_id]
		  FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1

		LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Process] as t2
		ON t1.[Dwg_id] = t2.[Dwg_id]

		LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t3
		ON t2.[Process_id] = t3.[Process_id]

        WHERE
			[Dwg_no] like @Search
		OR
			[PartNo] like @Search

        ORDER BY [Upload_Date] DESC
        "

        StandardFunction.fillDataTableToDataGrid(GrdDrawing, SqlDWGList, "")
        BtnSave.Enabled = False
    End Sub

    Private Sub GrdDrawing_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdDrawing.SelectedIndexChanged
        Dim row As GridViewRow = GrdDrawing.SelectedRow
        Dim n As Integer = 3
        TxtDrawing.Text = row.Cells(n).Text.Replace("&nbsp;", "")
        TxtRevision.Text = row.Cells(n + 1).Text.Replace("&nbsp;", "")
        TxtPartName.Text = row.Cells(n + 2).Text.Replace("&nbsp;", "").Replace("&#39;", "'")
        TxtPartNo.Text = row.Cells(n + 3).Text.Replace("&nbsp;", "")
        TxtDash.Text = row.Cells(n + 4).Text.Replace("&nbsp;", "")
        DrpProcess.SelectedValue = row.Cells(n + 5).Text.Replace("&nbsp;", "").Replace("&#39;", "'")

        Dim arr As Array = {TxtDrawing.Text, TxtRevision.Text, TxtPartName.Text, TxtPartNo.Text, TxtDash.Text, DrpProcess.SelectedValue}
        Session("Edit") = arr
        BtnSave.Enabled = True
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        TxtDrawing.Text = Nothing
        TxtRevision.Text = Nothing
        TxtPartName.Text = Nothing
        TxtPartNo.Text = Nothing
        TxtDash.Text = Nothing
        TxtSearch.Text = Nothing
        DrpProcess.SelectedIndex = 0
        GrdDrawing.SelectedIndex = -1
        LoadPage()
    End Sub

    Private Sub GrdDrawing_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdDrawing.PageIndexChanging
        GrdDrawing.PageIndex = e.NewPageIndex
        GrdDrawing.DataBind()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click

        Dim x, y As Integer
        Dim arr = Session("Edit")
        If Not IsNothing(Session("Edit")) Then


            If arr(0).Equals(TxtDrawing.Text) _
                And arr(1).Equals(TxtRevision.Text) _
                And arr(2).Equals(TxtPartName.Text) _
                And arr(3).Equals(TxtPartNo.Text) _
                And arr(4).Equals(TxtDash.Text) _
                And arr(5).Equals(DrpProcess.SelectedValue) Then
                Exit Sub
            Else
                Dim SqlGetDwgId = "
			SELECT
				--[Upload_Date]
			t1.[Dwg_id]
				--,[Dwg_no]
				--,[Dwg_rev]
				--,[PartName]
				--,[PartNo]
				--,[Process_Name]
				--,[Upload_By]
				--,[DwgProcess_id]
				--,t2.[Dwg_id]
				--,t2.[Process_id]
				--,t3.[Process_id]
			FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1

			LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Process] as t2
			ON t1.[Dwg_id] = t2.[Dwg_id]

			LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t3
			ON t2.[Process_id] = t3.[Process_id]

			WHERE
				[Dwg_no] = '" & arr(0) & "'
			AND
				[Dwg_rev] = '" & arr(1) & "'
			AND
				[PartName] = '" & arr(2) & "'
			AND
				[PartNo] = '" & arr(3) & "'
            AND
				[Dash] = '" & arr(4) & "'
			AND
				[Process_Name] = '" & arr(5).Replace("'", "''") & "'
		"
                'TxtTest.Text = SqlGetDwgId
                x = CInt(StandardFunction.getSQLDataString(SqlGetDwgId))
                'MsgBox(x)

                Dim SqlGetProcessId = "
                	SELECT [Process_id]
                	  FROM [Drawing_Mecha2].[dbo].[Process_Master]
                	WHERE [Process_Name] = '" & DrpProcess.SelectedValue.Replace("'", "''") & "'
                "
                y = StandardFunction.getSQLDataString(SqlGetProcessId)
                'MsgBox(y)
            End If
        End If

        Dim Check = True
        Dim Alert As New StringBuilder("กรุณาระบุ \n")

        If TxtDrawing.Text = Nothing Then
            Check = False
            Alert.Append("- Drawing No.\n")
        End If

        If TxtRevision.Text = Nothing Then
            Check = False
            Alert.Append("- Revision\n")
        End If

        If TxtPartName.Text = Nothing Then
            Check = False
            Alert.Append("- PartName\n")
        End If

        If TxtPartNo.Text = Nothing Then
            Check = False
            Alert.Append("- PartNo.\n")
        End If

        If DrpProcess.SelectedIndex = 0 Then
            Check = False
            Alert.Append("- Process\n")
        End If

        If Check = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & Alert.ToString & "');", True)
            Exit Sub
        End If

        Dim SqlUpdate = "
        UPDATE [Drawing_Mecha2].[dbo].[Drawing_Master]
           SET [Dwg_no] = '" & TxtDrawing.Text.ToUpper & "'
        	  ,[Dwg_rev] = '" & TxtRevision.Text & "'
        	  ,[PartName] = '" & TxtPartName.Text.ToUpper & "'
        WHERE [Dwg_id] = " & x & "

        UPDATE [Drawing_Mecha2].[dbo].[Drawing_Process]
           SET [Process_id] = " & y & "
        	  ,[PartNo] = '" & TxtPartNo.Text.ToUpper & "'
              ,[Dash] = '" & TxtDash.Text & "'
        WHERE
              [Dwg_id] = " & x & "
        AND
              [Dash] = " & arr(4) & "
        "

        Dim con As New SqlConnection
        Dim command As SqlCommand

        Try
            con.ConnectionString = StandardFunction.connectionString
            con.Open()
            command = New SqlCommand(SqlUpdate, con)
            command.ExecuteNonQuery()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message & ");", True)
        Finally
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('แก้ไข Drawing สำเร็จ!! (Edit Drawing Completed)');", True)
            con.Close()
        End Try

        LoadPage()
        BtnClear_Click(sender, e)
    End Sub

    Private Sub GrdDrawing_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GrdDrawing.RowDeleting
        Dim row As GridViewRow = GrdDrawing.Rows(e.RowIndex)
        'MsgBox(row.Cells(2).Text)
        Dim n As Integer = 3
        Dim SqlGetDwgId = "
			SELECT
				--[Upload_Date]
			t1.[Dwg_id]
				--,[Dwg_no]
				--,[Dwg_rev]
				--,[PartName]
				--,[PartNo]
				--,[Process_Name]
				--,[Upload_By]
				--,[DwgProcess_id]
				--,t2.[Dwg_id]
				--,t2.[Process_id]
				--,t3.[Process_id]
			FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1

			LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Process] as t2
			ON t1.[Dwg_id] = t2.[Dwg_id]

			LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t3
			ON t2.[Process_id] = t3.[Process_id]

			WHERE
				[Dwg_no] = '" & row.Cells(n).Text & "'
			AND
				[Dwg_rev] = '" & row.Cells(n + 1).Text & "'
			AND
				[PartName] = '" & row.Cells(n + 2).Text & "'
			AND
				[PartNo] = '" & row.Cells(n + 3).Text & "'
			AND
                [Dash] = '" & row.Cells(n + 4).Text & "'
			AND
				[Process_Name] = '" & row.Cells(n + 5).Text.Replace("&#39;", "''") & "'
		"

        TxtTest.Text = SqlGetDwgId
        Dim x As Integer = CInt(StandardFunction.getSQLDataString(SqlGetDwgId))
        ''MsgBox(x)

        Dim SqlDelete = "
            DELETE FROM [Drawing_Mecha2].[dbo].[Drawing_Process]
                  WHERE [Dwg_id] = " & x & "

            DELETE FROM [Drawing_Mecha2].[dbo].[Drawing_Master]
                  WHERE [Dwg_id] = " & x & "
        "

        Dim con As New SqlConnection
        Dim command As SqlCommand

        Try
            con.ConnectionString = StandardFunction.connectionString
            con.Open()
            command = New SqlCommand(SqlDelete, con)
            command.ExecuteNonQuery()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message & ");", True)
        Finally
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('ลบ Drawing สำเร็จ!! (Edit Drawing Completed)');", True)
            con.Close()
        End Try

        LoadPage()
        BtnClear_Click(sender, e)
    End Sub

    Private Sub GrdDrawing_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GrdDrawing.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim item As String = e.Row.Cells(3).Text
            For Each button As Button In e.Row.Cells(1).Controls.OfType(Of Button)()
                If button.CommandName = "Delete" Then
                    button.Attributes("onclick") = "if(!confirm('ยืนยัน การลบ " + item + "?')){ return false; };"
                End If
            Next
        End If
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        LoadPage()
    End Sub
End Class