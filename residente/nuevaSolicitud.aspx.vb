
Imports System.Data
Imports System.Data.SqlClient
Imports Docs.Excel

Partial Class nuevaSolicitud
    Inherits System.Web.UI.Page
    Dim fn As New Funciones
    Dim res As New residente
    Dim com As New comun
    Dim query As String

    Sub validarInicioSesion()
        If Session("proyecto") = "" Then
            Response.Redirect("resIngreso.aspx")
        End If
    End Sub

    Sub procesarElementosExtra()

        If cmbArticulo.SelectedValue = "Otro..." Then
            dtgArticuloDetalleS.Attributes("style") = "display:none;"
            cmbArticuloAuxS.Attributes("style") = ""
            cmbUMS.Attributes("style") = ""
        Else
            dtgArticuloDetalleS.Attributes("style") = ""
            cmbArticuloAuxS.Attributes("style") = "display:none;"
            cmbUMS.Attributes("style") = "display:none;"
        End If

        If cmbArticuloAux.SelectedValue = "Otro..." Then

            txtArticuloS.Attributes("style") = ""


        Else

            txtArticuloS.Attributes("style") = "display:none;"

            txtArticulo.Text = ""

        End If

        If cmbActividad.SelectedValue = "Otro..." Then
            txtActividadS.Attributes("style") = ""
        Else
            txtActividadS.Attributes("style") = "display:none;"
            txtActividad.Text = ""
        End If

        If cmbMaterial.SelectedValue = "Otro..." Then
            txtMaterialS.Attributes("style") = ""
        Else
            txtMaterialS.Attributes("style") = "display:none;"
            txtMaterial.Text = ""
        End If
    End Sub

    Sub llenarUM()  'Origen determina si la unidad de medida viene de presupuesto o almacenes

        Dim proyecto As String = lblUsuario.Text
        Dim articulo As String = obtenerArticulo()
        Dim umDef As String = cmbUM.SelectedValue

        If cmbArticulo.SelectedValue = "Otro..." Then
            cmbUM.SelectedValue = com.obtenerUMPresupuesto(proyecto, articulo, umDef)
            lblUM.Text = cmbUM.SelectedValue

            lblUMEq.Text = com.obtenerUMAlmacen(articulo, lblUM.Text)

            lblFactor.Text = com.obtenerFactorConversion(proyecto, articulo)
        Else
            lblUM.Text = com.obtenerUMPresupuesto(proyecto, articulo, umDef)

            lblUMEq.Text = com.obtenerUMAlmacen(articulo, lblUM.Text)

            lblFactor.Text = com.obtenerFactorConversion(proyecto, articulo)

        End If



    End Sub

    Function obtenerActividad() As String
        Dim actividad As String
        If cmbActividad.SelectedValue = "Otro..." Then
            actividad = txtActividad.Text
        Else
            actividad = cmbActividad.SelectedValue
        End If
        Return actividad
    End Function

    Function obtenerMaterial() As String
        Dim material As String
        If cmbMaterial.SelectedValue = "Otro..." Then
            material = txtMaterial.Text
        Else
            material = cmbMaterial.SelectedValue
        End If
        Return material
    End Function

    Function obtenerArticulo() As String
        Dim articulo As String
        If cmbArticulo.SelectedValue = "Otro..." Then
            If cmbArticuloAux.SelectedValue = "Otro..." Then
                articulo = txtArticulo.Text
            Else
                articulo = cmbArticuloAux.SelectedValue
            End If
        Else
            articulo = cmbArticulo.SelectedValue
        End If
        Return articulo
    End Function

    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_RES_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET CODIGO_CONTROL='B' WHERE ID = @ID"


        dtgDetalle.DataSourceID = "Detalles"


    End Sub

    'renderizar tabla
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_RES_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query

        dtgDetalle.DataSourceID = "Detalles"

        If dtgDetalle.Rows.Count > 0 Then
            'This replaces <td> with <th> and adds the scope attribute
            dtgDetalle.UseAccessibleHeader = True

            'This will add the <thead> and <tbody> elements
            dtgDetalle.HeaderRow.TableSection = TableRowSection.TableHeader

            'This adds the <tfoot> element. 
            'Remove if you don't have a footer row
            dtgDetalle.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()

        lblUsuario.Text = Session("proyecto")
        lblCodigoProyecto.Text = Session("correlativo")
        res.validarReimpresionRendicion(lblCodigoProyecto.Text)

        If Not Page.IsPostBack Then
            txtFecha.Text = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
            com.llenarComboActividad(cmbActividad, Session("proyecto"), "NUMERO", "FASE", "NOMBRE", 2)

            com.llenarComboMaterial(cmbMaterial, obtenerActividad, "NOMBRE", "FASE", "", 1)
            com.llenarComboArticulo(cmbArticulo, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
            com.llenarComboArticuloAux(cmbArticuloAux, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
            res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
            com.llenarComboUM(cmbUM, obtenerMaterial, "UNIDAD_MEDIDA", "UNIDAD_MEDIDA", "", 1)
            procesarElementosExtra()
            llenarUM()
            llenarTablaDetallesPedido()
            If dtgDetalle.Rows.Count > 0 Then
                btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
            Else
                btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md disabled"
            End If
        End If




    End Sub

    Protected Sub cmbActividad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbActividad.SelectedIndexChanged
        com.llenarComboMaterial(cmbMaterial, obtenerActividad, "NOMBRE", "FASE", "", 1)
        com.llenarComboArticulo(cmbArticulo, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
        'llenarComboArticuloAux()
        res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
        procesarElementosExtra()
        llenarUM()

        '(5000)
    End Sub
    Protected Sub cmbMaterial_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMaterial.SelectedIndexChanged
        fn.verificarUM(cmbMaterial.SelectedValue, cmbMaterial.SelectedItem.ToString, mensaje2, lblMensajes2)
        com.llenarComboArticulo(cmbArticulo, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
        com.llenarComboArticuloAux(cmbArticuloAux, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
        res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
        procesarElementosExtra()
        llenarUM()

        '(5000)
    End Sub
    Protected Sub cmbArticulo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbArticulo.SelectedIndexChanged
        com.llenarComboArticuloAux(cmbArticuloAux, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
        res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
        procesarElementosExtra()
        llenarUM()
        '(500)
    End Sub
    Protected Sub cmbArticuloAux_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbArticuloAux.SelectedIndexChanged
        procesarElementosExtra()
        If cmbArticuloAux.SelectedValue = "Otro..." Then
            cmbUM.SelectedValue = "--"
            lblUM.Text = cmbUM.SelectedValue

            lblUMEq.Text = cmbUM.SelectedValue

            lblFactor.Text = "1"
        Else
            llenarUM()
        End If


    End Sub
    Protected Sub cmbUM_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbUM.SelectedIndexChanged
        lblUM.Text = cmbUM.SelectedValue

        lblUMEq.Text = cmbUM.SelectedValue

        lblFactor.Text = "1"
    End Sub


    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim proyecto As String = lblUsuario.Text
        Dim actividad As String = obtenerActividad()
        Dim material As String = obtenerMaterial()
        Dim articulo As String = obtenerArticulo()

        Dim cant As Double
        Dim cantidadEq As Double

        Dim codProyecto As String = Session("correlativo")
        Dim um As String = lblUM.Text
        Dim umEq As String = lblUMEq.Text

        Dim factor As Double = CDbl(lblFactor.Text)

        If cmbUM.SelectedValue = "--" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese una unidad de medida para el Nuevo Artículo"
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "display:none;"
            lblMensajeS.Attributes("class") = "label label-info"
        End If

        'Cambiamos el punto decimal por coma para hacer las operaciones en el sistema
        txtCantidad.Text = Replace(txtCantidad.Text, ".", ",")
        'convertimos la cantidad en tipo Double
        Try
            cant = CDbl(txtCantidad.Text)
        Catch ex As Exception
            cant = -1
        End Try
        'verificamos que las cantidades coincidan, si no coinciden cambiamos el caracter decimal de coma por punto
        If txtCantidad.Text <> cant.ToString Then
            txtCantidad.Text = Replace(txtCantidad.Text, ",", ".")

            Try
                cant = CDbl(txtCantidad.Text)
            Catch ex As Exception
                cant = -1
            End Try
        End If

        txtCantidad.Text = ""
        '(500)
        If actividad = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese una Actividad."
            Exit Sub
        End If
        If material = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese un material."
            Exit Sub
        End If
        If articulo = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese un artículo."
            Exit Sub
        End If

        If cant < 0 Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese un monto válido."
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "display:none;"
            lblMensajeS.Attributes("class") = "label label-info"
        End If
        If lblId.Text = "0" Then
            If res.validarNuevaLinea(lblCodigoProyecto.Text, obtenerMaterial, obtenerArticulo) Then
                lblMensajeS.Attributes("Style") = ""
                lblMensajeS.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "Ya se ingresó el item. Intente con otro."
                Exit Sub
            Else
                lblMensajeS.Attributes("Style") = "display:none;"
                lblMensajeS.Attributes("class") = "label label-info"
            End If
        End If
        cantidadEq = cant / factor
        If lblId.Text = "0" Then
            res.insertarLinea(proyecto, actividad, material, articulo, cant, cantidadEq, um, umEq, codProyecto, lblMensaje, lblMensajeS, lblUsuario.Text)
        Else
            res.actualizarLinea(proyecto, actividad, material, articulo, cant, cantidadEq, um, umEq, codProyecto, lblId.Text, lblMensaje, lblMensajeS, lblUsuario.Text)
        End If


        btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
        llenarTablaDetallesPedido()

        com.llenarComboActividad(cmbActividad, Session("proyecto"), "NUMERO", "FASE", "NOMBRE", 2)
        com.llenarComboMaterial(cmbMaterial, obtenerActividad, "NOMBRE", "FASE", "", 1)
        com.llenarComboArticulo(cmbArticulo, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)

        res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
        lblId.Text = "0"
        lblUMEq.Text = ""
        txtCantidad.Text = ""
        btnAgregar.Text = "Agregar"
        fn.verificarUM(cmbMaterial.SelectedValue, cmbMaterial.SelectedItem.ToString, mensaje2, lblMensajes2)
        dtgDetalle.SelectedIndex = -1
    End Sub

    Protected Sub btnCancelarEdit_Click(sender As Object, e As EventArgs) Handles btnCancelarEdit.Click
        com.llenarComboActividad(cmbActividad, Session("proyecto"), "NUMERO", "FASE", "NOMBRE", 2)
        com.llenarComboMaterial(cmbMaterial, obtenerActividad, "NOMBRE", "FASE", "", 1)
        com.llenarComboArticulo(cmbArticulo, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)
        res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
        fn.verificarUM(cmbMaterial.SelectedValue, cmbMaterial.SelectedItem.ToString, mensaje2, lblMensajes2)

        lblId.Text = "0"
        txtCantidad.Text = ""
        lblCantEq.Text = ""
        btnAgregar.Text = "Agregar"
        dtgDetalle.SelectedIndex = -1
    End Sub

    Protected Sub btnGenerarAceptar_Click(sender As Object, e As System.EventArgs) Handles btnGenerarAceptar.ServerClick

        Dim fechaEntrega As Date = CDate(txtFecha.Text)
        Dim fechaActual As Date = DateTime.Now()
        If txtObservaciones.InnerText = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo observaciones."
            Exit Sub
        End If
        If txtSolicitante.Text = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo de Solicitante."
            Exit Sub
        End If

        If (DateDiff(DateInterval.Day, fechaActual, fechaEntrega) < 1) Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese una fecha mayor por mas de dos dias"
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "Display:none;"
            lblMensajeS.Attributes("class") = ""
            lblMensaje.Text = ""
        End If

        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA "
        query += " (CODIGO_SOLICITUD,ESTADO,FECHA,SOLICITANTE,Observaciones, RecordDate, UpdatedBy)"
        query += " VALUES "
        query += " ('" & lblCodigoProyecto.Text & "','P',GETDATE(),'" & fn.limpiarComillas(txtSolicitante.Text) & "', '" & fn.limpiarComillas(txtObservaciones.InnerText) & "', GETDATE(),'" & lblUsuario.Text & "')"
        Try
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        Catch ex As Exception

        End Try

        res.generarExcelSolicitudes(dtgDetalle, Session("correlativo"), Session("proyecto"), txtSolicitante.Text, txtFecha.Text, txtObservaciones.InnerText)
        Response.Redirect("Principal.aspx")

    End Sub

    Protected Sub btnDescartar_Click(sender As Object, e As System.EventArgs) Handles btnDescartar.ServerClick
        query = " SELECT count('a') FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA"
        query += " WHERE CODIGO_SOLICITUD = '" & lblCodigoProyecto.Text & "'"

        Dim cant As Integer = CInt(fn.DevolverDatoQuery(query))

        If cant = 0 Then

            query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET CODIGO_CONTROL='B', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()"
            query += " WHERE CODIGO_SOLICITUD='" & lblCodigoProyecto.Text & "'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            Response.Redirect("Principal.aspx")
        Else
            Response.Redirect("Principal.aspx")
        End If

    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("Principal.aspx")




    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("resIngreso.aspx")
    End Sub



    Protected Sub grdSolicitudDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound
        Dim desc As String = e.Row.Cells(13).Text ' CANTIDAD_DISPONIBLE
        Dim presup As String = e.Row.Cells(16).Text ' PRESUPUESTADO
        Dim presupAct As String = e.Row.Cells(15).Text 'PRESUPUESTADO_ACT
        Dim actas As String = e.Row.Cells(9).Text ' CANTIDAD_ACTAS
        Try
            dtgDetalle.SelectedRow.BackColor = Drawing.Color.FromName("#00BFFF")
        Catch ex As Exception

        End Try
        If desc = "&nbsp;" Or desc = "CANTIDAD DISPONIBLE" Then
            Dim B As Integer = 0
        Else
            If presup = "SI" Then
                If presupAct = "SI" Then
                    If CDbl(desc) < 0 Then
                        'e.Row.Font.Strikeout = True
                        'e.Row.ForeColor = Drawing.Color.Red
                        'e.Row.Font.Bold = True
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                        e.Row.BackColor = Drawing.Color.White
                    End If
                Else
                    If CDbl(actas) > 0 Then
                        e.Row.BackColor = Drawing.Color.GreenYellow
                    Else
                        e.Row.BackColor = Drawing.Color.Orange
                    End If
                End If
            Else
                If presupAct = "SI" Then
                    If CDbl(actas) > 0 Then
                        e.Row.BackColor = Drawing.Color.GreenYellow
                    Else
                        e.Row.BackColor = Drawing.Color.Orange
                    End If
                Else
                    e.Row.BackColor = Drawing.Color.Yellow
                End If

            End If

        End If
    End Sub
    Protected Sub grdSolicitudDetalle_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dtgDetalle.RowDeleting
        llenarTablaDetallesPedido()
    End Sub

    Protected Sub dtgDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalle.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgDetalle.SelectedRow

        Dim fila As Integer = row.RowIndex.ToString
        Dim cantA, cantP As Double
        cmbArticuloAux.SelectedIndex = 0



        Try
            cmbActividad.SelectedValue = dtgDetalle.Rows(fila).Cells(18).Text
        Catch ex As Exception
            cmbActividad.SelectedValue = "Otro..."
            txtActividad.Text = dtgDetalle.Rows(fila).Cells(18).Text
        End Try
        com.llenarComboMaterial(cmbMaterial, obtenerActividad, "NOMBRE", "FASE", "", 1)
        procesarElementosExtra()

        Try
            cmbMaterial.SelectedValue = dtgDetalle.Rows(fila).Cells(19).Text
        Catch ex As Exception
            cmbMaterial.SelectedValue = "Otro..."
            txtMaterial.Text = dtgDetalle.Rows(fila).Cells(19).Text
        End Try
        com.llenarComboArticulo(cmbArticulo, obtenerMaterial, "NOMBRE", "ARTICULO", "", 1)

        res.llenarTablaDetallesArticulo(datosArticulo, Session("proyecto"), obtenerMaterial, obtenerArticulo)
        procesarElementosExtra()


        Try
            cmbArticulo.SelectedValue = dtgDetalle.Rows(fila).Cells(20).Text

        Catch ex As Exception
            cmbArticulo.SelectedValue = "Otro..."
            procesarElementosExtra()


            Try
                cmbArticuloAux.SelectedValue = dtgDetalle.Rows(fila).Cells(20).Text
            Catch ex2 As Exception
                cmbArticuloAux.SelectedValue = "Otro..."
                procesarElementosExtra()

                txtArticulo.Text = dtgDetalle.Rows(fila).Cells(20).Text
                cmbUM.SelectedValue = dtgDetalle.Rows(fila).Cells(6).Text
            End Try
        End Try

        lblUM.Text = dtgDetalle.Rows(fila).Cells(6).Text
        lblUMEq.Text = dtgDetalle.Rows(fila).Cells(21).Text

        txtCantidad.Text = CDbl(dtgDetalle.Rows(fila).Cells(7).Text)

        cantP = CDbl(dtgDetalle.Rows(fila).Cells(7).Text)
        cantA = CDbl(dtgDetalle.Rows(fila).Cells(22).Text) '************************************************* cantidad equivalente
        lblFactor.Text = Math.Round(CDbl(((cantP / cantA))) * 100) / 100
        lblCantEq.Text = Math.Round(CDbl(dtgDetalle.Rows(fila).Cells(22).Text) * 100) / 100
        lblId.Text = dtgDetalle.Rows(fila).Cells(2).Text
        btnAgregar.Text = "Cambiar"
        'llenarComboActividad()

    End Sub

    Protected Sub dtgDetalle_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgDetalle.SelectedIndexChanging

        Dim row As GridViewRow

        row = dtgDetalle.Rows(e.NewSelectedIndex)

        If row.Cells(1).Text = "ANATR" Then
            e.Cancel = True
        End If
    End Sub
    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"

    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"

    End Sub

    Protected Sub dtgDetalle_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles dtgDetalle.RowDeleted
        If dtgDetalle.Rows.Count = 1 Then
            btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md disabled"
        Else
            btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
        End If

    End Sub
#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(2).Visible = False
        e.Row.Cells(17).Visible = False
        e.Row.Cells(18).Visible = False
        e.Row.Cells(19).Visible = False
        e.Row.Cells(20).Visible = False
        e.Row.Cells(32).Visible = False
        e.Row.Cells(33).Visible = False


        If lblUMActual.Text = "Presupuesto" Then
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False
            e.Row.Cells(23).Visible = False
            e.Row.Cells(24).Visible = False
            e.Row.Cells(25).Visible = False
            e.Row.Cells(25).Visible = False
            e.Row.Cells(26).Visible = False
            e.Row.Cells(27).Visible = False
            e.Row.Cells(28).Visible = False
            e.Row.Cells(29).Visible = False
            e.Row.Cells(30).Visible = False
            e.Row.Cells(31).Visible = False

        Else
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False

        End If


    End Sub


#End Region
End Class
