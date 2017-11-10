


Imports System.Data
    Imports System.Data.SqlClient
    Imports System.Web.Services

Partial Class ingenieria_precioUnitario
    Inherits System.Web.UI.Page
    Sub validarInicioSesion()
        If Session("usuario") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        validarInicioSesion()
        lblUsuario.Text = Session("usuario")
    End Sub

    <WebMethod()>
    Public Shared Function verCantidad(actividad As String) As String

        Dim query As String
        Dim fn As New Funciones

        query = "SELECT CANTIDAD FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT"
        query += " WHERE FASE='" & actividad & "'"
        Return fn.DevolverDatoQuery(query)
    End Function

    <WebMethod()>
    Public Shared Function verUnidadMedida(actividad As String) As String

        Dim query As String
        Dim fn As New Funciones

        query = "SELECT UM FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT"
        query += " WHERE FASE='" & actividad & "'"
        Return fn.DevolverDatoQuery(query)

    End Function

    <WebMethod()>
    Public Shared Function llenarComboProyectos() As List(Of ListItem)
        Dim query As String
        query = " SELECT PROYECTO FROM ("
        query += " SELECT PROYECTO"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO "
        query += " GROUP BY PROYECTO,NOM_MATERIAL,UM_P"
        query += " )VISTA"
        query += " GROUP BY PROYECTO"
        query += " ORDER BY COUNT('A') DESC"

        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim customers As New List(Of ListItem)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(New ListItem() With {
                          .Value = sdr("PROYECTO").ToString(),
                          .Text = sdr("PROYECTO").ToString()
                        })
                    End While
                End Using
                con.Close()
                Return customers
            End Using
        End Using
    End Function

    <WebMethod()>
    Public Shared Function llenarComboActividades(proyecto As String) As List(Of ListItem)
        Dim query As String
        query = " SELECT A.FASE FASE,A.NOMBRE NOMBRE,ISNULL(B.NRO ,0)NUMERO"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_PY A LEFT JOIN SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B"
        query += " ON A.FASE=B.FASE"
        query += " WHERE A.FASE NOT LIKE '%00.00'"
        query += " AND A.FASE LIKE '%00'"
        query += " AND A.PROYECTO='" & proyecto & "'"
        query += " ORDER BY A.FASE"

        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim customers As New List(Of ListItem)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(New ListItem() With {
                          .Value = sdr("FASE").ToString(),
                          .Text = sdr("NUMERO").ToString() + " --- " + sdr("NOMBRE").ToString()
                        })
                    End While
                End Using
                con.Close()
                Return customers
            End Using
        End Using
    End Function

    <WebMethod()>
    Public Shared Function obtenerTablaMateriales(actividad As String) As List(Of tabla)

        Dim query As String
        query = " SELECT MATERIAL,NOM_MATERIAL,UM_P,"
        query += " FORMAT((CANT_SOL_P),'##,0.00')CANT_SOL_P,FORMAT((CANT_PRESUP_P),'##,0.00')CANT_PRESUP_P,"
        query += " FORMAT((CANT_SOL_A),'##,0.00')CANT_SOL_A,FORMAT((CANT_PRESUP_A),'##,0.00')CANT_PRESUP_A,"
        query += " FORMAT((CANT_ACTAS_P),'##,0.00')CANT_ACTAS_P,FORMAT((CANT_PRESUP_ACTAS_P),'##,0.00')CANT_PRESUP_ACTAS_P,"
        query += " FORMAT((CANT_ACTAS_A),'##,0.00')CANT_ACTAS_A,FORMAT((CANT_PRESUP_ACTAS_A),'##,0.00')CANT_PRESUP_ACTAS_A,"
        query += " FORMAT((CANT_SOL_APROB_P),'##,0.00')CANT_SOL_APROB_P,FORMAT((CANT_DISP_P),'##,0.00')CANT_DISP_P,"
        query += " FORMAT((CANT_SOL_APROB_A),'##,0.00')CANT_SOL_APROB_A,FORMAT((CANT_DISP_A),'##,0.00')CANT_DISP_A,"
        query += " FORMAT((CANT_DEV_P),'##,0.00')CANT_DEV_P,FORMAT((CANT_DEV_A),'##,0.00')CANT_DEV_A, "
        query += " FORMAT((CANT_ACTAS_P)-(CANT_APROB_ACTAS_P),'##,0.00')CANT_APROB_ACTAS_P,"
        query += " FORMAT((CANT_ACTAS_A)-(CANT_APROB_ACTAS_A),'##,0.00')CANT_APROB_ACTAS_A,FORMAT((RENDIMIENTO_P),'##,0.00')RENDIMIENTO_P"
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO"
        query += " WHERE ACTIVIDAD='" & actividad & "'"


        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim lineas As New List(Of tabla)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        lineas.Add(New tabla() With {
                            .c1 = sdr("MATERIAL").ToString(),
                            .c2 = sdr("NOM_MATERIAL").ToString(),
                            .c3 = sdr("UM_P").ToString(),
                            .c4 = sdr("RENDIMIENTO_P").ToString(),
                            .c5 = sdr("CANT_SOL_P").ToString(),
                            .c6 = sdr("CANT_PRESUP_P").ToString(),
                            .c7 = sdr("CANT_ACTAS_P").ToString(),
                            .c8 = sdr("CANT_PRESUP_ACTAS_P").ToString(),
                            .c9 = sdr("CANT_DEV_P").ToString(),
                            .c10 = sdr("CANT_SOL_APROB_P").ToString(),
                            .c11 = sdr("CANT_APROB_ACTAS_P").ToString(),
                            .c12 = sdr("CANT_DISP_P").ToString()})


                    End While
                End Using
                con.Close()
                Return lineas
            End Using
        End Using

    End Function


    <WebMethod()>
    Public Shared Function obtenerTablaDetalles(proyecto As String, material As String) As List(Of tabla)

        Dim query As String

        material = HttpContext.Current.Server.HtmlDecode(material)

        query = " SELECT NOM_MATERIAL,UM_P,CODIGO_SOLICITUD,FORMAT((CANT_SOL_APROB_P),'##,0.00')CANT_SOL_APROB_P,FORMAT((CANT_APROB_ACTAS_P),'##,0.00')CANT_APROB_ACTAS_P,FORMAT((CANT_SOL_PEND_P),'##,0.00')CANT_SOL_PEND_P,FORMAT((CANT_SOL_RECH_P),'##,0.00')CANT_SOL_RECH_P,FORMAT(CANT_DISP_P2,'##,0.00')CANT_DISP_P,FECHA_APROBACION,COD_APROB_ACTAS "
        query += " ,CONCAT(SUBSTRING(FECHA_APROBACION,7,4),SUBSTRING(FECHA_APROBACION,4,2),LEFT(FECHA_APROBACION,2)) FECHA "
        query += " FROM SOL_PEDIDOS.PEDIDOS.PRECIO_UNITARIO_DET "
        query += " WHERE MATERIAL='" & material & "' "

        ' query += " AND CANT_SOL_APROB_P+CANT_APROB_ACTAS_P+CANT_SOL_PEND_P+CANT_SOL_RECH_P<>0"





        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim lineas As New List(Of tabla)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        lineas.Add(New tabla() With {
                            .c1 = sdr("NOM_MATERIAL").ToString(),
                            .c2 = sdr("UM_P").ToString(),
                            .c3 = sdr("CODIGO_SOLICITUD").ToString(),
                            .c4 = sdr("CANT_SOL_APROB_P").ToString(),
                            .c5 = sdr("CANT_APROB_ACTAS_P").ToString(),
                            .c6 = sdr("COD_APROB_ACTAS").ToString(),
                            .c7 = sdr("CANT_SOL_PEND_P").ToString(),
                            .c8 = sdr("CANT_SOL_RECH_P").ToString(),
                            .c9 = sdr("CANT_DISP_P").ToString(),
                            .c10 = sdr("FECHA_APROBACION").ToString(),
                            .c11 = sdr("FECHA").ToString()})



                    End While
                End Using
                con.Close()
                Return lineas
            End Using
        End Using

    End Function


    <WebMethod()>
    Public Shared Function obtenerTablaDevoluciones(proyecto As String, material As String) As List(Of tabla)
        material = HttpContext.Current.Server.HtmlDecode(material)
        Dim query As String
        query = "SELECT FECHA,PROYECTO,NOM_ACTIVIDAD,NOM_MATERIAL,UM_P,FORMAT(CANT_P,'##,0.00')CANT_P FROM SOL_PEDIDOS.PEDIDOS.DEVOLUCION_DET WHERE ESTADO_LIN='A' AND COD_CONT_LINEA<>'B' AND PROYECTO='" & proyecto & "'  AND NOM_MATERIAL='" & material & "' "


        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim lineas As New List(Of tabla)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        lineas.Add(New tabla() With {
                            .c1 = sdr("FECHA").ToString(),
                            .c2 = sdr("PROYECTO").ToString(),
                            .c3 = sdr("NOM_ACTIVIDAD").ToString(),
                            .c4 = sdr("NOM_MATERIAL").ToString(),
                            .c5 = sdr("UM_P").ToString(),
                            .c6 = sdr("CANT_P").ToString()})
                    End While
                End Using
                con.Close()
                Return lineas
            End Using
        End Using

    End Function

    <WebMethod()>
    Public Shared Function obtenerTablaSolicitudes(codigoSolicitud As String) As List(Of tabla)

        Dim query As String
        query = " SELECT NUMERO,NOM_ACTIVIDAD,NOM_MATERIAL,NOM_ARTICULO,UM_P,"
        query += " FORMAT((CANT_SOL_P),'##,0.00')CANT_SOL_P,"
        query += " FORMAT((CANT_PRESUP_P),'##,0.00')CANT_PRESUP_P,"
        query += " FORMAT((CANT_ACTAS_P),'##,0.00')CANT_ACTAS_P,"
        query += " FORMAT((CANT_SOL_APROB_P),'##,0.00')CANT_SOL_APROB_P,"
        query += " FORMAT((CANT_APROB_ACTAS_P),'##,0.00')CANT_APROB_ACTAS_P,"
        query += " FORMAT((CANT_DISP2_P),'##,0.00')CANT_DISP2_P,"
        query += " FORMAT((CANT_SOL_PEND_P),'##,0.00')CANT_SOL_PEND_P,"
        query += " ESTADO_LIN "
        query += " FROM SOL_PEDIDOS.PEDIDOS.SOL_ING_DET WHERE COD_SOL='" & codigoSolicitud & "' AND COD_CONT_LINEA<>'B'"

        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim lineas As New List(Of tabla)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        lineas.Add(New tabla() With {
                            .c1 = sdr("NUMERO").ToString(),
                            .c2 = sdr("NOM_ACTIVIDAD").ToString(),
                            .c3 = sdr("NOM_MATERIAL").ToString(),
                            .c4 = sdr("NOM_ARTICULO").ToString(),
                            .c5 = sdr("UM_P").ToString(),
                            .c6 = sdr("CANT_SOL_P").ToString(),
                            .c7 = sdr("CANT_PRESUP_P").ToString(),
                            .c8 = sdr("CANT_ACTAS_P").ToString(),
                            .c9 = sdr("CANT_SOL_APROB_P").ToString(),
                            .c10 = sdr("CANT_APROB_ACTAS_P").ToString(),
                            .c11 = sdr("CANT_DISP2_P").ToString(),
                            .c12 = sdr("CANT_SOL_PEND_P").ToString(),
                            .c13 = sdr("ESTADO_LIN").ToString()})

                    End While
                End Using
                con.Close()
                Return lineas
            End Using
        End Using

    End Function

    <WebMethod()>
    Public Shared Function obtenerTablaActas(codigoSolicitud As String) As List(Of tabla)

        Dim query As String
        query = " SELECT A.CODIGO_ACTA COD_ACTA,"

        query += " (SELECT TOP 1 CODIGO_ACTA_ING "
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA) ACTA_ING,"

        query += " ISNULL((SELECT B.NOMBRE  FROM SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE B.FASE=CONCAT(SUBSTRING(A.FASE,0,9),'.00')),'SIN FASE')NOM_ACTIVIDAD,"

        query += " ISNULL((SELECT B.NOMBRE  FROM SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE B.FASE=A.FASE),'SIN FASE')NOM_MATERIAL,"

        query += " FORMAT(CANTIDAD_APS,'##,0.00') CANT_P,"

        query += " CANTIDAD CANT_A,"

        query += "(SELECT TOP 1 FECHA_ACTA_ING "
        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_CABECERA B"
        query += " WHERE A.CODIGO_ACTA=B.CODIGO_ACTA) FECHA"

        query += " FROM SOL_PEDIDOS.PEDIDOS.ACTAS_LINEA A"
        query += " WHERE A.ID IN (SELECT ID_ACTA FROM SOL_PEDIDOS.PEDIDOS.SOLICITUD_ING_LINEA WHERE CODIGO_SOLICITUD='" & codigoSolicitud & "')"
        query += " AND CODIGO_CONTROL<>'B'"


        Dim constr As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand(query)
                Dim lineas As New List(Of tabla)()
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        lineas.Add(New tabla() With {
                            .c1 = sdr("COD_ACTA").ToString(),
                            .c2 = sdr("ACTA_ING").ToString(),
                            .c3 = sdr("NOM_ACTIVIDAD").ToString(),
                            .c4 = sdr("NOM_MATERIAL").ToString(),
                            .c5 = sdr("CANT_P").ToString(),
                            .c6 = sdr("FECHA").ToString(),
                            .c7 = sdr("CANT_A").ToString(),
                            .c8 = sdr("FECHA").ToString()})




                    End While
                End Using
                con.Close()
                Return lineas
            End Using
        End Using

    End Function

End Class
