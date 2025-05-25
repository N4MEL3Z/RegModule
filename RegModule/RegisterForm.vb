Imports MySql.Data.MySqlClient

Public Class RegisterForm

    Dim con As New MySqlConnection("server=localhost;userid=root;password=yourpassword;database=yourdbname")


    Private Sub RegisterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ComboBox
        PTCmb.DropDownStyle = ComboBoxStyle.DropDownList
        PTCmb.Items.Clear()
        PTCmb.Items.Add("Full")
        PTCmb.Items.Add("Partial")


    End Sub

    Private Sub ClearBtn_Click(sender As Object, e As EventArgs) Handles ClearBtn.Click
        ' Student Info
        StnoTxt.Clear()
        NmTxt.Clear()
        DobTxt.Clear()
        AddTxt.Clear()
        EmTxt.Clear()
        ConTxt.Clear()
        EmCTxt.Clear()

        ' Academic Detail
        CrsTxt.Clear()
        YrLvlTxt.Clear()
        EnStTxt.Clear()
        PrvTxt.Clear()

        ' Enrollment Detail
        EnDtPicker.Value = DateTime.Now
        AYrLTxt.Clear()
        StSTxt.Clear()
        PTCmb.SelectedIndex = -1

        ' Subjects
        Md1Txt.Clear()
        Md2Txt.Clear()
        Md3Txt.Clear()
        Md4Txt.Clear()
        Md5Txt.Clear()
        Md6Txt.Clear()
    End Sub


    Private Sub RgtBtn_Click(sender As Object, e As EventArgs) Handles RgtBtn.Click
        Try
            con.Open()

            Dim cmd As New MySqlCommand("INSERT INTO students 
            (student_no, name, dob, address, email, contact, emergency_contact, 
            course, year_level, enrollment_status, previous_school, 
            enrollment_date, academic_year, student_status, payment_type, 
            subject1, subject2, subject3, subject4, subject5, subject6) 
            VALUES 
            (@stno, @name, @dob, @addr, @email, @contact, @emergency, 
            @course, @year, @status, @previous, 
            @enrolldate, @ayear, @sstatus, @ptype, 
            @sub1, @sub2, @sub3, @sub4, @sub5, @sub6)", con)

            ' Student Info
            cmd.Parameters.AddWithValue("@stno", StnoTxt.Text)
            cmd.Parameters.AddWithValue("@name", NmTxt.Text)
            cmd.Parameters.AddWithValue("@dob", DobTxt.Text)
            cmd.Parameters.AddWithValue("@addr", AddTxt.Text)
            cmd.Parameters.AddWithValue("@email", EmTxt.Text)
            cmd.Parameters.AddWithValue("@contact", ConTxt.Text)
            cmd.Parameters.AddWithValue("@emergency", EmCTxt.Text)

            ' Academic Details
            cmd.Parameters.AddWithValue("@course", CrsTxt.Text)
            cmd.Parameters.AddWithValue("@year", YrLvlTxt.Text)
            cmd.Parameters.AddWithValue("@status", EnStTxt.Text)
            cmd.Parameters.AddWithValue("@previous", PrvTxt.Text)

            ' Enrollment Details
            cmd.Parameters.AddWithValue("@enrolldate", EnDtPicker.Value)
            cmd.Parameters.AddWithValue("@ayear", AYrLTxt.Text)
            cmd.Parameters.AddWithValue("@sstatus", StSTxt.Text)
            cmd.Parameters.AddWithValue("@ptype", PTCmb.Text)

            ' Subjects
            cmd.Parameters.AddWithValue("@sub1", Md1Txt.Text)
            cmd.Parameters.AddWithValue("@sub2", Md2Txt.Text)
            cmd.Parameters.AddWithValue("@sub3", Md3Txt.Text)
            cmd.Parameters.AddWithValue("@sub4", Md4Txt.Text)
            cmd.Parameters.AddWithValue("@sub5", Md5Txt.Text)
            cmd.Parameters.AddWithValue("@sub6", Md6Txt.Text)

            cmd.ExecuteNonQuery()
            MessageBox.Show("Registration Successful!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub SortingGradeFrmBtn_Click(sender As Object, e As EventArgs) Handles SortingGradeFrmBtn.Click
        Dim frm As SortingGradeFrm = Application.OpenForms.OfType(Of SortingGradeFrm)().FirstOrDefault()

        If frm Is Nothing Then
            frm = New SortingGradeFrm()
        End If

        frm.Show()
        frm.BringToFront()
        Me.Hide()
    End Sub


    Private Sub RegisterFrmBtn_Click(sender As Object, e As EventArgs) Handles RegisterFrmBtn.Click
        ' Since already in RegisterForm, show message
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