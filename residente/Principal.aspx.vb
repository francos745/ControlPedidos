Imports System.Data.SqlClient
Imports System.Data
Partial Class Principal
    Inherits System.Web.UI.Page
    Dim query As String

    Dim fn As New Funciones

    Sub validarInicioSesion()
        Dim deshabilitado As Boolean = False

        'VALIDAMOS QUE EXISTA UN PROYECTO REGISTRADO
        If Session("proyecto") = "" Then
            Response.Redirect("resIngreso.aspx")
        End If
        'VALIDAMOS QUE EXISTA UN CODIGO PARA NUMERAR LOS PEDIDOS.
        If Session("codigo").ToString = "ND" Then
            'fsFormulario.Disabled = True
            deshabilitado += True
            lblMensajeError.Text += "&nbsp; - El proyecto no tiene asignado un código de numeración, contáctese con el administrador para asignar un nuevo código. </br>"
        Else
            'fsFormulario.Disabled = False
            deshabilitado += False
        End If
        'VALIDAMOS QUE LA CONTRASEÑA POR DEFECTO NO SE REPITA

        If fn.Encriptar(lblPass.Text) = Session("defPass") Then

            'fsFormulario.Disabled = True
            deshabilitado += True
            lblMensajeError.Text += "- Debe cambiar la contraseña para continuar."
        Else
            'fsFormulario.Disabled = False
            deshabilitado += False
        End If

        If DateTime.Now.ToShortDateString = lblFecha.Text Then
            'fsFormulario.Disabled = True
            deshabilitado += True
            lblMensajeError.Text += "- El periodo de su contraseña a expirado. Debe cambiar la contraseña para continuar."
        Else
            'fsFormulario.Disabled = False
            deshabilitado += False
        End If

        fsFormulario.Disabled = deshabilitado
        If deshabilitado Then
            lblMensajeErrorS.Attributes("style") = ""
            lblMensajeErrorS.Attributes("class") = "alert alert-danger"
        Else
            lblMensajeErrorS.Attributes("style") = "display: none;"
        End If


    End Sub





    Protected Sub btnNueva_Click(sender As Object, e As EventArgs) Handles btnNueva.Click
        Dim query As String
        Dim cant As Integer
        Dim cantString As String
        Dim corr As String

        query = "SELECT COUNT(*)"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA"
        query += " WHERE CODIGO_SOLICITUD LIKE '" & Session("codigo") & "%'"
        cant = CInt(fn.DevolverDatoQuery(query))
        cant += 1
        cantString = CStr(cant)

        While Len(cantString) < 4
            cantString = "0" & cantString
        End While

        corr = Session("codigo") & "-" & cantString
        Session.Add("correlativo", corr)

        Response.Redirect("nuevaSolicitud.aspx")
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("resIngreso.aspx")
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblPass.Text = fn.Desencriptar(Session("pass"))
            lblFecha.Text = Session("fechaExp")
        End If
        lblMensajeError.Text = ""
        'Validamos que se inicie sesión correctamente
        validarInicioSesion()

        'validamos que no se use la contraseña por defecto
        'validarContraseniaDefecto()



        'validamos que el proyecto tenga un código de numeración
        'validarCodigoNumeracion()
        'fsFormulario.Disabled = True

        lblUsuario.Text = Session("proyecto")



    End Sub






    Protected Sub btnPassAceptar_click(sender As Object, e As System.EventArgs) Handles btnPassAceptar.ServerClick

        Dim passDef As String = Session("defPass").ToString

        Dim passAnt As String = fn.Encriptar(lblPass.Text)
        If fn.Encriptar(txtPassActual.Value.ToString) = passAnt Then
            If txtPassNueva.Value.ToString = txtPassConfirmar.Value.ToString Then
                If fn.Encriptar(txtPassNueva.Value.ToString) <> passDef Then
                    Dim cont As Integer
                    query = " SELECT COUNT('A') FROM ("
                    query += " SELECT TOP 5 PASS FROM SOL_PEDIDOS.PEDIDOS.PASSWORD"
                    query += " WHERE ID_USUARIO='" & Session("id").ToString & "'"
                    query += " ORDER BY FECHA_EXP DESC"
                    query += " )VISTA"
                    query += " WHERE PASS = '" & fn.Encriptar(txtPassNueva.Value.ToString) & "'"

                    cont = CInt(fn.DevolverDatoQuery(query))
                    If cont = 0 Then
                        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.PASSWORD (ID_USUARIO,PASS) VALUES ('" & Session("id").ToString & "','" & fn.Encriptar(txtPassConfirmar.Value.ToString) & "')"

                        fn.ejecutarComandoSQL2(query)
                        alert.Attributes("class") = "alert alert-success"
                        lblPass.Text = txtPassConfirmar.Value.ToString
                        lblFecha.Text = DateTime.Now.AddDays(90).ToShortDateString
                        Session.Remove("pass")
                        Session.Add("pass", txtPassConfirmar.Value.ToString)
                        Session.Remove("fechaExp")
                        Session.Add("fechaExp", DateTime.Now.AddDays(90).ToShortDateString)
                        lblMensaje.Text = "Contraseña cambiada exitosamente"
                        validarInicioSesion()
                    Else
                        alert.Attributes("class") = "alert alert-danger"
                        lblMensaje.Text = "No se pudo cambiar la contraseña, ya utilizó esta contraseña."
                    End If

                Else
                    alert.Attributes("class") = "alert alert-danger"
                    lblMensaje.Text = "No se pudo cambiar la contraseña. Seleccione una contraseña distinta e intente nuevamente."
                End If
            Else
                alert.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "No se pudo cambiar la contraseña, las contraseñas ingresadas no coinciden."

            End If
        Else
            alert.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "No se pudo cambiar la contraseña, la contraseña actual no coincide con la ingresada."
        End If
    End Sub

    Protected Sub btnMatRechazados_Click(sender As Object, e As EventArgs) Handles btnMatRechazado.Click
        Response.Redirect("materialesRechazados.aspx")
    End Sub

    Protected Sub btnCursadas_Click(sender As Object, e As EventArgs) Handles btnCursadas.Click
        Response.Redirect("solicitudesCursadas.aspx")
    End Sub


    Protected Sub btnAprobadas_Click(sender As Object, e As EventArgs) Handles btnAprobadas.Click
        Response.Redirect("solicitudesAprobadas.aspx")
    End Sub
    Protected Sub btnRechazadas_Click(sender As Object, e As EventArgs) Handles btnRechazadas.Click
        Response.Redirect("solicitudesRechazadas.aspx")
    End Sub

    Protected Sub btnPresup_Click(sender As Object, e As EventArgs) Handles btnPresup.Click
        Response.Redirect("presupuesto.aspx")
    End Sub

End Class
