
Imports System.Drawing
Imports System.IO

Partial Class ingenieria_presupuestoMateriales
    Inherits System.Web.UI.Page
    Dim query As String
    Dim fn As New Funciones
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


    Sub llenarTablaMateriales()

        query = " SELECT NOM_MATERIAL,UM_P,"
        query += " SUM(CANT_SOL_P)CANT_SOL_P,SUM(CANT_PRESUP_P)CANT_PRESUP_P,"
        query += " SUM(CANT_SOL_A)CANT_SOL_A,SUM(CANT_PRESUP_A)CANT_PRESUP_A,"
        query += " SUM(CANT_ACTAS_P)CANT_ACTAS_P,SUM(CANT_PRESUP_ACTAS_P)CANT_PRESUP_ACTAS_P,"
        query += " SUM(CANT_ACTAS_A)CANT_ACTAS_A,SUM(CANT_PRESUP_ACTAS_A)CANT_PRESUP_ACTAS_A,"
        query += " SUM(CANT_SOL_APROB_P)CANT_SOL_APROB_P,SUM(CANT_DISP_P)CANT_DISP_P,"
        query += " SUM(CANT_SOL_APROB_A)CANT_SOL_APROB_A,SUM(CANT_DISP_A)CANT_DISP_A,"
        query += " SUM(CANT_DEV_P)CANT_DEV_P,SUM(CANT_DEV_A)CANT_DEV_A, (SUM(CANT_ACTAS_P)-SUM(CANT_APROB_ACTAS_P))CANT_APROB_ACTAS_P,(SUM(CANT_ACTAS_A)-SUM(CANT_APROB_ACTAS_A))CANT_APROB_ACTAS_A"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO"
        query += " WHERE PROYECTO='" & cmbProyecto.SelectedValue & "'"
        query += " GROUP BY NOM_MATERIAL,UM_P"

        Materiales.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Materiales.SelectCommand = query


        dtgMateriales.DataSourceID = "Materiales"



    End Sub

    Sub llenarTablaDetalleActividades(ByVal MAT As String)

        'query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET WHERE NOM_MATERIAL='" & MAT & "' AND PROYECTO='" & cmbProyecto.SelectedValue & "' AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"
        query = " SELECT NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,SUM(CANT_SOL_APROB_P)CANT_SOL_APROB_P,SUM(CANT_APROB_ACTAS_P)CANT_APROB_ACTAS_P,SUM(CANT_SOL_PEND_P)CANT_SOL_PEND_P,SUM(CANT_SOL_RECH_P)CANT_SOL_RECH_P,(CANT_DISP_P2)CANT_DISP_P,FECHA_APROBACION"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET "
        query += " WHERE NOM_MATERIAL='" & MAT & "' "
        query += " AND PROYECTO='" & cmbProyecto.SelectedValue & "'"
        query += " AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"
        query += " GROUP BY NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,FECHA_APROBACION,CANT_DISP_P2"

        detalleActividad.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleActividad.SelectCommand = query

        dtgDetalleAct.DataSourceID = "detalleActividad"


    End Sub
    Sub llenarTablaDetalleActividades2(ByVal MAT As String)

        'query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET WHERE NOM_MATERIAL='" & MAT & "' AND PROYECTO='" & cmbProyecto.SelectedValue & "' AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"
        query = " SELECT NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,SUM(CANT_SOL_APROB_P)CANT_SOL_APROB_P,SUM(CANT_APROB_ACTAS_P)CANT_APROB_ACTAS_P,SUM(CANT_SOL_PEND_P)CANT_SOL_PEND_P,SUM(CANT_SOL_RECH_P)CANT_SOL_RECH_P,(CANT_DISP_P2)CANT_DISP_P,FECHA_APROBACION"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET "
        query += " WHERE NOM_MATERIAL='" & MAT & "' "
        query += " AND PROYECTO='" & cmbProyecto.SelectedValue & "'"
        query += " AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"
        query += " GROUP BY NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,FECHA_APROBACION,CANT_DISP_P2"

        detalleActividad.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleActividad.SelectCommand = query


        dtgDetalleAct2.DataSourceID = "detalleActividad"


    End Sub

    Sub llenarTablaDetalleDevoluciones(ByVal mat As String)
        query = "SELECT FECHA,PROYECTO,NOM_ACTIVIDAD,NOM_MATERIAL,UM_P,CANT_P FROM SOL_PEDIDOS.PEDIDOS.DEVOLUCION_DET WHERE ESTADO_LIN='A' AND COD_CONT_LINEA<>'B' AND PROYECTO='" & cmbProyecto.SelectedValue & "'  AND NOM_MATERIAL='" & mat & "' "

        detalleDevolucion.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleDevolucion.SelectCommand = query

        dtgDevolucion.DataSourceID = "detalleDevolucion"

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
    Protected Sub dtgMateriales_PreRender(sender As Object, e As EventArgs) Handles dtgMateriales.PreRender

        query = " SELECT NOM_MATERIAL,UM_P,"
        query += " SUM(CANT_SOL_P)CANT_SOL_P,SUM(CANT_PRESUP_P)CANT_PRESUP_P,"
        query += " SUM(CANT_SOL_A)CANT_SOL_A,SUM(CANT_PRESUP_A)CANT_PRESUP_A,"
        query += " SUM(CANT_ACTAS_P)CANT_ACTAS_P,SUM(CANT_PRESUP_ACTAS_P)CANT_PRESUP_ACTAS_P,"
        query += " SUM(CANT_ACTAS_A)CANT_ACTAS_A,SUM(CANT_PRESUP_ACTAS_A)CANT_PRESUP_ACTAS_A,"
        query += " SUM(CANT_SOL_APROB_P)CANT_SOL_APROB_P,SUM(CANT_DISP_P)CANT_DISP_P,"
        query += " SUM(CANT_SOL_APROB_A)CANT_SOL_APROB_A,SUM(CANT_DISP_A)CANT_DISP_A,"
        query += " SUM(CANT_DEV_P)CANT_DEV_P,SUM(CANT_DEV_A)CANT_DEV_A, (SUM(CANT_ACTAS_P)-SUM(CANT_APROB_ACTAS_P))CANT_APROB_ACTAS_P,(SUM(CANT_ACTAS_A)-SUM(CANT_APROB_ACTAS_A))CANT_APROB_ACTAS_A"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO"
        query += " WHERE PROYECTO='" & cmbProyecto.SelectedValue & "'"
        query += " GROUP BY NOM_MATERIAL,UM_P"

        Materiales.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Materiales.SelectCommand = query


        dtgMateriales.DataSourceID = "Materiales"

        If dtgMateriales.Rows.Count > 0 Then
            'This replaces <td> with <th> and adds the scope attribute
            dtgMateriales.UseAccessibleHeader = True

            'This will add the <thead> and <tbody> elements
            dtgMateriales.HeaderRow.TableSection = TableRowSection.TableHeader

            'This adds the <tfoot> element. 
            'Remove if you don't have a footer row
            dtgMateriales.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub dtgDetalleAct_PreRender(sender As Object, e As EventArgs) Handles dtgDetalleAct.PreRender

        Dim row As GridViewRow

        row = dtgMateriales.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString


        Dim material As String = Server.HtmlDecode(dtgMateriales.Rows(fila).Cells(1).Text)

        'query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET WHERE NOM_MATERIAL='" & MAT & "' AND PROYECTO='" & cmbProyecto.SelectedValue & "' AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"
        query = " SELECT NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,SUM(CANT_SOL_APROB_P)CANT_SOL_APROB_P,SUM(CANT_APROB_ACTAS_P)CANT_APROB_ACTAS_P,SUM(CANT_SOL_PEND_P)CANT_SOL_PEND_P,SUM(CANT_SOL_RECH_P)CANT_SOL_RECH_P,(CANT_DISP_P2)CANT_DISP_P,FECHA_APROBACION"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET "
        query += " WHERE NOM_MATERIAL='" & material & "' "
        query += " AND PROYECTO='" & cmbProyecto.SelectedValue & "'"
        query += " AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"
        query += " GROUP BY NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,FECHA_APROBACION,CANT_DISP_P2"

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

    '--------------------------------------------------------------------------------------------------------------------------
    'Sub ExportToExcel()
    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
    '    Response.Charset = ""
    '    Response.ContentType = "application/vnd.ms-excel"
    '    Using sw As New StringWriter()
    '        Dim hw As New HtmlTextWriter(sw)

    '        'To Export all pages
    '        dtgDetalleAct2.AllowPaging = False


    '        dtgDetalleAct2.HeaderRow.BackColor = Color.White
    '        For Each cell As TableCell In dtgDetalleAct2.HeaderRow.Cells
    '            cell.BackColor = dtgDetalleAct2.HeaderStyle.BackColor
    '        Next

    '        dtgDetalleAct2.RenderControl(hw)
    '        'style to format numbers to string
    '        Dim style As String = "<style> .textmode { } </style>"
    '        Response.Write(style)
    '        Response.Output.Write(sw.ToString())
    '        Response.Flush()
    '        Response.[End]()
    '    End Using
    'End Sub

    'Public Overrides Sub VerifyRenderingInServerForm(control As Control)
    '    ' Verifies that the control is rendered
    'End Sub

    Protected Sub ExportToExcel(ByVal origen As String)

        Dim row As GridViewRow

        row = dtgMateriales.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString

        Dim estado As String = dtgMateriales.Rows(fila).Cells(1).Text
        Dim material As String = Server.HtmlDecode(dtgMateriales.Rows(fila).Cells(1).Text)
        llenarTablaDetalleActividades2(material)
        If origen = "D" Then
            lblTitulo2.Text = "Lista de Devoluciones- " & material
        Else
            lblTitulo.Text = "Presupuesto de Materiales-" & material
        End If



        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & lblTitulo.Text & Now.Date.ToString & ".xls")

        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Response.ContentType = "application/vnd.xls"

        Response.ContentEncoding = System.Text.Encoding.UTF8
        Using sw As New StringWriter()
            Dim hw As New HtmlTextWriter(sw)
            If origen = "D" Then
                Panel2.RenderControl(hw)
            Else
                Panel1.RenderControl(hw)
            End If

            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()
        End Using
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub
    '--------------------------------------------------------------------------------------------------------------------------



    Protected Sub dtgSolicitud_PreRender(sender As Object, e As EventArgs) Handles dtgSolicitud.PreRender
        query = "Select * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_SOLICITUD And COD_CONT_LINEA<>'B'"

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

            llenarTablaMateriales()
            llenarTablaDetalleActividades("S")
            llenarTablaDetalleSolicitudes()

        End If

    End Sub

    Protected Sub cmbProyecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProyecto.SelectedIndexChanged
        llenarTablaMateriales()
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
    Protected Sub dtgMateriales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgMateriales.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgMateriales.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString

        Dim estado As String = dtgMateriales.Rows(fila).Cells(1).Text
        Dim material As String = Server.HtmlDecode(dtgMateriales.Rows(fila).Cells(1).Text)
        lblActividad.Text = (estado)
        lblActividadDetalle.Text = "Detalles del material: " & material
        lblDevolucionDetalle.Text = "Detalles de Devolucion del material: " & material

        llenarTablaDetalleActividades(material)
        llenarTablaDetalleDevoluciones(material)


    End Sub



    Protected Sub dtgDetalle_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgMateriales.SelectedIndexChanging

        Dim row As GridViewRow

        row = dtgMateriales.Rows(e.NewSelectedIndex)


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



    Protected Sub dtgDetalleAct_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgDetalleAct.SelectedIndexChanging
        Dim row As GridViewRow

        row = dtgDetalleAct.Rows(e.NewSelectedIndex)


        If row.Cells(1).Text = "ANATR" Then
            e.Cancel = True
        End If
    End Sub

#End Region

    Protected Sub btnCuadroCom_Click(sender As Object, e As EventArgs) Handles btnExcelDetalle.Click
        If dtgDetalleAct.Rows.Count <> 0 Then
            'ing.generarExcelPresupuestos(dtgDetalleAct, "PRESUPUESTO DE MATERIALES", lblActividadDetalle.Text.Replace("Detalles del material: ", ""))
            ExportToExcel("E")
        End If
    End Sub

    Protected Sub btnExcelDevoluciones_Click(sender As Object, e As EventArgs) Handles btnExcelDevoluciones.Click
        If dtgDetalleAct.Rows.Count <> 0 Then
            'ing.generarExcelPresupuestos(dtgDetalleAct, "PRESUPUESTO DE MATERIALES", lblActividadDetalle.Text.Replace("Detalles del material: ", ""))
            ExportToExcel("D")
        End If
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub


#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgMateriales.RowCreated



        If lblUMActual.Text = "Presupuesto" Then


            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False

        Else
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False

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
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False



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
