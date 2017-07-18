


Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports Docs.Excel
Partial Class logistica_pospuestos
    Inherits System.Web.UI.Page

    Dim fn As New Funciones
    Dim com As New comun
    Dim query As String
    Dim log As New logistica

    'variables para la generacion de excel
    Dim filadatos As String
    Dim columnadatos As String
    Dim objVentana As Object
    Dim objexcel As Object
    Dim objlibro As Object 'Excel.Workbook
    Dim objHojaExcel As Object
    Dim Archivo As String
    Dim Archivo2 As String

    Dim codRep As String
    Dim a As String
    Dim plantilla As String
    Dim rutaPlantilla As String
    Dim ext As String

    Sub generarExcel()

        Dim cantFilas As Integer = dtgDetalle.Rows.Count

        Dim Wbook As New ExcelWorkbook()

        plantilla = "SolicitudCompra"
        ext = ".xls"
        filadatos = "9"
        columnadatos = "1"
        rutaPlantilla = "C:\Plantillas Control de Pedidos\"
        Wbook = ExcelWorkbook.ReadXLS(rutaPlantilla & plantilla & ext)
        Dim Wsheet As ExcelWorksheet = Wbook.Worksheets(0)

        filadatos = filadatos - 1
        Dim Extension, ArchivoFecha As String
        Dim NombrePlantilla As String = ""

        Archivo = plantilla
        Archivo2 = "Solicitud de Compra " & lblCodigoProyecto.Text
        Extension = ".xls"
        ArchivoFecha = Archivo2 & "_" & Now.ToString("yyyyMMddhhmmss")
        ArchivoFecha += Extension
        Archivo += Extension



        Dim WorksheetName As String = Wbook.Worksheets(0).Name
        Dim j As Integer = 0
        Dim k As Integer = 0
        For i As Integer = 0 To dtgDetalle.Rows.Count - 1

            If (Server.HtmlDecode(dtgDetalle.Rows(i).Cells(14).Text)) <> "R" Then
                j += 1
                Wsheet.Cells(i - k + filadatos, 0).Value = (i + 1 - k)
                Wsheet.Cells(i - k + filadatos, 0).Style.PatternForeColor = Color.White

                Wsheet.Cells(i - k + filadatos, 1).Value = Server.HtmlDecode(dtgDetalle.Rows(i).Cells(7).Text)
                Wsheet.Cells(i - k + filadatos, 1).Style.PatternForeColor = Color.White

                Wsheet.Cells(i - k + filadatos, 2).Value = Server.HtmlDecode(dtgDetalle.Rows(i).Cells(8).Text)

                Wsheet.Cells(i - k + filadatos, 3).Value = Server.HtmlDecode(dtgDetalle.Rows(i).Cells(9).Text)
                Wsheet.Cells(i - k + filadatos, 3).Style.PatternForeColor = Color.White

                Wsheet.Cells(i - k + filadatos, 4).Value = CDbl(Server.HtmlDecode(dtgDetalle.Rows(i).Cells(10).Text))

                Wsheet.Cells("A" & (i - k + filadatos).ToString & ":E" & (i - k + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Medium, ColorPalette.Black)
            Else
                k += 1
            End If

        Next
        If j = 0 Then
            Exit Sub
            mostrarMensaje("No hay lineas para generar la solicitud de compra", "exito")
        End If
        Wsheet.Cells("A" & (filadatos - k + dtgDetalle.Rows.Count).ToString & ":E" & (filadatos - k + dtgDetalle.Rows.Count).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Medium, ColorPalette.Black)

        Wsheet.Cells(2, 2).Value = DateTime.Now()
        Wsheet.Cells(2, 2).Style.StringFormat = "DD-MM-YYYY hh:mm:ss"
        Wsheet.Cells(3, 2).Value = lblProyecto.Text
        Wsheet.Cells(4, 2).Value = txtSolicitante2.Text
        Wsheet.Cells(5, 2).Value = lblCodigoProyecto.Text
        Wsheet.Cells(6, 2).Value = txtCodigoSolicitud.Text

        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 1).ToString & ":E" & (dtgDetalle.Rows.Count + filadatos - k + 1).ToString).IsMerged = True
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 1).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k, 0).Style.Font.Bold = True
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k, 0).Style.PatternForeColor = Color.White
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k, 0).Value = "OBSERVACIONES:"

        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 2).ToString & ":E" & (dtgDetalle.Rows.Count + filadatos - k + 4).ToString).IsMerged = True
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 2).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 1, 0).Style.WrapText = True

        'Wsheet.Rows(dtgDetalle.Rows.Count + filadatos - k + 1).Height = 400
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 1, 0).Style.VerticalAlignment = TypeOfVAlignment.Top
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 1, 0).Value = txtObservaciones2.InnerText.ToString




        'CONFIGURACION PARA LA CELDA "ENTREGADO"
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 5).ToString & ":B" & (dtgDetalle.Rows.Count + filadatos - k + 5).ToString).IsMerged = True
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 5).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 1).Style.Font.Bold = True
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 1).Style.HorizontalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 1).Style.PatternForeColor = Color.White
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 1).Value = "ENTREGADO POR"
        'configuracion para la celda de firmas
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 6).ToString & ":B" & (dtgDetalle.Rows.Count + filadatos - k + 9).ToString).IsMerged = True
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 6).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)

        Wsheet.Cells("C" & (dtgDetalle.Rows.Count + filadatos - k + 5).ToString & ":E" & (dtgDetalle.Rows.Count + filadatos - k + 5).ToString).IsMerged = True
        Wsheet.Cells("C" & (dtgDetalle.Rows.Count + filadatos - k + 5).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 2).Style.Font.Bold = True
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 2).Style.PatternForeColor = Color.White
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 4, 2).Value = "RECIBIDO POR"

        Wsheet.Cells("C" & (dtgDetalle.Rows.Count + filadatos - k + 6).ToString & ":E" & (dtgDetalle.Rows.Count + filadatos - k + 9).ToString).IsMerged = True
        Wsheet.Cells("C" & (dtgDetalle.Rows.Count + filadatos - k + 6).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)

        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 10).ToString & ":E" & (dtgDetalle.Rows.Count + filadatos - k + 10).ToString).IsMerged = True
        Wsheet.Cells("A" & (dtgDetalle.Rows.Count + filadatos - k + 10).ToString).SetBordersStyles(TypeOfMultipleBorders.Outside, TypeOfBorderLine.Medium, ColorPalette.Black)
        Wsheet.Cells(dtgDetalle.Rows.Count + filadatos - k + 9, 0).Style.PatternForeColor = Color.White

        Wbook.WriteXLS(rutaPlantilla & "WriteXLS.xls")

        Response.AppendHeader("Content-Type", "application/vnd.ms-excel")
        Response.AppendHeader("Content-Disposition", [String].Format("attachment; filename={0}", ArchivoFecha))
        Response.BinaryWrite(Wbook.WriteXLS().ToArray())
        Response.Flush()
        Response.End()


    End Sub


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
        'Page.ClientScript.RegisterStartupScript(Page.ClientScript.[GetType](), "onLoad", "MostrarLabel();", True)
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

    ''Sub mostrarObservacionesRechazadas()

    'Dim cant As Integer = 0
    '    For i As Integer = 0 To dtgDetalle.Rows.Count - 1
    '        If dtgDetalle.Rows(i).Cells(17).Text = "R" Then
    '            cant += 1
    '            Exit For
    '        End If
    '    Next
    '    If cant = 0 Then
    '        contObsRechazadas.Attributes("Style") = "display:none;"
    '    Else
    '        contObsRechazadas.Attributes("Style") = ""
    '    End If


    'End Sub



    Sub llenarComboCodigoSolicitud()
        query = " SELECT CODIGO_SOLICITUD  FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA"
        'query += " WHERE CODIGO_SOLICITUD LIKE '" & Session("codigo") & "%'"
        query += " WHERE ESTADO IN ('GP','PO') ORDER BY CODIGO_SOLICITUD DESC"
        fn.llenarComboBox(cmbCodigoProyecto, query, "CODIGO_SOLICITUD", "CODIGO_SOLICITUD")

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

    Sub llenarComboActividad2()



        query = " (SELECT FASE, NOMBRE,"

        query += " ISNULL((SELECT NRO FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B"
        query += " WHERE A.FASE=B.FASE),0) NUMERO"

        query += " FROM   SOL_PEDIDOS.VITALICIA.FASE_PY A"
        query += " WHERE FASE NOT IN (SELECT FASE FROM  SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00.00' )"
        query += " AND FASE NOT IN (SELECT FASE FROM   SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00')"
        query += " AND PROYECTO = '" & lblProyecto.Text & "' AND FASE LIKE '%00')"
        query += " ORDER BY FASE"


        fn.llenarComboBoxOpciones2(cmbActividad2, query, "FASE", "FASE", "NOMBRE")









    End Sub

    Sub llenarComboMaterial2()
        Dim actividad As String
        Try
            actividad = cmbActividad2.SelectedValue.Substring(0, 8)
        Catch ex As Exception
            actividad = "Otro"
        End Try

        query = " SELECT FASE,NOMBRE FROM SOL_PEDIDOS.VITALICIA.FASE_PY "
        query += " WHERE FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00.00')"
        query += " AND FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00')"
        query += " AND TIPO='A'"

        query += " AND FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00')"
        query += " AND FASE LIKE'" & actividad & "%' "
        query += " ORDER BY NOMBRE"
        fn.llenarComboBoxOpciones2(cmbMaterial2, query, "FASE", "FASE", "NOMBRE")

    End Sub

    Sub llenarcomboArticulo2()
        query = " SELECT DISTINCT A.ARTICULO ARTICULO, B.DESCRIPCION DESCRIPCION "
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY_LOG A, SOL_PEDIDOS.VITALICIA.ARTICULO B "
        query += " WHERE FASE = '" & cmbMaterial2.SelectedValue & "' "

        query += " AND A.ARTICULO= B.ARTICULO ORDER BY DESCRIPCION"

        fn.llenarComboBoxOpciones2(cmbArticulo2, query, "ARTICULO", "ARTICULO", "DESCRIPCION")


    End Sub

    Sub llenarcomboArticulo()
        query = " SELECT DISTINCT A.ARTICULO ARTICULO, B.DESCRIPCION DESCRIPCION "
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY_LOG A, SOL_PEDIDOS.VITALICIA.ARTICULO B "
        query += " WHERE FASE = '" & lblMaterial.Text & "' "

        query += " AND A.ARTICULO= B.ARTICULO ORDER BY DESCRIPCION"
        fn.llenarComboBoxOpciones2(cmbArticulo, query, "ARTICULO", "ARTICULO", "DESCRIPCION")

        Dim UItem As New ListItem("***Articulo Original***", lblArticulo.Text)
        cmbArticulo.Items.Add(UItem)

        cmbArticulo.SelectedIndex = cmbArticulo.Items.Count - 1

    End Sub

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

        llenarComboMaterial2()
        llenarcomboArticulo2()
        llenarUM2()
    End Sub

    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_LOG_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' AND ESTADO_LIN IN ('PO','R','GA') ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET ESTADO=CASE WHEN ESTADO ='PO' THEN 'R' ELSE 'PO' END WHERE ID = @ID "
        dtgDetalle.DataSourceID = "Detalles"



        query = "SELECT DISTINCT BODEGA_O FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA WHERE CODIGO_SOLICITUD=@CODIGO_PROYECTO AND CODIGO_CONTROL<>'B' AND ESTADO NOT IN ('R','G')"
        dsBodegas.ConnectionString = fn.ObtenerCadenaConexion("conn")
        dsBodegas.SelectCommand = query
        dtgBodegas.DataSourceID = "dsBodegas"

        validarConcurrencia()
    End Sub

    'renderizar tabla
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_LOG_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' AND ESTADO_LIN IN ('PO','R','GA') ORDER BY ID"

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
            cmbBodegas.DataSource = GetData("SELECT DISTINCT BODEGA, NOMBRE FROM VITALERP.VITALICIA.BODEGA ORDER BY BODEGA")
            cmbBodegas.DataTextField = "bodega"
            cmbBodegas.DataValueField = "Bodega"
            cmbBodegas.DataBind()
            'Add Default Item in the DropDownList
            cmbBodegas.Items.Insert(0, New ListItem("ND"))
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

            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue


            llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
            Try
                txtBodegaDestino.Text = fn.DevolverDatoQuery("SELECT LOCALIZACION_D FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'")
            Catch ex As Exception

            End Try

        End If
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        verificarUM()
        ''llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
        llenarTablaDetallesPedido()
    End Sub



    Protected Sub dtgDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalle.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgDetalle.SelectedRow


        Dim fila As Integer = row.RowIndex.ToString
        Dim cantA, cantP As Double
        Dim estado As String = dtgDetalle.Rows(fila).Cells(14).Text


        If estado <> "R" Then

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

            llenarcomboArticulo()
            llenarUM()
            ocultarMostrarFormulario(False)
            mostrarMensaje("", "")

            Dim cantOriginal As Double
            Dim factor As Double
            Dim res As Integer

            Try
                cantOriginal = CDbl(lblCantOriginal.Text)
                factor = CDbl(lblFactor.Text)
                res = Math.Truncate(cantOriginal / factor)
                'btnAceptarEdit.Attributes("class") = "btn btn-vitalicia btn-md disabled"
                lblErrorArticuloS.Attributes("style") = ""
                lblErrorArticuloS.Attributes("class") = "alert alert-success"
                lblErrorArticulo.Text = "Cantidad sugerida: " & res

            Catch ex As Exception

            End Try
        Else
            mostrarMensaje("No se puede editar una fila rechazada.", "error")

        End If

    End Sub

    Protected Sub dtgDetalle_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles dtgDetalle.SelectedIndexChanging

        Dim row As GridViewRow

        row = dtgDetalle.Rows(e.NewSelectedIndex)

        If row.Cells(1).Text = "ANATR" Then
            e.Cancel = True
        End If
    End Sub

    'Protected Sub dtgDetalle2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalle2.SelectedIndexChanged

    '    Dim row As GridViewRow

    '    row = dtgDetalle2.SelectedRow

    '    Dim fila As Integer = row.RowIndex.ToString
    '    Dim cantA, cantP As Double
    '    Dim estado As String = dtgDetalle2.Rows(fila).Cells(17).Text

    '    If estado = "A" Then
    '        lblActividad.Text = dtgDetalle3.Rows(fila).Cells(3).Text
    '        lblMaterial.Text = dtgDetalle3.Rows(fila).Cells(5).Text
    '        lblArticulo.Text = dtgDetalle3.Rows(fila).Cells(7).Text
    '        lblUMAux.Text = dtgDetalle3.Rows(fila).Cells(9).Text
    '        txtCantidad.Text = dtgDetalle3.Rows(fila).Cells(16).Text
    '        lblUMEq.Text = dtgDetalle3.Rows(fila).Cells(20).Text
    '        cantP = CDbl(dtgDetalle3.Rows(fila).Cells(16).Text)
    '        cantA = CDbl(dtgDetalle3.Rows(fila).Cells(21).Text)
    '        lblFactor.Text = CStr((cantP / cantA))
    '        lblCantEq.Text = dtgDetalle3.Rows(fila).Cells(21).Text
    '        lblId.Text = dtgDetalle3.Rows(fila).Cells(2).Text
    '        'llenarComboActividad()
    '        'llenarComboMaterial()
    '        llenarcomboArticulo()
    '        llenarUM()
    '        ocultarMostrarFormulario(False)
    '        mostrarMensaje("", "")
    '    Else
    '        mostrarMensaje("No se puede editar una fila rechazada.", "error")

    '    End If


    'End Sub




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

            If estado = "R" Then
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

        Try
            txtBodegaDestino.Text = fn.DevolverDatoQuery("SELECT LOCALIZACION_D FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'")
        Catch ex As Exception

        End Try

        verificarUM()
        mostrarMensaje("", "")
    End Sub
    Protected Sub cmbActividad2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbActividad2.SelectedIndexChanged

        llenarComboMaterial2()
        llenarcomboArticulo2()
        llenarUM2()
    End Sub
    Protected Sub cmbMaterial2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMaterial2.SelectedIndexChanged
        llenarcomboArticulo2()
        llenarUM2()
    End Sub
    Protected Sub cmbArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArticulo.SelectedIndexChanged
        Dim cantOriginal As Double
        Dim factor As Double
        Dim res As Integer
        llenarUM()
        Try
            cantOriginal = CDbl(lblCantOriginal.Text)
            factor = CDbl(lblFactor.Text)
            res = Math.Truncate(cantOriginal / factor)
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
            query += " CANTIDAD='" & cant & "', CANTIDAD_APS='" & cantEq & "',  CODIGO_CONTROL='M', ESTADO='GA', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE() "
            query += " WHERE ID='" & id & "'"

            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
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



        'Cambiamos el punto decimal por coma para hacer las operaciones en el sistema
        txtCantidad2.Text = Replace(txtCantidad2.Text, ".", ",")
        'convertimos la cantidad en tipo Double
        Try
            cant = CDbl(txtCantidad2.Text)
        Catch ex As Exception
            cant = -1
        End Try
        'verificamos que las cantidades coincidan, si no coinciden cambiamos el caracter decimal de coma por punto
        If txtCantidad2.Text <> cant.ToString Then
            txtCantidad2.Text = Replace(txtCantidad2.Text, ",", ".")

            Try
                cant = CDbl(txtCantidad2.Text)
            Catch ex As Exception
                cant = -1
            End Try
        End If

        cantEq = cant * CDbl(lblFactor2.Text)
        If cant <> (-1) Then
            query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA "
            query += " (PROYECTO,ACTIVIDAD,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,UND_MED_E,CANTIDAD_APS,ESTADO, RecordDate, UpdatedBy)"
            query += " VALUES "
            query += " ('" & lblProyecto.Text & "','" & actividad & "','" & material & "','" & articulo & "','" & cant & "','" & cmbCodigoProyecto.SelectedValue & "', 'C','" & ume & "','" & um & "','" & cantEq & "','GA', GETDATE(),'" & lblUsuario.Text & "')"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

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
        Dim contador As Integer
        Dim contadorSQL As Integer
        Dim bodegasNA As String = ""

        Dim archivosGenerados As String = "" 'ALMACENA TODOS LOS ARCHIVOS TXT GENERADOS

        If txtBodegaDestino.Text = "ND" Then
            mostrarMensaje("Debe asignar la bodega de destino antes de generar el archivo de texto.", "error")
            Exit Sub
        End If


        'VERIFICAMOS QUE NO TODAS LAS LINEAS ESTÉN RECHAZADAS O GENERADAS
        If log.validarEstadoLineas(cmbCodigoProyecto.SelectedItem.ToString, "'G','R'") = 0 Then
            mostrarMensaje("No puede generar el archivo de texto de una solicitud con todos los artículos rechazados.", "error")
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
            If row.Cells(0).Text.ToString() <> "ND" Then
                archivosGenerados += log.generarTXT(dtgDetalle, cmbCodigoProyecto.SelectedValue, row.Cells(0).Text.ToString(), txtBodegaDestino.Text, "R", lblMensaje, lblMensajeS, lblUsuario.Text)
                archivosGenerados += ", "
            End If
        Next

        contadorSQL = log.validarEstadoLineas(cmbCodigoProyecto.SelectedItem.ToString, "'G','R'")

        If contadorSQL = 0 Then
            log.actualizarEstadoCabecera(cmbCodigoProyecto.SelectedItem.ToString, "G", lblMensaje, lblMensajeS, lblUsuario.Text)
        Else
            log.actualizarEstadoCabecera(cmbCodigoProyecto.SelectedItem.ToString, "GP", lblMensaje, lblMensajeS, lblUsuario.Text)
        End If

        If archivosGenerados <> "" Then
            'ACTUALIZAMOS LA FECHA DE REGISTRO DE LAS LINEAS (ESTO SE HACE EN UNA INSTRUCCION PARA TODAS LAS FILAS, PARA QUE SE TENGA LA MISMA FECHA-HORA DE REGISTRO)
            query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET FECHA_HORA_REG=GETDATE(), UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND CODIGO_CONTROL<>'B' AND ESTADO='G'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            llenarComboCodigoSolicitud()
            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
            liberarSolicitud()
            mostrarMensaje("Los archivos de texto " & archivosGenerados & "fueron generados exitosamente", "exito")

        End If

    End Sub
    Protected Sub btnRechazarSi_Click(sender As Object, e As EventArgs) Handles btnRechazarSi.ServerClick
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA SET ESTADO='G', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE() WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET ESTADO='R', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE() WHERE CODIGO_SOLICITUD ='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO<>'G' AND CODIGO_CONTROL<>'B'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        'query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET CODIGO_CONTROL='B', ESTADO='B' WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        'fn.ejecutarComandoSQL(query)
        liberarSolicitud()
        mostrarMensaje("Solicitud rechazada exitosamente.", "exito")
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
    End Sub
    Protected Sub btnGenerarAceptar_Click(sender As Object, e As System.EventArgs) Handles btnGenerarAceptar.ServerClick


        If txtObservaciones2.InnerText = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo observaciones."
            Exit Sub
        End If
        If txtSolicitante2.Text = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo de Solicitante."
            Exit Sub
        End If

        If txtCodigoSolicitud.Text = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe ingresar un código de solicitud."
            Exit Sub
        End If


        lblMensajeS.Attributes("Style") = "Display:none;"
        lblMensajeS.Attributes("class") = ""
        lblMensaje.Text = ""

        generarExcel()

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
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET BODEGA_O = '" & bodega & "', LOCALIZACION_O = '" & localizacion & "', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()"
            query += " WHERE ID='" & row.Cells(2).Text.ToString() & "'"

            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)



        Next

    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        liberarSolicitud()
        Response.Redirect("principal.aspx")

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


    Protected Sub btnTablaConversiones_Click(sender As Object, e As EventArgs) Handles btnTablaConversiones.Click
        Dim sUrl As String = "conversionUM.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)
    End Sub

    'Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
    '    lblUMActual.Text = "Almacén"
    '    If lblUMActual.Text = "Presupuesto" Then
    '        contDtgDetalle.Attributes("Style") = ""
    '        contDtgDetalle2.Attributes("Style") = "display:none;"

    '    Else
    '        contDtgDetalle.Attributes("Style") = "display:none;"
    '        contDtgDetalle2.Attributes("Style") = ""
    '    End If
    '    'mostrarObservacionesRechazadas()
    'End Sub

    'Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
    '    lblUMActual.Text = "Presupuesto"
    '    If lblUMActual.Text = "Presupuesto" Then
    '        contDtgDetalle.Attributes("Style") = ""
    '        contDtgDetalle2.Attributes("Style") = "display:none;"
    '    Else
    '        contDtgDetalle.Attributes("Style") = "display:none;"
    '        contDtgDetalle2.Attributes("Style") = ""
    '    End If
    'End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        liberarSolicitud()
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub


#End Region

End Class
