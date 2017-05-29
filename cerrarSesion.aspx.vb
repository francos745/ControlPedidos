
Partial Class cerrarSession
    Inherits System.Web.UI.Page
    Dim query As String
    Dim fn As New Funciones
    Sub liberarSolicitud()
        Dim usuario As String = ""


        If Session("almacenero") <> "" Then

            usuario = Session("almacenero")
        End If

        If Session("proyecto") <> "" Then

            usuario = Session("proyecto")
        End If

        If Session("usuario") <> "" Then

            usuario = Session("usuario")
        End If

        query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = 'ND' WHERE USUARIO='" & usuario & "'"

        fn.ejecutarComandoSQL2(query)



        Dim str_java As String
        str_java = "<script language='javascript'>"
        str_java += " window.close();"
        str_java += "</script>"
        Response.Write(str_java)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        liberarSolicitud()
    End Sub
End Class
