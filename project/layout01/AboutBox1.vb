Public NotInheritable Class Sobre

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
        Index.WindowState = FormWindowState.Normal

    End Sub

    Private Sub LabelVersion_Click(sender As Object, e As EventArgs) Handles LabelVersion.Click

    End Sub

    Private Sub TableLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel.Paint

    End Sub

    Private Sub LogoPictureBox_Click(sender As Object, e As EventArgs) Handles LogoPictureBox.Click
        System.Diagnostics.Process.Start("http://medsky.pt")
    End Sub

    Private Sub Descricao_TextChanged(sender As Object, e As EventArgs) Handles Descricao.TextChanged

    End Sub
End Class
