Public Class PrdHistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

        End If

        LoadPage()
    End Sub

    Sub LoadPage()

        Dim Sql = "
        SELECT TOP (1000) [History_Date]
              ,[Details]
              ,[EmpNo]
          FROM [Drawing_Mecha2].[dbo].[History]
        "
        StandardFunction.fillDataTableToDataGrid(GrdHistory, Sql, "-")
    End Sub

    Private Sub GrdHistory_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GrdHistory.Sorting
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "alert('Waiting... Developer');", True)
    End Sub
End Class