Public Class Leitor_PDF

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "Ficheiros .PDF | *.pdf"

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            PDF1.src = "C:\Users\MedSky\Desktop\MEDCD\Teste01\layout01\Report_201409221.pdf"
        End If
        'PDF1.LoadFile("")
        PDF1.Refresh()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Index.WindowState = FormWindowState.Normal
        Me.Close()
    End Sub
End Class