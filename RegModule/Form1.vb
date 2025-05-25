Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Form1

    Dim conn As New MySqlConnection("server=localhost;user id=root;password=yourpassword;database=yourdbname")

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Usertxt.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles LogInbtn.Click
        Try
            conn.Open()

            Dim cmd As New MySqlCommand("SELECT * FROM users WHERE username=@username AND password=@password", conn)
            cmd.Parameters.AddWithValue("@username", Usertxt.Text)
            cmd.Parameters.AddWithValue("@password", Passtxt.Text)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.HasRows Then
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Proceed to main form
                RegisterForm.Show()
                Me.Hide()
            Else
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            conn.Close()
        End Try

    End Sub
End Class
