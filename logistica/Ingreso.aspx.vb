

Imports System.Data.SqlClient
    Imports System.Data
Partial Class logistica_Ingreso
    Inherits System.Web.UI.Page
    Dim fn As New Funciones
    Dim query As String
    Dim cadena As String = fn.ObtenerCadenaConexion("conn")
    Dim conexion As New SqlConnection(cadena)

    Protected Sub btnIngresar_Click(sender As Object, e As System.EventArgs) Handles btnIngresar.Click
        conexion.Open()
        Dim cmd As New SqlCommand()
        Dim dr As SqlDataReader
        Dim id As String
        Dim pass, fechaExp As String
        cmd.Connection = conexion
        cmd.CommandText = " SELECT ID,USUARIO,CODIGO,DEFPASS,MODULO FROM SOL_PEDIDOS.PEDIDOS.ACCESO WHERE MODULO in ('L','P') AND ESTADO='H' AND USUARIO=@usuario"
        cmd.Parameters.Add(New SqlParameter("usuario", txtUsuarioLog.Text))

        dr = cmd.ExecuteReader
        If (dr.Read) Then

            id = CStr(dr(0))
            Session.Add("almacenero", CStr(dr(1)))
            Session.Add("codigo", CStr(dr(2)))
            Session.Add("defPass", CStr(dr(3)))
            Session.Add("modulo", CStr(dr(4)))
            Session.Add("id", id)
            query = "SELECT TOP 1 PASS FROM SOL_PEDIDOS.PEDIDOS.PASSWORD WHERE ID_USUARIO='" & id & "' ORDER BY ID DESC"

            pass = fn.DevolverDatoQuery(query)

            If (pass = fn.Encriptar(txtPassLog.Text)) Then
                query = "SELECT TOP 1 CONVERT (char(10), FECHA_EXP, 103) FECHA_EXP FROM SOL_PEDIDOS.PEDIDOS.PASSWORD WHERE ID_USUARIO='" & id & "' ORDER BY ID DESC"
                fechaExp = fn.DevolverDatoQuery(query)

                Session.Add("pass", pass)
                Session.Add("fechaExp", fechaExp)
                Response.Redirect("principal.aspx")
            Else
                alert.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "Contraseña Incorrecta"
            End If

        Else
            alert.Attributes("class") = "alert alert-warning"
            lblMensaje.Text = "Nombre de usuario inexistente"
        End If

    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("../Default.aspx")
    End Sub
End Class

