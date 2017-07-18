
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Docs.Excel
Partial Class ingenieria_solicitudesAprobadas
    Inherits System.Web.UI.Page

    Dim fn As New Funciones
    Dim com As New comun
    Dim ing As New ingenieria
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
    Dim ruta As String
    Dim codRep As String
    Dim a As String
    Dim plantilla As String
    Dim rutaPlantilla As String
    Dim ext As String

    Sub validarInicioSesion()
        If Session("usuario") = "" Then
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
            btnMoverACursadas.Attributes("class") = "btn btn-vitalicia btn-md enabled"
            mostrarMensaje3("", "exito")
        Else
            liberarSolicitud()
            btnMoverACursadas.Attributes("class") = "btn btn-vitalicia btn-md disabled"
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

    End Sub

    Sub llenarObsSolFecha(ByVal codigo As String)
        Dim solicitante, observaciones, fecha, fechaDo, fechaSo As String
        query = "SELECT SOLICITANTE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        solicitante = fn.DevolverDatoQuery(query)
        query = "SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        observaciones = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA_SOLICITUD, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fecha = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA_APROB_SO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fechaSo = fn.DevolverDatoQuery(query)
        query = "SELECT Convert(Char(10), FECHA_APROB_DO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        fechaDo = fn.DevolverDatoQuery(query)

        txtObservaciones.InnerHtml = observaciones
        txtSolicitante.Text = solicitante
        txtFechaSol.Text = fecha
        txtFechaSO.Text = fechaSo
        txtFechaDO.Text = fechaDo
    End Sub

    Sub llenarComboCodigoSolicitud()
        query = " SELECT CODIGO_SOLICITUD  FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA"
        'query += " WHERE CODIGO_SOLICITUD LIKE '" & Session("codigo") & "%'"
        query += " WHERE ESTADO = 'A' ORDER BY FECHA_APROB_ING DESC"
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

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' AND ESTADO_LIN<>'R' ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ESTADO=CASE WHEN ESTADO ='R' THEN 'P' ELSE 'R' END WHERE ID = @ID"


        dtgDetalle.DataSourceID = "Detalles"



        query = " SELECT '1' ID, NOM_ACTIVIDAD,NOM_MATERIAL,UM_P,SUM(CANT_SOL_P)CANT_SOL_P,PRESUPUESTADO"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET"
        query += " WHERE COD_SOL=@CODIGO_PROYECTO"
        query += " AND COD_CONT_LINEA<>'B'"
        query += " AND ESTADO_LIN<>'R'"
        query += " GROUP BY NOM_ACTIVIDAD,NOM_MATERIAL,UM_P,PRESUPUESTADO"

        detalleCuadroCom1.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleCuadroCom1.SelectCommand = query
        dtgCuadroCom1.DataSourceID = "detalleCuadroCom1"

        query = " SELECT DISTINCT NOM_MATERIAL,UM_P,CANT_PRESUP_ACUM_P, CANT_SOL_APROB_ACUM_P-CANT_ACTAS_APROB_ACUM_P CANT_SOL_APROB_ACUM_P,CANT_ACTAS_APROB_ACUM_P CANT_APROB_ACTAS_ACUM_P,(CANT_PRESUP_ACUM_P-CANT_SOL_APROB_ACUM_P+CANT_ACTAS_APROB_ACUM_P)CANT_DISP_P"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET"
        query += " WHERE COD_SOL=@CODIGO_PROYECTO"
        query += " AND COD_CONT_LINEA<>'B'"
        query += " AND ESTADO_LIN<>'R'"
        query += " AND PRESUPUESTADO='SI'"
        query += " GROUP BY NOM_MATERIAL,UM_P,CANT_PRESUP_ACUM_P,CANT_SOL_APROB_ACUM_P,CANT_ACTAS_APROB_ACUM_P"


        detalleCuadroCom2.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleCuadroCom2.SelectCommand = query
        dtgCuadroCom2.DataSourceID = "detalleCuadroCom2"




        If dtgDetalle.Rows.Count = 0 Then
            btnMoverACursadas.Attributes("class") = "btn btn-vitalicia btn-md disabled"
        Else
            btnMoverACursadas.Attributes("class") = "btn btn-vitalicia btn-md enabled"
        End If

        validarConcurrencia()

    End Sub

    'renderizar tabla
    Protected Sub dtgDetalle_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' AND ESTADO_LIN<>'R' ORDER BY ID"

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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("usuario")

        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 5) + ";Ingreso.aspx")
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
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue.ToString)
        llenarTablaDetallesPedido()
    End Sub
    Protected Sub btnExcel_click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Dim sUrl As String = "cargarExcel.aspx"
        com.abrirNuevaVentana(sUrl)

    End Sub
    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"


    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"


    End Sub

    Protected Sub cmbCodigoProyecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCodigoProyecto.SelectedIndexChanged
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)
        mostrarMensaje("", "exito")
    End Sub



    Function comprobarAprobados() As Boolean
        Dim a As Boolean
        Dim cont As Integer

        query = " SELECT COUNT('A') FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO NOT IN ('P','R')"
        cont = CInt(fn.DevolverDatoQuery(query))

        If cont = 0 Then
            a = True
        Else
            a = False
        End If

        Return a
    End Function

    Protected Sub btnAprobarSi_Click(sender As Object, e As EventArgs) Handles btnAprobarSi.ServerClick

        If comprobarAprobados() Then

            'Actualizamos la cabecera y las observaciones a aprobados
            query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA SET ESTADO='P', FECHA_APROB_ING= '17531212', USUARIO_APROB= '" + lblUsuario.Text + "'"
            query += ", OBS_RECHAZADOS='s/o', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            'Actualizamos las lineas que sean diferentes de rechazadas a aprobadas
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ESTADO='P', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO<>'R' "
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

            query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA SET ESTADO='P', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

            query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET ESTADO='P', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO<>'R' "
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

            query = " DELETE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            query = " DELETE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
            liberarSolicitud()
            mostrarMensaje("Solicitud " & cmbCodigoProyecto.SelectedValue & " movida a pendientes exitosamente", "exito")
            llenarComboCodigoSolicitud()
            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
            llenarObsSolFecha(cmbCodigoProyecto.SelectedValue)


        Else
            mostrarMensaje("No se pudo completar la operación. La solicitud ha sido procesada por otra área.", "error")
        End If


    End Sub


    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        liberarSolicitud()
        Response.Redirect("principal.aspx")
    End Sub

    Protected Sub btnCuadroCom_Click(sender As Object, e As EventArgs) Handles btnCuadroCom.Click
        ing.generarExcelCuadroCom(dtgCuadroCom1, dtgCuadroCom2, cmbCodigoProyecto.SelectedValue)
    End Sub

    Protected Sub btnPU_Click(sender As Object, e As EventArgs) Handles btnPU.Click
        Dim sUrl As String = "precioUnitario.aspx"
        com.abrirNuevaVentana(sUrl)
    End Sub

    Protected Sub btnPM_Click(sender As Object, e As EventArgs) Handles btnPM.Click
        Dim sUrl As String = "presupuestoMateriales.aspx"
        com.abrirNuevaVentana(sUrl)
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        liberarSolicitud()
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub

    Protected Sub btnSinc_Click(sender As Object, e As EventArgs) Handles btnSinc.Click
        query = " EXEC PEDIDOS.SINCRONIZAR"
        Try
            fn.ejecutarComandoSQL2(query)
            mostrarMensaje("Sincronización realizada satisfactoriamente", "exito")
        Catch ex As Exception
            mostrarMensaje("Hubo un problema en la sincronización. Inténtelo más tarde.", "error")
        End Try


    End Sub



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
        Dim cant As Integer
        Dim cantString As String

        query = " SELECT COUNT(*) FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA"
        cant = CInt(fn.DevolverDatoQuery(query))
        cant += 1
        cantString = CStr(cant)

        While Len(cantString) < 5
            cantString = "0" & cantString
        End While

        Session.Add("acta", cantString)
        Response.Redirect("nuevaActa.aspx")
    End Sub
    Protected Sub btnModActas_Click(sender As Object, e As EventArgs) Handles btnModActas.Click
        Response.Redirect("modificarActa.aspx")
    End Sub

#End Region


#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        If lblUMActual.Text = "Presupuesto" Then

            e.Row.Cells(17).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False
            e.Row.Cells(23).Visible = False
            e.Row.Cells(24).Visible = False
            e.Row.Cells(25).Visible = False
            e.Row.Cells(26).Visible = False
            e.Row.Cells(27).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(28).Visible = False
            e.Row.Cells(29).Visible = False

        Else

            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(25).Visible = False
            e.Row.Cells(26).Visible = False
            e.Row.Cells(27).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Visible = False

        End If


    End Sub


#End Region
End Class
