Imports System.Data.SqlClient

Public Class Search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("binaryData") = Nothing
        If Not Session("CanAccess") Then
            Response.Redirect("~/Login")
        End If

        If Not Page.IsPostBack Then
            LoadPage()
            DrpProcess.AutoPostBack = True
        End If
    End Sub

    Private Sub Search_Click(sender As Object, e As EventArgs) Handles Search.Click
        'https://www.aspsnippets.com/Articles/Upload-and-Download-PDF-file-Database-in-ASPNet-using-C-and-VBNet.aspx
        Dim alert As New StringBuilder("กรุณาระบุ\n")
        Dim check As String = True

        If DrpProcess.SelectedIndex = 0 Then
            alert.Append("- Process\n")
            check = False
        End If

        If TxtLotNo.Text = Nothing Then
            alert.Append("- Request Lot\n")
            check = False
        End If

        If TxtPartNo.Text = Nothing Then
            alert.Append("- Drawing ที่ Request\n")
            check = False
        End If

        Dim bytes As Byte() = Nothing
        If check = False Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & alert.ToString & "');", True)
            Exit Sub
        Else
            Dim sqlGetProcessId = "
            SELECT [Process_id]
              FROM [Drawing_Mecha2].[dbo].[Process_Master]
              WHERE [Process_Name] = '" & DrpProcess.SelectedValue.Replace(Chr(34), """").Replace("'", "''").Trim() & "'
            "
            Dim x = StandardFunction.getSQLDataString(sqlGetProcessId)

            Dim SqlGetPdf As String = "
            With TableA As
            (
            SELECT 
	            Max([Active_Date]) as [Active_Date]
	            ,[Dwg_id]
            FROM [Drawing_Mecha2].[dbo].[Active_Log]
            Group by [Dwg_id]
            ),

            TableB As
            (Select 
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
                  ,[Process_id]
                  ,[PartNo]
            FROM [Drawing_Mecha2].[dbo].[Drawing_Master] as t1
            Left Join [Drawing_Mecha2].[dbo].[Active_Log] as t2
            On t1.[Dwg_id] = t2.[Dwg_id]
            Left Join [Drawing_Mecha2].[dbo].[Drawing_Process] as t3
            On t1.[Dwg_id] = t3.[Dwg_id]
            ),

            TableC As
            (
            Select
	            t1.[Active_Date]
	            ,t1.[Dwg_id]
	            ,[Upload_Date]
                ,[Dwg_no]
                ,[Dwg_rev]
                ,[PartName]
                ,[Dwg_PDF]
                ,[Upload_By]
	            ,[Active_Id]
                ,[Active_By]
                ,[Status]
	            ,[DwgProcess_id]
                ,[Process_id]
                ,[PartNo]
            From TableA as t1
            Left Join TableB as t2
            on
	            t1.[Active_Date] = t2.[Active_Date]
            and
	            t1.[Dwg_id] = t2.[Dwg_id]
            )

            Select * from TableC
            Where
	            [Status] = 'ACTIVE'
            And
	            [PartNo] = '" & TxtPartNo.Text.ToUpper & "'
            And
	            [Process_id] = " & CInt(x) & "
        "

            'TxtTest.Text += SqlGetPdf

            Dim constr As String = StandardFunction.connectionString
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand()
                    cmd.CommandText = SqlGetPdf
                    cmd.Connection = con
                    con.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        If sdr.Read() Then
                            bytes = DirectCast(sdr("Dwg_PDF"), Byte())
                        End If
                    End Using
                    con.Close()
                End Using
            End Using
        End If

        If bytes IsNot Nothing Then
            Session("binaryData") = bytes
            Response.Write("<script>window.open('PDF.aspx','_blank');</script>")
            'Write Success Search Data into Database
            Dim conn As New SqlConnection(StandardFunction.connectionString)
            Dim sql As String = "
                INSERT INTO [Drawing_Mecha2].[dbo].[History]
                       ([History_Date]
                       ,[Details]
                       ,[EmpNo])
                 VALUES
                       (@HistoryDate
                       ,@Details
                       ,@EmpNo)
                "

            Dim cmd As SqlCommand = New SqlCommand(sql, conn)

            Try
                cmd.Parameters.AddWithValue("@HistoryDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                cmd.Parameters.AddWithValue("@Details", TxtPartNo.Text & " : " & TxtLotNo.Text & " : " & DrpProcess.SelectedValue)
                cmd.Parameters.AddWithValue("@EmpNo", Session("User"))
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('" & ex.Message.ToString & "');", True)
            Finally
                conn.Close()
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('เพิ่ม Drawing สำเร็จ!!(Completed)');", True)
            End Try
        Else
            Clearform()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('ไม่พบ Drawing ที่ Request');", True)
        End If

    End Sub

    Protected Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        DrpProcess.SelectedIndex = 0
        Clearform()
    End Sub

    Sub Clearform()
        TxtPartNo.Text = ""
        TxtLotNo.Text = ""
        LblProcess.Text = "Process"
        DrpProcess.SelectedIndex = 0
    End Sub

    Sub LoadPage()
        Dim Sql = "SELECT [Process_Name]
            FROM [Drawing_Mecha2].[dbo].[Process_Master]"
        StandardFunction.setDropdownlist(DrpProcess, Sql, True)
    End Sub

    Protected Sub DrpProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpProcess.SelectedIndexChanged
        If Not DrpProcess.SelectedIndex = 0 Then
            Dim sqlGetProcessId = "
            SELECT [Process_id]
              FROM [Drawing_Mecha2].[dbo].[Process_Master]
              WHERE [Process_Name] = '" & DrpProcess.SelectedValue & "'
            "
            Dim x = StandardFunction.getSQLDataString(sqlGetProcessId)
            LblProcess.Text = "Process (" & x & ")"
        Else
            LblProcess.Text = "Process"
        End If
    End Sub
End Class