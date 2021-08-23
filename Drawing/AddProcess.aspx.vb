Public Class AddProcess
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        LoadPage()
    End Sub

    Sub LoadPage()
        Dim sql = "
            SELECT [Process_id]
                  ,[Process_Name]
              FROM [Drawing_Mecha2].[dbo].[Process_Master]
        "
        StandardFunction.fillDataTableToDataGrid(GrdProcess, sql, "-")
    End Sub

End Class