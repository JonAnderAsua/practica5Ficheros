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
End Class
