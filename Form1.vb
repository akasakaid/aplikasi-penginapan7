﻿Imports BCrypt.Net.BCrypt
Imports System.Data.SqlClient

Public Class Form1
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'login

        Dim PanjangWindow = Me.ClientSize.Width
        Dim TinggiWindow = Me.ClientSize.Height
        'TextBox1.Left = (Me.ClientSize.Width - TextBox1.Width) \ 2
        'FormName.Location = New Point()
        'MsgBox(PanjangWindow / 2)
        Panel.Width = PanjangWindow - 700
        Panel.Height = TinggiWindow - 300
        Dim WidthPanel = Panel.Width
        Dim HeightPanel = Panel.Height
        Panel.Location = New Point((PanjangWindow - Panel.Width) \ 2, (TinggiWindow - Panel.Height) \ 2)
        FormName.Location = New Point((WidthPanel - FormName.Width) \ 2, (HeightPanel - FormName.Height) \ 250)
        'boxUsername.Enabled = False
        username.Location = New Point((WidthPanel - username.Width) \ 2, (HeightPanel - username.Height) - 200)
        password.Location = New Point((WidthPanel - password.Width) \ 2, (HeightPanel - password.Height) - 130)
        buttonLogin.Location = New Point((WidthPanel - buttonLogin.Width) \ 2, (HeightPanel - buttonLogin.Height) - 50)
    End Sub

    Private Sub buttonLogin_Click(sender As Object, e As EventArgs) Handles buttonLogin.Click
        If boxUsername.Text = "" And passwordBox.Text = "" Then
            MsgBox("Username dan Password harus diisi !")
        End If
        If boxUsername.Text <> "" And passwordBox.Text <> "" Then
            Dim username = boxUsername.Text
            Dim password = passwordBox.Text
            Try
                Dim connectionString As String = "Data Source=localhost;Initial Catalog=db_hotel;User ID=mine;Password=user@123;"
                Dim connection As New SqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT TOP (1) * FROM [tb_pegawai] WHERE [nama] = @value"
                Dim command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@value", username)
                Dim reader As SqlDataReader = command.ExecuteReader()
                'MsgBox(reader.Read())
                'If reader.Read() <> True Then
                'MsgBox("username atau password salah !")
                'connection.Close()
                'reader.Close()
                'Exit Sub
                'End If
                'MsgBox(reader.Read())
                'MsgBox("koneksi berhasil")
                While reader.Read()
                    'MsgBox("babi")
                    Dim resId As Integer = reader.GetInt32(0)
                    Dim resName As String = reader.GetString(1)
                    Dim resEmail As String = reader.GetString(2)
                    Dim resPassword As String = reader.GetString(3)
                    Dim resLevel As String = reader.GetString(4)
                    MsgBox(resPassword)
                    If BCrypt.Net.BCrypt.Verify(password, resPassword) Then
                        'MsgBox("password valdi")
                        Me.Hide()
                        Dim form2 As New Form2
                        form2.ShowDialog()
                    Else
                        MsgBox("username atau password salah !")
                    End If
                    'Console.WriteLine(Resusername)
                    'MsgBox(resName)
                End While
                reader.Close()
                connection.Close()
            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try


        End If
    End Sub
End Class