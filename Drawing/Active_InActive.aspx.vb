Imports System.Data.SqlClient

Public Class Active_InActive
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("CanAccess") And Not Session("Position") = "ENGINEER" Then 'Test Phase Due To Mecha2 No Manpower DB
            Response.Redirect("~/Login")
        End If

        If Not IsPostBack Then

        End If
        LoadPage()
    End Sub

    Sub LoadPage()
        Dim SqlLoadInActive = "
            DECLARE @DwgNo Varchar(20);
            SET @DwgNo = '%" & TxtSearchInActive.Text & "%';
            With TableA As
            (
            SELECT 
	            Max([Active_Date]) as [Active_Date]
	            ,[Dwg_id]
            FROM [Drawing_Mecha2].[dbo].[Active_Log]
            Group by [Dwg_id]
            ),

            TableB As
            (Select --*
				[Upload_Date]
				,t1.[Dwg_id]
				,[Dwg_no]
				,[Dwg_rev]
				,[PartName]
				,[Dwg_PDF]
				,[Upload_By]
				,[Active_Id]
				,[Active_Date]
				--,t2.[Dwg_id]
				,[Active_By]
				,[Status]
				,[DwgProcess_id]
				--,t3.[Dwg_id]
				,t3.[Process_id]
				,[PartNo]
				,[Process_Name]
            FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1
            Left Join [Drawing_Mecha2].[dbo].[Active_Log] as t2
            On t1.[Dwg_id] = t2.[Dwg_id]
			Left Join [Drawing_Mecha2].[dbo].[Drawing_Process] as t3
			On t1.[Dwg_id] = t3.[Dwg_id]
			Left Join [Drawing_Mecha2].[dbo].[Process_Master] as t4
			On t3.[Process_id] = t4.[Process_id]
            ),

            TableC As
            (
            Select
				t1.[Active_Date]
				,[Dwg_no]
	            ,[Dwg_rev]
	            ,[PartName]
				,[PartNo]
				,[Process_Name]
	            --,[Upload_By]
	            ,[Status]
            From TableA as t1
            Left Join TableB as t2
            on
	            t1.[Active_Date] = t2.[Active_Date]
            and
	            t1.[Dwg_id] = t2.[Dwg_id]
            )

            Select
				[Dwg_no]
	            ,[Dwg_rev]
	            ,[PartName]
				,[PartNo]
				,[Process_Name]
	            --,[Status]
			from TableC
            Where
	           [Status] = 'IN-ACTIVE'
            And
	            [Dwg_no] like @DwgNo
			Order by [Active_Date] desc
        "
        StandardFunction.fillDataTableToDataGrid(GrdInActive, SqlLoadInActive, "")

        Dim SqlLoadActive = "
            DECLARE @DwgNo Varchar(20);
            SET @DwgNo = '%" & TxtSearchActive.Text & "%';
            With TableA As
            (
            SELECT 
	            Max([Active_Date]) as [Active_Date]
	            ,[Dwg_id]
            FROM [Drawing_Mecha2].[dbo].[Active_Log]
            Group by [Dwg_id]
            ),

            TableB As
            (Select --*
				[Upload_Date]
				,t1.[Dwg_id]
				,[Dwg_no]
				,[Dwg_rev]
				,[PartName]
				,[Dwg_PDF]
				,[Upload_By]
				,[Active_Id]
				,[Active_Date]
				--,t2.[Dwg_id]
				,[Active_By]
				,[Status]
				,[DwgProcess_id]
				--,t3.[Dwg_id]
				,t3.[Process_id]
				,[PartNo]
				,[Process_Name]
            FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1
            Left Join [Drawing_Mecha2].[dbo].[Active_Log] as t2
            On t1.[Dwg_id] = t2.[Dwg_id]
			Left Join [Drawing_Mecha2].[dbo].[Drawing_Process] as t3
			On t1.[Dwg_id] = t3.[Dwg_id]
			Left Join [Drawing_Mecha2].[dbo].[Process_Master] as t4
			On t3.[Process_id] = t4.[Process_id]
            ),

            TableC As
            (
            Select
				t1.[Active_Date]
				,[Dwg_no]
	            ,[Dwg_rev]
	            ,[PartName]
				,[PartNo]
				,[Process_Name]
	            --,[Upload_By]
	            ,[Status]
            From TableA as t1
            Left Join TableB as t2
            on
	            t1.[Active_Date] = t2.[Active_Date]
            and
	            t1.[Dwg_id] = t2.[Dwg_id]
            )

            Select
				[Dwg_no]
	            ,[Dwg_rev]
	            ,[PartName]
				,[PartNo]
				,[Process_Name]
	            --,[Status]
			from TableC
            Where
	           Not [Status] = 'IN-ACTIVE'
            And
	            [Dwg_no] like @DwgNo
			Order by [Active_Date] desc
        "
        StandardFunction.fillDataTableToDataGrid(GrdActive, SqlLoadActive, "")
    End Sub

    Private Sub GrdInActive_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdInActive.SelectedIndexChanged

        Dim StrDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim row As GridViewRow = GrdInActive.SelectedRow
        Dim SqlGetId As New StringBuilder("
            SELECT
                t1.[Dwg_id]
            FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1

            LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Process] as t2
            On t1.[Dwg_id] = t2.[Dwg_id]

            LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t3
            On t2.[Process_id] = t3.[Process_id]

            WHERE 
	            [Dwg_no] = '" & row.Cells(1).Text & "'
            AND
	            [Dwg_rev] = '" & row.Cells(2).Text & "'
        ")
        If row.Cells(4).Text.Replace("&nbsp;", "") = Nothing Then
        Else
            SqlGetId.Append("AND
	            [PartNo] = '" & row.Cells(4).Text & "'")
        End If

        Dim x = StandardFunction.getSQLDataString(SqlGetId.ToString)

        Dim sql As String = "
            INSERT INTO [Drawing_Mecha2].[dbo].[Active_Log]
                   ([Active_Date]
                   ,[Dwg_id]
                   ,[Active_By]
                   ,[Status])
             VALUES
                   (@ActiveDate
                   ,@Dwgid
                   ,@ActiveBy
                   ,@Status)
            "
        Dim conn As New SqlConnection(StandardFunction.connectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)

        Try
            cmd.Parameters.AddWithValue("@ActiveDate", CDate(StrDate))
            cmd.Parameters.AddWithValue("@Dwgid", CInt(x))
            cmd.Parameters.AddWithValue("@ActiveBy", "D9302")
            cmd.Parameters.AddWithValue("@Status", "ACTIVE")

            conn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message.ToString & "');", True)
        Finally
            conn.Close()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('เพิ่ม Drawing สำเร็จ!!(Completed)');", True)
        End Try

        'TxtTest.Text += SqlGetId.ToString
        'TxtTest.Text += sql
        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Private Sub GrdActive_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GrdActive.SelectedIndexChanged

        Dim StrDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim row As GridViewRow = GrdActive.SelectedRow
        Dim SqlGetId As New StringBuilder("
            SELECT
                t1.[Dwg_id]
            FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1

            LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Process] as t2
            On t1.[Dwg_id] = t2.[Dwg_id]

            LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t3
            On t2.[Process_id] = t3.[Process_id]

            WHERE 
	            [Dwg_no] = '" & row.Cells(1).Text & "'
            AND
	            [Dwg_rev] = '" & row.Cells(2).Text & "'
        ")

        If row.Cells(4).Text.Replace("&nbsp;", "") = Nothing Then
        Else
            SqlGetId.Append("AND
	            [PartNo] = '" & row.Cells(4).Text & "'")
        End If

        Dim x = StandardFunction.getSQLDataString(SqlGetId.ToString)

        Dim sql As String = "
            INSERT INTO [Drawing_Mecha2].[dbo].[Active_Log]
                   ([Active_Date]
                   ,[Dwg_id]
                   ,[Active_By]
                   ,[Status])
             VALUES
                   (@ActiveDate
                   ,@Dwgid
                   ,@ActiveBy
                   ,@Status)
            "
        Dim conn As New SqlConnection(StandardFunction.connectionString)
        Dim cmd As SqlCommand = New SqlCommand(sql, conn)

        Try
            cmd.Parameters.AddWithValue("@ActiveDate", CDate(StrDate))
            cmd.Parameters.AddWithValue("@Dwgid", CInt(x))
            cmd.Parameters.AddWithValue("@ActiveBy", "D9302")
            cmd.Parameters.AddWithValue("@Status", "IN-ACTIVE")

            conn.Open()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message.ToString & "');", True)
        Finally
            conn.Close()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('เพิ่ม Drawing สำเร็จ!!(Completed)');", True)
        End Try

        Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
    End Sub

    Private Sub BtnActiveSearch_Click(sender As Object, e As EventArgs) Handles BtnActiveSearch.Click
        LoadPage()
    End Sub

    Private Sub BtnInActiveSearch_Click(sender As Object, e As EventArgs) Handles BtnInActiveSearch.Click
        LoadPage()
    End Sub

    Private Sub GrdActive_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdActive.PageIndexChanging
        GrdActive.PageIndex = e.NewPageIndex
        GrdActive.DataBind()
    End Sub

    Private Sub GrdInActive_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GrdInActive.PageIndexChanging
        GrdInActive.PageIndex = e.NewPageIndex
        GrdInActive.DataBind()
    End Sub
End Class