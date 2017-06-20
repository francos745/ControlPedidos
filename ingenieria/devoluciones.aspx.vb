


Imports System.Web.Services

Partial Class ingenieria_devoluciones
    Inherits System.Web.UI.Page
    Dim query As String
    Dim fn As New Funciones
    Dim com As New comun
    Sub llenarTablaConversion()

        query = " SELECT * , ISNULL((SELECT NRO FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B WHERE A.ACTIVIDAD=B.FASE),0) NUMERO FROM SOL_PEDIDOS.PEDIDOS.DEVOLUCION_DET A WHERE ESTADO_LIN='R'"

        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query

    End Sub

    'renderizar tabla
    Protected Sub dtgDetalle_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender


        query = " SELECT * , ISNULL((SELECT NRO FROM SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B WHERE A.ACTIVIDAD=B.FASE),0) NUMERO FROM SOL_PEDIDOS.PEDIDOS.DEVOLUCION_DET A WHERE ESTADO_LIN='R'"


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


    Protected Sub dtgDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound
        Dim proyecto As String = e.Row.Cells(0).Text ' PROYECTO
        Dim codControl As String = e.Row.Cells(14).Text ' CÓDIGO DE CONTROL

        If proyecto = "&nbsp;" Or proyecto = "PROYECTO" Then
            Dim Bq As Integer = 0

        Else
            If codControl = "B" Then
                e.Row.Font.Strikeout = True
                e.Row.Font.Overline = True
                e.Row.Font.Underline = True
                'e.Row.Font.Italic = True
                e.Row.Font.Size = 7
                e.Row.ForeColor = Drawing.Color.Red


            End If


        End If



    End Sub


    'LLENAR TABLA 
    Sub insertarRegistros()
        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.DEVOLUCION_LINEA (PROYECTO,ACTIVIDAD,MATERIAL,ARTICULO, CANTIDAD_APS,CANTIDAD,COD_DEV,CODIGO_CONTROL,UND_MED_E,UND_MED,ESTADO,RowPointer,REFERENCIA)"

        query += " SELECT DISTINCT PROYECTO,"
        query += " CONCAT(SUBSTRING(FASE,0,9),'.00')ACTIVIDAD,"
        query += " FASE MATERIAL,"
        query += " ARTICULO,"
        query += " CANTIDAD*FACTOR CANTIDAD_APS,"
        query += " CANTIDAD CANTIDAD,"
        query += " 'N/A'COD_DEV,"
        query += " 'C' CODIGO_CONTROL,"
        query += " UM_A UND_MED_E,"
        query += " UM_P UND_MED,"
        query += " 'R' ESTADO,"
        query += " RowPointer, REFERENCIA"
        query += "  FROM ("
        query += " SELECT D.PROYECTO,C.FASE,A.ARTICULO,E.UM_A,E.UM_P,(A.CANTIDAD*-1)CANTIDAD,B.REFERENCIA, E.FACTOR, A.RowPointer"
        query += " FROM VITALERP.VITALICIA.TRANSACCION_INV A "
        query += " LEFT JOIN VITALERP.VITALICIA.AUDIT_TRANS_INV B ON A.AUDIT_TRANS_INV=B.AUDIT_TRANS_INV "
        query += " LEFT JOIN VITALERP.VITALICIA.TRANS_INV_FASE_PY C ON A.AUDIT_TRANS_INV=C.AUDIT_TRANS_INV"
        query += " LEFT JOIN VITALERP.VITALICIA.FASE_PY D ON C.FASE=D.FASE"
        query += " LEFT JOIN SOL_PEDIDOS.PEDIDOS.CONVERSION E ON CONCAT(D.PROYECTO,A.ARTICULO,C.FASE) = CONCAT(E.PROYECTO,E.ARTICULO,E.FASE)"
        query += " WHERE A.NATURALEZA='E'"
        query += " AND A.CANTIDAD<0"
        query += " AND C.FASE IS NOT NULL"
        query += " AND E.UM_A IS NOT NULL"
        query += " AND E.FACTOR IS NOT NULL"
        query += " ) VISTA"
        query += " WHERE RowPointer NOT IN ("
        query += " SELECT B.RowPointer FROM SOL_PEDIDOS.PEDIDOS.DEVOLUCION_LINEA B)"


        fn.ejecutarComandoSQL2(query)
    End Sub



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'VALIDAMOS QUE EXISTA UN PROYECTO REGISTRADO
        If Session("usuario") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If


        If Not Page.IsPostBack Then
            llenarTablaConversion()
            insertarRegistros()
            lblUsuario.Text = Session("usuario")

        End If
    End Sub



#Region "Ocultar"
    Protected Sub dtgDetalle_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowCreated
        e.Row.Cells(6).Visible = True



    End Sub


#End Region

    Protected Sub btnCerrarSesion_Click(sender As Object, e As EventArgs) Handles btnCerrarSesion.Click
        Session.Clear()
        Response.Redirect("Ingreso.aspx")
    End Sub



    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("principal.aspx")

    End Sub


    <WebMethod()>
    Public Shared Function aprobarDevolucion(id As String, usuario As String) As String
        Dim query As String
        Dim fn As New Funciones
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.DEVOLUCION_LINEA SET ESTADO='A', UpdatedBy='" & usuario & "', RecordDate=GETDATE()  WHERE ID='" & id & "'"

        Try
            fn.ejecutarComandoSQL2(query)
        Catch ex As Exception
            Return query
        End Try

        Return "a"
    End Function

    <WebMethod()>
    Public Shared Function rechazarDevolucion(id As String, estado As String, usuario As String) As String
        Dim query As String
        Dim fn As New Funciones
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.DEVOLUCION_LINEA SET CODIGO_CONTROL='" & estado & "', UpdatedBy='" & usuario & "', RecordDate=GETDATE()  WHERE ID='" & id & "'"

        Try
            fn.ejecutarComandoSQL2(query)
        Catch ex As Exception
            Return query
        End Try

        Return "r"
    End Function

End Class
