


Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Docs.Excel
Partial Class logistica_pendientes
    Inherits System.Web.UI.Page

    Dim fn As New Funciones
    Dim log As New logistica
    Dim com As New comun
    Dim query As String


    Sub validarInicioSesion()
        If Session("almacenero") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
    End Sub

    Sub liberarSolicitud()
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = 'ND' WHERE USUARIO='" & lblUsuario.Text & "'"
        fn.ejecutarComandoSQL2(query)
    End Sub

    Sub validarConcurrencia()
        Dim a As String
        query = "SELECT USUARIO FROM SOL_PEDIDOS.PEDIDOS.ACCESO WHERE CODIGO = '" & cmbCodigoProyecto.SelectedValue & "' AND USUARIO <> '" & lblUsuario.Text & "'"
        a = fn.DevolverDatoQuery(query)
        If a = "" Then
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = '" & cmbCodigoProyecto.SelectedValue & "' WHERE USUARIO='" & lblUsuario.Text & "'"
            fn.ejecutarComandoSQL2(query)
            btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
            btnRechazar.Attributes("class") = "btn btn-default btn-md enabled"
            mostrarMensaje3("", "exito")
        Else
            liberarSolicitud()
            btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md disabled"
            btnRechazar.Attributes("class") = "btn btn-default btn-md disabled"
            mostrarMensaje3("La solicitud esta en uso por " & a & "", "error")
        End If
    End Sub

    Sub mostrarMensaje3(ByVal mensaje As String, ByVal tipo As String)
        If mensaje = "" Then
            lblMensaje3S.Attributes("Style") = "display:none;"
            lblMensaje3S.Attributes("class") = "alert alert-danger"
            lblMensaje3.Text = ""
        Else
            If tipo = "exito" Then
                lblMensaje3S.Attributes("class") = "alert alert-success"
            Else
                If tipo = "error" Then
                    lblMensaje3S.Attributes("class") = "label label-danger"
                Else
                    If tipo = "info" Then
                        lblMensaje3S.Attributes("class") = "alert alert-info"
                    End If
                End If
            End If
            lblMensaje3S.Attributes("Style") = ""

            lblMensaje3.Text = mensaje
        End If
        'Page.ClientScript.RegisterStartupScript(Page.ClientScript.[GetType](), "onLoad", "MostrarLabel();", True)
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

    Sub verificarUM()

        query = " SELECT COUNT ('A') FROM ("
        query += " SELECT DISTINCT PROYECTO PROYECTO,"

        query += " (SELECT TOP 1 DESCRIPCION FROM SOL_PEDIDOS.VITALICIA.ARTICULO B"
        query += " WHERE A.ARTICULO=B.articulo) DETALLE_FINANCIERO,"

        query += " (SELECT TOP 1 UNIDAD_ALMACEN FROM SOL_PEDIDOS.VITALICIA.ARTICULO B"
        query += " WHERE A.ARTICULO=B.articulo) UND_MED,"

        query += " (SELECT TOP 1 (CONVERT(VARCHAR(20), (CAST(ROUND(FACTOR,0) AS int)))) FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B"
        query += " WHERE A.PROYECTO=B.PROYECTO"
        query += " AND A.ARTICULO=B.articulo) FACTOR,"

        query += " (SELECT TOP 1 DESCRIPCION FROM SOL_PEDIDOS.VITALICIA.ARTICULO B"
        query += " WHERE A.ARTICULO=B.articulo) ITEM_GRAL_APS,"

        query += " (SELECT TOP 1 UM_P FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B"
        query += " WHERE A.PROYECTO=B.PROYECTO"
        query += " AND A.ARTICULO=B.articulo) UND_MED_APS,"

        query += " ARTICULO,"

        query += " '00.00.00.00' FASE,"

        query += " 'MULTIPLICAR' ACCION"



        query += "  FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY_LOG A"
        query += "  )VISTA"
        query += "  WHERE FACTOR IS NULL AND PROYECTO='" & lblProyecto.Text & "'"

        If CInt(fn.DevolverDatoQuery(query)) > 0 Then
            mensaje2.Attributes("Style") = ""
            lblMensajes2.Text = "Se encontraron " & CInt(fn.DevolverDatoQuery(query)) & " artículos sin datos asignados en la tabla de conversiones. Esto puede ocasionar problemas con el funcionamiento del módulo."
        Else
            lblMensajes2.Text = ""
            mensaje2.Attributes("Style") = "display:none;"
        End If


    End Sub


    Sub llenarObsSolFecha(ByVal codigo As String)
        Dim solicitante As String = ""
        Dim observaciones As String = ""
        Dim fecha As String = ""
        Dim fechaReq As String = ""
        Dim fechaDo As String = ""
        Dim fechaSo As String = ""
        Dim fechaIng As String = ""

        query = "SELECT SOLICITANTE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            solicitante = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            solicitante = ""
        End Try

        query = "SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            observaciones = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            observaciones = ""
        End Try

        query = "SELECT {fn CONCAT(CONVERT(nvarchar(30), FECHA_SOLICITUD , 103), {fn CONCAT(' - ', CONVERT(nvarchar(30), FECHA_SOLICITUD , 108))})} FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            fecha = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fecha = ""
        End Try


        query = "SELECT {fn CONCAT(CONVERT(nvarchar(30), FECHA_REQUERIDA , 103), {fn CONCAT(' - ', CONVERT(nvarchar(30), FECHA_REQUERIDA , 108))})} FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"

        Try
            fechaReq = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaReq = ""
        End Try

        query = "SELECT {fn CONCAT(CONVERT(nvarchar(30), FECHA_APROB_ING , 103), {fn CONCAT(' - ', CONVERT(nvarchar(30), FECHA_APROB_ING , 108))})} FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            fechaIng = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaIng = ""
        End Try

        query = "SELECT Convert(Char(10), FECHA_APROB_SO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"

        Try
            fechaSo = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaSo = ""
        End Try

        query = "SELECT Convert(Char(10), FECHA_APROB_DO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            fechaDo = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaDo = ""
        End Try


        txtObservaciones.InnerHtml = observaciones
        txtSolicitante.Text = solicitante
        txtFechaSol.Text = fecha
        txtFechaSO.Text = fechaSo
        txtFechaDO.Text = fechaDo
        txtFechaReq.Text = fechaReq
        txtFechaIng.Text = fechaIng
    End Sub

    Sub ocultarMostrarFormulario(ByVal bool As Boolean)
        If bool Then
            'contNavbar.Attributes("Style") = ""
            contEditarLinea.Attributes("Style") = "display:none;"
            contObservaciones.Attributes("Style") = ""
            contTitulos.Attributes("Style") = ""
            contTextos.Attributes("Style") = ""
            contTextos2.Attributes("Style") = ""
            GuardarBod.Attributes("Style") = ""
            'contObsRechazadas.Attributes("Style") = ""
            contDtgDetalle.Attributes("Style") = ""


        Else
            'contNavbar.Attributes("Style") = "display:none;"
            contEditarLinea.Attributes("Style") = ""
            contObservaciones.Attributes("Style") = "display:none;"
            contTitulos.Attributes("Style") = "display:none;"
            contTextos.Attributes("Style") = "display:none;"
            contTextos2.Attributes("Style") = "display:none;"
            GuardarBod.Attributes("Style") = "display:none;"
            'contObsRechazadas.Attributes("Style") = "display:none;"
            contDtgDetalle.Attributes("Style") = "display:none;"

        End If


    End Sub


    Sub ocultarMostrarFormulario2(ByVal bool As Boolean)
        If bool Then
            'contNavbar.Attributes("Style") = ""
            contNuevoArticulo.Attributes("Style") = "display:none;"
            contObservaciones.Attributes("Style") = ""
            contTitulos.Attributes("Style") = ""
            contTextos.Attributes("Style") = ""
            contTextos2.Attributes("Style") = ""
            GuardarBod.Attributes("Style") = ""
            'contObsRechazadas.Attributes("Style") = ""
            contDtgDetalle.Attributes("Style") = ""

            'mostrarObservacionesRechazadas()

        Else
            'contNavbar.Attributes("Style") = "displtyay:none;"
            contNuevoArticulo.Attributes("Style") = ""
            contObservaciones.Attributes("Style") = "display:none;"
            contTitulos.Attributes("Style") = "display:none;"
            contTextos.Attributes("Style") = "display:none;"
            contTextos2.Attributes("Style") = "display:none;"
            GuardarBod.Attributes("Style") = "display:none;"
            'contObsRechazadas.Attributes("Style") = "display:none;"
            contDtgDetalle.Attributes("Style") = "display:none;"

        End If


    End Sub


    Function validarAprobRechazados() As Boolean

        Dim cant As Integer = 0
        Dim filas As Integer = dtgDetalle.Rows.Count - 1
        For i As Integer = 0 To filas
            If dtgDetalle.Rows(i).Cells(17).Text = "PO" Then
                cant += 1
            End If
        Next

        If cant = filas + 1 Then
            Return False
        Else
            Return True
        End If


    End Function


    Sub llenarComboCodigoSolicitud()
        query = " SELECT CODIGO_SOLICITUD  FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA"
        'query += " WHERE CODIGO_SOLICITUD LIKE '" & Session("codigo") & "%'"
        query += " WHERE ESTADO = 'P' ORDER BY CODIGO_SOLICITUD DESC"
        fn.llenarComboBox(cmbCodigoProyecto, query, "CODIGO_SOLICITUD", "CODIGO_SOLICITUD")

    End Sub

    Sub llenarComboBodegaDestino()
        query = " SELECT 'ND' BODEGA, 'ND'LOCALIZACION"
        query += " UNION ALL"
        query += " SELECT BODEGA,"
        query += " ISNULL((SELECT LOCALIZACION FROM VITALERP.VITALICIA.LOCALIZACION B"
        query += " WHERE B.LOCALIZACION=A.BODEGA"
        query += " AND A.BODEGA=B.BODEGA),ISNULL((SELECT TOP 1 LOCALIZACION "
        query += " FROM VITALERP.VITALICIA.LOCALIZACION B "
        query += " WHERE A.BODEGA=B.BODEGA),'ND'))LOCALIZACION"
        query += "  FROM VITALERP.VITALICIA.BODEGA A"
        query += " ORDER BY BODEGA"


        fn.llenarComboBox(cmbBodegaDestino, query, "BODEGA", "LOCALIZACION")



    End Sub

    Function obtenerProyecto(ByVal a As String) As String
        Dim codSol As String
        Try
            codSol = a.Substring(0, 4)
        Catch ex As Exception
            codSol = ""
        End Try


        Dim query As String = "SELECT USUARIO FROM SOL_PEDIDOS.PEDIDOS.ACCESO WHERE CODIGO ='" & codSol & "' "
        Dim res As String = fn.DevolverDatoQuery(query)
        Return res
    End Function



    Sub llenarUM()  'Origen determina si la unidad de medida viene de presupuesto o almacenes

        Dim proyecto As String = lblProyecto.Text
        Dim articulo As String = cmbArticulo.SelectedValue
        Dim umDef As String = lblUMAux.Text

        lblUMEq.Text = com.obtenerUMPresupuesto(proyecto, articulo, umDef)

        lblUM.Text = com.obtenerUMAlmacen(articulo, lblUMEq.Text)

        lblFactor.Text = com.obtenerFactorConversion(proyecto, articulo)

    End Sub

    Sub llenarUM2()  'Origen determina si la unidad de medida viene de presupuesto o almacenes

        Dim proyecto As String = lblProyecto.Text
        Dim articulo As String = cmbArticulo2.SelectedValue
        Dim umDef As String = lblUMAux2.Text

        lblUMEq2.Text = com.obtenerUMPresupuesto(proyecto, articulo, umDef)
        lblUM2.Text = com.obtenerUMAlmacen(articulo, lblUMEq2.Text)
        lblFactor2.Text = com.obtenerFactorConversion(proyecto, articulo)

    End Sub

    Protected Sub cmbActividad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbActividad2.SelectedIndexChanged

        com.llenarComboMaterial(cmbMaterial2, cmbActividad2.SelectedValue, "FASE", "FASE", "NOMBRE", 2)
        com.llenarComboArticulo(cmbArticulo2, cmbMaterial2.SelectedValue, "ARTICULO", "ARTICULO", "NOMBRE", 2)
        llenarUM2()
    End Sub

    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_LOG_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET ESTADO=CASE WHEN ESTADO ='PO' THEN 'P' ELSE 'PO' END WHERE ID = @ID"
        dtgDetalle.DataSourceID = "Detalles"



        query = "SELECT DISTINCT BODEGA_O FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA WHERE CODIGO_SOLICITUD=@CODIGO_PROYECTO AND CODIGO_CONTROL<>'B' AND ESTADO<>'PO'"
        dsBodegas.ConnectionString = fn.ObtenerCadenaConexion("conn")
        dsBodegas.SelectCommand = query
        dtgBodegas.DataSourceID = "dsBodegas"


        validarConcurrencia()
        'If dtgDetalle.Rows.Count = 0 Then
        '    btnAgregar.Attributes("class") = "btn btn-default btn-md disabled"
        '    btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md disabled"
        '    btnRechazar.Attributes("class") = "btn btn-default btn-md disabled"

        'Else
        '    btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
        '    btnRechazar.Attributes("class") = "btn btn-default btn-md enabled"
        '    btnAgregar.Attributes("class") = "btn btn-default btn-md enabled"
        'End If

    End Sub

    'renderizar tabla
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_LOG_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY ID"

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

#Region "validar bodegas"

#End Region
#Region "Combo en tabla"
    Private Function GetData(query As String) As DataSet
        Dim conString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(conString)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con

                sda.SelectCommand = cmd
                Using ds As New DataSet()
                    sda.Fill(ds)
                    Return ds
                End Using
            End Using
        End Using
    End Function

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            'Find the DropDownList in the Row
            Dim cmbBodegas As DropDownList = CType(e.Row.FindControl("cmbBodegas"), DropDownList)
            cmbBodegas.DataSource = GetData("SELECT DISTINCT BODEGA, NOMBRE FROM SOL_PEDIDOS.VITALICIA.BODEGA ORDER BY BODEGA")
            cmbBodegas.DataTextField = "bodega"
            cmbBodegas.DataValueField = "Bodega"
            cmbBodegas.DataBind()
            'Add Default Item in the DropDownList
            cmbBodegas.Items.Insert(0, New ListItem("ND"))
            cmbBodegas.Items.Insert(1, New ListItem("OMITIR"))
            ' Select the Country of Customer in DropDownList
            Dim country As String = CType(e.Row.FindControl("lblBodegas"), Label).Text
            cmbBodegas.Items.FindByValue(country).Selected = True
        End If
    End Sub




#End Region


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("almacenero")
        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 5) + ";Ingreso.aspx")
        'validarReimpresionRendicion()



        If Not Page.IsPostBack Then
            'txtFecha.Text = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")

            llenarComboCodigoSolicitud()
            llenarComboBodegaDestino()
            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue


            llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
            Try
                cmbBodegaDestino.SelectedValue = fn.DevolverDatoQuery("SELECT LOCALIZACION_D FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'")
            Catch ex As Exception

            End Try
            llenarTablaDetallesPedido()
        End If
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        verificarUM()
        ''llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)

    End Sub



    Protected Sub dtgDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalle.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgDetalle.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString
        Dim cantA, cantP As Double
        Dim estado As String = dtgDetalle.Rows(fila).Cells(14).Text


        If estado <> "PO" Then

            lblActividad.Text = dtgDetalle.Rows(fila).Cells(3).Text
            lblActividadDesc.Text = dtgDetalle.Rows(fila).Cells(4).Text
            lblMaterial.Text = dtgDetalle.Rows(fila).Cells(5).Text
            lblMaterialDesc.Text = dtgDetalle.Rows(fila).Cells(6).Text
            lblArticulo.Text = dtgDetalle.Rows(fila).Cells(7).Text

            lblUMAux.Text = dtgDetalle.Rows(fila).Cells(9).Text
            txtCantidad.Text = CDbl(dtgDetalle.Rows(fila).Cells(10).Text)
            lblCantOriginal.Text = dtgDetalle.Rows(fila).Cells(16).Text
            lblUMEq.Text = dtgDetalle.Rows(fila).Cells(11).Text
            cantP = CDbl(dtgDetalle.Rows(fila).Cells(10).Text)
            cantA = CDbl(dtgDetalle.Rows(fila).Cells(12).Text)
            lblFactor.Text = CStr((cantP / cantA))
            lblCantEq.Text = dtgDetalle.Rows(fila).Cells(12).Text
            lblId.Text = dtgDetalle.Rows(fila).Cells(2).Text
            com.llenarComboArticulo(cmbArticulo, lblMaterial.Text, "ARTICULO", "ARTICULO", "NOMBRE", 2)
            llenarUM()
            ocultarMostrarFormulario(False)
            mostrarMensaje("", "")

            Dim cantOriginal As Double
            Dim factor As Double
            Dim res As Double

            Try
                cantOriginal = CDbl(lblCantOriginal.Text)
                factor = CDbl(lblFactor.Text)
                res = Math.Round((cantOriginal / factor) * 1000) / 1000
                'btnAceptarEdit.Attributes("class") = "btn btn-vitalicia btn-md disabled"
                lblErrorArticuloS.Attributes("style") = ""
                lblErrorArticuloS.Attributes("class") = "alert alert-success"
                lblErrorArticulo.Text = "Cantidad sugerida: " & res

            Catch ex As Exception

            End Try
        Else
            mostrarMensaje("No se puede editar una fila pospuesta.", "error")

        End If

    End Sub

    Protected Sub dtgDetalle_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgDetalle.SelectedIndexChanging

        Dim row As GridViewRow

        row = dtgDetalle.Rows(e.NewSelectedIndex)



            If row.Cells(1).Text = "ANATR" Then
            e.Cancel = True
        End If
    End Sub


    Protected Sub grdSolicitudDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound ', dtgDetalle2.RowDataBound
        'Dim actividad As String = e.Row.Cells(18).Text ' CANTIDAD_DISPONIBLE
        'Dim material As String = e.Row.Cells(19).Text ' CANTIDAD_DISPONIBLE
        Dim desc As String = e.Row.Cells(14).Text ' CANTIDAD_DISPONIBLE
        'Dim presup As String = e.Row.Cells(16).Text ' PRESUPUESTADO
        'Dim presupAct As String = e.Row.Cells(15).Text 'PRESUPUESTADO_ACT
        'Dim actas As String = e.Row.Cells(9).Text ' CANTIDAD_ACTAS
        Dim estado As String = e.Row.Cells(14).Text ' ESTADO


        If desc = "&nbsp;" Or desc = "ESTADO" Then
            Dim B As Integer = 0
            'e.Row.Cells(7).Text)
        Else
            'e.Row.Cells(7).Text)

            If estado = "PO" Then
                e.Row.Font.Strikeout = True
                e.Row.Font.Overline = True
                e.Row.Font.Underline = True
                'e.Row.Font.Italic = True
                e.Row.Font.Size = 7
                e.Row.ForeColor = Drawing.Color.Red
                e.Row.Font.Bold = True
                e.Row.BorderColor = Drawing.Color.Red

            End If

            If estado = "GA" Then
                e.Row.BackColor = Drawing.Color.SkyBlue
            End If

        End If


    End Sub
    Protected Sub cmbCodigoProyecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCodigoProyecto.SelectedIndexChanged

        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
        llenarTablaDetallesPedido()

        Try
            cmbBodegaDestino.SelectedValue = fn.DevolverDatoQuery("SELECT LOCALIZACION_D FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'")
        Catch ex As Exception

        End Try

        verificarUM()
        mostrarMensaje("", "")
    End Sub
    Protected Sub cmbActividad2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbActividad2.SelectedIndexChanged
        com.llenarComboMaterial(cmbMaterial2, cmbActividad2.SelectedValue, "FASE", "FASE", "NOMBRE", 2)
        com.llenarComboArticulo(cmbArticulo2, cmbMaterial2.SelectedValue, "ARTICULO", "ARTICULO", "NOMBRE", 2)
        llenarUM2()
    End Sub
    Protected Sub cmbMaterial2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMaterial2.SelectedIndexChanged
        com.llenarComboArticulo(cmbArticulo2, cmbMaterial2.SelectedValue, "ARTICULO", "ARTICULO", "NOMBRE", 2)
        llenarUM2()
    End Sub
    Protected Sub cmbArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArticulo.SelectedIndexChanged
        Dim cantOriginal As Double
        Dim factor As Double
        Dim res As double
        llenarUM()
        Try
            cantOriginal = CDbl(lblCantOriginal.Text)
            factor = CDbl(lblFactor.Text)
            res = Math.Round((cantOriginal / factor) * 1000) / 1000
            'btnAceptarEdit.Attributes("class") = "btn btn-vitalicia btn-md disabled"
            lblErrorArticuloS.Attributes("style") = ""
            lblErrorArticuloS.Attributes("class") = "alert alert-success"
            lblErrorArticulo.Text = "Cantidad sugerida: " & res

        Catch ex As Exception

        End Try



    End Sub
    Protected Sub cmbArticulo2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArticulo2.SelectedIndexChanged
        llenarUM2()
    End Sub
    Protected Sub btnAceptarEdit_Click(sender As Object, e As EventArgs) Handles btnAceptarEdit.Click
        Dim actividad As String = lblActividad.Text
        Dim material As String = lblMaterial.Text
        Dim articulo As String = cmbArticulo.SelectedValue
        Dim um As String = lblUM.Text
        Dim ume As String = lblUMEq.Text
        Dim cant As Double = 0
        Dim cantEq As Double = 0
        Dim cantOr As Double = 0
        Dim cantRest As Double = 0
        Dim id As String = lblId.Text

        cant = com.validarNumero(txtCantidad.Text)

        cantEq = cant * CDbl(lblFactor.Text)
        If cant <> (-1) Then
            cantOr = CDbl(lblCantOriginal.Text)
            cantRest = cantOr - cantEq
            If cantRest > 0 Then
                query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA "
                query += " (PROYECTO,ACTIVIDAD,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,UND_MED_E,CANTIDAD_APS,ESTADO, RecordDate, UpdatedBy)"
                query += " VALUES "
                query += " ('" & lblProyecto.Text & "','" & actividad & "','" & material & "','" & articulo & "','" & cantRest / CDbl(lblFactor.Text) & "','" & cmbCodigoProyecto.SelectedValue & "', 'C','" & ume & "','" & um & "','" & cantRest & "','GA', GETDATE(),'" & lblUsuario.Text & "')"

                fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            End If
            query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET ACTIVIDAD='" & actividad & "', FASE='" & material & "', ARTICULO='" & articulo & "', UND_MED='" & ume & "', UND_MED_E='" & um & "',"
            query += " CANTIDAD='" & cant & "', CANTIDAD_APS='" & cantEq & "',  CODIGO_CONTROL='M', ESTADO='P', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE() "
            query += " WHERE ID='" & id & "'"

            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

            llenarTablaDetallesPedido()
            mostrarMensaje("", "exito")
        Else
            mostrarMensaje("Ha ocurrido un error con la cantidad ingresada. Intente la transacción nuevamente.", "error")
        End If


        ocultarMostrarFormulario(True)
    End Sub

    Protected Sub btnAcepAgre_Click(sender As Object, e As EventArgs) Handles btnAcepAgre.Click
        Dim actividad As String = cmbActividad2.SelectedValue
        Dim material As String = cmbMaterial2.SelectedValue
        Dim articulo As String = cmbArticulo2.SelectedValue
        Dim um As String = lblUM2.Text
        Dim ume As String = lblUMEq2.Text
        Dim cant As Double = 0
        Dim cantEq As Double = 0



        cant = com.validarNumero(txtCantidad.Text)

        cantEq = cant * CDbl(lblFactor2.Text)
        If cant <> (-1) Then
            query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA "
            query += " (PROYECTO,ACTIVIDAD,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,UND_MED_E,CANTIDAD_APS,ESTADO, RecordDate, UpdatedBy)"
            query += " VALUES "
            query += " ('" & lblProyecto.Text & "','" & actividad & "','" & material & "','" & articulo & "','" & cant & "','" & cmbCodigoProyecto.SelectedValue & "', 'C','" & ume & "','" & um & "','" & cantEq & "','GA', GETDATE(),'" & lblUsuario.Text & "')"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

            llenarTablaDetallesPedido()
            mostrarMensaje("", "exito")
        Else
            mostrarMensaje("Ha ocurrido un error con la cantidad ingresada. Intente la transacción nuevamente.", "error")
        End If


        ocultarMostrarFormulario2(True)
    End Sub


    Protected Sub btnCancelarEdit_Click(sender As Object, e As EventArgs) Handles btnCancelarEdit.Click
        ocultarMostrarFormulario(True)
    End Sub

    Protected Sub btnCancAgre_Click(sender As Object, e As EventArgs) Handles btnCancAgre.Click
        ocultarMostrarFormulario2(True)
    End Sub

    Protected Sub btnAprobarSi_Click(sender As Object, e As EventArgs) Handles btnAprobarSi.ServerClick
        'VERIFICAMOS QUE SE ASIGNEN TODAS LAS BODEGAS

        Dim contador As Integer
        Dim bodegasNA As String
        Dim contadorSQL As Integer
        Dim archivosGenerados As String = "" 'ALMACENA TODOS LOS ARCHIVOS TXT GENERADOS
        contador = dtgDetalle.Rows.Count
        If cmbBodegaDestino.SelectedItem.ToString = "ND" Then
            mostrarMensaje("Debe asignar la bodega de destino antes de generar el archivo de texto.", "error")
            Exit Sub
        End If

        If log.validarBodegasND(dtgBodegas) = True Then
            mostrarMensaje("Debe asignar todas las bodegas de origen en las líneas antes de continuar", "error")
            Exit Sub
        End If

        'VERIFICAMOS QUE NO TODAS LAS LINEAS ESTÉN POSPUESTAS
        If log.validarEstadoLineas(cmbCodigoProyecto.SelectedItem.ToString, "'PO'") = 0 Then
            mostrarMensaje("No puede generar el archivo de texto de una solicitud con todos los artículos pospuestos.", "error")
            Exit Sub
        End If

        'VERIFICAMOS QUE TODAS LAS RUTAS DE IMPRESIÓN DE TXT PARA LAS BODEGAS EXISTAN
        bodegasNA = log.validarRutaBodegas(dtgBodegas)
        If bodegasNA <> "" Then
            mostrarMensaje("No se encontró una ruta válida para la bodega " & bodegasNA & ", configure una e intente nuevamente", "error")
            Exit Sub
        End If

        mostrarMensaje("", "exito")
        For Each row As GridViewRow In dtgBodegas.Rows

            archivosGenerados += log.generarTXT(dtgDetalle, cmbCodigoProyecto.SelectedValue, row.Cells(0).Text.ToString(), cmbBodegaDestino.SelectedValue, "PO", lblMensaje, lblMensajeS, lblUsuario.Text)
            archivosGenerados += ", "
        Next


        contadorSQL = log.validarEstadoLineas(cmbCodigoProyecto.SelectedItem.ToString, "'PO'")

        If contador = contadorSQL Then
            log.actualizarEstadoCabecera(cmbCodigoProyecto.SelectedItem.ToString, "G", lblMensaje, lblMensajeS, lblUsuario.Text)
        Else
            log.actualizarEstadoCabecera(cmbCodigoProyecto.SelectedItem.ToString, "GP", lblMensaje, lblMensajeS, lblUsuario.Text)
        End If



        'ACTUALIZAMOS LA FECHA DE REGISTRO DE LAS LINEAS (ESTO SE HACE EN UNA INSTRUCCION PARA TODAS LAS FILAS, PARA QUE SE TENGA LA MISMA FECHA-HORA DE REGISTRO)
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET FECHA_HORA_REG=GETDATE() WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND CODIGO_CONTROL<>'B' AND ESTADO='G'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        liberarSolicitud()
        mostrarMensaje("Los archivos de texto " & archivosGenerados & "fueron generados exitosamente", "exito")

    End Sub
    Protected Sub btnRechazarSi_Click(sender As Object, e As EventArgs) Handles btnRechazarSi.ServerClick
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA SET ESTADO='PO', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET ESTADO='PO', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD ='" & cmbCodigoProyecto.SelectedValue & "' AND CODIGO_CONTROL<>'B'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        'query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET CODIGO_CONTROL='B', ESTADO='B' WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        'fn.ejecutarComandoSQL(query)
        liberarSolicitud()
        mostrarMensaje("Solicitud pospuesta exitosamente.", "exito")
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.ServerClick
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue

        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
        mostrarMensaje("", "error")
    End Sub
    Protected Sub btnGuardarBod_Click(sender As Object, e As EventArgs) Handles btnGuardarBod.Click
        Dim bodega As String = ""
        Dim localizacion As String = ""
        For Each row As GridViewRow In dtgDetalle.Rows
            'dtgdetalles-********ojo con cambios en la cantidad de columnas
            bodega = TryCast(row.Cells(13).FindControl("cmbBodegas"), DropDownList).SelectedItem.Value
            query = " SELECT "
            query += " ISNULL((SELECT LOCALIZACION FROM VITALERP.VITALICIA.LOCALIZACION B"
            query += " WHERE B.LOCALIZACION=A.BODEGA"
            query += " AND A.BODEGA=B.BODEGA),(SELECT TOP 1 LOCALIZACION "
            query += " FROM VITALERP.VITALICIA.LOCALIZACION B "
            query += " WHERE A.BODEGA=B.BODEGA))LOCALIZACION"
            query += "  FROM VITALERP.VITALICIA.BODEGA A"
            query += " WHERE BODEGA='" & bodega & "'"

            localizacion = fn.DevolverDatoQuery(query)
            If localizacion = "" Then
                localizacion = "ND"
            End If
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET BODEGA_O = '" & bodega & "', LOCALIZACION_O = '" & localizacion & "'"
            query += " WHERE ID='" & row.Cells(2).Text.ToString() & "'"

            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)



        Next
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA SET BODEGA_D='" & cmbBodegaDestino.SelectedItem.ToString & "', LOCALIZACION_D='" & cmbBodegaDestino.SelectedValue.ToString & "', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue.ToString & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        llenarTablaDetallesPedido()
    End Sub
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        com.llenarComboActividad(cmbActividad2, lblProyecto.Text, "FASE", "FASE", "NOMBRE", 2)
        com.llenarComboMaterial(cmbMaterial2, cmbActividad2.SelectedValue, "FASE", "FASE", "NOMBRE", 2)
        com.llenarComboArticulo(cmbArticulo2, cmbMaterial2.SelectedValue, "ARTICULO", "ARTICULO", "NOMBRE", 2)
        llenarUM2()
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
        ocultarMostrarFormulario2(False)
        mostrarMensaje("", "")
    End Sub
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        liberarSolicitud()
        Response.Redirect("principal.aspx")

    End Sub

    Protected Sub grdSolicitudDetalle_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dtgDetalle.RowDeleting
        llenarTablaDetallesPedido()
    End Sub


    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(2).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(15).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(17).Visible = False
        'e.Row.Cells(18).Visible = False
        'e.Row.Cells(19).Visible = False
    End Sub




#Region "BOTONES NAVEGACION DERECHA"




    Protected Sub btnSinc_Click(sender As Object, e As EventArgs) Handles btnSinc.Click
        query = " EXEC SOL_PEDIDOS.PEDIDOS.SINCRONIZAR"
        Try
            fn.ejecutarComandoSQL2(query)
            mostrarMensaje("Sincronización realizada satisfactoriamente", "exito")
        Catch ex As Exception
            mostrarMensaje("Hubo un problema en la sincronización. Inténtelo más tarde.", "error")
        End Try
    End Sub

    Protected Sub btnPM_Click(sender As Object, e As EventArgs) Handles btnPM.Click
        Dim sUrl As String = "presupuestoLog.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)
    End Sub

    Protected Sub btnTablaConversiones_Click(sender As Object, e As EventArgs) Handles btnTablaConversiones.Click
        Dim sUrl As String = "conversionUM.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        liberarSolicitud()
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub


#End Region

End Class
