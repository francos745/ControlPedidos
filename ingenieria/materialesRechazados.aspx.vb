
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Docs.Excel
Partial Class ingenieria_materialesRechazados
    Inherits System.Web.UI.Page

    Dim fn As New Funciones
    Dim com As New comun
    Dim query As String

    Sub validarInicioSesion()
        If Session("usuario") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
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
        Page.ClientScript.RegisterStartupScript(Page.ClientScript.[GetType](), "onLoad", "MostrarLabel();", True)
    End Sub



    Sub llenarObsSolFecha(ByVal codigo As String)
        Dim solicitante, observaciones, obsRech, fecha, fechaDo, fechaSo As String
        query = "SELECT SOLICITANTE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        solicitante = fn.DevolverDatoQuery(query)
        query = "SELECT OBS_RECHAZADOS FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        obsRech = fn.DevolverDatoQuery(query)
        query = "SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        observaciones = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA_SOLICITUD, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fecha = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA_APROB_SO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fechaSo = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA_APROB_DO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fechaDo = fn.DevolverDatoQuery(query)

        txtObservaciones.InnerHtml = observaciones
        txtObsRechazados.InnerHtml = obsRech
        txtSolicitante.Text = solicitante
        txtFechaSol.Text = fecha
        txtFechaSO.Text = fechaSo
        txtFechaDO.Text = fechaDo
    End Sub



    Sub mostrarObservacionesRechazadas()

        Dim cant As Integer = 0
        For i As Integer = 0 To dtgDetalle.Rows.Count - 1
            If dtgDetalle.Rows(i).Cells(16).Text = "R" Then
                cant += 1
                Exit For
            End If
        Next
        If cant = 0 Then
            contObsRechazadas.Attributes("Style") = ""
        Else
            contObsRechazadas.Attributes("Style") = ""
        End If


    End Sub

    Function validarAprobRechazados() As Boolean

        Dim cant As Integer = 0
        Dim filas As Integer = dtgDetalle.Rows.Count - 1
        For i As Integer = 0 To filas
            If dtgDetalle.Rows(i).Cells(16).Text = "R" Then
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
        query = " SELECT CODIGO_SOLICITUD  FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA"
        'query += " WHERE CODIGO_SOLICITUD LIKE '" & Session("codigo") & "%'"
        query += " WHERE ESTADO = 'A' AND OBS_RECHAZADOS<>'S/O' ORDER BY CODIGO_SOLICITUD DESC"
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





    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' AND ESTADO_LIN='R' ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query


        dtgDetalle.DataSourceID = "Detalles"





    End Sub

    'renderizar tabla
    Protected Sub dtgDetalle_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' AND ESTADO_LIN='R' ORDER BY ID"

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
#Region "ComboEnTabla"
    Protected Sub EditCustomer(sender As Object, e As GridViewEditEventArgs)
        dtgDetalle.EditIndex = e.NewEditIndex
        'dtgDetalle2.EditIndex = e.NewEditIndex
        llenarTablaDetallesPedido()
        'ocultarMostrarTablas()
    End Sub

    'Protected Sub RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    Dim actividad As String = e.Row.Cells(17).Text
    '    '-------------------------COMBO EN TABLAS----------------------------
    '    If e.Row.RowType = DataControlRowType.DataRow AndAlso dtgDetalle.EditIndex = e.Row.RowIndex Then
    '       
    '        ' CANTIDAD_DISPONIBLE
    '        'Dim material As String = dtgDetalle.Rows(e.Row.RowIndex).Cells(8).Text
    '        'Dim cantidad As Double


    '        'Try
    '        '    cantidad = CDbl(dtgDetalle.Rows(e.Row.RowIndex).Cells(7).Text)
    '        'Catch ex As Exception
    '        '    cantidad = 0
    '        'End Try
    '        Dim cmbActas As DropDownList = DirectCast(e.Row.FindControl("cmbActas"), DropDownList)
    '        query = " SELECT {FN CONCAT(CONVERT(VARCHAR(20), (CAST(ROUND(CANTIDAD_APS,4) AS FLOAT))),{FN CONCAT(' ',{FN CONCAT(UND_MED,{FN CONCAT(' - ',(SELECT CODIGO_ACTA_ING "
    '        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
    '        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA))})})})}CANTIDAD, CODIGO_ACTA"
    '        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA A"
    '        ' query += " WHERE ACTIVIDAD='" & actividad & "'"
    '        'query += " AND FASE='" & material & "'"
    '        ' query += " AND CANTIDAD_APS=" & cantidad
    '        'query += " WHERE A.CODIGO_ACTA NOT IN ("
    '        'query += " SELECT CODIGO_ACTA"
    '        'query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
    '        'query += " WHERE B.ESTADO='B')"
    '        '
    '        Dim cmd As New SqlCommand(query)
    '        cmbActas.DataSource = GetData(cmd)
    '        cmbActas.DataTextField = "CANTIDAD"
    '        cmbActas.DataValueField = "CODIGO_ACTA"
    '        cmbActas.DataBind()
    '        'cmbActas.Items.FindByText(TryCast(e.Row.FindControl("lblActas"), Label).Text).Selected = True
    '    End If
    '    '-------------------------COMBO EN TABLAS----------------------------

    'End Sub



    Private Function GetData(cmd As SqlCommand) As DataTable
        Dim strConnString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(strConnString)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function


#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("usuario")


        'validarReimpresionRendicion()



        If Not Page.IsPostBack Then
            'txtFecha.Text = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")

            llenarComboCodigoSolicitud()
            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue

            'llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
            llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
            'llenarObsSolFecha(lblCodigoProyecto.Text)
        End If
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        'llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
        llenarTablaDetallesPedido()
    End Sub

    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)

    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
    End Sub



    Protected Sub cmbCodigoProyecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCodigoProyecto.SelectedIndexChanged
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)

    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("principal.aspx")
    End Sub

    Protected Sub btnExcel_click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Dim sUrl As String = "cargarExcel.aspx"
        com.abrirNuevaVentana(sUrl)

    End Sub
    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub

    Protected Sub btnSinc_Click(sender As Object, e As EventArgs) Handles btnSinc.Click
        query = " EXEC PEDIDOS.SINCRONIZAR"
        Try
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            mostrarMensaje("Sincronización realizada satisfactoriamente", "exito")
        Catch ex As Exception
            mostrarMensaje("Hubo un problema en la sincronización. Inténtelo más tarde.", "error")
        End Try


    End Sub

#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(0).Visible = False
        e.Row.Cells(1).Visible = True
        e.Row.Cells(2).Visible = False
        e.Row.Cells(3).Visible = True
        e.Row.Cells(4).Visible = True
        e.Row.Cells(5).Visible = False

        e.Row.Cells(8).Visible = False
        e.Row.Cells(9).Visible = False
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
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

        e.Row.Cells(25).Visible = False
        e.Row.Cells(25).Visible = False
        e.Row.Cells(26).Visible = False
        e.Row.Cells(27).Visible = False
        e.Row.Cells(28).Visible = False
        e.Row.Cells(29).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(31).Visible = False
        e.Row.Cells(32).Visible = False
        e.Row.Cells(33).Visible = False
        e.Row.Cells(34).Visible = False
        e.Row.Cells(17).Visible = False

        If lblUMActual.Text = "Presupuesto" Then
            e.Row.Cells(23).Visible = False
            e.Row.Cells(24).Visible = False
        Else
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False

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
        Response.Redirect("nuevaActa.aspx")
    End Sub
    Protected Sub btnModActas_Click(sender As Object, e As EventArgs) Handles btnModActas.Click
        Response.Redirect("modificarActa.aspx")
    End Sub

#End Region
End Class
