Imports System.IO
Imports Microsoft.VisualBasic

Public Class logistica
    Private query As String
    Dim fn As New Funciones
    Function generarTXT(ByVal tabla As GridView, ByVal codSol As String, ByVal bodegaOrigen As String, ByVal bodegaDestino As String, ByVal estado As String, ByVal lblMensaje As Label, ByVal lblMensajeS As HtmlContainerControl, ByVal usuario As String) As String

        Dim rutaArchivo As String
        Dim archivo As String
        Dim consec As Integer
        Dim consecutivo As String
        Dim accion As String
        Dim codigoVitalicia As String
        Dim correlativo As String
        Dim fechaSolicitud As String
        Dim fechaRequerida As String
        Dim cabecera As String
        Dim cantString As String
        Dim cant As Double

        Dim cont As Integer
        Dim linea As String

        Dim archivosTXT As String = ""
        '----------------------------------------------------GENERACIÓN DE LA CABECERA----------------------------------------------------------------------------
        'obtenermos la bodega de origen

        'obtenemos la ruta donde se almacenará el archivo de texgo generado, ya se validó la ruta anteriormente
        rutaArchivo = fn.DevolverDatoQuery("SELECT U_DESCRIP FROM SOL_PEDIDOS.VITALICIA.U_BODEGA_RUTA where U_ARCHIVO='PEDI' AND U_CODIGO='" & bodegaOrigen & "'")
        'obtenemos el numero de consecutivo
        consec = CInt(fn.DevolverDatoQuery("SELECT U_ULTIMOVALOR FROM SOL_PEDIDOS.VITALICIA.U_CLEV_CONSECUTIVO WHERE U_CODIGO='TXT'")) + 1
        'llenamos el numero de consecutivo con "0"
        consecutivo = CStr(consec)
        While Len(consecutivo) < 5
            consecutivo = "0" & consecutivo
        End While
        'definimos el codigo de la empresa
        codigoVitalicia = "203"
        'definimos el correlativo para el archivo txt Ejemplo:TMB1-0001-00187
        correlativo = codSol & "-" & consecutivo
        'obtenemos la fecha de solicitud y fecha requerida con formato aaaammdd concatenada con "000000"
        fechaSolicitud = fn.DevolverDatoQuery("SELECT {fn CONCAT(CONVERT(nvarchar(30), FECHA_SOLICITUD , 112), '000000')} FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codSol & "'")
        fechaRequerida = fn.DevolverDatoQuery("SELECT {fn CONCAT(CONVERT(nvarchar(30), FECHA_REQUERIDA , 112), '000000')} FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codSol & "'")
        'obtenemos la bodega de destino

        'definimos la acción(create para todos los casos)
        accion = "CREATE"
        'concatenamos los datos de la cabecera
        cabecera = consecutivo + "|" + bodegaOrigen + "|" & codigoVitalicia & "|PE" + correlativo + "|STO|" + fechaSolicitud + "|" + fechaRequerida + "||" + bodegaDestino + "||||||" + accion + "|||||||||||||||||||||||1||||||||||||||||||||||||||||||||||||||||||||||||||||||"
        'definimos el nombre del archivo txt.
        archivo = rutaArchivo + "ORH" + correlativo + ".txt"
        Try
            Using flujoArchivo As New FileStream(archivo, FileMode.Create, FileAccess.Write, FileShare.None)
                Using escritor As New StreamWriter(flujoArchivo)
                    escritor.WriteLine(cabecera)
                End Using
            End Using
        Catch ex As Exception
            archivosTXT = "Bodega " & bodegaOrigen & " tuvo un error."
            Return archivosTXT
            Exit Function
        End Try

        '----------------------------------------------------GENERACIÓN DE LAS LÍNEAS----------------------------------------------------------------------------

        cont = 1
        archivo = rutaArchivo + "ORD" + correlativo + ".txt"
        Try
            Using flujoArchivo As New FileStream(archivo, FileMode.Create, FileAccess.Write, FileShare.None)


                Using escritor As New StreamWriter(flujoArchivo)

                    For Each row As GridViewRow In tabla.Rows
                        If row.Cells(17).Text.ToString() = bodegaOrigen And row.Cells(14).Text.ToString() <> estado Then
                            cant = CDbl(row.Cells(10).Text.ToString())
                            cantString = cant.ToString.Replace(",", ".")
                            linea = consecutivo + "|" + bodegaOrigen + "|" & codigoVitalicia & "|PE" + correlativo + "|" + CStr(cont) + "||" + row.Cells(7).Text.ToString() + "|||||||0|0|0|" + cantString + "||" + accion + "|||||0|0||||||||||||||||||||||||||||||||||||||||||||||||||||"
                            escritor.WriteLine(linea)
                            'ACTUALIZAMOS EL ESTADO DE LAS LINEAS
                            query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET CONSEC_TXT='PE" + correlativo & "', ESTADO='G', LINEA='" & cont.ToString & "' , UpdatedBy='" & usuario & "', RecordDate=GETDATE() WHERE ID='" & row.Cells(2).Text.ToString() & "'"
                            Try
                                fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                            Catch ex As Exception
                                '"ERRROR")
                            End Try

                            cont += 1
                        End If


                    Next

                End Using
            End Using
        Catch ex As Exception
            archivosTXT = "Bodega " & bodegaOrigen & " tuvo un error."
            Return archivosTXT
            Exit Function
        End Try

        '---------------------------------------------------------------------ACTUALIZAMOS EL CORRELATIVO------------------------------------------------------------------------
        query = "UPDATE SOL_PEDIDOS.VITALICIA.U_CLEV_CONSECUTIVO SET U_ULTIMOVALOR='" & consec.ToString & "', UpdatedBy='" & usuario & "', RecordDate=GETDATE() WHERE U_CODIGO='TXT'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        archivosTXT = "PE" + correlativo
        Return archivosTXT
    End Function

    'verifica si es que las bodegas de la tabla de logistica tienen algun valor no definido, en caso de no tener ninguno devuelve false
    Function validarBodegasND(ByVal tabla As GridView) As Boolean
        Dim res As Boolean = False
        For Each row As GridViewRow In tabla.Rows
            If row.Cells(0).Text.ToString() = "ND" Then
                res = True
                Exit For
            End If
        Next
        Return res
    End Function

    'verifica que no todaslas lineas de una solicitud en la tabla de logistica esten rechazadas o pospuestas 
    'según sea el caso, si devuelve True todas las lineas estan rechazadas o pospuestas
    Function validarEstadoLineas(ByVal codSol As String, ByVal estado As String) As Integer

        Dim contador As Integer
        query = " SELECT COUNT('A') FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA"
        query += " WHERE CODIGO_SOLICITUD='" & codSol & "'"
        query += " AND ESTADO NOT IN (" & estado & ")"
        query += " AND CODIGO_CONTROL <>'B'"

        contador = fn.DevolverDatoQuery(query)

        Return contador
    End Function
    'verificamos que existan todas las rutas para las bodegas en caso de devolver una bodega, una ruta no existe.   
    Function validarRutaBodegas(ByVal tabla As GridView) As String
        Dim bodega As String = ""
        Dim rutaArchivo As String
        For Each row As GridViewRow In tabla.Rows
            If row.Cells(0).Text.ToString() <> "ND" Then
                rutaArchivo = fn.DevolverDatoQuery("SELECT U_DESCRIP FROM SOL_PEDIDOS.VITALICIA.U_BODEGA_RUTA where U_ARCHIVO='PEDI' AND U_CODIGO='" & row.Cells(0).Text.ToString() & "'")
                If rutaArchivo = "" Then 'si no existe la ruta, cancelamos la operacion
                    bodega = row.Cells(0).Text.ToString()
                    Exit For
                Else
                    If Directory.Exists(rutaArchivo) = False Then 'si no existe la ruta, cancelamos la operacion
                        bodega = row.Cells(0).Text.ToString()
                        Exit For
                    End If
                End If
            End If
        Next
        Return bodega
    End Function

    Sub actualizarEstadoCabecera(ByVal codSol As String, ByVal estado As String _
, ByVal lblMensaje As Label, ByVal lblMensajeS As HtmlContainerControl, ByVal usuario As String)
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA SET ESTADO='" & estado & "' , UpdatedBy='" & usuario & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & codSol & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
    End Sub
End Class
