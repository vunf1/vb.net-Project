Imports System.Net.Mail
Public Class Form3

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MyMessage As New MailMessage
        Try
            MyMessage.From = New MailAddress("jokass.workplace@gmail.com")
            MyMessage.To.Add("jokass.workplace@gmail.com")
            MyMessage.Subject = TextBox3.Text
            MyMessage.Body = TextBox1.Text
            Dim SMTP As New SmtpClient("smtp.gmail.com")
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("jokass.workplace@gmail.com", "a4386bjoao")
            SMTP.Send(MyMessage)
        Catch ex As Exception
            Me.Hide()

        End Try
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Duvidas.WindowState = FormWindowState.Normal
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("Sure you want Clear the form?", MsgBoxStyle.YesNoCancel, "clear form")
        If MsgBoxResult.Yes Then
            MsgBox("You chose to clear the form", MsgBoxStyle.OkOnly, "")
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        End If

        If MsgBoxResult.No Then
            MsgBox("Operation Canceled", MsgBoxStyle.OkOnly, "")
        End If


    End Sub
End Class