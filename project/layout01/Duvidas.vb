Public Class Duvidas

    Private Sub Duvidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ToolStripLabel1.DoubleClickEnabled = True Then

        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Index.WindowState = FormWindowState.Normal
        Me.Close()

    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripLabel1.Click
        MsgBox("http://www.medsky.pt", MsgBoxStyle.Information, "Website")
    End Sub

    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs)
        'Me.WindowState = FormWindowState.Minimized
        'Form3.Show()

    End Sub
End Class