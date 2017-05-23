Imports System.Data.SqlClient
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim fn As New Funciones
    Dim query As String
    Dim cadena As String = fn.ObtenerCadenaConexion("conn")
    Dim conexion As New SqlConnection(cadena)

    Protected Sub btnIngresar_Click(sender As Object, e As System.EventArgs) Handles btnResidente.Click
        Response.Redirect("residente/resIngreso.aspx")
    End Sub
    Protected Sub btnIngenieria_Click(sender As Object, e As System.EventArgs) Handles btnIngenieria.Click
        Response.Redirect("ingenieria/Ingreso.aspx")
    End Sub
    Protected Sub btnLogistica_Click(sender As Object, e As System.EventArgs) Handles btnLogistica.Click
        Response.Redirect("logistica/Ingreso.aspx")
    End Sub

End Class
