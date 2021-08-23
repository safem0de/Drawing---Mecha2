Public Class CycletimeCalculator
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DrpSpec.AutoPostBack = True
        End If
        LoadPage()

        TxtInsideDia.Enabled = False
        TxtOutsideDia.Enabled = False
        TxtLength.Enabled = False
        TxtFeed.Enabled = False
        BtnAdd.Enabled = False
    End Sub

    Sub LoadPage()

        If IsNothing(Session("Test")) Then
            Dim arr As New ArrayList
            arr.Add("Spec")
            arr.Add("Feed (mm/rev)")
            arr.Add("Rev (min^-1)")
            arr.Add("Cycle Time (min)")
            arr.Add("Roughness (micron)")
            arr.Add("Volume of Chip (cm^3/min)")

            BindColumn(GrdCalCyTime, AddColumns(arr), "Test")
        Else
            BindColumn(GrdCalCyTime, Session("Test"), "Test")
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

    Sub ClearEmptyRows(dt As DataTable)
        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            Dim row As DataRow = dt.Rows(i)
            If row.Item(0) Is Nothing Then
                dt.Rows.Remove(row)
            ElseIf row.Item(0).ToString = "" Then
                dt.Rows.Remove(row)
            End If
        Next
    End Sub

    Sub UpdateLabel(lblName As Label, GV As GridView)
        Dim Sum As Double = 0
        For Each x In GV.Rows
            If Not IsNothing(x.Cells(4).Text.Replace("&nbsp;", "")) Then
                Sum += CDbl(x.Cells(4).Text.Replace("&nbsp;", "0"))
            End If
        Next
        lblName.Text = "Total CycleTime : " & Sum & " (min)"
    End Sub

    Sub Clearform()
        TxtFeed.Text = Nothing
        TxtInsideDia.Text = Nothing
        TxtLength.Text = Nothing
        TxtOutsideDia.Text = Nothing
        DrpSpec.SelectedIndex = 0
    End Sub

    Private Sub DrpSpec_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpSpec.SelectedIndexChanged
        If DrpSpec.SelectedValue = "ID" Then
            TxtOutsideDia.Enabled = False
            TxtInsideDia.Enabled = True
            TxtLength.Enabled = True
            BtnAdd.Enabled = True
            TxtFeed.Enabled = True
        ElseIf DrpSpec.SelectedValue = "OD" Then
            TxtInsideDia.Enabled = False
            TxtOutsideDia.Enabled = True
            TxtLength.Enabled = True
            BtnAdd.Enabled = True
            TxtFeed.Enabled = True
        ElseIf DrpSpec.SelectedValue = "Facing" Then
            TxtLength.Enabled = False
            TxtInsideDia.Enabled = True
            TxtOutsideDia.Enabled = True
            BtnAdd.Enabled = True
            TxtFeed.Enabled = True
        End If
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim dt As DataTable = Session("Test")
        Dim CT As Double
        Dim Rx As Double
        Dim Qc As Double
        If DrpSpec.SelectedValue = "ID" Then
            CT = (CDbl(TxtLength.Text) * Math.PI * CDbl(TxtInsideDia.Text) / CDbl(TxtFeed.Text) * 1000 * CDbl(TxtRev.Text))
        ElseIf DrpSpec.SelectedValue = "OD" Then
            CT = (CDbl(TxtLength.Text) * Math.PI * CDbl(TxtOutsideDia.Text) / CDbl(TxtFeed.Text) * 1000 * CDbl(TxtRev.Text))
        ElseIf DrpSpec.SelectedValue = "Facing" Then
            CT = (Math.PI * (Math.Pow(CDbl(TxtOutsideDia.Text), 2) - Math.Pow(CDbl(TxtOutsideDia.Text), 2))) / (4000 * CDbl(TxtRev.Text) * CDbl(TxtFeed.Text))
        End If
        dt.Rows.Add(DrpSpec.SelectedValue,
                    TxtFeed.Text,
                    TxtFeed.Text,
                    CT,
                    Rx,
                    Qc)
        ClearEmptyRows(dt)
        BindColumn(GrdCalCyTime, dt, "Test")

        UpdateLabel(LblSum, GrdCalCyTime)
        Clearform()
    End Sub

    Private Sub GrdCalCyTime_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GrdCalCyTime.RowDeleting
        'Dim row = GrdCalCyTime.Rows(e.RowIndex)
        'MsgBox("test")
        Dim deldt As DataTable = Session("Test")

        If e.RowIndex >= 0 And GrdCalCyTime.Rows.Count <> 1 Then
            deldt.Rows.RemoveAt(e.RowIndex)
            BindColumn(GrdCalCyTime, deldt, "Test")
        Else
            Session("Test") = Nothing
            LoadPage()
        End If

        UpdateLabel(LblSum, GrdCalCyTime)
        Clearform()
    End Sub
End Class