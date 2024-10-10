Public Class CRegistro
    Private referencia As String
    Private precio As Double

    'Contructor sin parametros
    Public Sub New()
        referencia = ""
    End Sub
    'Constructor con parametros
    Public Sub New(ref As String, pre As Double)
        referencia = ref
        precio = pre
    End Sub

    'Metodos geter
    Public Property Referencia() As Integer
        Set(ByVal ref As Integer)
            referencia = ref
        End Set
        Get
            Return Referencia
        End Get
    End Property

    Public Property Precio() As Double
        Set(ByVal pre As Double)
            precio = pre
        End Set
        Get
            Return Precio
        End Get
    End Property




End Class
