Imports System
Imports System.ComponentModel.Design

Module Program
    Sub Main(args As String())

    End Sub
    Sub menu()
        Dim opc As Integer
        Do
            Console.WriteLine("FICHEROS")
            Console.WriteLine("--------")
            Console.WriteLine("1)Crear Fichero")
            Console.WriteLine("2)Abrir Fichero")
            Console.WriteLine("3)Añadir Fichero")
            Console.WriteLine("4)Modificar Fichero")
            Console.WriteLine("5)Eliminar Fichero")
            Console.WriteLine("6)Visualizar Fichero")
            Console.WriteLine("7)Salir")
            Try
                opc = Console.ReadLine()
            Catch ex As Exception
                Console.WriteLine("Error" & ex.Message)
            End Try
            Select Case opc
                Case 1
                Case 2
                Case 3
                Case 4
                Case 5
                Case 6
                Case 7
                    Console.Write("Agur")
                Case Else
                    Console.WriteLine("Introduce una opcion correcta")

            End Select

        Loop While opc <> 7
    End Sub
End Module
