Imports MySql.Data.MySqlClient

Public Class SortingGradeFrm

    Dim con As New MySqlConnection("server=localhost;userid=root;password=yourpassword;database=yourdbname")

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub RgtBtn_Click(sender As Object, e As EventArgs) Handles EnterBtn.Click
        ' Optional: Validate that fields are not empty
        If Md1Txt.Text = "" OrElse Md2Txt.Text = "" OrElse Md3Txt.Text = "" OrElse
           Md4Txt.Text = "" OrElse Md5Txt.Text = "" OrElse Md6Txt.Text = "" Then
            MessageBox.Show("Please fill in all subject grades.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            con.Open()
            Dim cmd As New MySqlCommand("INSERT INTO grades (student_no, subject1_grade, subject2_grade, subject3_grade, subject4_grade, subject5_grade, subject6_grade)
                                         VALUES (@stno, @g1, @g2, @g3, @g4, @g5, @g6)", con)

            ' Optional: Replace with actual Student No textbox if available
            cmd.Parameters.AddWithValue("@stno", StnoTxt.Text)

            cmd.Parameters.AddWithValue("@g1", Md1Txt.Text)
            cmd.Parameters.AddWithValue("@g2", Md2Txt.Text)
            cmd.Parameters.AddWithValue("@g3", Md3Txt.Text)
            cmd.Parameters.AddWithValue("@g4", Md4Txt.Text)
            cmd.Parameters.AddWithValue("@g5", Md5Txt.Text)
            cmd.Parameters.AddWithValue("@g6", Md6Txt.Text)

            cmd.ExecuteNonQuery()
            MessageBox.Show("Grades successfully recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles SearchBtn.Click
        If StnoTxt.Text = "" Then
            MessageBox.Show("Please enter the student number.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            con.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM grades WHERE student_no = @stno", con)
            cmd.Parameters.AddWithValue("@stno", StnoTxt.Text)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Display subject names
                Sub1Lbl.Text = reader("subject1_name").ToString()
                Sub2Lbl.Text = reader("subject2_name").ToString()
                Sub3Lbl.Text = reader("subject3_name").ToString()
                Sub4Lbl.Text = reader("subject4_name").ToString()
                Sub5Lbl.Text = reader("subject5_name").ToString()
                Sub6Lbl.Text = reader("subject6_name").ToString()

                ' Display grades
                Md1Txt.Text = reader("subject1_grade").ToString()
                Md2Txt.Text = reader("subject2_grade").ToString()
                Md3Txt.Text = reader("subject3_grade").ToString()
                Md4Txt.Text = reader("subject4_grade").ToString()
                Md5Txt.Text = reader("subject5_grade").ToString()
                Md6Txt.Text = reader("subject6_grade").ToString()
            Else
                MessageBox.Show("Student not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub RegisterFrmBtn_Click(sender As Object, e As EventArgs) Handles RegisterFrmBtn.Click
        Dim frm As RegisterForm = Application.OpenForms.OfType(Of RegisterForm)().FirstOrDefault()

        If frm Is Nothing Then
            frm = New RegisterForm()
        End If

        frm.Show()
        frm.BringToFront()
        Me.Hide()
    End Sub



    Private Sub SortingGradeFrmBtn_Click(sender As Object, e As EventArgs) Handles SortingGradeFrmBtn.Click
        MessageBox.Show("You're already in this form.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub LogOutBtn_Click(sender As Object, e As EventArgs) Handles LogOutBtn.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim loginForm As Form1 = Application.OpenForms.OfType(Of Form1)().FirstOrDefault()

            If loginForm Is Nothing Then
                loginForm = New Form1()
            End If

            loginForm.Show()
            loginForm.BringToFront()

            Me.Hide() ' Hide current form instead of closing
        End If
    End Sub
End Class