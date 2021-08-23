Public Class PDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("binaryData") IsNot Nothing Then
            Response.ContentType = "Application/pdf"
            Response.BinaryWrite(Session("binaryData"))
            Response.End()
        Else
            Response.Redirect("/")
        End If
    End Sub

    Private Sub PDF_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Session("binaryData") = Nothing
    End Sub
End Class