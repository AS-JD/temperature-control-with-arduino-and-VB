Imports System
Imports System.IO.Ports
Public Class Form1
    Dim value1 As Decimal

    Dim value2 As Decimal


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        ButtonConnect.Enabled = False
        ButtonConnect.BringToFront()
        ButtonDisconnect.Enabled = False
        ButtonDisconnect.SendToBack()
        ComboBoxBaudRate.SelectedItem = "9600"

    End Sub

    Private Sub ButtonScanPort_Click(sender As Object, e As EventArgs) Handles ButtonScanPort.Click

        ComboBoxPort.Items.Clear()
        Dim myPort As Array
        Dim i As Integer
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBoxPort.Items.AddRange(myPort)
        i = ComboBoxPort.Items.Count
        i = i - i
        Try
            ComboBoxPort.SelectedIndex = i
            ButtonConnect.Enabled = True
        Catch ex As Exception
            MsgBox("Com port not detected", MsgBoxStyle.Critical, "Warning !!!")
            ComboBoxPort.Text = ""
            ComboBoxPort.Items.Clear()
            Return
        End Try
        ButtonConnect.Enabled = True
        ButtonConnect.BringToFront()
        ComboBoxPort.DroppedDown = True
    End Sub

    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        ButtonConnect.Enabled = False
        ButtonConnect.SendToBack()

        SerialPort1.BaudRate = ComboBoxBaudRate.SelectedItem
        SerialPort1.PortName = ComboBoxPort.SelectedItem

        SerialPort1.Open()


        Timer1.Start()





        ButtonDisconnect.Enabled = True
        ButtonDisconnect.BringToFront()
    End Sub

    Private Sub ButtonDisconnect_Click(sender As Object, e As EventArgs) Handles ButtonDisconnect.Click
        ButtonDisconnect.Enabled = False
        ButtonDisconnect.SendToBack()
        Timer1.Stop()




        SerialPort1.Close()
        ButtonConnect.Enabled = False
        ButtonConnect.BringToFront()



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim s As String


        s = TextBox1.Text + "," + "," + "," + ","
        Dim somstring() As String
        somstring = s.Split(New Char() {","c})
        TextBox2.Text = somstring(0)
        Try
            value1 = Convert.ToDecimal(TextBox2.Text)
        Catch ex As Exception
            TextBox2.Text = " "
        End Try

        TextBox3.Text = somstring(1)
        Try
            value2 = Convert.ToDecimal(TextBox3.Text)
        Catch ex As Exception
            TextBox3.Text = " "
        End Try
        'SerialPort1.Write(TrackBar1.Value & Chr(10))










    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived




        Try
            Dim data As String = " "

            data = SerialPort1.ReadExisting()

            If TextBox1.InvokeRequired Then
                TextBox1.Invoke(DirectCast(Sub() TextBox1.Text &= data, MethodInvoker))
            Else
                TextBox1.Text &= data

            End If
        Catch ex As Exception
        End Try
    End Sub










    'Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    'Try
    'Dim j As String = SerialPort1.ReadExisting
    'Label2.Text = j.ToString
    'h ex As Exception
    'End Try

    'End Sub
End Class



