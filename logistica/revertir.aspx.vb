Imports System.Data
Imports System.Data.SqlClient
    Imports System.Drawing
    Imports System.IO
    Imports Docs.Excel
Partial Class logistica_revertir
    Inherits System.Web.UI.Page

    Dim fn As New Funciones
    Dim com As New comun
    Dim query As String
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
            btnRevertir.Attributes("class") = "btn btn-vitalicia btn-md enabled"

            mostrarMensaje3("", "exito")
        Else
            liberarSolicitud()
            btnRevertir.Attributes("class") = "btn btn-vitalicia btn-md disabled"

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



    Sub llenarObsSolFecha(ByVal codigo As String)
        Dim Generados As String = ""
        Dim observaciones As String = ""
        Dim fecha As String = ""
        Dim fechaReq As String = ""
        Dim bodega As String = ""
        Dim localizacion As String = ""
        Dim fechaIng As String = ""
        Dim fechaLog As String = ""
        Dim estado As String = ""

        query = " DECLARE @valores VARCHAR(1000)"
        query += " SELECT @valores= COALESCE(@valores + ', ', '') + CONSEC_TXT "
        query += " FROM (SELECT DISTINCT(CONVERT(VARCHAR(20), (CAST((CONSEC_TXT) AS varchar(20))))) CONSEC_TXT  "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA"
        query += " WHERE CODIGO_SOLICITUD='" & codigo & "' AND CODIGO_CONTROL<>'B')VISTA"
        query += " select  @valores as valores"
        Try
            Generados = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            Generados = ""
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

        query = "SELECT TOP 1 {fn CONCAT(CONVERT(nvarchar(30), FECHA_HORA_REG , 103), {fn CONCAT(' - ', CONVERT(nvarchar(30), FECHA_HORA_REG , 108))})} FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA WHERE CODIGO_SOLICITUD='" & codigo & "' ORDER BY FECHA_HORA_REG DESC"
        Try
            fechaLog = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaLog = ""
        End Try

        query = "SELECT BODEGA_D FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"

        Try
            bodega = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            bodega = ""
        End Try

        query = "SELECT LOCALIZACION_D FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            localizacion = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            localizacion = ""
        End Try

        query = "SELECT ESTADO FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            estado = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            estado = ""
        End Try
        If estado = "G" Then
            estado = "GENERADO"
            txtEstadoS.Attributes("class") = "form-group has-success has-feedbac"
        End If

        If estado = "GP" Then
            estado = "GENERADO PARCIALMENTE"
            txtEstadoS.Attributes("class") = "form-group has-warning has-feedbac"
        End If

        If estado = "R" Then
            estado = "RECHAZADO"
            txtEstadoS.Attributes("class") = "form-group has-error has-feedbac"
        End If




        txtObservaciones.InnerHtml = observaciones
        txtGenerados.Text = Generados
        txtFechaSol.Text = fecha
        txtBodDest.Text = bodega
        txtLocDest.Text = localizacion
        txtFechaReq.Text = fechaReq
        txtFechaIng.Text = fechaIng
        txtFechaLog.Text = fechaLog
        txtEstado.Text = estado
    End Sub




    Function validarAprobRechazados() As Boolean

        Dim cant As Integer = 0
        Dim filas As Integer = dtgDetalle.Rows.Count - 1
        For i As Integer = 0 To filas
            If dtgDetalle.Rows(i).Cells(17).Text = "R" Then
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
        query += " WHERE ESTADO NOT IN ('P','PO') ORDER BY CODIGO_SOLICITUD DESC"
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


        fn.llenarComboBoxOpciones2(cmbActividad2, query, "NUMERO", "FASE", "NOMBRE")

    End Sub












    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_LOG_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY CONSEC_TXT, LINEA"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET ESTADO=CASE WHEN ESTADO ='R' THEN 'P' ELSE 'R' END WHERE ID = @ID AND ID NOT IN (SELECT CODIGO_SOLICITUD FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA)"
        dtgDetalle.DataSourceID = "Detalles"


        query = "SELECT DISTINCT BODEGA_O FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA WHERE CODIGO_SOLICITUD=@CODIGO_PROYECTO AND CODIGO_CONTROL<>'B' AND ESTADO<>'R'"
        dsBodegas.ConnectionString = fn.ObtenerCadenaConexion("conn")
        dsBodegas.SelectCommand = query
        dtgBodegas.DataSourceID = "dsBodegas"

        validarConcurrencia()





    End Sub

    'renderizar tabla
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_LOG_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY CONSEC_TXT, LINEA"

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
    '#Region "ComboEnTabla"
    '    Protected Sub EditCustomer(sender As Object, e As GridViewEditEventArgs)
    '        dtgDetalle.EditIndex = e.NewEditIndex
    '        'dtgDetalle2.EditIndex = e.NewEditIndex
    '        llenarTablaDetallesPedido()
    '        'ocultarMostrarTablas()
    '    End Sub

    '    'Protected Sub RowDataBound(sender As Object, e As GridViewRowEventArgs)
    '    '    Dim actividad As String = e.Row.Cells(17).Text
    '    '    '-------------------------COMBO EN TABLAS----------------------------
    '    '    If e.Row.RowType = DataControlRowType.DataRow AndAlso dtgDetalle.EditIndex = e.Row.RowIndex Then
    '    '        'actividad)
    '    '        ' CANTIDAD_DISPONIBLE
    '    '        'Dim material As String = dtgDetalle.Rows(e.Row.RowIndex).Cells(8).Text
    '    '        'Dim cantidad As Double


    '    '        'Try
    '    '        '    cantidad = CDbl(dtgDetalle.Rows(e.Row.RowIndex).Cells(7).Text)
    '    '        'Catch ex As Exception
    '    '        '    cantidad = 0
    '    '        'End Try
    '    '        Dim cmbActas As DropDownList = DirectCast(e.Row.FindControl("cmbActas"), DropDownList)
    '    '        query = " SELECT {FN CONCAT(CONVERT(VARCHAR(20), (CAST(ROUND(CANTIDAD_APS,4) AS FLOAT))),{FN CONCAT(' ',{FN CONCAT(UND_MED,{FN CONCAT(' - ',(SELECT CODIGO_ACTA_ING "
    '    '        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
    '    '        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA))})})})}CANTIDAD, CODIGO_ACTA"
    '    '        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA A"
    '    '        ' query += " WHERE ACTIVIDAD='" & actividad & "'"
    '    '        'query += " AND FASE='" & material & "'"
    '    '        ' query += " AND CANTIDAD_APS=" & cantidad
    '    '        'query += " WHERE A.CODIGO_ACTA NOT IN ("
    '    '        'query += " SELECT CODIGO_ACTA"
    '    '        'query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
    '    '        'query += " WHERE B.ESTADO='B')"
    '    '        'actividad)
    '    '        Dim cmd As New SqlCommand(query)
    '    '        cmbActas.DataSource = GetData(cmd)
    '    '        cmbActas.DataTextField = "CANTIDAD"
    '    '        cmbActas.DataValueField = "CODIGO_ACTA"
    '    '        cmbActas.DataBind()
    '    '        'cmbActas.Items.FindByText(TryCast(e.Row.FindControl("lblActas"), Label).Text).Selected = True
    '    '    End If
    '    '    '-------------------------COMBO EN TABLAS----------------------------

    '    'End Sub

    '    Protected Sub UpdateCustomer(sender As Object, e As GridViewUpdateEventArgs)
    '        Dim acta As String = TryCast(dtgDetalle.Rows(e.RowIndex).FindControl("cmbActas"), DropDownList).SelectedItem.Value
    '        Dim id As String = dtgDetalle.DataKeys(e.RowIndex).Value.ToString()
    '        Dim actividad As String = dtgDetalle.Rows(e.RowIndex).Cells(18).Text
    '        Dim material As String = dtgDetalle.Rows(e.RowIndex).Cells(19).Text
    '        Dim strConnString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString

    '        Using con As New SqlConnection(strConnString)
    '            Dim query As String
    '            If acta = "0" Then
    '                query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA SET CODIGO_SOLICITUD='0'"
    '                query += " WHERE CODIGO_SOLICITUD=@ID"
    '            Else
    '                query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA SET CODIGO_SOLICITUD='0'"
    '                query += " WHERE CODIGO_SOLICITUD='" & id & "'"
    '                query += " UPDATE SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA SET CODIGO_SOLICITUD=@ID"
    '                query += " WHERE CODIGO_ACTA='" & acta & "'"
    '                query += " AND ACTIVIDAD='" & actividad & "'"
    '                query += " AND FASE='" & material & "'"
    '            End If
    '            'fn.ejecutarComandoSQL(query)

    '            Using cmd As New SqlCommand(query)
    '                cmd.Connection = con

    '                cmd.Parameters.AddWithValue("@ID", id)
    '                con.Open()


    '                cmd.ExecuteNonQuery()
    '                con.Close()

    '                Response.Redirect(Request.Url.AbsoluteUri)
    '            End Using
    '        End Using
    '    End Sub

    '    Protected Sub CancelEdit(sender As Object, e As GridViewCancelEditEventArgs)
    '        dtgDetalle.EditIndex = -1
    '        'dtgDetalle2.EditIndex = -1
    '        llenarTablaDetallesPedido()
    '        ocultarMostrarTablas()
    '    End Sub

    '    Private Function GetData(cmd As SqlCommand) As DataTable
    '        Dim strConnString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
    '        Using con As New SqlConnection(strConnString)
    '            Using sda As New SqlDataAdapter()
    '                cmd.Connection = con
    '                sda.SelectCommand = cmd
    '                Using dt As New DataTable()
    '                    sda.Fill(dt)
    '                    Return dt
    '                End Using
    '            End Using
    '        End Using
    '    End Function


    '#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("almacenero")


        'validarReimpresionRendicion()



        If Not Page.IsPostBack Then
            'txtFecha.Text = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy")

            llenarComboCodigoSolicitud()

            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue

            llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)


        End If
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)

        ''llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
        llenarTablaDetallesPedido()
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

            If estado = "R" Or estado = "B" Then
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

        mostrarMensaje("", "")
    End Sub




    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.ServerClick
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue

        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
        mostrarMensaje("", "error")
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        liberarSolicitud()
        Response.Redirect("principal.aspx")

    End Sub

    Protected Sub btnRevertirSi_Click(sender As Object, e As EventArgs) Handles btnRevertirSi.ServerClick
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA SET ESTADO='P', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA SET CONSEC_TXT='0', CODIGO_CONTROL='P', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE ESTADO='G' AND CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        liberarSolicitud()
        mostrarMensaje("Solicitud revertida exitosamente.", "exito")
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
    End Sub


    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(0).Visible = False
        e.Row.Cells(1).Visible = False
        e.Row.Cells(2).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(15).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(13).Visible = False
        'e.Row.Cells(18).Visible = False
        'e.Row.Cells(19).Visible = False
    End Sub




#Region "BOTONES NAVEGACION DERECHA"




    'Protected Sub btnCuadroCom_Click(sender As Object, e As EventArgs) Handles btnCuadroCom.Click
    '    generarExcel()
    'End Sub








    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        liberarSolicitud()
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub


#End Region

End Class
