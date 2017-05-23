



Imports System.Data.SqlClient
    Imports System.Data
Partial Class logistica_principal
    Inherits System.Web.UI.Page
    Dim query As String

    Dim fn As New Funciones

    Sub validarInicioSesion()
        Dim deshabilitado As Boolean = False

        'VALIDAMOS QUE EXISTA UN PROYECTO REGISTRADO
        If Session("almacenero") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
        If Session("modulo") = "P" Then
            fsOpciones.Disabled = True
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


    Sub insertarRegistros()
        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.CONVERSION (PROYECTO,FASE,ARTICULO,DESCRIPCION,UM_P, UM_A,ORDEN_CAMBIO,RowPointer)"
        query += " SELECT A.PROYECTO, A.FASE, B.ARTICULO,B.DESCRIPCION,B.UNIDAD_ALMACEN,B.UNIDAD_ALMACEN,A.ORDEN_CAMBIO, A.RowPointer"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY A"
        query += " LEFT JOIN SOL_PEDIDOS.VITALICIA.ARTICULO B ON A.ARTICULO=B.ARTICULO"
        query += " LEFT JOIN SOL_PEDIDOS.VITALICIA.FASE_PY C ON A.FASE=C.FASE"
        query += " WHERE A.RowPointer NOT IN ("
        query += " SELECT B.RowPointer FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B)"
        Try
            fn.ejecutarComandoSQL2(query)
        Catch ex As Exception
            MsgBox("error " + ex.Message)
        End Try

    End Sub




    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
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

        lblUsuario.Text = Session("almacenero")



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
    Protected Sub btnAprobIng_Click(sender As Object, e As EventArgs) Handles btnAprobIng.Click
        Response.Redirect("pendientes.aspx")

    End Sub

    Protected Sub btnPospuestas_Click(sender As Object, e As EventArgs) Handles btnPospuestas.Click
        Response.Redirect("pospuestos.aspx")
    End Sub
    Protected Sub btnRevertir_Click(sender As Object, e As EventArgs) Handles btnRevertir.Click
        Response.Redirect("revertir.aspx")
    End Sub
    Protected Sub btnConversion_Click(sender As Object, e As EventArgs) Handles btnConversion.Click

        insertarRegistros()

        Response.Redirect("conversionUM.aspx")

    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        Response.Redirect("presupuestoLog.aspx")
    End Sub

    'Protected Sub btnModActas_Click(sender As Object, e As EventArgs) Handles btnModActas.Click
    '    Response.Redirect("modificarActa.aspx")
    'End Sub

    'Protected Sub btnActas_Click(sender As Object, e As EventArgs) Handles btnActas.Click
    '    Dim cant As Integer
    '    Dim cantString As String

    '    query = " SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA"
    '    cant = CInt(fn.DevolverDatoQuery(query))
    '    cant += 1
    '    cantString = CStr(cant)

    '    While Len(cantString) < 5
    '        cantString = "0" & cantString
    '    End While

    '    Session.Add("acta", cantString)
    '    Response.Redirect("nuevaActa.aspx")

    'End Sub
End Class
