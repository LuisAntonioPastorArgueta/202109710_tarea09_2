Imports System.Data.SqlClient

Module Module1
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer

    Sub Main()
        Try
            con.ConnectionString = "Data Source=LAPTOP-LP\SQLEXPRESS;Initial Catalog=tarea9;Integrated Security=True"
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            disp_data()

            ' Menú de opciones
            While True
                Console.WriteLine("Seleccione una opción:")
                Console.WriteLine("1. Realizar una conversión")
                Console.WriteLine("2. Mostrar datos")
                Console.WriteLine("3. Salir")
                Dim opcion As String = Console.ReadLine()

                Select Case opcion
                    Case "1"
                        RealizarConversion()
                    Case "2"
                        disp_data()
                    Case "3"
                        Console.WriteLine("¡Hasta luego!")
                        Exit While
                    Case Else
                        Console.WriteLine("Opción no válida. Por favor, seleccione nuevamente.")
                End Select
            End While

            con.Close()
        Catch ex As Exception
            Console.WriteLine("Se ha producido un error: " & ex.Message)
        End Try
    End Sub

    Sub RealizarConversion()
        Try
            Console.WriteLine("Ingrese la masa en libras:")
            Dim libras As Double = Double.Parse(Console.ReadLine())
            Dim kilogramos As Double = libras * 0.453592
            Console.WriteLine("Masa en kilogramos: " & kilogramos)

            Console.WriteLine("Ingrese la longitud en pulgadas:")
            Dim pulgadas As Double = Double.Parse(Console.ReadLine())
            Dim metros As Double = pulgadas * 0.0254
            Console.WriteLine("Longitud en metros: " & metros)

            Console.WriteLine("Ingrese la temperatura en Celsius:")
            Dim celsius As Double = Double.Parse(Console.ReadLine())
            Dim fahrenheit As Double = (celsius * 9 / 5) + 32
            Console.WriteLine("Temperatura en Fahrenheit: " & fahrenheit)

            Console.WriteLine("Ingrese el volumen en galones:")
            Dim galones As Double = Double.Parse(Console.ReadLine())
            Dim litros As Double = galones * 3.78541
            Console.WriteLine("Volumen en litros: " & litros)

            Console.WriteLine("Ingrese el tiempo en minutos:")
            Dim minutos As Double = Double.Parse(Console.ReadLine())
            Dim segundos As Double = minutos * 60
            Console.WriteLine("Tiempo en segundos: " & segundos)

            ' Guardar los datos en la base de datos
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "INSERT INTO conversiones (kilogramos, metros, celsius, galones, minutos, libras, pulgadas, fahrenheit, litros, segundos) VALUES (@kilogramos, @metros, @celsius, @galones, @minutos, @libras, @pulgadas, @fahrenheit, @litros, @segundos)"
            cmd.Parameters.AddWithValue("@kilogramos", kilogramos)
            cmd.Parameters.AddWithValue("@metros", metros)
            cmd.Parameters.AddWithValue("@celsius", celsius)
            cmd.Parameters.AddWithValue("@galones", galones)
            cmd.Parameters.AddWithValue("@minutos", minutos)
            cmd.Parameters.AddWithValue("@libras", libras)
            cmd.Parameters.AddWithValue("@pulgadas", pulgadas)
            cmd.Parameters.AddWithValue("@fahrenheit", fahrenheit)
            cmd.Parameters.AddWithValue("@litros", litros)
            cmd.Parameters.AddWithValue("@segundos", segundos)
            cmd.ExecuteNonQuery()

            Console.WriteLine("Registro insertado correctamente.")
        Catch ex As FormatException
            Console.WriteLine("Error de formato: asegúrate de ingresar un número válido.")
        Catch ex As Exception
            Console.WriteLine("Se ha producido un error: " & ex.Message)
        End Try
    End Sub

    Public Sub disp_data()
        Try
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT * FROM conversiones"
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)

            ' Mostrar datos en la consola
            Console.WriteLine("Datos en la tabla conversiones:")
            For Each row As DataRow In dt.Rows
                For Each col As DataColumn In dt.Columns
                    Console.Write(row(col.ColumnName) & " ")
                Next
                Console.WriteLine()
            Next
        Catch ex As Exception
            Console.WriteLine("Se ha producido un error al mostrar los datos: " & ex.Message)
        End Try
    End Sub
End Module
