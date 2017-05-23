
Imports System.Web.Services

Partial Class logistica_conversionUM
    Inherits System.Web.UI.Page
    Dim query As String
    Dim fn As New Funciones
    Dim com As New comun
    Sub llenarTablaConversion()

        query = " SELECT DISTINCT PROYECTO,ARTICULO,DESCRIPCION,UM_P,UM_A,FACTOR,"
        query += " (SELECT TOP 1 ORDEN_CAMBIO FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B"
        query += " WHERE A.PROYECTO=B.PROYECTO"
        query += " AND A.ARTICULO=B.ARTICULO"
        query += " ORDER BY ORDEN_CAMBIO)OC"

        query += " FROM SOL_PEDIDOS.PEDIDOS.CONVERSION A"
        query += lblOC.Text
        query += " ORDER BY ARTICULO"
        Detalles.ConnectionString = fn.ObtenerCadenaConexion("conn")
        Detalles.SelectCommand = query

    End Sub

    'renderizar tabla
    Protected Sub dtgDetalle_PreRender(sender As Object, e As EventArgs) Handles dtgDetalle.PreRender


        query = " SELECT DISTINCT PROYECTO,ARTICULO,DESCRIPCION,UM_P,UM_A,FACTOR,"
        query += " (SELECT TOP 1 ORDEN_CAMBIO FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B"
        query += " WHERE A.PROYECTO=B.PROYECTO"
        query += " AND A.ARTICULO=B.ARTICULO"
        query += " ORDER BY ORDEN_CAMBIO)OC"

        query += " FROM SOL_PEDIDOS.PEDIDOS.CONVERSION A"
        query += lblOC.Text
        query += " ORDER BY ARTICULO"

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

    'LLENAR TABLA 
    Sub insertarRegistros()
        query = " INSERT INTO SOL_PEDIDOS.PEDIDOS.CONVERSION (PROYECTO,FASE,ARTICULO,DESCRIPCION,UM_P, UM_A,ORDEN_CAMBIO,RowPointer)"
        query += " SELECT A.PROYECTO, A.FASE, B.ARTICULO,B.DESCRIPCION,B.UNIDAD_ALMACEN,B.UNIDAD_ALMACEN,A.ORDEN_CAMBIO, A.RowPointer"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY A"
        query += " LEFT JOIN SOL_PEDIDOS.VITALICIA.ARTICULO B ON A.ARTICULO=B.ARTICULO"
        query += " LEFT JOIN SOL_PEDIDOS.VITALICIA.FASE_PY C ON A.FASE=C.FASE"
        query += " WHERE A.RowPointer NOT IN ("
        query += " SELECT B.RowPointer FROM SOL_PEDIDOS.PEDIDOS.CONVERSION B)"
        fn.ejecutarComandoSQL2(query)
    End Sub
    Function contarCifras(ByVal numero As Integer) As Integer

        Dim cifras As Integer

        While numero <> 0
            numero = numero Mod 10
            cifras += 1

        End While
        Return cifras
    End Function

    Function quitarRepetidos(ByVal numero As Integer, ByVal cifra As Integer) As Integer
        Dim aux As Integer
        Dim cont As Integer = 0
        Dim n As Integer
        While numero <> 0
            aux = numero Mod 10
            If cifra = aux Then
                n = n + 0
            Else
                n = n + (aux * 10 ^ cont)
                cont += 1
            End If
            numero = CInt(numero / 10)
        End While
        Return n

    End Function


    Function comprimir(ByVal numero As Integer) As Boolean
        Dim res As Boolean = False

        Dim primerCifra As Integer
        Dim n As Integer = 0
        Dim contador As Integer = 0
        While numero <> 0
            primerCifra = CInt(numero / 10 ^ (contarCifras(numero) - 1))
            numero = quitarRepetidos(numero, primerCifra)

            n = (n * 10 ^ contador) + primerCifra
        End While

        Return n
    End Function


    'Function problema(ByVal numero As Integer) As Integer
    '    Dim aux As Integer
    '    Dim aux2 As Integer = numero
    '    If verificarRepetidos(numero) Then

    '    End If


    'End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'VALIDAMOS QUE EXISTA UN PROYECTO REGISTRADO
        MsgBox(comprimir(44444445))
        If Session("almacenero") = "" Then
            Response.Redirect("Ingreso.aspx")
        End If
        If Session("modulo") = "P" Then
            divBoton.Attributes("Style") = ""
            lblOC.Text = " WHERE ORDEN_CAMBIO=1"
        Else
            divBoton.Attributes("Style") = "display:none;"
            lblOC.Text = " WHERE ORDEN_CAMBIO<>0"
        End If

        If Not Page.IsPostBack Then
            llenarTablaConversion()
            com.llenarComboUM(cmbUM, "", "UNIDAD_MEDIDA", "UNIDAD_MEDIDA", "", 1)
            insertarRegistros()
            lblUsuario.Text = Session("almacenero")

        End If
    End Sub

    Protected Sub dtgDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dtgDetalle.RowDataBound
        Dim OC As String = e.Row.Cells(6).Text ' CANTIDAD_DISPONIBLE

        If OC = "1" And Session("modulo") <> "P" Then
            e.Row.BackColor = Drawing.Color.Yellow
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

    Protected Sub btnExcel_click(sender As Object, e As EventArgs) Handles btnExcel.Click

        Dim sUrl As String = "cargarExcelConv.aspx"
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & sUrl & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        Response.Write(sScript)


    End Sub

    Protected Sub btnSinc_Click(sender As Object, e As EventArgs) Handles btnSinc.Click
        query = " EXEC PEDIDOS.SINCRONIZAR"
        Try
            fn.ejecutarComandoSQL2(query)
            insertarRegistros() '--0014-0081-0003
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("principal.aspx")

    End Sub

    <WebMethod()>
    Public Shared Function obtenerSugerencia(articulo As String) As String
        Dim query As String
        Dim sugerencias As String
        Dim fn As New Funciones
        query = " DECLARE @valores VARCHAR(1000)"
        query += " SELECT @valores= COALESCE(@valores + ', ', '') + CONVERT(VARCHAR(30),FACTOR)+' ' + UM_P"
        query += " FROM (SELECT DISTINCT convert(numeric(9,2),round(FACTOR,2,1)) FACTOR, UM_P  FROM SOL_PEDIDOS.PEDIDOS.CONVERSION"
        query += " WHERE FACTOR IS NOT NULL"
        query += " AND ARTICULO='" & articulo & "')VISTA"
        query += " select  ISNULL(@valores,'Sin coincidencias') as valores"

        sugerencias = "Factores sugeridos: " & fn.DevolverDatoQuery(query)

        Return sugerencias
    End Function



    <WebMethod()>
    Public Shared Function actualizaFactores(factor As Double, um As String, proyecto As String, articulo As String, usuario As String, oc As String) As String
        Dim query As String
        Dim fn As New Funciones
        query = " UPDATE SOL_PEDIDOS.PEDIDOS.CONVERSION SET FACTOR='" & factor & "', UM_P='" & um & "', UpdatedBy='" & usuario & "', RecordDate=GETDATE()"
        query += oc + " AND PROYECTO='" & proyecto & "' AND ARTICULO='" & articulo & "'"
        Try
            fn.ejecutarComandoSQL2(query)
        Catch ex As Exception
            Return query
        End Try

        Return query
    End Function

End Class
