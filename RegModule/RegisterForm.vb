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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If String.IsNullOrWhiteSpace(StnoTxt.Text) AndAlso String.IsNullOrWhiteSpace(NmTxt.Text) Then
            MessageBox.Show("Please enter either a Student Number or Name to search.", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            con.Open()

            Dim query As String = "SELECT * FROM students WHERE student_no = @stno OR name = @name"
            Dim cmd As New MySqlCommand(query, con)

            cmd.Parameters.AddWithValue("@stno", StnoTxt.Text)
            cmd.Parameters.AddWithValue("@name", NmTxt.Text)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Student Information
                StnoTxt.Text = reader("student_no").ToString()
                NmTxt.Text = reader("name").ToString()
                DobTxt.Text = reader("dob").ToString()
                AddTxt.Text = reader("address").ToString()
                EmTxt.Text = reader("email").ToString()
                ConTxt.Text = reader("contact").ToString()
                EmCTxt.Text = reader("emergency_contact").ToString()

                ' Academic Details
                CrsTxt.Text = reader("course").ToString()
                YrLvlTxt.Text = reader("year_level").ToString()
                EnStTxt.Text = reader("enrollment_status").ToString()
                PrvTxt.Text = reader("previous_school").ToString()

                ' Enrollment Details
                EnDtPicker.Value = Convert.ToDateTime(reader("enrollment_date"))
                AYrLTxt.Text = reader("academic_year").ToString()
                StSTxt.Text = reader("student_status").ToString()
                PTCmb.SelectedItem = reader("payment_type").ToString()

                ' Subjects
                Md1Txt.Text = reader("subject1").ToString()
                Md2Txt.Text = reader("subject2").ToString()
                Md3Txt.Text = reader("subject3").ToString()
                Md4Txt.Text = reader("subject4").ToString()
                Md5Txt.Text = reader("subject5").ToString()
                Md6Txt.Text = reader("subject6").ToString()

            Else
                MessageBox.Show("No student record found with the given Student Number or Name.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

    End Sub
End Class