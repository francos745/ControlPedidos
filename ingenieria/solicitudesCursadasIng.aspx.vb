
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports Docs.Excel
Imports System.Web.Services

Partial Class ingenieria_solicitudesCursadasIng
    Inherits System.Web.UI.Page

    Dim fn As New Funciones
    Dim ing As New ingenieria
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
    Dim ruta As String
    Dim codRep As String
    Dim a As String
    Dim plantilla As String
    Dim rutaPlantilla As String
    Dim ext As String
    '#Region "FUNCIONES"



    'VALIDAR INICIO DE SESION
    Sub validarInicioSesion()

        If Session("usuario") = "" Then

            Response.Redirect("Ingreso.aspx")
        End If
    End Sub
    'renderizar tabla
    Protected Sub dtgActividades_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY ID"

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

    'MOSTRAR MENSAJE DE EXITO O FRACASO EXISTEN: exito, error, info
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
    'LLENAR DATOS DE CABECERA
    Sub llenarDatosCabecera()
        Dim solicitante As String = ""
        Dim observaciones As String = ""
        Dim fecha As String = ""
        Dim fechaDo As String = ""
        Dim fechaSo As String = ""
        Dim codigo As String = cmbCodigoProyecto.SelectedValue
        query = "SELECT SOLICITANTE FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            solicitante = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            solicitante = ""
        End Try

        query = "SELECT OBSERVACIONES FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            observaciones = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            observaciones = ""
        End Try
        query = "SELECT {fn CONCAT(CONVERT(nvarchar(30), FECHA_SOLICITUD , 103), {fn CONCAT(' - ', CONVERT(nvarchar(30), FECHA_SOLICITUD , 108))})} FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            fecha = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fecha = ""
        End Try

        query = "SELECT Convert(Char(10), FECHA_APROB_SO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            fechaSo = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaSo = ""
        End Try

        query = "SELECT Convert(Char(10), FECHA_APROB_DO, 103) FECHA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA WHERE CODIGO_SOLICITUD='" & codigo & "'"
        Try
            fechaDo = fn.DevolverDatoQuery(query)
        Catch ex As Exception

            fechaDo = ""
        End Try


        txtObservaciones.InnerText = observaciones
        txtSolicitante.Text = solicitante
        txtFechaSol.Text = fecha
        txtFechaSO.Text = fechaSo
        txtFechaDO.Text = fechaDo
    End Sub
    'OCULTAR O MOSTRAR EL FORMULARIO DE EDICION DE ARTICULO
    Sub ocultarMostrarFormulario(ByVal bool As Boolean)
        If bool Then
            contNavbar.Attributes("Style") = ""
            contEditarLinea.Attributes("Style") = "display:none;"
            contObservaciones.Attributes("Style") = ""
            contTitulos.Attributes("Style") = ""
            contTextos.Attributes("Style") = ""
            contObsRechazadas.Attributes("Style") = ""
            contDtgDetalle.Attributes("Style") = ""


            mostrarObservacionesRechazadas()

        Else
            contNavbar.Attributes("Style") = "display:none;"
            contEditarLinea.Attributes("Style") = ""
            contObservaciones.Attributes("Style") = "display:none;"
            contTitulos.Attributes("Style") = "display:none;"
            contTextos.Attributes("Style") = "display:none;"
            contObsRechazadas.Attributes("Style") = "display:none;"
            contDtgDetalle.Attributes("Style") = "display:none;"

        End If


    End Sub


    'MOSTRAR CUADRO DE OBSERVACIONES RECHAZADAS (CAMBIO DE ESTADO)
    Sub mostrarObservacionesRechazadas()

        Dim cant As Integer = 0
        For i As Integer = 0 To dtgDetalle.Rows.Count - 1
            If dtgDetalle.Rows(i).Cells(18).Text = "R" Then
                cant += 1
                Exit For
            End If
        Next
        If cant = 0 Then
            contObsRechazadas.Attributes("Style") = "display:none;"
        Else
            contObsRechazadas.Attributes("Style") = ""
        End If


    End Sub

    Function validarAprobRechazados() As Boolean

        Dim cant As Integer = 0
        Dim filas As Integer = dtgDetalle.Rows.Count - 1
        For i As Integer = 0 To filas
            If dtgDetalle.Rows(i).Cells(18).Text = "R" Then
                cant += 1
            End If
        Next

        If cant = filas + 1 Then
            Return False
        Else
            Return True
        End If


    End Function

    Sub verificarUM2()
        Dim res As String
        query = " DECLARE @valores VARCHAR(1000)"
        query += " SELECT @valores= COALESCE(@valores + ', ', '') + DESCRIPCION "
        query += " FROM (SELECT DISTINCT ISNULL(DESCRIPCION,'ND')DESCRIPCION FROM SOL_PEDIDOS.PEDIDOS.CONVERSION A"
        query += " WHERE PROYECTO = '" & lblProyecto.Text & "'"
        query += " AND ARTICULO IN (SELECT ARTICULO FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA B WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND CODIGO_CONTROL<>'B')"
        query += " AND FACTOR IS NULL)VISTA"
        query += " select  @valores as valores"

        res = fn.DevolverDatoQuery(query)

        If res <> "" Then
            mensaje3.Attributes("Style") = ""
            lblMensajes3.Text = "Se encontraron los siguientes artículos sin Factor de Conversión:" & res & " . Esto puede ocasionar problemas con el funcionamiento del módulo."

        Else
            lblMensajes3.Text = ""
            mensaje3.Attributes("Style") = "display:none;"
        End If

    End Sub

    Function validarAprobMaterial() As String
        '14

        Dim material As Integer = 0 'variable para contar si c[odigo del material es igual al nombre de material
        Dim disponible As Integer = 0 'variable para contar los registros que tengan cantidad menor a 0
        Dim actas As Integer = 0 ' variable para determinar si hace falta asignar algun acta
        Dim filas As Integer = dtgDetalle.Rows.Count - 1
        Dim cantDisp As Decimal
        Dim cantActa As Decimal
        Dim cantEjecActa As Decimal
        Dim cantAsigActa As Decimal
        Dim a As Decimal
        Dim b As Decimal
        Dim c As Decimal



        For i As Integer = 0 To filas

            If dtgDetalle.Rows(i).Cells(18).Text = "P" Then

                If dtgDetalle.Rows(i).Cells(5).Text = dtgDetalle.Rows(i).Cells(20).Text Then
                    material += 1

                End If
                If dtgDetalle.Rows(i).Cells(14).Text < 0 Then

                    disponible += 1

                End If

                cantDisp = CDec(dtgDetalle.Rows(i).Cells(14).Text)
                cantActa = CDec(dtgDetalle.Rows(i).Cells(10).Text)
                cantEjecActa = CDec(dtgDetalle.Rows(i).Cells(13).Text)
                cantAsigActa = CDec(dtgDetalle.Rows(i).Cells(22).Text)
                a = cantDisp - cantActa
                b = cantEjecActa + cantAsigActa
                c = a + b

                'cantidad disponible - (cantidad en actas - cantidad ejecutada en actas) + cantidad acta asignada 
                If c < 0 Then
                    actas += 1
                    ' MsgBox(c.ToString)
                End If

            End If

        Next


        If material = 0 Then
            If actas = 0 Then

                If disponible = 0 Then

                    Return "ok"

                Else

                    Return "d" ' regresa a = falta acta, en caso de que exista un error en falta de asignacion de actas

                End If

            Else

                Return "a" ' regresa d = disponible, en caso de que exista un error en cantidades disponibles menores a 0

            End If
        Else

            Return "m" ' regresa m=(material), en caso de que haya un error de codigo de material
        End If


    End Function
    Sub liberarSolicitud()
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = 'ND' WHERE USUARIO='" & lblUsuario.Text & "'"

        fn.ejecutarComandoSQL2(query)
    End Sub

    Sub validarConcurrencia()
        Dim a As String
        Dim difMinutos As Integer = 0
        query = "SELECT USUARIO FROM SOL_PEDIDOS.PEDIDOS.ACCESO WHERE CODIGO = '" & cmbCodigoProyecto.SelectedValue & "' AND USUARIO <> '" & lblUsuario.Text & "'"
        a = fn.DevolverDatoQuery(query)
        If a = "" Then
            query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = '" & cmbCodigoProyecto.SelectedValue & "', RecordDate=getdate() WHERE USUARIO='" & lblUsuario.Text & "'"
            fn.ejecutarComandoSQL2(query)
            btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
            btnRechazar.Attributes("class") = "btn btn-default btn-md enabled"
            mostrarMensaje3("", "exito")
        Else
            query = "SELECT DATEDIFF(minute, RecordDate, GETDATE()) FROM SOL_PEDIDOS.PEDIDOS.ACCESO WHERE CODIGO = '" & cmbCodigoProyecto.SelectedValue & "' AND USUARIO <> '" & lblUsuario.Text & "'"
            difMinutos = CInt(fn.DevolverDatoQuery(query))
            If difMinutos > com.obtenerTiempo() Then
                query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = 'ND' WHERE CODIGO='" & cmbCodigoProyecto.SelectedValue & "'"
                fn.ejecutarComandoSQL2(query)
                query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = '" & cmbCodigoProyecto.SelectedValue & "', RecordDate=getdate()  WHERE USUARIO='" & lblUsuario.Text & "'"
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
        End If
    End Sub





#Region "Combos"
    Sub llenarComboCodigoSolicitud()
        query = " SELECT CODIGO_SOLICITUD  FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA"
        query += " WHERE ESTADO = 'P' ORDER BY CODIGO_SOLICITUD DESC"
        fn.llenarComboBox(cmbCodigoProyecto, query, "CODIGO_SOLICITUD", "CODIGO_SOLICITUD")
    End Sub

    Sub llenarComboActividad()
        query = " (SELECT FASE, NOMBRE,"
        query += " ISNULL((SELECT NRO FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B"
        query += " WHERE A.FASE=B.FASE),0) NUMERO"
        query += " FROM   SOL_PEDIDOS.VITALICIA.FASE_PY A"
        query += " WHERE FASE NOT IN (SELECT FASE FROM  SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00.00' )"
        query += " AND FASE NOT IN (SELECT FASE FROM   SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00')"
        query += " AND PROYECTO = '" & Server.HtmlDecode(lblProyecto.Text) & "' AND FASE LIKE '%00')"
        query += " ORDER BY FASE"
        fn.llenarComboBoxOpciones2(cmbActividad, query, "NUMERO", "FASE", "NOMBRE")
        Dim UItem As New ListItem("***Actividad Original***", Server.HtmlDecode(lblActividad.Text))
        cmbActividad.Items.Add(UItem)
    End Sub

    Sub llenarComboMaterial()
        Dim actividad As String
        Try
            actividad = cmbActividad.SelectedValue.Substring(0, 8)
        Catch ex As Exception
            actividad = "Otro"
        End Try
        query = " SELECT FASE,NOMBRE FROM SOL_PEDIDOS.VITALICIA.FASE_PY "
        query += " WHERE FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00.00')"
        query += " AND FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00.00')"
        query += " AND TIPO='A'"
        query += " AND FASE NOT IN (SELECT FASE FROM SOL_PEDIDOS.VITALICIA.FASE_PY WHERE FASE LIKE '%00')"
        query += " AND FASE LIKE'" & Server.HtmlDecode(actividad) & "%' "
        query += " ORDER BY NOMBRE"
        fn.llenarComboBox2(cmbMaterial, query, "NOMBRE", "FASE")
        Dim UItem As New ListItem("***Material Original***", Server.HtmlDecode(lblMaterial.Text))
        cmbMaterial.Items.Add(UItem)
    End Sub

    Sub llenarcomboArticulo()
        query = " SELECT DISTINCT A.ARTICULO ARTICULO, B.DESCRIPCION DESCRIPCION "
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY A, SOL_PEDIDOS.VITALICIA.ARTICULO B "
        query += " WHERE FASE = '" & Server.HtmlDecode(cmbMaterial.SelectedValue) & "' "
        query += " AND A.ARTICULO= B.ARTICULO ORDER BY DESCRIPCION"
        fn.llenarComboBox2(cmbArticulo, query, "DESCRIPCION", "ARTICULO")
        Dim UItem As New ListItem("***Articulo Original***", Server.HtmlDecode(lblArticulo.Text))
        cmbArticulo.Items.Add(UItem)

    End Sub
    Sub llenarComboActas()
        query = " SELECT ID,"
        query += " {FN CONCAT(CONVERT(VARCHAR(20), (CAST(ROUND(CANT_ACTA_APS- CANT_ACTA_ING_APS,4) AS FLOAT))),{FN CONCAT(' ',{FN CONCAT(UND_MED,{FN CONCAT(' - ',CODIGO_ACTA_ING)})})})}CANTIDAD"
        query += " FROM ("
        query += " SELECT C.ID,C.CODIGO_ACTA_ING,C.UND_MED,C.UND_MED_A,"
        query += " ISNULL(SUM(D.CANT_ACTA),0)CANT_ACTA_ING,"
        query += " ISNULL(SUM(D.CANT_ACTA_APS),0)CANT_ACTA_ING_APS, "
        query += " ISNULL((C.CANTIDAD),0)CANT_ACTA, "
        query += " ISNULL((C.CANTIDAD_APS),0)CANT_ACTA_APS"
        query += " FROM ("
        query += " SELECT B.ID, A.CODIGO_ACTA_ING,CANTIDAD,CANTIDAD_APS,B.UND_MED,B.UND_MED_A"
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA A "
        query += " JOIN SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA B "
        query += " ON A.CODIGO_ACTA=B.CODIGO_ACTA"
        query += " WHERE FASE = '" & Server.HtmlDecode(cmbMaterial.SelectedValue) & "' "
        query += " AND B.CODIGO_CONTROL<>'B'"
        query += " ) C LEFT JOIN SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA D"
        query += " ON C.ID=D.ID_ACTA"
        query += " AND D.ESTADO='A'"
        query += " GROUP BY C.ID,CODIGO_ACTA_ING,C.UND_MED,C.UND_MED_A,C.CANTIDAD_APS,C.CANTIDAD"
        query += " )VISTA"
        query += " WHERE CANT_ACTA_APS- CANT_ACTA_ING_APS<>0"

        fn.llenarComboBox2(cmbActa, query, "CANTIDAD", "ID")
        Dim UItem As New ListItem("ND", "0")
        cmbActa.Items.Add(UItem)
    End Sub
#End Region


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

    Sub llenarDatosActa(ByVal ID As String)

        query = "SELECT CANT_ACTA_APS FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE ID='" & ID & "'"
        txtCantActa.Text = Math.Round(CDec(fn.DevolverDatoQuery(query)) * 100) / 100

        Try
            cmbActa.SelectedValue = fn.DevolverDatoQuery(" SELECT ID_ACTA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE ID='" & ID & "'")
        Catch ex As Exception

        End Try

        query = " SELECT CANTIDAD_APS - "
        query += " (SELECT ISNULL(SUM(CANT_ACTA_APS),0) SUMA"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA"
        query += " WHERE ID_ACTA='" & cmbActa.SelectedValue & "'"
        query += " AND ESTADO='A'"
        query += " )CANTIDAD"
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA "
        query += " WHERE ID ='" & cmbActa.SelectedValue & "'"
        Try
            lblCantActaDisp.Text = fn.DevolverDatoQuery(query)
            If lblCantActaDisp.Text = "" Then
                lblCantActaDisp.Text = "0"
            End If
        Catch ex As Exception
            lblCantActaDisp.Text = "0"
        End Try
    End Sub

    Sub llenarUM()  'Origen determina si la unidad de medida viene de presupuesto o almacenes

        Dim proyecto As String = lblProyecto.Text
        Dim articulo As String = Server.HtmlDecode(cmbArticulo.SelectedValue)

        Dim umDef As String = lblUMAux.Text

        lblUM.Text = com.obtenerUMPresupuesto(proyecto, articulo, umDef)

        lblUMEq.Text = com.obtenerUMAlmacen(articulo, lblUM.Text)

        lblFactor.Text = com.obtenerFactorConversion(proyecto, articulo)

    End Sub


    Sub llenarTablaDetallesPedido()

        query = "SELECT * FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL=@CODIGO_PROYECTO AND COD_CONT_LINEA<>'B' ORDER BY ID"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query
        Detalles.DeleteCommand = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ESTADO=CASE WHEN ESTADO ='R' THEN 'P' ELSE 'R' END WHERE ID = @ID AND ID NOT IN (SELECT CODIGO_SOLICITUD FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA)"

        dtgDetalle.DataSourceID = "Detalles"
    End Sub

    Sub llenarTablaCuadroComunicacion()

        query = " SELECT '1' ID, NOM_ACTIVIDAD,NOM_MATERIAL,UM_P,SUM(CANT_SOL_P)CANT_SOL_P,PRESUPUESTADO"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET"
        query += " WHERE COD_SOL=@CODIGO_PROYECTO"
        query += " AND COD_CONT_LINEA<>'B'"
        query += " AND ESTADO_LIN<>'R'"
        query += " GROUP BY NOM_ACTIVIDAD,NOM_MATERIAL,UM_P,PRESUPUESTADO"

        detalleCuadroCom1.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleCuadroCom1.SelectCommand = query
        dtgCuadroCom1.DataSourceID = "detalleCuadroCom1"


        query = " SELECT DISTINCT NOM_MATERIAL,"
        query += " UM_P,"
        query += " CANT_PRESUP_ACUM_P, "
        query += " (CANT_SOL_APROB_ACUM_P-"
        query += " CANT_ACTAS_APROB_ACUM_P+"
        query += " ( SELECT SUM(B.CANT_SOL_P) "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET B"
        query += " WHERE B.NOM_MATERIAL=A.NOM_MATERIAL"
        query += " AND A.COD_SOL=B.COD_SOL"
        query += " AND A.PRESUPUESTADO=B.PRESUPUESTADO"
        query += " AND B.COD_CONT_LINEA<>'B'"
        query += " AND A.ESTADO_LIN=B.ESTADO_LIN"
        query += " )-"
        query += " ( SELECT SUM(B.CANT_ACTA_LINEA_P) "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET B"
        query += " WHERE B.NOM_MATERIAL=A.NOM_MATERIAL"
        query += " AND A.COD_SOL=B.COD_SOL"
        query += " AND A.PRESUPUESTADO=B.PRESUPUESTADO"
        query += " AND B.COD_CONT_LINEA<>'B'"
        query += " AND A.ESTADO_LIN=B.ESTADO_LIN"
        query += " )"
        query += " ) CANT_SOL_APROB_ACUM_P,"
        query += " ((CANT_ACTAS_APROB_ACUM_P)+"
        query += " ( SELECT SUM(B.CANT_ACTA_LINEA_P) "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET B"
        query += " WHERE B.NOM_MATERIAL=A.NOM_MATERIAL"
        query += " AND A.COD_SOL=B.COD_SOL"
        query += " AND A.PRESUPUESTADO=B.PRESUPUESTADO"
        query += " AND B.COD_CONT_LINEA<>'B'"
        query += " AND A.ESTADO_LIN=B.ESTADO_LIN"
        query += " ))CANT_APROB_ACTAS_ACUM_P,"

        query += " (CANT_PRESUP_ACUM_P-"
        query += " (CANT_SOL_APROB_ACUM_P-CANT_ACTAS_APROB_ACUM_P+"
        query += " (SELECT SUM(B.CANT_SOL_P) "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET B"
        query += " WHERE B.NOM_MATERIAL=A.NOM_MATERIAL"
        query += " AND A.COD_SOL=B.COD_SOL"
        query += " AND A.PRESUPUESTADO=B.PRESUPUESTADO"
        query += " AND B.COD_CONT_LINEA<>'B'"
        query += " AND A.ESTADO_LIN=B.ESTADO_LIN"
        query += " )-"
        query += " ( SELECT SUM(B.CANT_ACTA_LINEA_P) "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET B"
        query += " WHERE B.NOM_MATERIAL=A.NOM_MATERIAL"
        query += " AND A.COD_SOL=B.COD_SOL"
        query += " AND A.PRESUPUESTADO=B.PRESUPUESTADO"
        query += " AND B.COD_CONT_LINEA<>'B'"
        query += " AND A.ESTADO_LIN=B.ESTADO_LIN"
        query += " )))CANT_DISP_P"

        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET A"
        query += " WHERE COD_SOL=@CODIGO_PROYECTO"
        query += " And COD_CONT_LINEA<>'B'"
        query += " AND ESTADO_LIN<>'R'"
        query += " AND PRESUPUESTADO='SI'"
        query += " GROUP BY NOM_MATERIAL,UM_P,CANT_PRESUP_ACUM_P,CANT_SOL_APROB_ACUM_P,CANT_ACTAS_APROB_ACUM_P,CANT_ACTA_LINEA_P,COD_SOL,PRESUPUESTADO,COD_CONT_LINEA,ESTADO_LIN"


        detalleCuadroCom2.ConnectionString = fn.ObtenerCadenaConexion("conn")
        detalleCuadroCom2.SelectCommand = query
        dtgCuadroCom2.DataSourceID = "detalleCuadroCom2"


    End Sub

    '---------------------------------------------------------------- REGION CONTENIDO DEL FORMULARIO ----------------------------------------------------------------

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblUsuario.Text = Session("usuario")

        Response.AddHeader("Refresh", Convert.ToString((Session.Timeout * 60) - 5) + ";Ingreso.aspx")

        validarInicioSesion()

        If Not Page.IsPostBack Then
            llenarComboCodigoSolicitud()
            lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue

            llenarDatosCabecera()
            lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
            llenarTablaDetallesPedido()
            mostrarObservacionesRechazadas()
            'validarConcurrencia()
        End If
        verificarUM2
    End Sub

#Region "BOTONES DE NAVEGACION IZQUIERDA"
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

#Region "BOTONES NAVEGACION DERECHA"


    Protected Sub btnSinc_Click(sender As Object, e As EventArgs) Handles btnSinc.Click
        Dim validador As String = ""

        query = " EXEC SOL_PEDIDOS.PEDIDOS.SINCRONIZAR"
        validador = fn.ejecutarComandoSQL4(query)

        If validador = "ok" Then
            mostrarMensaje("Sincronización realizada satisfactoriamente", "exito")
        Else
            mostrarMensaje("Hubo un problema en la sincronización. Inténtelo más tarde." + validador, "error")
        End If

        llenarDatosCabecera()
    End Sub

    Protected Sub btnCuadroCom_Click(sender As Object, e As EventArgs) Handles btnCuadroCom.Click
        llenarTablaCuadroComunicacion()
        llenarDatosCabecera()
        mostrarMensaje("", "exito")
        ing.generarExcelCuadroCom(dtgCuadroCom1, dtgCuadroCom2, cmbCodigoProyecto.SelectedValue)
    End Sub

    Protected Sub btnPU_Click(sender As Object, e As EventArgs) Handles btnPU.Click
        Dim sUrl As String = "precioUnitario.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)
        mostrarMensaje("", "exito")
        mostrarMensaje("", "exito")
        llenarDatosCabecera()
    End Sub

    Protected Sub btnPM_Click(sender As Object, e As EventArgs) Handles btnPM.Click
        Dim sUrl As String = "presupuestoMateriales.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)
        mostrarMensaje("", "exito")
        llenarDatosCabecera()
    End Sub

    Protected Sub btnExcel_click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Dim sUrl As String = "cargarExcel.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)
        mostrarMensaje("", "exito")
        llenarDatosCabecera()

    End Sub

    Protected Sub btnAlmacenes_Click(sender As Object, e As EventArgs) Handles btnAlmacenes.Click
        lblUMActual.Text = "Almacén"
        llenarTablaDetallesPedido()
        mostrarMensaje("", "exito")
        llenarDatosCabecera()
    End Sub

    Protected Sub btnPresupuesto_Click(sender As Object, e As EventArgs) Handles btnPresupuesto.Click
        lblUMActual.Text = "Presupuesto"
        llenarTablaDetallesPedido()
        mostrarMensaje("", "exito")
        llenarDatosCabecera()
    End Sub

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        liberarSolicitud()
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
        mostrarMensaje("", "exito")
        llenarDatosCabecera()
    End Sub


#End Region

    Protected Sub cmbCodigoProyecto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCodigoProyecto.SelectedIndexChanged
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        llenarDatosCabecera()
        llenarTablaDetallesPedido()


        ' validarConcurrencia()
    End Sub

    Protected Sub btnAprobarSi_Click(sender As Object, e As EventArgs) Handles btnAprobarSi.ServerClick
        'validamos que se haya escrito una fecha en el campo de fecha de aprobacion
        Dim validador As String = ""
        If txtFechaSO.Text.Contains("20") Or txtFechaDO.Text.Contains("20") = True Then
            Dim A = 0
        Else
            mostrarMensaje("Debe cargar otra solicitud con la fecha de aprobación del Supervisor de Obra o del Director de Obra.", "error")
            Exit Sub
        End If
        validador = validarAprobMaterial()

        If validador = "ok" Then
            If validarAprobRechazados() Then
                If contObsRechazadas.Attributes("Style") = "" Then
                    If txtObsRechazados.InnerText <> "" Then
                        'Actualizamos la cabecera y las observaciones a aprobados
                        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA SET ESTADO='A', FECHA_APROB_ING= '" + DateTime.Now().ToString("yyyy/MM/dd HH:mm:ss") + "', USUARIO_APROB= '" + lblUsuario.Text + "'"
                        query += ", OBS_RECHAZADOS='" & txtObsRechazados.InnerText & "', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                        'Actualizamos las lineas que sean diferentes de rechazadas a aprobadas
                        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ESTADO='A', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO<>'R' AND CODIGO_CONTROL<>'B'"
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                        'ACTUALIZAMOS LAS TABLAS DEL RESIDENTE DE OBRA CON LOS MATERIALES APROBADOS POR INGENIERÍA
                        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA SET ESTADO='A'"
                        query += ", OBS_RECHAZADOS='" & txtObsRechazados.InnerText & "', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

                        'BORRAMOS LA LINEAS QUE ESCRIBIÓ EL RESIDENTE DE OBRA 
                        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET CODIGO_CONTROL='B', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' "
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                        'INSERTAMOS LAS LINEAS DE INGENIERIA EN LA TABLA DEL RESIDENTE
                        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA"
                        query += " (PROYECTO,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E,ESTADO, RecordDate, UpdatedBy)"
                        query += " SELECT PROYECTO,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E,ESTADO, GETDATE(),'" & lblUsuario.Text & "'"
                        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA B WHERE CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"
                        query += " AND CODIGO_CONTROL<>'B'"
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                        'COPIAMOS LA SOLICITUD APROBADA AL MÓDULO DE LOGISTICA

                        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA ("
                        query += " CODIGO_SOLICITUD,"
                        query += " ESTADO,"
                        query += " FECHA_SOLICITUD,"
                        query += " FECHA_REQUERIDA,"
                        query += " Observaciones,"
                        query += " SOLICITANTE,"
                        query += " FECHA_APROB_SO,"
                        query += " FECHA_APROB_DO,"
                        query += " FECHA_APROB_ING,"
                        query += " USUARIO_APROB, RecordDate, UpdatedBy)"

                        query += " SELECT "
                        query += " CODIGO_SOLICITUD,"
                        query += " 'P' ESTADO,"
                        query += " FECHA_SOLICITUD,"
                        query += " FECHA_REQUERIDA,"
                        query += " Observaciones,"
                        query += " SOLICITANTE,"
                        query += " FECHA_APROB_SO,"
                        query += " FECHA_APROB_DO,"
                        query += " FECHA_APROB_ING,"
                        query += " USUARIO_APROB, GETDATE(),'" & lblUsuario.Text & "'"
                        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA"
                        query += " WHERE CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"

                        Try
                            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                        Catch ex As Exception

                        End Try
                        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA ("
                        query += " PROYECTO,"
                        query += " ACTIVIDAD,"
                        query += " FASE,"
                        query += " ARTICULO,	"
                        query += " CANTIDAD,"
                        query += " CANTIDAD_APS,"
                        query += " ESTADO,"
                        query += " OBSERVACIONES,"
                        query += " CODIGO_SOLICITUD,"
                        query += " CODIGO_CONTROL,"
                        query += " UND_MED,"
                        query += " UND_MED_E, RecordDate, UpdatedBy)"

                        query += " SELECT PROYECTO,"
                        query += " ACTIVIDAD,"
                        query += " FASE,"
                        query += " ARTICULO,"
                        query += " CANTIDAD,"
                        query += " CANTIDAD_APS,"
                        query += " ESTADO,"
                        query += " OBSERVACIONES,"
                        query += " CODIGO_SOLICITUD,"
                        query += " CODIGO_CONTROL,"
                        query += " UND_MED,"
                        query += " UND_MED_E , GETDATE(),'" & lblUsuario.Text & "'"

                        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA "
                        query += " WHERE CODIGO_CONTROL<>'B' AND ESTADO<>'R' "


                        query += " AND CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"

                        Try
                            fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                        Catch ex As Exception

                        End Try


                        mostrarMensaje("Solicitud " & cmbCodigoProyecto.SelectedValue & " aprobada exitosamente", "exito")
                        llenarComboCodigoSolicitud()
                        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue

                        llenarTablaDetallesPedido()
                        txtObsRechazados.InnerText = ""
                    Else
                        mostrarMensaje("Debe llenar las observaciones para los materiales rechazados.", "error")
                        'contObsRechazadas.Attributes("class") = "row has-error"
                    End If
                Else
                    'Actualizamos la cabecera y las observaciones a aprobados
                    query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA SET ESTADO='A', FECHA_APROB_ING= '" + DateTime.Now().ToString("yyyy/MM/dd HH:mm:ss") + "', USUARIO_APROB= '" + lblUsuario.Text + "'"
                    query += ", OBS_RECHAZADOS='S/O', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
                    fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                    'Actualizamos las lineas que sean diferentes de rechazadas a aprobadas
                    query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ESTADO='A', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO<>'R' AND CODIGO_CONTROL<>'B' "
                    fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                    'ACTUALIZAMOS LAS TABLAS DEL RESIDENTE DE OBRA CON LOS MATERIALES APROBADOS POR INGENIERÍA
                    query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA SET ESTADO='A'"
                    query += ", OBS_RECHAZADOS='S/O', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
                    fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

                    'BORRAMOS LA LINEAS QUE ESCRIBIÓ EL RESIDENTE DE OBRA 
                    query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET CODIGO_CONTROL='B' WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND ESTADO<>'R' "
                    fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                    'INSERTAMOS LAS LINEAS DE INGENIERIA EN LA TABLA DEL RESIDENTE
                    query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA"
                    query += " (PROYECTO,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E,ESTADO, RecordDate, UpdatedBy)"
                    query += " SELECT PROYECTO,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E,ESTADO, GETDATE(),'" & lblUsuario.Text & "'"
                    query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA B WHERE CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"
                    query += " AND CODIGO_CONTROL<>'B'"
                    fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)



                    'COPIAMOS LA SOLICITUD APROBADA AL MÓDULO DE LOGISTICA

                    query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_CABECERA ("
                    query += " CODIGO_SOLICITUD,"
                    query += " ESTADO,"
                    query += " FECHA_SOLICITUD,"
                    query += " FECHA_REQUERIDA,"
                    query += " Observaciones,"
                    query += " SOLICITANTE,"
                    query += " FECHA_APROB_SO,"
                    query += " FECHA_APROB_DO,"
                    query += " FECHA_APROB_ING,"
                    query += " USUARIO_APROB, RecordDate, UpdatedBy)"

                    query += " SELECT "
                    query += " CODIGO_SOLICITUD,"
                    query += " 'P' ESTADO,"
                    query += " FECHA_SOLICITUD,"
                    query += " FECHA_REQUERIDA,"
                    query += " Observaciones,"
                    query += " SOLICITANTE,"
                    query += " FECHA_APROB_SO,"
                    query += " FECHA_APROB_DO,"
                    query += " FECHA_APROB_ING,"
                    query += " USUARIO_APROB, GETDATE(),'" & lblUsuario.Text & "'"
                    query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA"
                    query += " WHERE CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"
                    'query)
                    Try
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                    Catch ex As Exception

                    End Try
                    query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_LOG_LINEA ("
                    query += " PROYECTO,"
                    query += " ACTIVIDAD,"
                    query += " FASE,"
                    query += " ARTICULO,	"
                    query += " CANTIDAD,"
                    query += " CANTIDAD_APS,"
                    query += " ESTADO,"
                    query += " OBSERVACIONES,"
                    query += " CODIGO_SOLICITUD,"
                    query += " CODIGO_CONTROL,"
                    query += " UND_MED,"
                    query += " UND_MED_E, RecordDate, UpdatedBy)"

                    query += " SELECT PROYECTO,"
                    query += " ACTIVIDAD,"
                    query += " FASE,"
                    query += " ARTICULO,"
                    query += " CANTIDAD,"
                    query += " CANTIDAD_APS,"
                    query += " ESTADO,"
                    query += " OBSERVACIONES,"
                    query += " CODIGO_SOLICITUD,"
                    query += " CODIGO_CONTROL,"
                    query += " UND_MED,"
                    query += " UND_MED_E, GETDATE(),'" & lblUsuario.Text & "' "

                    query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA "
                    query += " WHERE CODIGO_CONTROL<>'B'"

                    query += " AND CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"
                    'query)
                    Try
                        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
                    Catch ex As Exception

                    End Try
                    mostrarMensaje("Solicitud " & cmbCodigoProyecto.SelectedValue & " aprobada exitosamente", "exito")
                    liberarSolicitud()
                    Response.Redirect("solicitudesAprobadas.aspx")
                    llenarComboCodigoSolicitud()
                    lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
                    llenarDatosCabecera()
                    llenarTablaDetallesPedido()
                    txtObsRechazados.InnerText = ""

                End If


            Else
                mostrarMensaje("No es posible aprobar una solicitud con todas las lineas rechazadas.", "error")
            End If
        Else
            If validador = "m" Then
                mostrarMensaje("No es posible aprobar una solicitud con cantidades disponibles menores a 0", "error")
            Else
                If validador = "a" Then
                    mostrarMensaje("Existen líneas de pedido que requieren de un acta para ser aprobadas", "error")
                Else
                    mostrarMensaje("No es posible aprobar una solicitud con materiales inexistentes.", "error")
                End If
            End If

        End If


    End Sub
    Protected Sub btnRechazarSi_Click(sender As Object, e As EventArgs) Handles btnRechazarSi.ServerClick

        'Actualizamos la cabecera y las observaciones a aprobados
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_CABECERA SET ESTADO='R'"
        query += ", OBS_RECHAZADOS='S/O', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        'Actualizamos las lineas que sean diferentes de rechazadas a aprobadas
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ESTADO='R' WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' AND CODIGO_CONTROL<>'B' "
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        'ACTUALIZAMOS LAS TABLAS DEL RESIDENTE DE OBRA CON LOS MATERIALES APROBADOS POR INGENIERÍA
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_CABECERA SET ESTADO='R'"
        query += ", OBS_RECHAZADOS='S/O', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

        'BORRAMOS LA LINEAS QUE ESCRIBIÓ EL RESIDENTE DE OBRA 
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA SET CODIGO_CONTROL='B', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  WHERE CODIGO_SOLICITUD='" & cmbCodigoProyecto.SelectedValue & "' "
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        'INSERTAMOS LAS LINEAS DE INGENIERIA EN LA TABLA DEL RESIDENTE
        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.SOLICITUD_RES_LINEA"
        query += " (PROYECTO,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E,ESTADO, RecordDate, UpdatedBy)"
        query += " SELECT PROYECTO,FASE,ARTICULO,CANTIDAD,CODIGO_SOLICITUD,CODIGO_CONTROL,UND_MED,CANTIDAD_APS,ACTIVIDAD,UND_MED_E,ESTADO, GETDATE(),'" & lblUsuario.Text & "'"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA B WHERE CODIGO_SOLICITUD = '" & cmbCodigoProyecto.SelectedValue & "'"
        query += " AND CODIGO_CONTROL<>'B'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)
        liberarSolicitud()
        mostrarMensaje("Solicitud " & cmbCodigoProyecto.SelectedValue & " rechazada exitosamente", "exito")
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        llenarDatosCabecera()
        llenarTablaDetallesPedido()
        txtObsRechazados.InnerText = ""

    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.ServerClick
        llenarComboCodigoSolicitud()
        lblCodigoProyecto.Text = cmbCodigoProyecto.SelectedValue
        llenarDatosCabecera()
        lblProyecto.Text = obtenerProyecto(cmbCodigoProyecto.SelectedValue)
        llenarTablaDetallesPedido()
        mostrarObservacionesRechazadas()


        mostrarMensaje("", "exito")
    End Sub

    Protected Sub dtgDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgDetalle.SelectedIndexChanged
        ' Get the currently selected row using the SelectedRow property.

        Dim row As GridViewRow

        row = dtgDetalle.SelectedRow

        Dim fila As Integer = row.RowIndex.ToString
        Dim cantA, cantP As Decimal
        Dim estado As String = dtgDetalle.Rows(fila).Cells(18).Text


        If estado <> "R" Then

            lblActividad.Text = dtgDetalle.Rows(fila).Cells(19).Text
            lblMaterial.Text = dtgDetalle.Rows(fila).Cells(20).Text
            lblArticulo.Text = dtgDetalle.Rows(fila).Cells(23).Text '************************************************* codigo de articulo
            lblUMAux.Text = dtgDetalle.Rows(fila).Cells(7).Text
            txtCantidad.Text = CDec(dtgDetalle.Rows(fila).Cells(8).Text)
            lblUMEq.Text = dtgDetalle.Rows(fila).Cells(24).Text '************************************************* unidad equivalente
            cantP = CDec(dtgDetalle.Rows(fila).Cells(8).Text)
            cantA = CDec(dtgDetalle.Rows(fila).Cells(25).Text) '************************************************* cantidad equivalente
            If cantA = 0 Then
                cantA = 1
            End If
            lblFactor.Text = Math.Round(CDec(((cantP / cantA))) * 100) / 100
            lblCantEq.Text = Math.Round(CDec(dtgDetalle.Rows(fila).Cells(25).Text) * 100) / 100
            lblId.Text = dtgDetalle.Rows(fila).Cells(3).Text
            llenarComboActividad()
            Try
                cmbActividad.SelectedValue = lblActividad.Text
            Catch ex As Exception
                cmbActividad.SelectedIndex = cmbActividad.Items.Count - 1
            End Try
            llenarComboMaterial()
            Try
                cmbMaterial.SelectedValue = lblMaterial.Text
            Catch ex As Exception
                cmbMaterial.SelectedIndex = cmbMaterial.Items.Count - 1
            End Try
            llenarcomboArticulo()
            Try
                cmbArticulo.SelectedValue = lblArticulo.Text
            Catch ex As Exception
                cmbArticulo.SelectedIndex = cmbArticulo.Items.Count - 1
            End Try
            llenarComboActas()
            llenarDatosActa(lblId.Text)
            llenarUM()
            ocultarMostrarFormulario(False)
            mostrarMensaje("", "")
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

    Protected Sub grdSolicitudDetalle_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles dtgDetalle.RowDeleting
        Dim id As String = dtgDetalle.Rows(e.RowIndex).Cells(3).Text
        Dim acta As String = ""
        query = "SELECT ID_ACTA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE ID='" & id & "'"

        acta = fn.DevolverDatoQuery(query)

        If acta <> "0" Then
            e.Cancel = True
            mostrarMensaje("No se puede eliminar una linea con un acta asignada.", "Error")
        Else
            e.Cancel = False
            mostrarMensaje("", "Error")
        End If


        llenarTablaDetallesPedido()
    End Sub


    Protected Sub grdSolicitudDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound
        Dim actividad As String = e.Row.Cells(19).Text ' CANTIDAD_DISPONIBLE
        Dim material As String = e.Row.Cells(20).Text ' CANTIDAD_DISPONIBLE
        Dim desc As String = e.Row.Cells(14).Text ' CANTIDAD_DISPONIBLE
        Dim presup As String = e.Row.Cells(17).Text ' PRESUPUESTADO
        Dim presupAct As String = e.Row.Cells(16).Text 'PRESUPUESTADO_ACT
        Dim actas As String = e.Row.Cells(10).Text ' CANTIDAD_ACTAS
        Dim estado As String = e.Row.Cells(18).Text ' ESTADO


        If desc = "&nbsp;" Or desc = "CANTIDAD DISPONIBLE" Then
            Dim B As Integer = 0
            'e.Row.Cells(7).Text)
        Else
            'e.Row.Cells(7).Text)
            Dim cantidad As Decimal
            Try
                cantidad = CDec(e.Row.Cells(8).Text)
            Catch ex As Exception
                cantidad = 0
            End Try


            If estado = "R" Then
                e.Row.Font.Strikeout = True
                e.Row.Font.Overline = True
                e.Row.Font.Underline = True
                'e.Row.Font.Italic = True
                e.Row.Font.Size = 7
                e.Row.ForeColor = Drawing.Color.Red
                e.Row.Font.Bold = True
                e.Row.BorderColor = Drawing.Color.Red

            Else

                If presup = "SI" Then
                    If presupAct = "SI" Then
                        If CDec(desc) < 0 Then
                            'e.Row.Font.Strikeout = True
                            'e.Row.ForeColor = Drawing.Color.Red
                            'e.Row.Font.Bold = True
                            e.Row.BackColor = Drawing.Color.Red
                        Else
                            e.Row.BackColor = Drawing.Color.White
                        End If
                    Else
                        If CDec(actas) > 0 Then
                            e.Row.BackColor = Drawing.Color.GreenYellow
                        Else
                            e.Row.BackColor = Drawing.Color.Orange
                        End If
                    End If
                Else
                    If presupAct = "SI" Then
                        If CDec(actas) > 0 Then
                            e.Row.BackColor = Drawing.Color.GreenYellow
                        Else
                            e.Row.BackColor = Drawing.Color.Orange
                        End If
                    Else
                        e.Row.BackColor = Drawing.Color.Yellow
                    End If

                End If
            End If


        End If

        mostrarObservacionesRechazadas()
        If dtgDetalle.Rows.Count = 0 Then
            btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md disabled"
        Else
            btnAprobar.Attributes("class") = "btn btn-vitalicia btn-md enabled"
        End If
        validarConcurrencia()
    End Sub

    Protected Sub cmbActividad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbActividad.SelectedIndexChanged

        llenarComboMaterial()
        llenarcomboArticulo()
        llenarComboActas()
        llenarDatosActa(lblId.Text)
        llenarUM()
    End Sub

    Protected Sub cmbMaterial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMaterial.SelectedIndexChanged
        fn.verificarUM(cmbMaterial.SelectedValue, cmbMaterial.SelectedItem.ToString, mensaje2, lblMensaje2)
        llenarcomboArticulo()
        llenarComboActas()
        llenarDatosActa(lblId.Text)
        llenarUM()
    End Sub

    Protected Sub cmbArticulo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbArticulo.SelectedIndexChanged

        llenarUM()
    End Sub

    Protected Sub cmbActa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbActa.SelectedIndexChanged

        query = " Select CANTIDAD_APS - "
        query += " (Select ISNULL(SUM(CANT_ACTA_APS),0) SUMA"
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA"
        query += " WHERE ID_ACTA='" & cmbActa.SelectedValue & "'"
        query += " AND ESTADO='A'"
        query += " )CANTIDAD"
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA "
        query += " WHERE ID ='" & cmbActa.SelectedValue & "'"
        Try
            lblCantActaDisp.Text = fn.DevolverDatoQuery(query)
            If lblCantActaDisp.Text = "" Then
                lblCantActaDisp.Text = "0"
            End If
        Catch ex As Exception
            lblCantActaDisp.Text = "0"
        End Try

        query = "SELECT CANTIDAD_APS FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE ID='" & lblId.Text & "' AND ID_ACTA='" & cmbActa.SelectedValue & "'"
        txtCantActa.Text = Math.Round(CDec(fn.DevolverDatoQuery(query)) * 100) / 100
        If txtCantActa.Text = "" Then
            txtCantActa.Text = "0"
        End If
    End Sub

    Protected Sub btnAceptarEdit_Click(sender As Object, e As EventArgs) Handles btnAceptarEdit.Click

        Dim actividad As String = cmbActividad.SelectedValue
        Dim material As String = Server.HtmlDecode(cmbMaterial.SelectedValue)
        Dim articulo As String = Server.HtmlDecode(cmbArticulo.SelectedValue)
        Dim um As String = lblUM.Text
        Dim ume As String = lblUMEq.Text
        Dim cantActa As Decimal
        Dim idActa As String = cmbActa.SelectedValue

        Dim cant As Decimal
        cant = 1
        cant = com.validarNumero(txtCantidad.Text)


        Dim cantEq As Decimal = cant / CDec(lblFactor.Text)

        cantActa = com.validarNumero(txtCantActa.Text)


        Dim cantActaEq As Decimal = cantActa / CDec(lblFactor.Text)



        Dim id As String = lblId.Text
        query = "UPDATE SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA SET ACTIVIDAD='" & actividad & "', FASE='" & material & "', ARTICULO='" & articulo & "', UND_MED='" & um & "', UND_MED_E='" & ume & "',"
        query += " CANTIDAD='" & cantEq & "', CANTIDAD_APS='" & cant & "', CANT_ACTA='" & cantActaEq & "', CANT_ACTA_APS='" & cantActa & "', ID_ACTA='" & idActa & "',  CODIGO_CONTROL='M', UpdatedBy='" & lblUsuario.Text & "', RecordDate=GETDATE()  "
        query += " WHERE ID='" & id & "'"
        fn.ejecutarComandoSQL(query, lblMensaje, lblMensajeS)

        ocultarMostrarFormulario(True)
        llenarTablaDetallesPedido()
    End Sub

    Protected Sub btnCancelarEdit_Click(sender As Object, e As EventArgs) Handles btnCancelarEdit.Click
        ocultarMostrarFormulario(True)
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        liberarSolicitud()
        Response.Redirect("principal.aspx")

    End Sub
#Region "Ocultar"
    Protected Sub GrdVwSecciones_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(3).Visible = False
        e.Row.Cells(18).Visible = False
        e.Row.Cells(19).Visible = False
        e.Row.Cells(20).Visible = False

        e.Row.Cells(23).Visible = False

        e.Row.Cells(33).Visible = False
        e.Row.Cells(34).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(17).Visible = False
        e.Row.Cells(15).Visible = False

        If lblUMActual.Text = "Presupuesto" Then
            e.Row.Cells(24).Visible = False
            e.Row.Cells(25).Visible = False
            e.Row.Cells(26).Visible = False
            e.Row.Cells(27).Visible = False
            e.Row.Cells(28).Visible = False
            e.Row.Cells(29).Visible = False
            e.Row.Cells(30).Visible = False
            e.Row.Cells(31).Visible = False
            e.Row.Cells(32).Visible = False

            e.Row.Cells(35).Visible = False
        Else

            e.Row.Cells(7).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(11).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False
        End If


    End Sub


#End Region

#Region "Funciones WEB"
    <WebMethod()>
    Public Shared Function liberarSolicitud(usuario As String) As String
        Dim query As String
        Dim fn As New Funciones
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.ACCESO SET CODIGO = 'ND' WHERE USUARIO='" & usuario & "'"


        Try
            fn.ejecutarComandoSQL2(query)
        Catch ex As Exception
            Return query
        End Try

        Return "a"
    End Function
#End Region
End Class
