Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO

Public Class Upload
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Session("CanAccess") And Not Session("Position") = "ENGINEER" Then 'Test Phase Due To Mecha2 No Manpower DB
            Response.Redirect("~/Login")
        End If

        If Not IsPostBack Then
            Dim SqlDrpProcess = "SELECT [Process_Name] FROM [Drawing_Mecha2].[dbo].[Process_Master]"
            StandardFunction.setDropdownlist(DrpProcess, SqlDrpProcess, True)
            DrpProcess.AutoPostBack = True
        End If

        LoadPage()
    End Sub

    Sub LoadPage()

        If IsNothing(Session("PartNo")) Then
            Dim arr As New ArrayList
            arr.Add("No.")
            arr.Add("PartNo")
            BindColumn(GrdPartNo, AddColumns(arr), "PartNo")
        Else
            BindColumn(GrdPartNo, Session("PartNo"), "PartNo")
        End If

        If IsNothing(Session("Process")) Then
            Dim arr As New ArrayList
            arr.Add("Process")
            BindColumn(GrdProcess, AddColumns(arr), "Process")
        Else
            BindColumn(GrdProcess, Session("Process"), "Process")
        End If
    End Sub

    Sub BindColumn(GV As GridView, dt As DataTable, Optional SsName As String = Nothing)
        If Not SsName = Nothing Then
            Session(SsName) = dt
        End If
        GV.DataSource = dt
        GV.DataBind()
    End Sub

    Function AddColumns(ArrCol As ArrayList) As DataTable
        Dim dt As New DataTable
        For Each x In ArrCol
            dt.Columns.Add(x)
        Next
        dt.Rows.Add()
        Return dt
    End Function

    Protected Sub BtnView_Click(sender As Object, e As EventArgs) Handles BtnView.Click
        CreateByteSession(FileUploadPdf, "binaryData", LblUploadPdf)
        If Not IsNothing(Session("binaryData")) Then
            Response.Write("<script>window.open('PDF.aspx','_blank');</script>")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('ไม่พบไฟล์');", True)
        End If
    End Sub

    Protected Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click
        Dim StrDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Dim Alert As New StringBuilder("กรุณาระบุ\n")
        Dim Check As String = True

        CreateByteSession(FileUploadPdf, "binaryData", LblUploadPdf)
        If IsNothing(Session("binaryData")) Then
            Check = False
            Alert.Append("- ไฟล์ pdf เท่านั้น\n")
        End If

        If TxtDwgNo.Text = Nothing Then
            Check = False
            Alert.Append("- Drawing No.\n")
        End If

        If TxtPartName.Text = Nothing Then
            Check = False
            Alert.Append("- PartName\n")
        End If

        If TxtRev.Text = Nothing Then
            Check = False
            Alert.Append("- Revision\n")
        End If

        Dim dtPartNo As DataTable = Session("PartNo")
        Dim dtProcess As DataTable = Session("Process")

        For Each x As DataRow In dtPartNo.Rows
            If x.Item("PartNo").ToString.Trim = Nothing Then
                Check = False
                Alert.Append("- PartNo\n")
            End If

            If x.Item("No.").ToString.Trim = Nothing Then
                Check = False
                Alert.Append("- No.\n")
            End If
        Next

        For Each x As DataRow In dtProcess.Rows
            If x.Item("Process").ToString.Trim = Nothing Then
                Check = False
                Alert.Append("- Process\n")
            End If
        Next

        For Each i As DataRow In dtPartNo.Rows
            For Each j As DataRow In dtProcess.Rows
                Dim SqlChkDup = "
                    DECLARE	@DwgNo Varchar(25)
		            ,@Dwgrev Varchar(5)
		            ,@PartNo Varchar(25)
		            ,@PrcsName Varchar(25);

                    SET @DwgNo = '" & TxtDwgNo.Text & "';
                    SET @Dwgrev = '" & TxtRev.Text & "';
                    SET @PartNo = '" & i.Item("PartNo") & "';
                    SET @PrcsName = '" & j.Item("Process").Replace(Chr(34), """").Replace("'", "''").ToUpper.Trim() & "';

                    If
                    (SELECT
	                    'TRUE'
                      FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1

                      LEFT JOIN [Drawing_Mecha2].[dbo].[Drawing_Process] as t2
                      ON t1.[Dwg_id] = t2.[Dwg_id]

                      LEFT JOIN [Drawing_Mecha2].[dbo].[Process_Master] as t3
                      ON t2.[Process_id] = t3.[Process_id]

                    WHERE
	                    t1.[Dwg_no] = @DwgNo
                    AND
	                    [Dwg_rev] = @Dwgrev
                    AND
	                    [PartNo] = @PartNo
                    AND
	                    [Process_Name] = @PrcsName
                    ) = 'TRUE'

                    Select 'TRUE' Else Select 'FALSE'
                "
                'TxtTest.Text += SqlChkDup
                Dim bool = StandardFunction.getSQLDataString(SqlChkDup)

                If CBool(bool) = True Then
                    Check = False
                    Alert.Append("- Drawing " & i.Item("PartNo") & "," & j.Item("Process") & " ซ้ำ\n")
                End If

            Next
        Next


        If Check = True Then

            Dim conn As New SqlConnection(StandardFunction.connectionString)
            Dim sql As String = "
                INSERT INTO [Drawing_Mecha2].[dbo].[Drawing_Master]
                       ([Upload_Date]
                       ,[Dwg_no]
                       ,[Dwg_rev]
                       ,[PartName]
                       ,[Dwg_PDF]
                       ,[Upload_By])
                 VALUES
                       (@UploadDate
                       ,@DwgNo
                       ,@DwgRev
                       ,@PartName
                       ,@Pdf
                       ,@User)
                "

            Dim cmd As SqlCommand = New SqlCommand(sql, conn)

            Try
                cmd.Parameters.AddWithValue("@UploadDate", CDate(StrDate))
                cmd.Parameters.AddWithValue("@DwgNo", TxtDwgNo.Text.ToUpper.Trim())
                cmd.Parameters.AddWithValue("@DwgRev", TxtRev.Text.ToUpper.Trim())
                cmd.Parameters.AddWithValue("@PartName", TxtPartName.Text.ToUpper.Trim())
                cmd.Parameters.AddWithValue("@Pdf", Session("binaryData"))
                cmd.Parameters.AddWithValue("@User", Session("User"))

                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message.ToString & "');", True)
            Finally
                conn.Close()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('เพิ่ม Drawing สำเร็จ!!(Completed)');", True)
            End Try

            Dim SqlGetDwgId = "
            SELECT [Dwg_id]
              FROM [Drawing_Mecha2].[dbo].[Drawing_Master]
              WHERE	[Upload_Date] = '" & StrDate & "'
	            AND	[Dwg_no] = '" & TxtDwgNo.Text.Replace(Chr(34), """").Replace("'", "''").ToUpper.Trim() & "'
	            AND	[Dwg_rev] = '" & TxtRev.Text.Replace(Chr(34), """").Replace("'", "''").ToUpper.Trim() & "'
	            AND	[Upload_By] = '" & Session("User") & "'
            "
            Dim z = StandardFunction.getSQLDataString(SqlGetDwgId)

            Dim sql2 = "
                INSERT INTO [Drawing_Mecha2].[dbo].[Drawing_Process]
                       ([Dwg_id]
                       ,[Process_id]
                       ,[Dash]
                       ,[PartNo])
                 VALUES
                       (@Dwg_id
                       ,@Process_id
                       ,@Dash
                       ,@PartNo)
            "
            'Dim dtPartNo As DataTable = Session("PartNo")
            'Dim dtProcess As DataTable = Session("Process")

            For Each i As DataRow In dtPartNo.Rows
                For Each j As DataRow In dtProcess.Rows
                    'MsgBox(z & "_" & i.Item("PartNo") & "_" & j.Item("Process"))

                    Dim sqlGetProcessId = "
            SELECT [Process_id]
              FROM [Drawing_Mecha2].[dbo].[Process_Master]
              WHERE [Process_Name] = '" & j.Item("Process").Replace(Chr(34), """").Replace("'", "''").ToUpper.Trim() & "'
            "
                    Dim x = StandardFunction.getSQLDataString(sqlGetProcessId)

                    cmd = New SqlCommand(sql2, conn)

                    Try
                        cmd.Parameters.AddWithValue("@Dwg_id", CInt(z))
                        cmd.Parameters.AddWithValue("@Process_id", CInt(x))
                        cmd.Parameters.AddWithValue("@Dash", i.Item("No.")) '<--Add Dash
                        cmd.Parameters.AddWithValue("@PartNo", i.Item("PartNo"))

                        conn.Open()
                        cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message.ToString & "');", True)
                    Finally
                        conn.Close()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('เพิ่ม Drawing สำเร็จ!!(Completed)');", True)
                    End Try
                Next
            Next

            Dim sql3 As String = "
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

            cmd = New SqlCommand(sql3, conn)
            Try
                cmd.Parameters.AddWithValue("@ActiveDate", CDate(StrDate))
                cmd.Parameters.AddWithValue("@Dwgid", CInt(z))
                cmd.Parameters.AddWithValue("@ActiveBy", Session("User"))
                cmd.Parameters.AddWithValue("@Status", "IN-ACTIVE")

                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message.ToString & "');", True)
            Finally
                conn.Close()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('เพิ่ม Drawing สำเร็จ!!(Completed)');", True)
            End Try
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & Alert.ToString & "');", True)
            Exit Sub
        End If

        Clearform()
    End Sub

    Protected Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        Clearform()
    End Sub

    Sub Clearform()
        TxtDwgNo.Text = ""
        TxtRev.Text = ""
        TxtDash.Text = ""
        TxtPartNo.Text = ""
        TxtPartName.Text = ""
        LblProcess.Text = "Process"
        DrpProcess.SelectedIndex = 0
        FileUploadPdf.Dispose()
        FileUploadPdf.Enabled = True

        Dim arr0 As New ArrayList
        arr0.Add("Process")
        BindColumn(GrdProcess, AddColumns(arr0), "Process")

        Dim arr1 As New ArrayList
        arr1.Add("No.")
        arr1.Add("PartNo")
        BindColumn(GrdPartNo, AddColumns(arr1), "PartNo")

        Session("binaryData") = Nothing
    End Sub

    Private Sub DrpProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpProcess.SelectedIndexChanged
        Dim dtProcess As DataTable = Session("Process")
        CreateByteSession(FileUploadPdf, "binaryData", LblUploadPdf)

        If Not DrpProcess.SelectedIndex = 0 Then
            Dim sqlGetProcessId = "
            SELECT [Process_id]
              FROM [Drawing_Mecha2].[dbo].[Process_Master]
              WHERE [Process_Name] = '" & DrpProcess.SelectedValue & "'
            "
            Dim x = StandardFunction.getSQLDataString(sqlGetProcessId)
            LblProcess.Text = "Process (" & x & ")"

            For i As Integer = dtProcess.Rows.Count - 1 To 0 Step -1
                Dim row As DataRow = dtProcess.Rows(i)
                If row.Item(0) Is Nothing Or row.Item(0).ToString = "" Then
                    dtProcess.Rows.Remove(row)
                    'else ถ้าซ้ำ
                ElseIf row.Item("Process").Equals(DrpProcess.SelectedValue) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('Process : " & DrpProcess.SelectedValue & " ซ้ำ!!(Duplicate)');", True)
                    Exit Sub
                End If
            Next

            dtProcess.Rows.Add(DrpProcess.SelectedValue)
            TxtPartNo.Text = Nothing

            BindColumn(GrdProcess, dtProcess, "Process")
        Else
            LblProcess.Text = "Process"
        End If
    End Sub

    Private Sub GrdPartNo_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GrdPartNo.RowDeleting
        Dim deldtPartNo As DataTable = Session("PartNo")
        If e.RowIndex >= 0 And GrdPartNo.Rows.Count <> 1 Then
            deldtPartNo.Rows.RemoveAt(e.RowIndex)
            BindColumn(GrdPartNo, deldtPartNo, "PartNo")
        Else
            Dim arr As New ArrayList
            arr.Add("No.")
            arr.Add("PartNo")
            BindColumn(GrdPartNo, AddColumns(arr), "PartNo")
        End If
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        Dim dtPartNo As DataTable = Session("PartNo")
        CreateByteSession(FileUploadPdf, "binaryData", LblUploadPdf)

        Dim Check As Boolean = True
        Dim Alert As New StringBuilder()

        If TxtDash.Text = Nothing Then
            Check = False
            Alert.Append("กรุณาระบุ No.\n")
        End If

        If TxtPartNo.Text = Nothing Then
            Check = False
            Alert.Append("กรุณาระบุ Part No./SAP No.\n")
        End If

        If Check = True Then

            For i As Integer = dtPartNo.Rows.Count - 1 To 0 Step -1
                Dim row As DataRow = dtPartNo.Rows(i)

                'If row.Item("No.").Equals(TxtDash.Text) Then
                '    Alert.Append("No." & TxtDash.Text & "\n")
                'End If

                'If row.Item("PartNo").Equals(TxtPartNo.Text) Then
                '    Alert.Append("Part No." & TxtPartNo.Text & "\n")
                'End If

                If row.Item("No.").Equals(TxtDash.Text) And row.Item("PartNo").Equals(TxtPartNo.Text) Then
                    Alert.Append("No." & TxtDash.Text & "\nPart No." & TxtPartNo.Text & "\n ซ้ำ")
                End If


                If row.Item(0) Is Nothing Or row.Item(0).ToString = "" Then
                    dtPartNo.Rows.Remove(row)
                End If
            Next

            If Not Alert.ToString = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Duplicate", "alert('" & Alert.ToString & "\nซ้ำ (Duplicate)')", True)
                Exit Sub
            End If

            dtPartNo.Rows.Add(TxtDash.Text.ToUpper, TxtPartNo.Text.ToUpper)
            DrpProcess.SelectedIndex = 0

            BindColumn(GrdPartNo, dtPartNo, "PartNo")
            TxtDash.Text = Nothing
            TxtPartNo.Text = Nothing
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "EmptyText", "alert('" & Alert.ToString & "')", True)
        End If

        Alert.Clear()
    End Sub

    Private Sub GrdProcess_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GrdProcess.RowDeleting
        Dim deldtProcess As DataTable = Session("Process")
        If e.RowIndex >= 0 And GrdProcess.Rows.Count <> 1 Then
            deldtProcess.Rows.RemoveAt(e.RowIndex)
            BindColumn(GrdProcess, deldtProcess, "Process")
        Else
            Dim arr As New ArrayList
            arr.Add("Process")
            BindColumn(GrdProcess, AddColumns(arr), "Process")
        End If
    End Sub

    Sub CreateByteSession(target As FileUpload, SessionName As String, LabelName As Label)
        Dim bytes As Byte() = Nothing

        If IsNothing(Session(SessionName)) Then
            If ".pdf" = Path.GetExtension(target.FileName) And target.HasFile Then

                Using br As BinaryReader = New BinaryReader(target.PostedFile.InputStream)
                    bytes = br.ReadBytes(target.PostedFile.ContentLength)
                End Using

                If Not IsNothing(bytes) Then
                    LabelName.Text = "Pdf file Upload (File Selected)"
                    LabelName.ForeColor = Color.Green
                    LabelName.Font.Bold = True
                    target.Enabled = False
                End If
            End If

            Session(SessionName) = bytes
        End If
    End Sub
End Class