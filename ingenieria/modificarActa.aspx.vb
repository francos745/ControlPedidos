
Imports System.Data
Imports System.Data.SqlClient
Imports Docs.Excel

Partial Class ingenieria_modificarActa
    Inherits System.Web.UI.Page
    Dim fn As New Funciones
    Dim com As New comun
    Dim query As String

    Dim filadatos As String
    Dim columnadatos As String
    Dim objVentana As Object
    Dim objexcel As Object
    Dim objlibro As Object 'Excel.Workbook
    Dim objHojaExcel As Object
    Dim Archivo As String
    Dim Archivo2 As String
    Dim ruta As String
    Dim codRep As String
    Dim a As String
    Dim plantilla As String
    Dim rutaPlantilla As String
    Dim ext As String


    Sub validarInicioSesion()
        If Session("usuario") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
    End Sub

    Sub llenarComboProyecto()
        query = " SELECT USUARIO FROM SOL_PEDIDOS.PEDIDOS.ACCESO WHERE CODIGO<>'ND'"
        fn.llenarComboBox2(cmbProyecto, query, "USUARIO", "USUARIO")
    End Sub

    Sub llenarComboCodigoActa()
        query = " SELECT CODIGO_ACTA,FECHA_ACTA_ING, CASE WHEN ESTADO='R' THEN {FN CONCAT(CODIGO_ACTA_ING,' - ANULADA ')} ELSE CODIGO_ACTA_ING END CODIGO_ACTA_ING FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA ORDER BY CODIGO_ACTA DESC"
        fn.llenarComboBoxOpciones2(cmbCodigoActa, query, "CODIGO_ACTA_ING", "CODIGO_ACTA", "FECHA_ACTA_ING")
    End Sub

    Sub llenarComboActividad()

        query = " (SELECT FASE, NOMBRE,"
        query += " ISNULL((SELECT NRO FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B"
        query += " WHERE A.FASE=B.FASE),0) NUMERO"
        query += " FROM   SOL_PEDIDOS.VITALICIA.FASE_PY A"
        query += " WHERE FASE NOT IN (SELECT FASE FROM  SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00.00' )"
        query += " AND FASE NOT IN (SELECT FASE FROM   SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00')"
        query += " AND PROYECTO = '" & cmbProyecto.SelectedValue & "' AND FASE LIKE '%00') "
        query += " ORDER BY FASE"

        fn.llenarComboBoxOpciones2(cmbActividad, query, "NUMERO", "FASE", "NOMBRE")

    End Sub

    Sub llenarComboMaterial()

        Dim actividad As String
        Try
            actividad = cmbActividad.SelectedValue.Substring(0, 8)
        Catch ex As Exception
            actividad = "Otro..."
        End Try
        query = " SELECT FASE,NOMBRE FROM SOL_PEDIDOS.VITALICIA.FASE_PY "
        query += " WHERE FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00.00')"
        query += " AND FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00')"
        query += " AND TIPO='A'"
        query += " AND FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00')"
        query += " AND FASE LIKE'" & actividad & "%'"
        query += " ORDER BY NOMBRE"

        fn.llenarComboBox2(cmbMaterial, query, "NOMBRE", "FASE")

    End Sub

    Sub llenarComboArticulo()

        query = " SELECT DISTINCT A.ARTICULO ARTICULO, B.DESCRIPCION DESCRIPCION"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY A, SOL_PEDIDOS.VITALICIA.ARTICULO B "
        query += " WHERE FASE = '" & cmbMaterial.SelectedValue.ToString & "' "
        query += " And A.ARTICULO = B.ARTICULO ORDER BY DESCRIPCION"
        fn.llenarComboBox(cmbArticulo, query, "DESCRIPCION", "ARTICULO")

    End Sub

    Sub llenarComboArticuloAux()
        query = " SELECT DISTINCT B.ARTICULO ARTICULO, B.DESCRIPCION DESCRIPCION FROM SOL_PEDIDOS.VITALICIA.ARTICULO B ORDER BY DESCRIPCION"
        fn.llenarComboBox(cmbArticuloAux, query, "DESCRIPCION", "ARTICULO")

    End Sub


    Sub llenarComboUM()
        query = " SELECT UNIDAD_MEDIDA FROM SOL_PEDIDOS.VITALICIA.UNIDAD_DE_MEDIDA"
        fn.llenarComboBox(cmbUM, query, "UNIDAD_MEDIDA", "UNIDAD_MEDIDA")

    End Sub

    Sub procesarElementosExtra()

        If cmbArticulo.SelectedValue = "Otro..." Then
            cmbArticuloAuxS.Attributes("style") = ""
        Else
            cmbArticuloAuxS.Attributes("style") = "display:none;"
        End If

        If cmbArticuloAux.SelectedValue = "Otro..." Then

            txtArticuloS.Attributes("style") = ""
            cmbUMS.Attributes("style") = ""

        Else

            txtArticuloS.Attributes("style") = "display:none;"
            cmbUMS.Attributes("style") = "display:none;"
            txtArticulo.Text = ""

        End If




    End Sub
    Protected Sub grdSolicitudDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound
        Try
            dtgDetalle.SelectedRow.BackColor = Drawing.Color.FromName("#00BFFF")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub dtgDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalle.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgDetalle.SelectedRow

        Dim fila As Integer = row.RowIndex.ToString
        Dim cantA, cantP As Double
        cmbArticuloAux.SelectedIndex = 0
        cmbArticuloAux.SelectedIndex = 0


        Dim id As String = dtgDetalle.Rows(fila).Cells(2).Text
        Dim pedido As String = ""
        query = "SELECT CODIGO_SOLICITUD FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE ID_ACTA='" & id & "'"

        pedido = fn.DevolverDatoQuery(query)

        If pedido <> "" Then
            fsCombos.Attributes("style") = "display:none;"
            lblCantAux.Text = CDbl(dtgDetalle.Rows(fila).Cells(8).Text) * 1

        Else
            fsCombos.Attributes("style") = ""
            lblCantAux.Text = 0

        End If




        Try
            cmbProyecto.SelectedValue = dtgDetalle.Rows(fila).Cells(3).Text
        Catch ex As Exception
            mostrarMensaje("Hubo un error cargando el proyecto. ", "error")
        End Try
        llenarComboActividad()
        procesarElementosExtra()

        Try
            cmbActividad.SelectedValue = dtgDetalle.Rows(fila).Cells(11).Text
        Catch ex As Exception
            mostrarMensaje("Hubo un error cargando la actividad. ", "error")
        End Try
        llenarComboMaterial()
        procesarElementosExtra()

        Try
            cmbMaterial.SelectedValue = dtgDetalle.Rows(fila).Cells(12).Text
        Catch ex As Exception
            mostrarMensaje("Hubo un error cargando el material. ", "error")
        End Try
        llenarComboArticulo()
        procesarElementosExtra()


        Try
            cmbArticulo.SelectedValue = dtgDetalle.Rows(fila).Cells(13).Text

        Catch ex As Exception
            cmbArticulo.SelectedValue = "Otro..."
            procesarElementosExtra()


            Try
                cmbArticuloAux.SelectedValue = dtgDetalle.Rows(fila).Cells(13).Text
            Catch ex2 As Exception
                cmbArticuloAux.SelectedValue = "Otro..."
                procesarElementosExtra()

                txtArticulo.Text = dtgDetalle.Rows(fila).Cells(13).Text
                cmbUM.SelectedValue = dtgDetalle.Rows(fila).Cells(7).Text
            End Try
        End Try

        lblUM.Text = dtgDetalle.Rows(fila).Cells(7).Text
        lblUMEq.Text = dtgDetalle.Rows(fila).Cells(9).Text

        txtCantidad.Text = CDbl(dtgDetalle.Rows(fila).Cells(8).Text)

        cantP = CDbl(dtgDetalle.Rows(fila).Cells(8).Text)
        cantA = CDbl(dtgDetalle.Rows(fila).Cells(10).Text) '************************************************* cantidad equivalente
        lblFactor.Text = Math.Round(CDbl(((cantP / cantA))) * 100) / 100
        lblCantEq.Text = Math.Round(CDbl(dtgDetalle.Rows(fila).Cells(10).Text) * 100) / 100
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

    Protected Sub grdSolicitudDetalle_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dtgDetalle.RowDeleting

        Dim id As String = dtgDetalle.Rows(e.RowIndex).Cells(2).Text
        Dim pedido As String = ""
        query = "SELECT CODIGO_SOLICITUD FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE ID_ACTA='" & id & "'"

        pedido = fn.DevolverDatoQuery(query)

        If pedido <> "" Then
            e.Cancel = True
            mostrarMensaje("La línea que intenta eliminar ya fue asignada al pedido " & pedido, "error")
        Else
            e.Cancel = False
            mostrarMensaje("Línea eliminada exitosamente", "exito")
        End If

        llenarTablaDetallesPedido()
    End Sub
    Sub mostrarMensaje(ByVal mensaje As String, ByVal tipo As String)
        If mensaje = "" Then
            lblMensajeS.Attributes("Style") = "display:none;"
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = ""
        Else
            If tipo = "exito" Then
                lblMensajeS.Attributes("class") = "alert alert-success"
            Else
                If tipo = "error" Then
                    lblMensajeS.Attributes("class") = "alert alert-danger"
                Else
                    If tipo = "info" Then
                        lblMensajeS.Attributes("class") = "alert alert-info"
                    End If
                End If
            End If
            lblMensajeS.Attributes("Style") = ""

            lblMensaje.Text = mensaje
        End If

    End Sub
    Sub llenarUM()  'Origen determina si la unidad de medida viene de presupuesto o almacenes

        Dim proyecto As String = cmbProyecto.SelectedValue
        Dim articulo As String = obtenerArticulo()
        Dim umDef As String = cmbUM.SelectedValue

        lblUM.Text = com.obtenerUMPresupuesto(proyecto, articulo, umDef)

        lblUMEq.Text = com.obtenerUMAlmacen(articulo, lblUM.Text)

        lblFactor.Text = com.obtenerFactorConversion(proyecto, articulo)

    End Sub

    Sub llenarCodActaFecha(ByVal codigo As String)
        Dim codActaIng, fecha As String

        query = "SELECT CODIGO_ACTA_ING FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA WHERE CODIGO_ACTA='" & codigo & "'"
        codActaIng = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA WHERE CODIGO_ACTA='" & codigo & "'"
        fecha = fn.DevolverDatoQuery(query)

        txtCodigoActaAux.Text = codActaIng

        txtFechaAux.Text = fecha


    End Sub



    Function obtenerActividad() As String
        Dim actividad As String

        actividad = cmbActividad.SelectedValue

        Return actividad
    End Function

    Function obtenerMaterial() As String
        Dim material As String

        material = cmbMaterial.SelectedValue

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

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ACTAS WHERE COD_ACTA=@CODIGO_ACTA AND COD_CONTROL<>'B'"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA SET CODIGO_CONTROL='B', CODIGO_SOLICITUD='0' WHERE ID = @ID"


        dtgDetalle.DataSourceID = "Detalles"


        'If dtgDetalle.Rows.Count = 0 Then
        '    btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md disabled"
        'Else
        '    btnGenerar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
        'End If

    End Sub

    'renderizar tabla
    Protected Sub dtgDetalle_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender


        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ACTAS WHERE COD_ACTA=@CODIGO_ACTA AND COD_CONTROL<>'B'"

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

    Function validarIngreso() As Boolean 'si devuelve TRUE significa que existen registros con los mismos valores
        Dim contador As Integer
        query = " SELECT COUNT('A') FROM SOL_PEDIDOS.PEDIDOS.SOL_ACTAS"
        query += " WHERE COD_ACTA='" & cmbCodigoActa.SelectedValue.ToString & "'"
        query += " AND ACTIVIDAD='" & obtenerActividad() & "'"
        query += " And MATERIAL ='" & obtenerMaterial() & "'"
        query += " AND ARTICULO='" & obtenerArticulo() & "'"
        query += " And COD_CONTROL<>'B'"
        contador = CInt(fn.DevolverDatoQuery(query))
        If contador > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub aceptarRechazar()
        Dim estado As String
        query = " SELECT ESTADO FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA WHERE CODIGO_ACTA='" & cmbCodigoActa.SelectedValue & "'"
        estado = fn.DevolverDatoQuery(query)
        If estado = "A" Then
            btnGenerar.Value = "Anular Acta"

            lblEstadoS.Attributes("class") = "alert alert-success"
            lblEstadoS.Attributes("Style") = ""
            lblEstado.Text = "Acta aprobada"

        Else
            btnGenerar.Value = "Aprobar Acta"

            lblEstadoS.Attributes("class") = "alert alert-danger"
            lblEstadoS.Attributes("Style") = ""
            lblEstado.Text = "Acta anulada"
        End If

    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("usuario")
        'lblProyecto.Text = Session("proyecto")


        If Not Page.IsPostBack Then

            llenarComboCodigoActa()
            llenarComboProyecto()
            llenarComboActividad()
            llenarComboMaterial()
            llenarComboArticulo()
            llenarComboArticuloAux()

            llenarComboUM()
            procesarElementosExtra()
            llenarUM()
            llenarTablaDetallesPedido()
            llenarCodActaFecha(cmbCodigoActa.SelectedValue.ToString)

        End If

        aceptarRechazar()
        llenarCodActaFecha(cmbCodigoActa.SelectedValue.ToString)



    End Sub
    Protected Sub cmbCodigoActa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbCodigoActa.SelectedIndexChanged
        mostrarMensaje("", "exito")


        llenarTablaDetallesPedido()
        lblCodigoActa.Text = cmbCodigoActa.SelectedValue
        llenarCodActaFecha(lblCodigoActa.Text)
        btnAgregar.Text = "Agregar"
        txtCantidad.Text = ""
        lblCantEq.Text = ""
        fsCombos.Attributes("style") = ""

    End Sub

    Protected Sub cmbProyecto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbProyecto.SelectedIndexChanged
        llenarComboActividad()
        mostrarMensaje("", "exito")

    End Sub
    Protected Sub btnCambiarSi_Click(sender As Object, e As System.EventArgs) Handles btnCambiarSi.ServerClick

        Dim FechaActa As Date = CDate(txtFecha.Text)
        Dim fechaActual As Date = DateTime.Now()

        If txtCodigoActa.Text = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo de Código de Acta."
            Exit Sub
        End If

        query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA SET CODIGO_ACTA_ING='" & txtCodigoActa.Text & "', FECHA_ACTA_ING='" & FechaActa.ToString("yyyy/MM/dd") & "', FECHA=GETDATE(), UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE() WHERE CODIGO_ACTA='" & cmbCodigoActa.SelectedValue.ToString & "'"

        Try
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        Catch ex As Exception

        End Try
        llenarComboCodigoActa()
    End Sub
    Protected Sub cmbActividad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbActividad.SelectedIndexChanged
        llenarComboMaterial()
        llenarComboArticulo()
        llenarComboArticuloAux()

        procesarElementosExtra()
        llenarUM()
        mostrarMensaje("", "exito")
        'System.Threading.Thread.Sleep(5000)
    End Sub
    Protected Sub cmbMaterial_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbMaterial.SelectedIndexChanged
        llenarComboArticulo()
        llenarComboArticuloAux()

        procesarElementosExtra()
        llenarUM()
        mostrarMensaje("", "exito")
        'System.Threading.Thread.Sleep(5000)
    End Sub
    Protected Sub cmbArticulo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbArticulo.SelectedIndexChanged
        llenarComboArticuloAux()

        procesarElementosExtra()
        llenarUM()
        mostrarMensaje("", "exito")
        'System.Threading.Thread.Sleep(500)
    End Sub
    Protected Sub cmbArticuloAux_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbArticuloAux.SelectedIndexChanged
        procesarElementosExtra()
        llenarUM()
    End Sub
    Protected Sub cmbUM_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbUM.SelectedIndexChanged
        llenarUM()
    End Sub


    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim proyecto As String = cmbProyecto.SelectedValue
        Dim actividad As String = obtenerActividad()
        Dim material As String = obtenerMaterial()
        Dim articulo As String = obtenerArticulo()
        Dim codActa As String = cmbCodigoActa.SelectedValue
        Dim cantidad As Double
        Dim cantidadEq As Double

        'Dim codProyecto As String = Session("correlativo")
        Dim um As String = lblUM.Text
        Dim umEq As String = lblUMEq.Text

        Dim factor As Double = CDbl(lblFactor.Text)

        Try
            cantidad = CDbl(txtCantidad.Text)
        Catch ex As Exception
            cantidad = -1
        End Try
        'System.Threading.Thread.Sleep(500)
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

        If cantidad < 0 Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese un monto válido."
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "display:none;"
            lblMensajeS.Attributes("class") = "label label-info"
        End If
        If lblId.Text = "0" Then
            If validarIngreso() Then
                lblMensajeS.Attributes("Style") = ""
                lblMensajeS.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "Ya se ingresó el item. Intente con otro."
                Exit Sub
            Else
                lblMensajeS.Attributes("Style") = "display:none;"
                lblMensajeS.Attributes("class") = "label label-info"
            End If
        End If


        cantidadEq = cantidad / factor

        If lblId.Text = "0" Then
            query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA "
            query += " (PROYECTO,ACTIVIDAD,FASE,ARTICULO,CANTIDAD,CODIGO_ACTA,CODIGO_CONTROL,UND_MED,UND_MED_A,CANTIDAD_APS, RecordDate, UpdatedBy)"
            query += " VALUES "
            query += " ('" & proyecto & "','" & actividad & "','" & material & "','" & articulo & "','" & cantidadEq & "','" & codActa & "', 'C','" & um & "','" & umEq & "','" & cantidad & "', GETDATE(), '" & lblUsuario.Text & "')"

        Else
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA "
            query += " SET PROYECTO='" & proyecto & "',"
            query += " ACTIVIDAD='" & actividad & "',"
            query += " FASE='" & material & "',"
            query += " ARTICULO='" & articulo & "',"
            query += " CANTIDAD='" & cantidadEq & "',"
            query += " CODIGO_ACTA='" & codActa & "',"
            query += " CODIGO_CONTROL='M',"
            query += " UND_MED='" & um & "',"
            query += " UND_MED_A='" & umEq & "',"
            query += " CANTIDAD_APS='" & cantidad & "', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE() "
            query += " WHERE ID='" & lblId.Text & "'"
        End If
        'lblMensajeS.Attributes("Style") = ""

        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

        llenarTablaDetallesPedido()

        llenarComboActividad()
        llenarComboMaterial()
        llenarComboArticulo()
        txtArticulo.Text = ""
        cmbArticuloAux.SelectedIndex = 0
        llenarUM()
        procesarElementosExtra()
        lblCantAux.Text = 0
        btnAgregar.Text = "Agregar"
        lblId.Text = "0"
        txtCantidad.Text = ""
        lblCantEq.Text = ""
        fsCombos.Attributes("style") = ""
        dtgDetalle.SelectedIndex = -1

    End Sub

    Protected Sub btnCancelarEdit_Click(sender As Object, e As EventArgs) Handles btnCancelarEdit.Click
        llenarComboActividad()
        llenarComboMaterial()
        llenarComboArticulo()
        txtArticulo.Text = ""
        cmbArticuloAux.SelectedIndex = 0
        llenarUM()
        procesarElementosExtra()

        btnAgregar.Text = "Agregar"
        lblId.Text = "0"
        lblCantAux.Text = 0
        txtCantidad.Text = ""
        lblCantEq.Text = ""
        fsCombos.Attributes("style") = ""
        dtgDetalle.SelectedIndex = -1
    End Sub

    Protected Sub btnGenerarAceptar_Click(sender As Object, e As System.EventArgs) Handles btnGenerarAceptar.ServerClick

        Dim solicitudes As String = ""


        If btnGenerar.Value.ToString = "Aprobar Acta" Then
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA SET ESTADO='A', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_ACTA='" & cmbCodigoActa.SelectedValue & "'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        Else
            query = " DECLARE @valores VARCHAR(1000)"
            query += " SELECT @valores= COALESCE(@valores + ', ', '') + CODIGO_SOLICITUD "
            query += " FROM (SELECT DISTINCT B.CODIGO_SOLICITUD"

            query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA A JOIN SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA B"
            query += " ON A.ID=B.ID_ACTA"

            query += " WHERE CODIGO_ACTA='" & cmbCodigoActa.SelectedValue & "'"
            query += " AND A.CODIGO_CONTROL<>'B'"
            query += " AND B.CODIGO_CONTROL<>'B')VISTA"
            query += " select  @valores as valores"
            solicitudes = fn.DevolverDatoQuery(query)
            If solicitudes = "" Then
                query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA SET ESTADO='R', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_ACTA='" & cmbCodigoActa.SelectedValue & "'"
                fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            Else
                mostrarMensaje("No fue posible anular el acta. Se encuentra asociada a las solicitudes: " & solicitudes, "error")
            End If


        End If

        aceptarRechazar()
    End Sub



    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("Principal.aspx")




    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub


    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"

    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"

    End Sub

#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(2).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(13).Visible = False

        If lblUMActual.Text = "Presupuesto" Then

            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
        Else

            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False

        End If


    End Sub


#End Region
#Region "BOTONES DE NAVEGACION"
    Protected Sub btnCursadas_Click(sender As Object, e As EventArgs) Handles btnCursadas.Click
        Response.Redirect("solicitudesCursadasIng.aspx")
    End Sub
    Protected Sub btnAprobadas_Click(sender As Object, e As EventArgs) Handles btnAprobadas.Click
        Response.Redirect("solicitudesAprobadas.aspx")
    End Sub
    Protected Sub btnRechazadas_Click(sender As Object, e As EventArgs) Handles btnRechazadas.Click
        Response.Redirect("solicitudesRechazadas.aspx")
    End Sub
    Protected Sub btnMatRech_Click(sender As Object, e As EventArgs) Handles btnMatRech.Click
        Response.Redirect("materialesRechazados.aspx")
    End Sub
    Protected Sub btnActas_Click(sender As Object, e As EventArgs) Handles btnActas.Click
        Dim cant As Integer
        Dim cantString As String

        query = " SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA"
        cant = CInt(fn.DevolverDatoQuery(query))
        cant += 1
        cantString = CStr(cant)

        While Len(cantString) < 5
            cantString = "0" & cantString
        End While

        Session.Add("acta", cantString)
        Response.Redirect("nuevaActa.aspx")
    End Sub
    Protected Sub btnModActas_Click(sender As Object, e As EventArgs) Handles btnModActas.Click
        Response.Redirect("modificarActa.aspx")
    End Sub

#End Region

End Class
