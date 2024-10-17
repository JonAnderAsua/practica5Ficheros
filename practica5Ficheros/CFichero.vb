Imports System.IO

Public Class CFichero
    Private pFS As FileStream
    Private pBW As BinaryWriter = Nothing
    Private pBR As BinaryReader = Nothing
    Private sFicheroActual As String
    Private iNregs As Integer
    Private iTamanioReg As Integer
    Private bBorrar As Boolean

    Public Sub New(nombre As String)
        Me.sFicheroActual = nombre
    End Sub

    Public Sub cerrarFich()
        If pBW IsNot Nothing Then
            pBW.Close()
            pBW = Nothing
        End If
        If pBR IsNot Nothing Then
            pBR.Close()
            pBR = Nothing
        End If
    End Sub

    Public Function longitudFich() As Long
        Return pFS.Length
    End Function

    Public Sub modificarReg(ByVal posicion As Integer, ByVal nuevoRegistro As CRegistro)
        Using pBW As New BinaryWriter(File.Open(sFicheroActual, FileMode.Open))
            pBW.BaseStream.Seek(posicion * iTamanioReg, SeekOrigin.Begin)
            pBW.Write(nuevoRegistro.Referencia())
            pBW.Write(nuevoRegistro.Precio())
        End Using
    End Sub


    Public Sub aniadirReg(ByVal referencia As String, ByVal precio As Double)
        pFS.Seek(0, SeekOrigin.End)
        pBW.Write(referencia)
        pBW.Write(precio)
        pBW.Flush()
        iNregs += 1
    End Sub

    Public Function leerReg(ByVal posicion As Integer) As CRegistro
        Dim reg As CRegistro
        Using pBR As New BinaryReader(File.Open(sFicheroActual, FileMode.Open))
            pBR.BaseStream.Seek(posicion * iTamanioReg.ToString, SeekOrigin.Begin)
            reg.Referencia() = pBR.ReadString()
            reg.Precio() = pBR.ReadDouble()
        End Using
        Return reg
    End Function

    Public Function buscarReg(ByVal referencia As String) As Integer
        Dim reg As CRegistro
        For i As Integer = 0 To iNregs - 1
            reg = leerReg(i)
            If reg.Referencia = referencia Then
                Return i
            End If
        Next
        Return -1
    End Function

    Public Function eliminarReg(ByVal referencia As String) As Boolean
        'Este método se puede aprovechar del creado anteriormente
        Dim encontrado As Boolean = False
        Dim reg As CRegistro
        For i As Integer = 0 To iNregs - 1
            reg = leerReg(i)
            If reg.Referencia = referencia Then
                modificarReg(i, New CRegistro("-1", 0.0))
                bBorrar = True
                encontrado = True
            End If
        Next
        Return encontrado
    End Function

    Public Function hayRegsEliminados() As Boolean
        Return bBorrar
    End Function

    Public Sub actualizarFich()
        If bBorrar Then
            Dim nuevoNombre As String = sFicheroActual & ".temp"
            Using pFSTemp As New FileStream(nuevoNombre, FileMode.Create, FileAccess.Write)
                Dim pBWTemp As New BinaryWriter(pFSTemp)
                For i As Integer = 0 To iNregs - 1
                    pFS.Seek(i * iTamanioReg, SeekOrigin.Begin)
                    Dim referencia As String = pBR.ReadString
                    Dim precio As Double = pBR.ReadDouble
                    If Not String.IsNullOrEmpty(referencia) Then
                        pBWTemp.Write(referencia)
                        pBWTemp.Write(precio)
                    End If
                Next
                pBWTemp.Close()
            End Using
            File.Replace(nuevoNombre, sFicheroActual, Nothing)
            bBorrar = False
        End If
    End Sub
End Class
