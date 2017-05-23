
Imports System.Data
Imports System.Data.SqlClient
Imports Docs.Excel

Partial Class solicitudesRechazadas
    Inherits System.Web.UI.Page
    Dim fn As New Funciones
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
        If Session("proyecto") = "" Then
            Response.Redirect("resIngreso.aspx")
        End If
    End Sub



    Sub llenarComboCodigoSolicitud()
        query = " SELECT CODIGO_SOLICITUD, CONVERT (char(10), FECHA, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA"
        query += " WHERE CODIGO_SOLICITUD LIKE '" & Session("codigo") & "%'"
        query += " AND ESTADO = 'R' ORDER BY CODIGO_SOLICITUD DESC"
        fn.llenarComboBoxOpciones(cmbCodigoProyecto, query, "CODIGO_SOLICITUD", "CODIGO_SOLICITUD", "FECHA")

    End Sub





    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_RES_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B'ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query

        dtgDetalle.DataSourceID = "Detalles"



    End Sub

    'renderizar tabla
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_RES_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B'ORDER BY ID"

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



    Sub llenarObsSolFecha(ByVal codigo As String)
        Dim solicitante, observaciones, fecha As String
        query = "SELECT SOLICITANTE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        solicitante = fn.DevolverDatoQuery(query)
        query = "SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        observaciones = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fecha = fn.DevolverDatoQuery(query)

        txtObservacionesAux.Text = observaciones
        txtSolicitanteAux.Text = solicitante
        txtFechaAux.Text = fecha

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("proyecto")
        lblProyecto.Text = Session("proyecto")

        'validarReimpresionRendicion()


        If Not Page.IsPostBack Then
            txtFecha.Text = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")

            llenarComboCodigoSolicitud()
            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
            'llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
            'llenarObsSolFecha(lblCodigoProyecto.Text)
        End If
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
        llenarTablaDetallesPedido()
    End Sub






    Protected Sub cmbCodigoProyecto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbCodigoProyecto.SelectedIndexChanged
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        llenarObsSolFecha(lblCodigoProyecto.Text)

    End Sub


    Protected Sub btnGenerarAceptar_Click(sender As Object, e As System.EventArgs) Handles btnGenerarAceptar.ServerClick

        Dim fechaEntrega As Date = CDate(txtFecha.Text)
        Dim fechaActual As Date = DateTime.Now()
        If txtObservaciones.InnerText = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo observaciones."
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "Display:none;"
            lblMensajeS.Attributes("class") = ""
            lblMensaje.Text = ""
        End If
        If txtSolicitante.Text = "" Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Debe llenar el campo de Solicitante."
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "Display:none;"
            lblMensajeS.Attributes("class") = ""
            lblMensaje.Text = ""
        End If

        If (DateDiff(DateInterval.Day, fechaActual, fechaEntrega) < 2) Then
            lblMensajeS.Attributes("Style") = ""
            lblMensajeS.Attributes("class") = "alert alert-danger"
            lblMensaje.Text = "Ingrese una fecha mayor por mas de dos dias"
            Exit Sub
        Else
            lblMensajeS.Attributes("Style") = "Display:none;"
            lblMensajeS.Attributes("class") = ""
            lblMensaje.Text = ""
        End If




        Response.Redirect("Principal.aspx")

    End Sub



    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("Principal.aspx")




    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("resIngreso.aspx")
    End Sub

    Protected Sub grdSolicitudDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound
        Dim desc As String = e.Row.Cells(12).Text ' CANTIDAD_DISPONIBLE
        Dim presup As String = e.Row.Cells(15).Text ' PRESUPUESTADO
        Dim presupAct As String = e.Row.Cells(14).Text 'PRESUPUESTADO_ACT
        Dim actas As String = e.Row.Cells(9).Text ' CANTIDAD_ACTAS

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
    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"

    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"

    End Sub

#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(0).Visible = False
        e.Row.Cells(1).Visible = False
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
