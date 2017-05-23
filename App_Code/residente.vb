Imports Microsoft.VisualBasic
Imports Docs.Excel
Imports System.Data
Imports System.Data.SqlClient

Public Class residente
    Private query As String
    Dim fn As New Funciones
#Region "FUNCIONES INTERFAZ"

    Sub validarInicioSesion(ByVal usuario As String)
        If usuario = "" Then
            HttpContext.Current.Response.Redirect("resIngreso.aspx")
        End If
    End Sub

    Sub llenarComboCodigoSolicitud(ByVal cmb As DropDownList, ByVal codigo As String, ByVal columna1 As String, ByVal columna2 As String, ByVal columna3 As String, ByVal opcion As Integer, ByVal estado As String)
        query = " SELECT CODIGO_SOLICITUD, CONVERT (char(10), FECHA, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA"
        query += " WHERE CODIGO_SOLICITUD LIKE '" & codigo & "%'"
        query += " AND ESTADO = '" & estado & "' ORDER BY CODIGO_SOLICITUD DESC"

        If opcion = 1 Then
            fn.llenarComboBox2(cmb, query, columna1, columna2)
        Else
            fn.llenarComboBoxOpciones2(cmb, query, columna1, columna2, columna3)
        End If
    End Sub


    Function contarFilas(ByVal codigoProyecto As String) As Integer
        Dim contador As Integer
        query = " SELECT COUNT('A') FROM SOL_PEDIDOS.PEDIDOS.SOL_RES_DET"
        query += " WHERE COD_SOL='" & codigoProyecto & "'"
        query += " AND COD_CONT_LINEA<>'B'"
        contador = CInt(fn.DevolverDatoQuery(query))
        Return contador
    End Function


    Sub llenarTablaDetallesArticulo(ByVal tabla As SqlDataSource, ByVal proyecto As String, ByVal material As String, ByVal articulo As String)

        query = " SELECT DISTINCT "
        query += " B.ARTICULO,"

        query += " C.DESCRIPCION,"

        query += " ISNULL((SELECT TOP 1 UM_P FROM SOL_PEDIDOS.PEDIDOS.CONVERSION D"
        query += " WHERE D.PROYECTO=A.PROYECTO"
        query += " AND D.articulo=C.ARTICULO),ISNULL(C.UNIDAD_ALMACEN,A.UM_P)) UNIDAD_ALMACEN,"

        query += " CANT_DISP2_P CANT_DISP_P"

        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_RES A LEFT JOIN SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY B "
        query += " ON A.MATERIAL=B.FASE LEFT JOIN SOL_PEDIDOS.VITALICIA.ARTICULO C "
        query += " ON B.ARTICULO=C.ARTICULO"

        query += " WHERE A.PROYECTO='" & proyecto & "'"
        query += " AND MATERIAL='" & material & "'"
        query += " AND C.ARTICULO='" & articulo & "'"

        query += " UNION ALL"

        query += " SELECT DISTINCT"
        query += " '...',"

        query += " 'EQUIVALENCIA',"

        query += " ISNULL((SELECT TOP 1 UM_A FROM SOL_PEDIDOS.PEDIDOS.CONVERSION D"
        query += " WHERE D.PROYECTO=A.PROYECTO"
        query += " AND D.ARTICULO=C.ARTICULO),ISNULL(C.UNIDAD_ALMACEN,A.UM_A)) UNIDAD_ALMACEN,"

        query += " CANT_DISP2_A CANT_DISP_A"

        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_RES A LEFT JOIN SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY B "
        query += " ON A.MATERIAL=B.FASE LEFT JOIN SOL_PEDIDOS.VITALICIA.ARTICULO C "
        query += " ON B.ARTICULO=C.ARTICULO"
        query += " WHERE A.PROYECTO='" & proyecto & "'"
        query += " AND MATERIAL='" & material & "'"
        query += " AND C.ARTICULO='" & articulo & "'"



        tabla.ConnectionString = fn.ObtenerCadenaConexion("conn")
        tabla.SelectCommand = query

    End Sub










    Sub insertarLinea(ByVal proyecto As String, ByVal actividad As String, ByVal material As String,
                      ByVal articulo As String, ByVal cantidad As Double, ByVal cantidadEq As Double,
                      ByVal um As String, ByVal umEq As String, ByVal codProyecto As String, ByVal lblMensaje As Label, ByVal lblMensajeS As HtmlContainerControl, ByVal usuario As String)
        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA "
        query += " (PROYECTO,ACTIVIDAD,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,UND_MED_E,CANTIDAD_APS,RecordDate, UpdatedBy)"
        query += " VALUES "
        query += " ('" & proyecto & "','" & fn.limpiarComillas(actividad) & "','" & fn.limpiarComillas(material) & "','" & fn.limpiarComillas(articulo) & "',"
        query += " '" & cantidadEq & "','" & codProyecto & "', 'C','" & um & "','" & umEq & "','" & cantidad & "', GETDATE(),'" & usuario & "')"

        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
    End Sub

    Sub actualizarLinea(ByVal proyecto As String, ByVal actividad As String, ByVal material As String,
                        ByVal articulo As String, ByVal cantidad As Double, ByVal cantidadEq As Double,
                        ByVal um As String, ByVal umEq As String, ByVal codProyecto As String, ByVal id As String,
                        ByVal lblMensaje As Label, ByVal lblMensajeS As HtmlContainerControl, ByVal usuario As String)
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET PROYECTO='" & proyecto & "', ACTIVIDAD='" & fn.limpiarComillas(actividad) & "', "
        query += " FASE='" & fn.limpiarComillas(material) & "',ARTICULO='" & fn.limpiarComillas(articulo) & "', CANTIDAD='" & cantidadEq & "',CODIGO_SOLICITUD='" & codProyecto & "',"
        query += " CODIGO_CONTROL='C',UND_MED='" & um & "',UND_MED_E='" & umEq & "',CANTIDAD_APS='" & cantidad & "', UpdatedBy='" & usuario & "', RecordDate=GETDATE()  WHERE ID='" & id & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
    End Sub


    'antes llamada validarIngreso: valida que no se ingresen dos articulos iguales al crear una solicitud o modificarla
    Function validarNuevaLinea(ByVal codProyecto As String, ByVal material As String, ByVal articulo As String) As Boolean 'si devuelve TRUE significa que existen registros con los mismos valores
        Dim contador As Integer
        query = " SELECT COUNT('A') FROM SOL_PEDIDOS.PEDIDOS.SOL_RES_DET"
        query += " WHERE COD_SOL='" & codProyecto & "'"
        query += " AND MATERIAL ='" & material & "'"
        query += " AND ARTICULO='" & articulo & "'"
        query += " AND COD_CONT_LINEA<>'B'"
        contador = CInt(fn.DevolverDatoQuery(query))
        If contador > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub validarReimpresionRendicion(ByVal codigoProyecto As String)
        Dim cont As Integer
        query = "SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA WHERE CODIGO_SOLICITUD='" & codigoProyecto & "'"
        cont = CInt(fn.DevolverDatoQuery(query))
        If cont > 0 Then
            HttpContext.Current.Response.Redirect("Principal.aspx")
        End If
    End Sub
    'genera un archivo excel para una nueva solitud o una solicitud cursada
    Sub generarExcelSolicitudes(ByVal tabla As GridView, ByVal codProyecto As String, ByVal proyecto As String, ByVal solicitante As String, ByVal fecha As String, ByVal observaciones As String)

        Dim Wbook As New ExcelWorkbook()
        Dim plantilla, ext, filadatos, columnadatos, rutaPlantilla, Archivo As String

        plantilla = "Plantilla"
        ext = ".xls"
        filadatos = "14"
        columnadatos = "1"
        rutaPlantilla = "C:\Plantillas Control de Pedidos\"
        Wbook = ExcelWorkbook.ReadXLS(rutaPlantilla & plantilla & ext)
        Dim Wsheet As ExcelWorksheet = Wbook.Worksheets(0)

        filadatos = filadatos - 1
        Dim Extension, ArchivoFecha As String
        Dim NombrePlantilla As String = ""



        Archivo = "Solicitud " & codProyecto
        Extension = ".xls"
        ArchivoFecha = Archivo & "_" & Now.ToString("yyyyMMddhhmmss")
        ArchivoFecha += Extension
        Archivo += Extension



        Dim WorksheetName As String = Wbook.Worksheets(0).Name
        For i As Integer = 0 To tabla.Rows.Count - 1
            Dim j As Integer = 0
            j += 1
            Wsheet.Cells(i + filadatos, 0).Value = (i + 1)

            Wsheet.Cells(i + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(18).Text)
            Wsheet.Cells(i + filadatos, 2).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(3).Text)

            Wsheet.Cells(i + filadatos, 3).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(19).Text)
            Wsheet.Cells(i + filadatos, 4).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(4).Text)
            Wsheet.Cells(i + filadatos, 5).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(20).Text)
            Wsheet.Cells(i + filadatos, 6).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(5).Text)
            Wsheet.Cells(i + filadatos, 7).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(6).Text)
            Wsheet.Cells(i + filadatos, 8).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(8).Text))
            Wsheet.Cells(i + filadatos, 9).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(9).Text))
            Wsheet.Cells(i + filadatos, 10).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(32).Text))
            Wsheet.Cells(i + filadatos, 11).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(14).Text))
            Wsheet.Cells(i + filadatos, 12).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(11).Text))
            Wsheet.Cells(i + filadatos, 13).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(33).Text))
            Wsheet.Cells(i + filadatos, 14).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(7).Text))
            Wsheet.Cells(i + filadatos, 15).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(21).Text)
            If HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(16).Text) = "SI" Then
                If HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(15).Text) = "SI" Then
                    If CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(13).Text)) < 0 Then
                        Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.Red
                        Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.Red
                    Else
                        Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.White
                        Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.White
                    End If
                Else
                    If CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(9).Text)) > 0 Then
                        Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.LightGreen
                        Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.LightGreen
                    Else
                        Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.Orange
                        Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.Orange
                    End If
                End If
            Else
                If HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(15).Text) = "SI" Then
                    If CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(9).Text)) > 0 Then
                        Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.LightGreen
                        Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.LightGreen
                    Else
                        Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.Orange
                        Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.Orange
                    End If
                Else
                    Wsheet.Cells(i + filadatos, 13).Style.PatternForeColor = ColorPalette.Yellow
                    Wsheet.Cells(i + filadatos, 6).Style.PatternForeColor = ColorPalette.Yellow
                End If

            End If

            Wsheet.Cells("A" & (i + filadatos).ToString & ":O" & (i + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
        Next
        Wsheet.Cells("A" & (filadatos + tabla.Rows.Count).ToString & ":O" & (filadatos + tabla.Rows.Count).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

        Wsheet.Cells(2, 3).Value = DateTime.Now()
        Wsheet.Cells(2, 3).Style.StringFormat = "DD-MM-YYYY hh:mm:ss"
        Wsheet.Cells(3, 3).Value = proyecto
        Wsheet.Cells(4, 3).Value = solicitante
        Wsheet.Cells(5, 3).Value = fecha
        Wsheet.Cells(5, 3).Style.StringFormat = "DD-MM-YYYY"
        Wsheet.Cells(6, 3).Value = codProyecto

        Wsheet.Cells(10, 0).Value = observaciones
        Wsheet.Cells(tabla.Rows.Count + filadatos, 0).Value = "***"

        Wbook.WriteXLS(rutaPlantilla & "WriteXLS.xls")

        HttpContext.Current.Response.AppendHeader("Content-Type", "application/vnd.ms-excel")
        HttpContext.Current.Response.AppendHeader("Content-Disposition", [String].Format("attachment; filename={0}", ArchivoFecha))
        HttpContext.Current.Response.BinaryWrite(Wbook.WriteXLS().ToArray())
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Sub

#End Region
End Class
