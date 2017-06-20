
Imports System.IO

Partial Class ingenieria_precioUnitario
    Inherits System.Web.UI.Page
    Dim query As String
    Dim fn As New Funciones
    Dim com As New comun
    Dim ing As New ingenieria
    Sub validarInicioSesion()
        If Session("usuario") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
    End Sub


    Sub llenarComboProyecto()
        cmbProyecto.Items.Clear()
        query = " SELECT PROYECTO FROM SOL_PEDIDOS.VITALICIA.PROYECTO_PY"
        fn.llenarComboBox(cmbProyecto, query, "PROYECTO", "PROYECTO")
    End Sub

    Sub verCantidad()
        Dim actividad As String
        Dim query As String
        actividad = cmbActividad.SelectedValue

        query = "SELECT CANTIDAD FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT"
        query += " WHERE FASE='" & actividad & "'"
        lblCantidad.Text = fn.DevolverDatoQuery(query)
    End Sub

    Sub verUnidadMedidaActividad()
        Dim actividad As String
        Dim query As String

        actividad = cmbActividad.SelectedValue

        query = "SELECT UM FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT"
        query += " WHERE FASE='" & actividad & "'"
        lblUm.Text = fn.DevolverDatoQuery(query)

    End Sub

    Sub llenarTablaActividades()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO WHERE ACTIVIDAD=@CODIGO_ACTIVIDAD"

        Actividades.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Actividades.SelectCommand = query


        dtgActividades.DataSourceID = "Actividades"


    End Sub

    Sub llenarTablaDetalleActividades()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET WHERE MATERIAL=@DETALLE_ACTIVIDAD AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"

        detalleActividad.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleActividad.SelectCommand = query

        dtgDetalleAct.DataSourceID = "detalleActividad"

    End Sub
    Sub llenarTablaDetalleActividades2()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET WHERE MATERIAL=@DETALLE_ACTIVIDAD AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"

        detalleActividad.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleActividad.SelectCommand = query

        dtgDetalleAct2.DataSourceID = "detalleActividad"

    End Sub

    Sub llenarTablaDetalleSolicitudes()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_SOLICITUD AND COD_CONT_LINEA<>'B'"

        detalleSolicitudes.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleSolicitudes.SelectCommand = query

        dtgSolicitud.DataSourceID = "detalleSolicitudes"

    End Sub

    Sub llenarTablaDetallesActas()

        query = " SELECT A.CODIGO_ACTA COD_ACTA,"

        query += " (SELECT TOP 1 CODIGO_ACTA_ING "
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA) ACTA_ING,"

        query += " ISNULL((SELECT B.NOMBRE  FROM SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE B.FASE=CONCAT(SUBSTRING(A.FASE,0,9),'.00')),'SIN FASE')NOM_ACTIVIDAD,"

        query += " ISNULL((SELECT B.NOMBRE  FROM SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE B.FASE=A.FASE),'SIN FASE')NOM_MATERIAL,"

        query += " CANTIDAD_APS CANT_P,"

        query += " CANTIDAD CANT_A,"

        query += "(SELECT TOP 1 FECHA_ACTA_ING "
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA) FECHA"

        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA A"
        query += " WHERE A.ID IN (SELECT ID_ACTA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE CODIGO_SOLICITUD=@CODIGO_SOLICITUD)"
        query += " AND CODIGO_CONTROL<>'B'"



        detallesActa.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detallesActa.SelectCommand = query

        dtgActas.DataSourceID = "detallesActa"

    End Sub


#Region "Renderizar cabeceras"
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgActividades.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO WHERE ACTIVIDAD=@CODIGO_ACTIVIDAD"

        Actividades.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Actividades.SelectCommand = query


        dtgActividades.DataSourceID = "Actividades"

        If dtgActividades.Rows.Count > 0 Then
            'This replaces <td> with <th> and adds the scope attribute
            dtgActividades.UseAccessibleHeader = True

            'This will add the <thead> and <tbody> elements
            dtgActividades.HeaderRow.TableSection = TableRowSection.TableHeader

            'This adds the <tfoot> element. 
            'Remove if you don't have a footer row
            dtgActividades.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub dtgDetalleAct_PreRender(sender As Object, e As EventArgs) Handles dtgDetalleAct.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET WHERE MATERIAL=@DETALLE_ACTIVIDAD AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"

        detalleActividad.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleActividad.SelectCommand = query

        dtgDetalleAct.DataSourceID = "detalleActividad"

        If dtgDetalleAct.Rows.Count > 0 Then
            'This replaces <td> with <th> and adds the scope attribute
            dtgDetalleAct.UseAccessibleHeader = True

            'This will add the <thead> and <tbody> elements
            dtgDetalleAct.HeaderRow.TableSection = TableRowSection.TableHeader

            'This adds the <tfoot> element. 
            'Remove if you don't have a footer row
            dtgDetalleAct.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub dtgSolicitud_PreRender(sender As Object, e As EventArgs) Handles dtgSolicitud.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_SOLICITUD AND COD_CONT_LINEA<>'B'"

        detalleSolicitudes.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleSolicitudes.SelectCommand = query

        dtgSolicitud.DataSourceID = "detalleSolicitudes"

        If dtgSolicitud.Rows.Count > 0 Then
            'This replaces <td> with <th> and adds the scope attribute
            dtgSolicitud.UseAccessibleHeader = True

            'This will add the <thead> and <tbody> elements
            dtgSolicitud.HeaderRow.TableSection = TableRowSection.TableHeader

            'This adds the <tfoot> element. 
            'Remove if you don't have a footer row
            dtgSolicitud.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub dtgActas_PreRender(sender As Object, e As EventArgs) Handles dtgActas.PreRender

        query = " SELECT A.CODIGO_ACTA COD_ACTA,"

        query += " (SELECT TOP 1 CODIGO_ACTA_ING "
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA) ACTA_ING,"

        query += " ISNULL((SELECT B.NOMBRE  FROM SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE B.FASE=CONCAT(SUBSTRING(A.FASE,0,9),'.00')),'SIN FASE')NOM_ACTIVIDAD,"

        query += " ISNULL((SELECT B.NOMBRE  FROM SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE B.FASE=A.FASE),'SIN FASE')NOM_MATERIAL,"

        query += " CANTIDAD_APS CANT_P,"

        query += " CANTIDAD CANT_A,"

        query += "(SELECT TOP 1 FECHA_ACTA_ING "
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA) FECHA"

        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA A"
        query += " WHERE A.ID IN (SELECT ID_ACTA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE CODIGO_SOLICITUD=@CODIGO_SOLICITUD)"
        query += " AND CODIGO_CONTROL<>'B'"



        detallesActa.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detallesActa.SelectCommand = query

        dtgActas.DataSourceID = "detallesActa"

        If dtgActas.Rows.Count > 0 Then
            'This replaces <td> with <th> and adds the scope attribute
            dtgActas.UseAccessibleHeader = True

            'This will add the <thead> and <tbody> elements
            dtgActas.HeaderRow.TableSection = TableRowSection.TableHeader

            'This adds the <tfoot> element. 
            'Remove if you don't have a footer row
            dtgActas.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("usuario")




        If Not Page.IsPostBack Then

            llenarComboProyecto()
            com.llenarComboActividad(cmbActividad, cmbProyecto.SelectedValue, "NUMERO", "FASE", "NOMBRE", 2)
            verCantidad()
            verUnidadMedidaActividad()
            llenarTablaActividades()
            llenarTablaDetalleActividades()
            llenarTablaDetalleSolicitudes()

            lblDetalleAct.Text = "DETALLES DE LA ACTIVIDAD: " + cmbActividad.SelectedItem.ToString
        End If

    End Sub

    Protected Sub cmbProyecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProyecto.SelectedIndexChanged
        com.llenarComboActividad(cmbActividad, cmbProyecto.SelectedValue, "NUMERO", "FASE", "NOMBRE", 2)
    End Sub
    Protected Sub cmbActividad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbActividad.SelectedIndexChanged
        verCantidad()
        verUnidadMedidaActividad()
        llenarTablaActividades()
        llenarTablaDetalleSolicitudes()
        lblDetalleAct.Text = "DETALLES DE LA ACTIVIDAD: " + cmbActividad.SelectedItem.ToString

        lblCodSolicitud.Text = ""
        lblActividad.Text = ""
        lblActividadDetalle.Text = ""
        lblSolicitudDesc.Text = ""
        lblSolicitudDesc2.Text = ""

    End Sub
    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"

    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"

    End Sub

#Region "CHANGE SELECTED INDEX TABLA ACTIVIDADES"
    Protected Sub dtgDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgActividades.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgActividades.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString

        Dim estado As String = dtgActividades.Rows(fila).Cells(1).Text
        Dim material As String = dtgActividades.Rows(fila).Cells(2).Text
        lblActividad.Text = (estado)
        lblActividadDetalle.Text = "Detalles del material: " & material
        llenarTablaDetalleActividades()
    End Sub



    Protected Sub dtgDetalle_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgActividades.SelectedIndexChanging

        Dim row As GridViewRow
        row = dtgActividades.Rows(e.NewSelectedIndex)



        If row.Cells(1).Text = "ANATR" Then
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "CHANGE SELECTED INDEX TABLA DETALLES ACTIVIDAD"
    Protected Sub dtgDetalleAct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalleAct.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgDetalleAct.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString

        Dim estado As String = dtgDetalleAct.Rows(fila).Cells(3).Text
        lblCodSolicitud.Text = (estado)

        lblSolicitudDesc.Text = "Detalle de la solicitud: " & estado
        lblSolicitudDesc2.Text = "Detalle de Actas para la solicitud: " & estado
        llenarTablaDetalleSolicitudes()
        llenarTablaDetallesActas()
    End Sub



    Protected Sub dtgDetalleAct_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgDetalleAct.SelectedIndexChanging, dtgDetalleAct.SelectedIndexChanging

        Dim row As GridViewRow

        row = dtgDetalleAct.Rows(e.NewSelectedIndex)


        If row.Cells(1).Text = "ANATR" Then
            e.Cancel = True
        End If
    End Sub

#End Region

    Protected Sub btnCuadroCom_Click(sender As Object, e As EventArgs) Handles btnCuadroCom.Click
        If dtgDetalleAct.Rows.Count <> 0 Then
            ExportToExcel()

            'ing.generarExcelPresupuestos(dtgDetalleAct, "ANALISIS DE PRECIO UNITARIO", lblActividadDetalle.Text.Replace("Detalles del material: ", ""))
        End If
    End Sub
    Protected Sub ExportToExcel()

        Dim row As GridViewRow

        row = dtgActividades.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString

        Dim estado As String = dtgActividades.Rows(fila).Cells(1).Text

        llenarTablaDetalleActividades2()
        lblTitulo.Text = "Presupuesto de Materiales"



        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Analisis de Precio Unitario" & Now.Date.ToString & ".xls")

        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Response.ContentType = "application/vnd.xls"

        Response.ContentEncoding = System.Text.Encoding.UTF8
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            Panel1.RenderControl(hw)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub

#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgActividades.RowCreated

        e.Row.Cells(1).Visible = False
        e.Row.Cells(2).Visible = True

        If lblUMActual.Text = "Presupuesto" Then
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False

        Else

            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False

        End If


    End Sub

    Protected Sub dtgDetalleAct_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalleAct.RowCreated


        If lblUMActual.Text = "Presupuesto" Then
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False
            e.Row.Cells(17).Visible = False


        Else

            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False

        End If


    End Sub

    Protected Sub dtgSolicitud_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgSolicitud.RowCreated

        e.Row.Cells(0).Visible = True

        e.Row.Cells(1).Visible = True
        e.Row.Cells(2).Visible = True
        e.Row.Cells(3).Visible = True

        If lblUMActual.Text = "Presupuesto" Then

            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False
            e.Row.Cells(23).Visible = False
            e.Row.Cells(24).Visible = False
            e.Row.Cells(25).Visible = False

            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False

        Else


            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False

            e.Row.Cells(23).Visible = False
            e.Row.Cells(24).Visible = False
        End If


    End Sub


    Protected Sub dtgActas_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgActas.RowCreated



        If lblUMActual.Text = "Presupuesto" Then
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
        Else
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
        End If


    End Sub
#End Region
End Class
