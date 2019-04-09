Imports System.Text
Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Permissions
Imports System.Web
Imports System.Linq



Module INI
    <DllImport("kernel32.dll", SetLastError:=True)> Public Function WritePrivateProfileString _
      (ByVal lpApplicationName As String,
      ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    End Function

    Public Function lerINI(ByVal novo_valor As String, ByVal diretorio_arquivo As String, ByVal secao As String) As String
        ''INI READER ''
        Dim entrada As StringBuilder = New StringBuilder(255)
        Dim intSize As Integer
        intSize = GetPrivateProfileString(diretorio_arquivo, secao, "", entrada, 255, novo_valor)
        Return entrada.ToString
    End Function
    Public Function lerINIEntrada(ByVal novo_valor As String, ByVal diretorio_arquivo As String, ByVal secao As String) As String
        Dim bytes As StringBuilder = New StringBuilder(255)
        Dim intSize2 As Integer
        intSize2 = GetPrivateProfileString(diretorio_arquivo, secao, "", bytes, 255, novo_valor)
        Return bytes.ToString
    End Function
    ''' <summary>
    ''' READ ini as this config by default parameters
    ''' </summary>
    ''' <param path="valor"></param>
    ''' <param area(section)="secao"></param>
    ''' <param key="chave"></param>
    ''' <returns>full section data grab by Key</returns>
    Public Function LerConfiguracao(valor As String, secao As String, chave As String) As String
        Dim linhas As String() = valor.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

        Dim emsecao As Boolean = False

        For Each linha As String In linhas
            If emsecao Then

                If linha.StartsWith("[") Then
                    emsecao = False
                    Continue For
                End If

                If linha.Contains("=") Then
                    Dim nomechave = linha.Split("=")(0)

                    If (nomechave = chave) Then
                        Return linha.Split("=")(1)
                    End If
                End If

            Else
                If linha.StartsWith("[") Then
                    Dim nomesecao As String = linha.Substring(1, linha.IndexOf("]") - 1)

                    If nomesecao = secao Then
                        emsecao = True
                        Continue For
                    End If
                End If
            End If


        Next

        Return String.Empty
    End Function





    <DllImport("kernel32")> _
    Private Function GetPrivateProfileString(Section As String, Key As String, Value As String, Result As StringBuilder, Size As Integer, FileName As String) As Integer
    End Function


    <DllImport("kernel32")> _
    Private Function GetPrivateProfileString(Section As String, Key As Integer, Value As String, <MarshalAs(UnmanagedType.LPArray)> Result As Byte(), Size As Integer, FileName As String) As Integer
    End Function

    <DllImport("kernel32")> _
    Private Function GetPrivateProfileString(Section As Integer, Key As String, Value As String, <MarshalAs(UnmanagedType.LPArray)> Result As Byte(), Size As Integer, FileName As String) As Integer
    End Function

    Public path As String

    Public Function GetSectionNames() As String()
        Dim maxsize As Integer = 500
        While True
            Dim bytes As Byte() = New Byte(maxsize - 1) {}
            Dim size As Integer = GetPrivateProfileString(0, "", "", bytes, maxsize, path)
            If size < maxsize - 2 Then
                Dim Selected As String = Encoding.ASCII.GetString(bytes, 0, size - (If(size > 0, 1, 0)))
                Return Selected.Split(New Char() {ControlChars.NullChar})
            End If
            maxsize *= 2
        End While
    End Function
    Public Function GetEntryNames(section As String) As String()
        Dim maxsize As Integer = 500
        While True
            Dim bytes As Byte() = New Byte(maxsize - 1) {}
            Dim size As Integer = GetPrivateProfileString(section, 0, "", bytes, maxsize, path)
            If size < maxsize - 2 Then
                Dim entries As String = Encoding.ASCII.GetString(bytes, 0, size - (If(size > 0, 1, 0)))
                Return entries.Split(New Char() {ControlChars.NullChar})
            End If
            maxsize *= 2
        End While
    End Function
    Public Function GetEntryValue(section As String, entry As String) As Object
        Dim maxsize As Integer = 250
        While True
            Dim result As New StringBuilder(maxsize)
            Dim size As Integer = GetPrivateProfileString(section, entry, "", result, maxsize, path)
            If size < maxsize - 1 Then
                Return result.ToString()
            End If
            maxsize *= 2
        End While


    End Function




End Module


Module dire

    Public path As String = AppDomain.CurrentDomain.BaseDirectory
    Public dt As String = IO.Path.Combine(path, "settings.ini")
    Dim dtxt As String = "C:\Users\MedSky\Desktop\MEDCD\Teste01\layout01\settings.txt"
    Dim dtxml As String = "C:\Users\MedSky\Desktop\MEDCD\Teste01\layout01\settings.xml"
End Module

Module GERAR

    Public Function open(ByVal med As Boolean, ByVal viewer As Boolean, ByVal report As Boolean)
        'med = INI.lerINI(dt, "general", "open_mediamenu")
        'viewer = INI.lerINI(dt, "general", "open_viewer")
        'report = INI.lerINI(dt, "general", "open_report")
        Dim r As String = lerINI(dt, "general", "n_exam")
        Dim rr As Boolean = med
        Dim rr2 As Boolean = viewer
        Dim rr3 As Boolean = report



        Select Case rr
            Case True
                Index.Show()
            Case False
                Index.Close()

            Case Else
                MsgBox("ERRO , FALTA DE INFO", MsgBoxStyle.Information, "")

        End Select


        Select Case rr2
            Case True
                System.Diagnostics.Process.Start(lerINI(dt, "general", "pd_viewer"))
            Case False

            Case Else
                MsgBox("ERRO , FALTA DE INFO", MsgBoxStyle.Information, "")

        End Select

        Select Case rr3
            Case True
                For fff As Integer = 1 To r
                    System.Diagnostics.Process.Start(lerINI(dt, "Exam" & fff, "relatorio_pdf"))
                Next
            Case False

            Case Else
                MsgBox("ERRO , FALTA DE INFO", MsgBoxStyle.Information, "")

        End Select










        If viewer = True Then

            Process.Start(lerINI(dt, "general", "pd_viewer"))
        Else


        End If

        If report = True Then
            
        Else

        End If
        If med = True Then
            Index.Show()

        Else
            End
        End If
    End Function



End Module
Public Class Index

    Public path As String = AppDomain.CurrentDomain.BaseDirectory
    Public dt As String = IO.Path.Combine(path, "config\settings.ini")
    Dim dtxt As String = IO.Path.Combine(path, "config\settings.txt")
    Dim dtxml As String = IO.Path.Combine(path, "config\settings.xml")

    Private Sub pic_click(sender As Object, e As EventArgs)
        '...

    End Sub





    Private Sub ToolStripLabel4_Click(sender As Object, e As EventArgs)


        Sobre.Show()
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripProgressBar1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub Exit_Button_Click(sender As Object, e As EventArgs)




        'Dim r As String
        'r = MsgBox("Terminar programa?", MsgBoxStyle.YesNo, "Exit")
        'If r = vbYes Then
        '    End
        'Else
        '    MsgBox("Boa Continuação de Leitura", MsgBoxStyle.OkOnly, "Greetings MedSky")
        'End If

    End Sub

    Private Sub IndividualToolStripMenuItem_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub ToolStripLabel5_Click(sender As Object, e As EventArgs)
        Duvidas.Show()
    End Sub
    Private Sub Ind_Relatorio03_CheckedChanged(sender As Object, e As EventArgs) Handles Ind_Relatorio03.CheckedChanged

    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click


    End Sub

    Private Sub PDFToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub TableLayoutPanel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)


    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        System.Diagnostics.Process.Start(lerINI(dt, "general", "pd_viewer"))
        ' System.Diagnostics.Process.Start(lerINI(dtxt, "general", "pd_viewer"))
        'System.Diagnostics.Process.Start(lerINI(dtxml, "general", "pd_viewer"))

    End Sub

    Private Sub Info_Patient_Name_Click(sender As Object, e As EventArgs) Handles Info_Patient_Name.Click

    End Sub
    Private Sub Index_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' INI Option
        GERAR.open(lerINI(dt, "general", "open_mediamenu"), lerINI(dt, "general", "open_viewer"), lerINI(dt, "general", "open_report"))





        Dim c As String = lerINI(dt, "general", "n_exam")
        'Dim n_row As String = 5
        Dim Top(c) As TableLayoutPanel
        Dim report(c) As TableLayoutPanel
        Dim b(c) As Button
        Dim labels(5) As Label
        Dim labele(5) As Label
        Dim label As Label
        Dim txtopen(c) As Label
        Dim topbor(c) As TableLayoutPanel
        Dim picopenreport(c) As PictureBox

        'general.ini

        PictureBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(lerINI(dt, "general", "picture1_color"))
        PictureLogo.BackColor = System.Drawing.ColorTranslator.FromHtml(lerINI(dt, "general", "picture1_color"))
        PictureLogo.ImageLocation = lerINI(dt, "general", "logo")
        Me.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "index_color"))
        Button1.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "button_open_viewer_color"))
        Button2.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "button_company_color"))
        Info_Patient_Name.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "info_pacient_name_forecolor"))
        Info_Patient_ID.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "info_pacient_id_forecolor"))
        Info_Patient_Name.Text = lerINI(dt, "general", "patient_name")
        Info_Patient_ID.Text = lerINI(dt, "general", "patient_id")
        Label1.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "label1_backcolor"))
        Label2.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "label2_backcolor"))
        Label1.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "label1_color_word"))
        Label2.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "label2_color_word"))
        n_exam_mediamenu.Text = "Exams Number: " & lerINI(dt, "general", "n_exam")
        help_button.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "top_help"))
        about_button.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "top_aboutus"))
        PictureBox3.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "color_behind_button"))

        help_button.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "picture1_color"))
        about_button.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "picture1_color"))


        'Dim open_viewer As String = lerINI(dt, "general", "open_viewer")
        'Dim open_report As String = lerINI(dt, "general", "open_report")
        'Dim mediamenu As String = lerINI(dt, "general", "open_mediamenu")

        'If open_viewer = "1" Then
        '    System.Diagnostics.Process.Start(lerINI(dt, "general", "pd_viewer"))
        'Else

        'End If
        'If open_report = "1 " Then
        '    For x As Integer = 0 To c

        '        Process.Start(lerINI(dt, "exam" & x, "relatorio_pdf"))
        '    Next
        'Else
        'End If
        'If mediamenu = "0" Then
        '    End
        'Else

        'End If





        'MsgBox(lerINI("C:\Users\MedSky\Desktop\Teste01\layout01\settings.ini", "general", "index_color").ToString)



        ' AREA DE CONSTRUÇÃO



        PictureBox3.BackColor = ColorTranslator.FromHtml("#3c89b9")



        'Dim lblNew As New Label()
        'lblNew.Text = "b"
        'lblNew.ForeColor = System.Drawing.Color.White
        'lblNew.Visible = True
        'lblNew.TextAlign = ContentAlignment.MiddleCenter
        'lblNew.Font = New Font("Decker", 8)
        'lblNew.Dock = DockStyle.Top

        'Dim lblNew2 As New Label()
        'lblNew2.Text = "a"
        'lblNew2.ForeColor = System.Drawing.Color.White
        'lblNew2.Visible = True
        'lblNew2.TextAlign = ContentAlignment.MiddleCenter
        'lblNew2.Font = New Font("Decker", 8)
        'lblNew2.Dock = DockStyle.Left


        'Dim lblNew3 As New Label()
        'lblNew3.Text = "c"
        'lblNew3.ForeColor = System.Drawing.Color.White
        'lblNew3.Visible = True
        'lblNew3.TextAlign = ContentAlignment.MiddleCenter
        'lblNew3.Font = New Font("Decker", 8)
        'lblNew3.Dock = DockStyle.Top




        Dim m As String = lerINI(dt, "general", "n_entrada_exam")
        Dim filereader As System.IO.StreamReader
        filereader = My.Computer.FileSystem.OpenTextFileReader(dt)
        Dim key(Convert.ToInt32(m) - 1) As String
        Dim stringreader As String


        'loop - READ ALL EXAM ON INI FILE'
        Do While filereader.Peek >= 0
            stringreader = filereader.ReadLine()
            If stringreader = "[Exam1]" Then
                stringreader = filereader.ReadLine()
                Dim aa As Integer = 1
                Do While aa <= Convert.ToInt32(m) And stringreader.Contains("=")
                    Dim stringarray() As String
                    stringarray = stringreader.Split("=")
                    key.SetValue(stringarray(0), aa - 1)
                    aa = aa + 1
                    stringreader = filereader.ReadLine()
                Loop

            End If
        Loop







        Dim pic As PictureBox

        'pic bottom medcd

        pic = New PictureBox
        pic.ImageLocation = lerINI(dt, "general", "logo_medcd")
        'pic.Anchor = System.Windows.Forms.AnchorStyles.Top
        pic.BackColor = System.Drawing.Color.Transparent
        pic.ImeMode = System.Windows.Forms.ImeMode.NoControl
        pic.Location = New System.Drawing.Point(TableLayoutPanel1.Location.X, TableLayoutPanel1.Location.Y)
        pic.Name = "pic"
        pic.Dock = DockStyle.Bottom
        pic.Size = New System.Drawing.Size(111, 64)
        pic.TabIndex = 11
        pic.TabStop = False
        pic.Visible = True
        'Me.Controls.Add(pic)


        PictureBox2.ImageLocation = lerINI(dt, "general", "logo_medcd")
        PictureBox2.Size = New System.Drawing.Size(125, 45)
        PictureBox2.BringToFront()
        'PictureBox2.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "medcd_logo_backcolor"))


        TableLayoutPanel1.SendToBack()


        PictureBox2.BackColor = TableLayoutPanel1.BackColor



        Dim letra As String = lerINI(dt, "general", "font_table")
        For x As Integer = 1 To c

            Top(x) = New TableLayoutPanel
            topbor(x) = New TableLayoutPanel


            Top(x).ColumnCount = 3
            Top(x).RowCount = m

            Dim header As Integer = 50
            'Dim tamanhotabela As Integer = Height
            'Dim tamanhotabela2 As Integer = Height
            'Dim tamanhobotao As Integer = Height

            'topbor(x).ColumnCount = 0
            'topbor(x).RowCount = 1
            'topbor(x).BringToFront()

            'topbor(x).Location = New Point(Top(x).Location.X, Top(x).Location.Y)
            'topbor(x).Size = New Size(Top(x).Size.Width + 200, Top(x).Size.Height)

            'topbor(x).CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset



            Top(x).ImeMode = Windows.Forms.ImeMode.HangulFull


            AddHandler Top(x).CellPaint, AddressOf Me.TableLayoutPanel4_CellPaint


            Top(x).CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            Top(x).AutoSize = False
            Top(x).Width = 665
            Top(x).SendToBack()

            'Top(x).Anchor = AnchorStyles.Top
            Top(x).GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            Top(x).ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))

            Dim aa As Integer = 0
            For Each s In key

                Top(x).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
                Top(x).RowStyles(aa).Height = 100

                label = New Label
                label.Text = s
                label.Font = New Font(letra, 8.0!, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                label.Anchor = AnchorStyles.Top
                label.BackColor = Color.Transparent
                label.AutoSize = True
                Top(x).Controls.Add(label, 0, aa)
                label = New Label
                label.Text = lerINI(dt, "Exam" & x, s)
                label.AutoSize = True
                label.BackColor = Color.Transparent
                label.Font = New Font(letra, 8.0!, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                label.Anchor = AnchorStyles.Top
                Top(x).Controls.Add(label, 1, aa)
                aa = aa + 1
                label.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "table_letters_forecolor"))
                label.Font = New Font(letra, 11.25!, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Next

            Top(x).Location = New Point(43, 400 + (x - 1) * (aa * 20 + header))
            Top(x).SendToBack()
            Top(x).Margin = New Padding(10, 3, 10, 3)
            Top(x).BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "table_exam_color"))

            'labels(0) = New Label
            'labels(1) = New Label
            'labels(2) = New Label
            'labels(3) = New Label
            'labels(4) = New Label

            'labels(0).Text = lerINI(dt, "Exam" & x, "modality").ToString
            'labels(1).Text = lerINI(dt, "Exam" & x, "accession_number").ToString
            'labels(2).Text = lerINI(dt, "Exam" & x, "date").ToString
            'labels(3).Text = lerINI(dt, "Exam" & x, "exam").ToString
            'labels(4).Text = lerINI(dt, "Exam" & x, "images").ToString

            'Top(x).Controls.Add(labels(0), 1, 0)
            'Top(x).Controls.Add(labels(1), 1, 1)
            'Top(x).Controls.Add(labels(2), 1, 2)
            'Top(x).Controls.Add(labels(3), 1, 3)
            'Top(x).Controls.Add(labels(4), 1, 4)

            'labele(0) = New Label
            'labele(1) = New Label
            'labele(2) = New Label
            'labele(3) = New Label
            'labele(4) = New Label

            'labele(0).Text = "123"
            'labele(1).Text = "GF"
            'labele(2).Text = "HGFH"
            'labele(3).Text = "DFDS"
            'labele(4).Text = "DFDG"

            'Top(x).Controls.Add(labele(0), 0, 1)
            'Top(x).Controls.Add(labele(1), 1, 1)
            'Top(x).Controls.Add(labele(2), 2, 1)
            'Top(x).Controls.Add(labele(3), 3, 1)
            'Top(x).Controls.Add(labele(4), 4, 1)


            'For laa As Integer = 0 To 4


            '    labels(laa) = New Label
            '    'labels(laa).Text = lerINI(dt, "Exam" & x, "modality").ToString
            '    Top(x).Controls.Add(labels(laa), 1, laa)
            'Next












            'For lab As Integer = 0 To 4
            '    labels(lab) = New Label

            '    labels(lab).Text = "MNMNMNNN"


            '    Top(x).Controls.Add(labels(lab), 1, lab)

            ' Next















            'For ii As Integer = 0 To 4

            '    Top(x).Controls.Add(labels(ii), ii, 1)
            '    labels(ii) = New Label
            '    labels(i).ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "table_letters_forecolor"))
            '    labels(0).Text = lerINI(dt, "exam" + x, "modality")
            '    labels(1).Text = lerINI(dt, "exam" + x, "accession_number")
            '    labels(2).Text = lerINI(dt, "exam" + x, "date")
            '    labels(3).Text = lerINI(dt, "exam" + x, "exam")
            '    labels(4).Text = lerINI(dt, "exam" + x, "images")



            'Next


            report(x) = New TableLayoutPanel
            b(x) = New Button
            txtopen(x) = New Label










            report(x).ColumnCount = 0
            report(x).RowCount = 0
            report(x).CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
            report(x).AutoSize = False
            report(x).BringToFront()
            report(x).Size = New System.Drawing.Size(300, 100)
            report(x).BackColor = ColorTranslator.FromHtml("#FFFFFF")
            'report(x).Anchor = AnchorStyles.Right
            report(x).Location = New Point(428, 400 + (x - 1) * (aa * 20 + header))
            report(x).GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            report(x).ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
            report(x).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            report(x).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            report(x).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            report(x).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            report(x).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            report(x).Margin = New Padding(3, 3, 3, 3)
            report(x).AutoSize = False
            report(x).BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "table_color_rel"))



            b(x).Width = 80
            b(x).Height = 80
            b(x).Location = New Point(Top(x).Location.X + 544, 430 + (x - 1) * (aa * 20 + header))
            b(x).UseVisualStyleBackColor = False
            'b(x).Anchor = System.Windows.Forms.AnchorStyles.Right
            'b(x).Appearance = Appearance.Button
            ' b(x).CheckAlign = ContentAlignment.MiddleCenter
            b(x).Font = New Font("Century Gothic", 14.25!, FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            b(x).ForeColor = Color.Transparent
            b(x).BackColor = Top(x).BackColor
            b(x).BackgroundImage = Global.MEDCD.My.Resources.Resources.icon_pdf
            b(x).BackgroundImageLayout = ImageLayout.Zoom
            b(x).ImeMode = ImeMode.NoControl
            b(x).Size = New Size(50, 50)
            b(x).TabIndex = 5
            b(x).TabStop = True
            b(x).TextAlign = ContentAlignment.BottomCenter
            'b(x).Checked = False
            b(x).Margin = New Padding(3, 3, 3, 3)
            b(x).AutoSize = False
            b(x).Name = lerINI(dt, "Exam" & x, "relatorio_pdf").ToString
            b(x).FlatStyle = FlatStyle.Flat
            AddHandler b(x).Click, AddressOf Me.button_click
            AddHandler txtopen(x).Click, AddressOf Me.txtopen_click




            'picopenreport(x).Location = New Point(593, 400 + (x - 1) * (aa * 20 + header))

            'picopenreport(x).Width = 136
            'picopenreport(x).Height = 104
            'picopenreport(x).BackColor = Color.Transparent
            'picopenreport(x).BackgroundImage = Global.layout01.My.Resources.Resources.icon_pdf
            'picopenreport(x).Name = lerINI(dt, "Exam" & x, "relatorio_pdf").ToString
            'picopenreport(x).BackgroundImageLayout = ImageLayout.Zoom











            txtopen(x).Text = "Open Report"
            txtopen(x).ForeColor = ColorTranslator.FromHtml("#697f8d")
            txtopen(x).Location = New Point(b(x).Location.X - 8, b(x).Location.Y + 50)
            'txtopen(x).Location = New Point(picopenreport(x).Location.X + 20, picopenreport(x).Location.Y + 100)
            txtopen(x).Font = New Font(letra, 8.0!, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            txtopen(x).Name = lerINI(dt, "Exam" & x, "relatorio_pdf").ToString
            txtopen(x).BackColor = Top(x).BackColor

            txtopen(x).Size = New Size(80, 15)



            Me.Controls.Add(topbor(x))
            Me.Controls.Add(Top(x))
            Me.Controls.Add(txtopen(x))
            Me.Controls.Add(b(x))
            'Me.Controls.Add(report(x))


            txtopen(x).BringToFront()

            b(x).BringToFront()

        Next



        TableLayoutPanel1.Location = New Point(0, Top(c).Location.Y + 142)
        PictureBox2.Location = New Point(636, Top(c).Location.Y + 142)

        '+ 285

        '+ 285

        Label3.BackColor = PictureBox1.BackColor

        If c >= 3 Then
            PictureBox1.Size = New Size(766, PictureBox1.Size.Height)
            PictureBox3.Size = New Size(757, PictureBox3.Size.Height)
            TableLayoutPanel1.Size = New Size(757, TableLayoutPanel1.Size.Height)
            PictureBox2.Location = New Point(PictureBox2.Location.X - 10, PictureBox2.Location.Y)
        Else


        End If




        'bottom

        'info_bottom.Text = lerINI(dt, "general", "info_bottom")
        info_bottom_1.Text = lerINI(dt, "general", "info_bottom_1")
        info_bottom_2.Text = lerINI(dt, "general", "info_bottom_2")
        info_bottom_3.Text = lerINI(dt, "general", "info_bottom_3")
        info_bottom_4.Text = lerINI(dt, "general", "info_bottom_4")
        'WebLink.Text = lerINI(dt, "general", "info_bottom_weblink")

        info_total.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "info_bottom_color"))

        'WebLink.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "picture_background_bottom"))
        info_total.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "picture_background_bottom"))
        info_total_2.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "picture_background_bottom"))
        info_total.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "bottom_letters_forecolor"))
        info_total_2.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "bottom_letters_forecolor"))
        info_total.Font = New Font(letra, 10.0!, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        info_total_2.Font = New Font(letra, 10.0!, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        'WebLink.LinkColor = ColorTranslator.FromHtml(lerINI(dt, "general", "bottom_link_letters_forecolor"))
        info_bottom.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "info_bottom_color"))

        'info_bottom.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "picture_background_bottom"))


        info_total.Text = "," & info_bottom_1.Text & ", " & info_bottom_2.Text & " " & vbNewLine
        info_total_2.Text = info_bottom_3.Text & ", " & info_bottom_4.Text



        'PictureBox2.BackColor = ColorTranslator.FromHtml(lerINI(dt, "general", "logo_medcd_color"))






























        'TXT OPTION



        'GERAR.open(lerINI(dtxt, "general", "open_mediamenu"), lerINI(dtxt, "general", "open_viewer"), lerINI(dtxt, "general", "open_report"))





        'Dim ctxt As String = lerINI(dtxt, "general", "n_exam")
        'Dim n_row As String = 5
        'Dim Toptxt(ctxt) As TableLayoutPanel
        'Dim reporttxt(ctxt) As TableLayoutPanel
        'Dim btxt(ctxt) As Button
        'Dim labelstxt(5) As Label
        'Dim labeletxt(5) As Label
        'Dim labeltxt As Label


        'general.txt()

        'PictureBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(lerINI(dtxt, "general", "picture1_color"))
        'PictureLogo.BackColor = System.Drawing.ColorTranslator.FromHtml(lerINI(dtxt, "general", "picture1_color"))
        'PictureLogo.ImageLocation = lerINI(dtxt, "general", "logo")
        'Me.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "index_color"))
        'Button1.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "button_open_viewer_color"))
        'Button2.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "button_company_color"))
        'Info_Patient_Name.ForeColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "info_pacient_name_forecolor"))
        'Info_Patient_ID.ForeColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "info_pacient_id_forecolor"))
        'Info_Patient_Name.Text = lerINI(dtxt, "general", "patient_name")
        'Info_Patient_ID.Text = lerINI(dtxt, "general", "patient_id")
        'Label1.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "label1_backcolor"))
        'Label2.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "label2_backcolor"))
        'Label1.ForeColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "label1_color_word"))
        'Label2.ForeColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "label2_color_word"))
        'n_exam_mediamenu.Text = "Numero de Exames : " & lerINI(dtxt, "general", "n_exam") & " Exames"



        'Dim mtxt As String = lerINI(dtxt, "general", "n_entrada_exam")
        'Dim filereadertxt As System.IO.StreamReader
        'filereadertxt = My.Computer.FileSystem.OpenTextFileReader(dtxt)
        'Dim keytxt(Convert.ToInt32(mtxt) - 1) As String
        'Do While filereadertxt.Peek >= 0
        '    stringreader = filereadertxt.ReadLine()
        '    If stringreader = "[Exam1]" Then
        '        stringreader = filereadertxt.ReadLine()
        '        Dim aa As Integer = 1
        '        Do While aa <= Convert.ToInt32(mtxt) And stringreader.Contains("=")
        '            Dim stringarray() As String
        '            stringarray = stringreader.Split("=")
        '            key.SetValue(stringarray(0), aa - 1)
        '            aa = aa + 1
        '            stringreader = filereadertxt.ReadLine()
        '        Loop

        '    End If
        'Loop






        'PictureBox2.ImageLocation = lerINI(dtxt, "general", "logo_medcd")
        'PictureBox2.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "medcd_logo_backcolor"))





        'Dim letratxt As String = lerINI(dtxt, "general", "font_table")
        'For tx As Integer = 1 To ctxt

        '    Toptxt(tx) = New TableLayoutPanel

        '    Toptxt(tx).ColumnCount = 2
        '    Toptxt(tx).RowCount = m

        '    Dim header As Integer = 20
        '    Dim tamanhotabela As Integer = Height
        '    Dim tamanhotabela2 As Integer = Height
        '    Dim tamanhobotao As Integer = Height



        '    Toptxt(tx).CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
        '    Toptxt(tx).AutoSize = False
        '    Toptxt(tx).Width = 393
        '    Top(x).Anchor = AnchorStyles.Top
        '    Toptxt(tx).GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        '    Toptxt(tx).ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))

        '    Dim aa As Integer = 0
        '    For Each s In key

        '        Toptxt(tx).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        '        Toptxt(tx).RowStyles(aa).Height = 40
        '        labeltxt = New Label
        '        labeltxt.Text = s
        '        labeltxt.Font = New Font(letra, 11.25!, FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        '        labeltxt.AutoSize = True
        '        Toptxt(tx).Controls.Add(label, 0, aa)
        '        labeltxt = New Label
        '        labeltxt.Text = lerINI(dt, "Exam" & tx, s)
        '        labeltxt.AutoSize = True
        '        Toptxt(tx).Controls.Add(label, 1, aa)
        '        aa = aa + 1
        '        labeltxt.ForeColor = ColorTranslator.FromHtml(lerINI(dt, "general", "table_letters_forecolor"))
        '        labeltxt.Font = New Font(letra, 11.25!, FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '    Next

        '    Toptxt(tx).Location = New Point(43, 400 + (tx - 1) * (aa * 20 + header))
        '    Toptxt(tx).SendToBack()
        '    Toptxt(tx).Margin = New Padding(3, 3, 3, 3)
        '    Toptxt(tx).BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "table_exam_color"))


        '    reporttxt(tx) = New TableLayoutPanel
        '    btxt(tx) = New Button

        '    reporttxt(tx).ColumnCount = 0
        '    reporttxt(tx).RowCount = 0
        '    reporttxt(tx).CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset
        '    reporttxt(tx).AutoSize = False
        '    reporttxt(tx).BringToFront()
        '    reporttxt(tx).Size = New System.Drawing.Size(300, 100)
        '    reporttxt(tx).BackColor = ColorTranslator.FromHtml("#00FFFF")
        '    report(x).Anchor = AnchorStyles.Right
        '    reporttxt(tx).Location = New Point(428, 400 + (tx - 1) * (aa * 20 + header))
        '    reporttxt(tx).GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        '    reporttxt(tx).ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        '    reporttxt(tx).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        '    reporttxt(tx).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        '    reporttxt(tx).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        '    reporttxt(tx).RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        '    reporttxt(tx).Margin = New Padding(3, 3, 3, 3)
        '    reporttxt(tx).AutoSize = False
        '    reporttxt(tx).BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "table_color_rel"))



        '    btxt(tx).Width = 136
        '    btxt(tx).Height = 104
        '    btxt(tx).Location = New Point(593, 400 + (tx - 1) * (aa * 20 + header))
        '    btxt(tx).UseVisualStyleBackColor = False
        '    b(x).Anchor = System.Windows.Forms.AnchorStyles.Right
        '    b(x).Appearance = Appearance.Button
        '    b(x).CheckAlign = ContentAlignment.MiddleCenter
        '    btxt(tx).Font = New Font("Century Gothic", 14.25!, FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        '    btxt(tx).ForeColor = Color.Gold
        '    btxt(tx).BackColor = Color.DarkGreen
        '    btxt(tx).BackgroundImage = Global.layout01.My.Resources.Resources._787538_dark_blue_digital_art_gray_minimalistic_orange_pastel_pastels_red_simple_simple_background_simplistic_squares_tapeta
        '    btxt(tx).ImeMode = ImeMode.NoControl
        '    btxt(tx).Size = New Size(136, 100)
        '    btxt(tx).TabIndex = 5
        '    btxt(tx).TabStop = True
        '    btxt(tx).Text = "Open Report"
        '    btxt(tx).TextAlign = ContentAlignment.MiddleCenter
        '    b(x).Checked = False
        '    btxt(tx).Margin = New Padding(3, 3, 3, 3)
        '    btxt(tx).AutoSize = False

        '    btxt(tx).Name = lerINI(dtxt, "Exam" & tx, "relatorio_pdf").ToString
        '    AddHandler btxt(tx).Click, AddressOf Me.button_click






        '    Me.Controls.Add(Toptxt(tx))

        '    Me.Controls.Add(btxt(tx))
        '    Me.Controls.Add(reporttxt(tx))
        'Next



        'TableLayoutPanel1.Location = New Point(0, Toptxt(ctxt).Location.Y + 150)
        'PictureBox2.Location = New Point(640, Toptxt(ctxt).Location.Y + 150)



        'info_bottom.Text = lerINI(dtxt, "general", "info_bottom")
        'info_bottom_1.Text = lerINI(dtxt, "general", "info_bottom_1")
        'info_bottom_2.Text = " | " & lerINI(dtxt, "general", "info_bottom_2")
        'info_bottom_3.Text = " | " & lerINI(dtxt, "general", "info_bottom_3")
        'WebLink.Text = lerINI(dtxt, "general", "info_bottom_weblink")

        'info_total.ForeColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "info_bottom_color"))

        'WebLink.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "picture_background_bottom"))
        'info_total.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "picture_background_bottom"))

        'WebLink.LinkColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "bottom_link_letters_forecolor"))
        'info_bottom.ForeColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "info_bottom_color"))

        'info_bottom.BackColor = ColorTranslator.FromHtml(lerINI(dtxt, "general", "picture_background_bottom"))


        'info_total.Text = "    " & info_bottom_1.Text & " " & " " & info_bottom_2.Text & " " & "" & info_bottom_3.Text
























        'XML OPTION




    End Sub
    Private Sub button_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim b As Button = DirectCast(sender, Button)



        Process.Start(b.Name)




    End Sub
    Private Sub txtopen_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim bc As Label = DirectCast(sender, Label)

        ' Dim c As String = lerINI(dt, "general", "n_exam")
        'For cc As Integer = c

        Process.Start(bc.Name)
        'Me.WindowState = FormWindowState.Minimized
        ' Me.SendToBack()

        'Next


    End Sub

    Private Sub TableLayoutPanel4_CellPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.TableLayoutCellPaintEventArgs) Handles TableLayoutPanel3.CellPaint
        Dim hexCol As String = lerINI(dt, "general", "frist_row")
        Dim sb As New SolidBrush(System.Drawing.ColorTranslator.FromHtml(hexCol))


        If e.Row = 0 Then
            e.Graphics.FillRectangle(sb, e.CellBounds)
        Else
            'e.Graphics.FillRectangle(sb, e.CellBounds)
        End If
    End Sub
    Private Sub panel1_MouseHover(sender As Object, e As System.EventArgs) Handles Button1.MouseHover
        
    End Sub
    Private Sub Ind_Relatorio_1_CheckedChanged(sender As Object, e As EventArgs) Handles Ind_Relatorio_1.CheckedChanged
        'System.Diagnostics.Process.Start(lerINI(dt, "exam", "relatorio"))
    End Sub

    Private Sub Ind_Relatorio_2_CheckedChanged(sender As Object, e As EventArgs) Handles Ind_Relatorio_2.CheckedChanged
        System.Diagnostics.Process.Start(lerINI(dt, "exam2", "relatorio"))
    End Sub

    Private Sub Ind_Relatorio_4_CheckedChanged(sender As Object, e As EventArgs) Handles Ind_Relatorio_4.CheckedChanged
        System.Diagnostics.Process.Start(lerINI(dt, "exam4", "relatorio"))
    End Sub

    Private Sub Ind_Relatorio_5_CheckedChanged(sender As Object, e As EventArgs) Handles Ind_Relatorio_5.CheckedChanged
        System.Diagnostics.Process.Start(lerINI(dt, "exam5", "relatorio"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        System.Diagnostics.Process.Start(lerINI(dt, "general", "company"))
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles info_bottom_2.Click

    End Sub

    Private Sub WebLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles WebLink.LinkClicked
        System.Diagnostics.Process.Start(lerINI(dt, "general", "info_bottom_weblink"))
    End Sub

    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs)

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles info_total.Click

    End Sub

    Private Sub Index_Scroll(sender As Object, e As ScrollEventArgs) Handles MyBase.Scroll

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub help_button_Click(sender As Object, e As EventArgs) Handles help_button.Click
        Duvidas.Show()

    End Sub

    Private Sub about_button_Click(sender As Object, e As EventArgs) Handles about_button.Click
        Sobre.Show()

    End Sub
End Class

