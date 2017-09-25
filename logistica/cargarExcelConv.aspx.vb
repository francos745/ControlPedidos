

Imports System.Data.OleDb
    Imports System.Data
    Imports System.IO

Partial Class logistica_cargarExcelConv
    Inherits System.Web.UI.Page
    Private carpeta As String
    Private carpeta_final As String
    Dim query As String
    Dim fn As New Funciones
    Dim codigoSolicitud As String
    Dim com As New comun


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
                                    "Extended Properties='Excel 12.0 Xml;HDR=Yes'")
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
        If Session("almacenero") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
        lblUsuario.Text = Session("almacenero")
        If Session("modulo") <> "P" Then
            Response.Redirect("Ingreso.aspx")
        End If

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
                Case ".xls"
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



                carpeta_final = Path.Combine(carpeta, archivo)
                lblCarpetaFinal.Text = carpeta_final

                fileUpload.PostedFile.SaveAs(carpeta_final)
                Dim rango As String
                Dim hoja As String

                rango = "A1:F2500"
                hoja = "Hoja1"
                LeerArchivoExcel(dtgExcel, carpeta_final, rango, hoja) ' Invocamos a la funcion LeerArchivoExcel, la cual devolverá un Dataset y sera el origen de los datos para el GridView
                ' MsgBox("1")

                Dim cantFilas As Integer = 0
                'While dtgExcel.Rows(cantFilas).Cells(0).Text <> "***"
                '    cantFilas += 1
                '    If cantFilas > 990 Then
                '        lblMensajeS.Attributes("Style") = ""
                '        lblMensajeS.Attributes("class") = "alert alert-danger"
                '        lblMensaje.Text = "No se cargó el archivo. No se encontró el caracter delimitador."
                '        Exit Sub
                '    End If
                'End While


                While dtgExcel.Rows(cantFilas).Cells(0).Text <> "***"
                    cantFilas += 1
                    If cantFilas > 2500 Then
                        lblMensajeS.Attributes("Style") = ""
                        lblMensajeS.Attributes("class") = "alert alert-danger"
                        lblMensaje.Text = "No se cargó el archivo. No se encontró el caracter delimitador."
                        Exit Sub
                    End If
                End While

                Dim proyecto(cantFilas) As String
                Dim articulo(cantFilas) As String
                Dim umP(cantFilas) As String
                Dim factor(cantFilas) As String


                For j As Int16 = 0 To cantFilas - 1
                    proyecto(j) = dtgExcel.Rows(j).Cells(0).Text
                    articulo(j) = dtgExcel.Rows(j).Cells(2).Text
                    umP(j) = dtgExcel.Rows(j).Cells(5).Text

                    factor(j) = dtgExcel.Rows(j).Cells(3).Text

                Next

                For j As Int16 = 0 To cantFilas - 1



                    query = " UPDATE SOL_PEDIDOS.PEDIDOS.CONVERSION"
                    query += " SET FACTOR='" & com.validarNumero(factor(j)) & "',"
                    query += " UM_P='" & umP(j) & "', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()"

                    query += " WHERE PROYECTO='" & proyecto(j) & "'"
                    query += " AND ARTICULO='" & articulo(j) & "'"

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



End Class
