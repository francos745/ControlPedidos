Imports System.Data.OleDb
Imports System.Data
Imports System.IO

Partial Class cargarExcel
    Inherits System.Web.UI.Page
    Private carpeta As String
    Dim usuario As String
    Private carpeta_final As String
    Dim query As String
    Dim fn As New Funciones
    Dim codigoSolicitud As String
    Function multiplicador(ByVal proyecto As String, ByVal articulo As String) As Double
        Dim multi As Double
        query = " SELECT top 1 cast(ISNULL (B.FACTOR,1) as decimal(28,8))"
        query += " FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B"
        query += " WHERE B.ARTICULO='" & articulo & "'  "
        query += " AND B.PROYECTO='" & proyecto & "'"
        query += " AND B.FACTOR IS NOT NULL"
        query += " ORDER BY B.ORDEN_CAMBIO"

        Try
            multi = CDbl(fn.DevolverDatoQuery(query))
        Catch ex As Exception
            multi = 1
        End Try
        If multi = 0 Then
            multi = 1
        End If


        Return multi
    End Function


    Sub LeerArchivoExcel(ByVal tabla As GridView, ByVal path As String, ByVal rango As String, ByVal hoja As String)
        If path <> "" Then

            Dim ExcelFile As String = path
            Dim ds As New DataSet
            Dim da As OleDbDataAdapter
            Dim dt As DataTable
            Dim conn As OleDbConnection

            conn = New OleDbConnection(
                                    "Provider=Microsoft.ACE.OLEDB.12.0;" &
                                    "data source=" & ExcelFile & "; " &
                                    "Extended Properties='Excel 12.0;HDR=NO; IMEX=1'")
            Try
                da = New OleDbDataAdapter("SELECT * FROM  [" & hoja & "$" & rango & "]", conn)
                conn.Open()
                da.Fill(ds, "MyData")
                dt = ds.Tables("MyData")
                tabla.DataSource = ds
                tabla.DataBind()
            Catch ex As Exception

                lblMensajeS.Attributes("Style") = ""
                lblMensajeS.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "Error: " + ex.Message
            Finally
                conn.Close()

            End Try

        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        carpeta = Path.Combine(Request.PhysicalApplicationPath, "upload")
        btnConfirmar.Attributes("Style") = "display:none;"
        usuario = Session("usuario")
    End Sub

    Protected Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        'VERIFICAR SI SE SELECCIONO UN ARCHIVO

        If fileUpload.PostedFile.FileName = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "No se a seleccionado ningún archivo. Intente de nuevo."

        Else
            'VERIFICAR LA EXTENSION
            Dim extension As String = Path.GetExtension(fileUpload.PostedFile.FileName)
            Select Case extension.ToLower()
                    'validas
                Case ".xlsx"
                    Exit Select
                Case Else

                    'no validas
                    lblMensajeS.Attributes("Style") = ""
                    lblMensajeS.Attributes("class") = "alert alert-danger"
                    lblMensaje.Text = "Extensión no válida, solo se admiten archivos con la extensión xls."
                    Return
            End Select

            'COPIAR EL ARCHIVO
            Try
                Dim archivo As String = Path.GetFileName(fileUpload.PostedFile.FileName)

                Dim aprob, rech, cant As Integer
                'NOMBRE DEL ARCHIVO
                If archivo.Substring(0, 9) <> "Solicitud" Then
                    lblMensajeS.Attributes("Style") = ""
                    lblMensajeS.Attributes("class") = "alert alert-danger"
                    lblMensaje.Text = "Nombre de archivo no válido"
                    Exit Sub
                End If

                carpeta_final = Path.Combine(carpeta, archivo)
                lblCarpetaFinal.Text = carpeta_final
                codigoSolicitud = archivo.Substring(10, 9)
                lblCodigoSolicitud.Text = codigoSolicitud
                query = " SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "' AND ESTADO = 'A'"

                aprob = CInt(fn.DevolverDatoQuery(query))

                query = " SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "' AND ESTADO = 'R'"

                rech = CInt(fn.DevolverDatoQuery(query))

                query = " SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "' AND ESTADO = 'P'"

                cant = CInt(fn.DevolverDatoQuery(query))

                If aprob > 0 Then
                    'fileUpload.PostedFile.SaveAs(carpeta_final)
                    lblMensajeS.Attributes("Style") = ""
                    lblMensajeS.Attributes("class") = "alert alert-warning"
                    lblMensaje.Text = "La solicitud " & codigoSolicitud & " se encuentra APROBADA, cambie el estado a PENDIENTE para poder sobreescribirla."
                    btnConfirmar.Attributes("Style") = "display:none;"
                    Exit Sub
                End If
                If rech > 0 Then
                    'fileUpload.PostedFile.SaveAs(carpeta_final)
                    lblMensajeS.Attributes("Style") = ""
                    lblMensajeS.Attributes("class") = "alert alert-warning"
                    lblMensaje.Text = "La solicitud " & codigoSolicitud & " se encuentra RECHAZADA, cambie el estado a PENDIENTE para poder sobreescribirla."
                    btnConfirmar.Attributes("Style") = "display:none;"
                    Exit Sub
                End If
                If cant > 0 Then
                    fileUpload.PostedFile.SaveAs(carpeta_final)
                    lblMensajeS.Attributes("Style") = ""
                    lblMensajeS.Attributes("class") = "alert alert-info"
                    lblMensaje.Text = "La solicitud " & codigoSolicitud & " se encuentra PENDIENTE, ¿Desea sobreescribir los datos?."
                    btnConfirmar.Attributes("Style") = ""
                    Exit Sub
                End If
                fileUpload.PostedFile.SaveAs(carpeta_final)
                Dim rango As String = "D3:D9"
                Dim hoja As String = "Hoja1"
                Dim obsDO, obsRes, obsSO, proyecto, solicitante As String
                Dim fechaSol, fechaE, fechaDO, fechaSO As Date
                Dim fechaDOAux, fechaSOAux As String
                Dim multi, div, div2 As Double
                LeerArchivoExcel(dtgCabecera, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView

                rango = "I3:I7"
                hoja = "Hoja1"
                LeerArchivoExcel(dtgObsSup, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView


                rango = "A13:P500"
                hoja = "Hoja1"
                LeerArchivoExcel(dtgExcel, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView

                Try

                    fechaSol = dtgCabecera.Rows(0).Cells(0).Text.Substring(0, 19)
                    fechaE = dtgCabecera.Rows(3).Cells(0).Text.Substring(0, 10)
                    Try
                        fechaSO = dtgCabecera.Rows(5).Cells(0).Text.Substring(0, 10)
                        fechaSO = fechaSO.ToString("yyyy/MM/dd")
                        fechaSOAux = fechaSO
                    Catch ex As Exception
                        fechaSOAux = ""
                    End Try

                    Try
                        fechaDO = dtgCabecera.Rows(6).Cells(0).Text.Substring(0, 10)
                        fechaDO = fechaDO.ToString("yyyy/MM/dd")
                        fechaDOAux = fechaDO
                    Catch ex As Exception
                        fechaDOAux = ""
                    End Try


                Catch ex As Exception
                    lblMensajeS.Attributes("Style") = ""
                    lblMensajeS.Attributes("class") = "alert alert-danger"
                    lblMensaje.Text = "No se cargó el archivo. Los formatos en las fecha son incorrectos." + ex.Message
                    Exit Sub
                End Try

                proyecto = dtgCabecera.Rows(1).Cells(0).Text.ToString
                solicitante = HttpUtility.HtmlDecode(dtgCabecera.Rows(2).Cells(0).Text.ToString)

                obsRes = fn.DevolverDatoQuery("SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "'")

                obsSO = HttpUtility.HtmlDecode(dtgObsSup.Rows(0).Cells(0).Text)

                obsDO = HttpUtility.HtmlDecode(dtgObsSup.Rows(4).Cells(0).Text)

                Dim cantFilas As Integer = 0
                While dtgExcel.Rows(cantFilas).Cells(0).Text <> "&nbsp;" And dtgExcel.Rows(cantFilas).Cells(0).Text <> "***"
                    cantFilas += 1
                    If cantFilas > 500 Then
                        lblMensajeS.Attributes("Style") = ""
                        lblMensajeS.Attributes("class") = "alert alert-danger"
                        lblMensaje.Text = "No se cargó el archivo. No se encontró el caracter delimitador."
                        Exit Sub
                    End If
                End While

                query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA "
                query += " (CODIGO_SOLICITUD,ESTADO,FECHA_SOLICITUD,FECHA_REQUERIDA,OBSERVACIONES,SOLICITANTE,FECHA_APROB_SO,FECHA_APROB_DO, RecordDate, UpdatedBy)"
                query += " VALUES "
                query += " ('" & codigoSolicitud & "','P','" & fechaSol.ToString("yyyy/MM/dd H:mm:ss") & "', '" & fechaE.ToString("yyyy/MM/dd") & "', '" & obsRes & "|" & obsSO & "|" & obsDO & "', '" & solicitante & "', '" & fechaSOAux & "' , '" & fechaDOAux & "', GETDATE(),'" & usuario & "')"
                fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

                Dim codAct(cantFilas) As String
                Dim codMat(cantFilas) As String
                Dim codArt(cantFilas) As String


                Dim cantSol(cantFilas) As String
                Dim um(cantFilas) As String
                Dim ume(cantFilas) As String
                Dim com As New comun

                For j As Int16 = 0 To cantFilas - 1
                    codAct(j) = dtgExcel.Rows(j).Cells(1).Text
                    codMat(j) = dtgExcel.Rows(j).Cells(3).Text
                    codArt(j) = dtgExcel.Rows(j).Cells(5).Text

                    cantSol(j) = dtgExcel.Rows(j).Cells(14).Text

                    um(j) = dtgExcel.Rows(j).Cells(7).Text
                    ume(j) = dtgExcel.Rows(j).Cells(15).Text
                Next

                For j As Int16 = 0 To cantFilas - 1

                    Dim A As Double
                    If cantSol(j) = "" Then
                        A = 0
                    Else
                        A = com.validarNumero(cantSol(j))
                    End If

                    multi = multiplicador(proyecto, codArt(j))
                    div = CDbl(A) / multi

                    div2 = Replace(div.ToString, ",", ".")

                    query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA "
                    query += " (PROYECTO,FASE,ARTICULO,CANTIDAD,ESTADO,OBSERVACIONES,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E, RecordDate, UpdatedBy )"
                    query += " VALUES "
                    query += " ('" & proyecto & "','" & HttpUtility.HtmlDecode(codMat(j)) & "','" & HttpUtility.HtmlDecode(codArt(j)) & "','" & div2 & "','P','NO','" & codigoSolicitud & "','C','" & um(j) & "','" & A & "','" & HttpUtility.HtmlDecode(codAct(j)) & "','" & HttpUtility.HtmlDecode(ume(j)) & "', GETDATE(),'" & usuario & "')"
                    'queryupdate)
                    fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                Next

                lblMensajeS.Attributes("Style") = ""
                lblMensajeS.Attributes("class") = "alert alert-success"
                lblMensaje.Text = "La solicitud " & codigoSolicitud & " fue cargada exitosamente."

            Catch ex As Exception
                lblMensajeS.Attributes("Style") = ""
                lblMensajeS.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "Error: " + ex.Message

            End Try
        End If
    End Sub





    Protected Sub btnSi_Click(sender As Object, e As EventArgs) Handles btnSi.Click
        Dim rango As String = "D3:D9"
        Dim hoja As String = "Hoja1"
        Dim obsDO, obsRes, obsSO, proyecto, solicitante As String
        Dim fechaSol, fechaE, fechaDO, fechaSO As Date
        Dim fechaSOAux, fechaDOAux As String
        Dim multi, div, div2 As Double
        codigoSolicitud = lblCodigoSolicitud.Text
        carpeta_final = lblCarpetaFinal.Text
        LeerArchivoExcel(dtgCabecera, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView

        rango = "I3:I7"
        hoja = "Hoja1"
        LeerArchivoExcel(dtgObsSup, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView


        rango = "A13:P500"
        hoja = "Hoja1"
        LeerArchivoExcel(dtgExcel, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView

        Try

            fechaSol = dtgCabecera.Rows(0).Cells(0).Text.Substring(0, 19)
            fechaE = dtgCabecera.Rows(3).Cells(0).Text.Substring(0, 10)
            Try
                fechaSO = dtgCabecera.Rows(5).Cells(0).Text.Substring(0, 10)
                fechaSO = fechaSO.ToString("yyyy/MM/dd")
                fechaSOAux = fechaSO
            Catch ex As Exception
                fechaSOAux = ""
            End Try

            Try
                fechaDO = dtgCabecera.Rows(6).Cells(0).Text.Substring(0, 10)
                fechaDO = fechaDO.ToString("yyyy/MM/dd")
                fechaDOAux = fechaDO
            Catch ex As Exception
                fechaDOAux = ""
            End Try

        Catch ex As Exception
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "No se cargó el archivo. Los formatos en las fecha son incorrectos. " + ex.Message
            Exit Sub
        End Try

        proyecto = dtgCabecera.Rows(1).Cells(0).Text.ToString
        solicitante = HttpUtility.HtmlDecode(dtgCabecera.Rows(2).Cells(0).Text.ToString)

        obsRes = fn.DevolverDatoQuery("SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "'")

        obsSO = HttpUtility.HtmlDecode(dtgObsSup.Rows(0).Cells(0).Text)

        obsDO = HttpUtility.HtmlDecode(dtgObsSup.Rows(4).Cells(0).Text)

        Dim cantFilas As Integer = 0

        While dtgExcel.Rows(cantFilas).Cells(0).Text <> "&nbsp;" And dtgExcel.Rows(cantFilas).Cells(0).Text <> "***"
            cantFilas += 1

            If cantFilas > 500 Then
                lblMensajeS.Attributes("Style") = ""
                lblMensajeS.Attributes("class") = "alert alert-danger"
                lblMensaje.Text = "No se cargó el archivo. No se encontró el caracter delimitador."
                Exit Sub
            End If
        End While

        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA"
        query += " SET FECHA_SOLICITUD='" & fechaSol.ToString("yyyy/MM/dd H:mm:ss") & "',"
        query += " FECHA_REQUERIDA='" & fechaE.ToString("yyyy/MM/dd") & "',"
        query += " Observaciones='" & obsRes & "|" & obsSO & "|" & obsDO & "',"
        query += " SOLICITANTE='" & solicitante & "',"
        query += " FECHA_APROB_SO='" & fechaSOAux & "',"
        query += " FECHA_APROB_DO='" & fechaDOAux & "', UpdatedBy='" & usuario & "', RecordDate=GETDATE() "

        query += " WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "'"

        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET CODIGO_CONTROL='B', "
        query += " ID_ACTA='0', CANT_ACTA='0',CANT_ACTA_APS='0', UpdatedBy='" & usuario & "', RecordDate=GETDATE() "
        query += " WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

        Dim codAct(cantFilas) As String
        Dim codMat(cantFilas) As String
        Dim codArt(cantFilas) As String
        Dim com As New comun

        Dim cantSol(cantFilas) As String
        Dim um(cantFilas) As String
        Dim ume(cantFilas) As String
        For j As Int16 = 0 To cantFilas - 1
            codAct(j) = dtgExcel.Rows(j).Cells(1).Text
            codMat(j) = dtgExcel.Rows(j).Cells(3).Text
            codArt(j) = dtgExcel.Rows(j).Cells(5).Text

            cantSol(j) = dtgExcel.Rows(j).Cells(14).Text

            um(j) = dtgExcel.Rows(j).Cells(7).Text
            ume(j) = dtgExcel.Rows(j).Cells(15).Text
        Next

        For j As Int16 = 0 To cantFilas - 1

            Dim A As Double
            If cantSol(j) = "" Then
                A = 0
            Else
                A = com.validarNumero(cantSol(j))
            End If

            multi = multiplicador(proyecto, codArt(j))
            div = CDbl(A) / multi

            div2 = Replace(div.ToString, ",", ".")

            query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA "
            query += " (PROYECTO,FASE,ARTICULO,CANTIDAD,ESTADO,OBSERVACIONES,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E, RecordDate, UpdatedBy)"
            query += " VALUES "
            query += " ('" & proyecto & "','" & HttpUtility.HtmlDecode(codMat(j)) & "','" & HttpUtility.HtmlDecode(codArt(j)) & "','" & div2 & "','P','NO','" & codigoSolicitud & "','C','" & um(j) & "','" & A & "','" & HttpUtility.HtmlDecode(codAct(j)) & "','" & HttpUtility.HtmlDecode(ume(j)) & "', GETDATE(),'" & usuario & "')"

            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        Next

        lblMensajeS.Attributes("Style") = ""
        lblMensajeS.Attributes("class") = "alert alert-success"
        lblMensaje.Text = "La solicitud " & codigoSolicitud & " fue cargada exitosamente."


    End Sub
    Protected Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        lblMensajeS.Attributes("Style") = "display:None;"
        lblMensajeS.Attributes("class") = "alert alert-success"
        lblMensaje.Text = "La solicitud " & codigoSolicitud & " fue cargada exitosamente."
    End Sub
End Class
