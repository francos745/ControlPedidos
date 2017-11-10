Imports Microsoft.VisualBasic

Public Class comun
    Private query As String
    Dim fn As New Funciones
    Private tiempo As Integer = 5
    'LAS COLUMNAS DISPONIBLES EL COMBO DE ACTIVIDAD SON: NUMERO, FASE Y NOMBRE 
    '---- columna 1 descripcion principal
    '---- columna 2 codigo del combo box
    '---- columna 3 descripcion adicional
    Function obtenerTiempo() As Integer
        Return tiempo
    End Function

    Sub llenarComboActividad(ByVal cmb As DropDownList, ByVal proyecto As String, ByVal columna1 As String, ByVal columna2 As String, ByVal columna3 As String, ByVal opcion As Integer)

        query = " SELECT A.FASE,A.NOMBRE,ISNULL(B.NRO ,0)NUMERO"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_PY A LEFT JOIN SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT B"
        query += " ON A.FASE=B.FASE"
        query += " WHERE A.FASE NOT LIKE '%00.00'"
        query += " AND A.FASE LIKE '%00'"
        query += " AND A.PROYECTO='" & proyecto & "'"
        query += " ORDER BY A.FASE"
        If opcion = 1 Then
            fn.llenarComboBox(cmb, query, columna1, columna2)
        Else
            fn.llenarComboBoxOpciones(cmb, query, columna1, columna2, columna3)
        End If
    End Sub

    Sub llenarComboMaterial(ByVal cmb As DropDownList, ByVal act As String, ByVal columna1 As String, ByVal columna2 As String, ByVal columna3 As String, ByVal opcion As Integer)

        Dim actividad As String
        Try
            actividad = act.Substring(0, 8)
        Catch ex As Exception
            actividad = "Otro..."
        End Try


        query = " SELECT A.FASE FASE,A.NOMBRE NOMBRE"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_PY A "
        query += " WHERE A.FASE NOT LIKE '%.00'"
        query += " AND A.TIPO='A'"
        query += " AND A.FASE LIKE '" & actividad & "%'"
        query += " ORDER BY A.NOMBRE"

        If opcion = 1 Then
            fn.llenarComboBox(cmb, query, columna1, columna2)
        Else
            fn.llenarComboBoxOpciones(cmb, query, columna1, columna2, columna3)
        End If

    End Sub

    Sub llenarComboArticulo(ByVal cmb As DropDownList, ByVal material As String, ByVal columna1 As String, ByVal columna2 As String, ByVal columna3 As String, ByVal opcion As Integer)

        query = " SELECT DISTINCT A.ARTICULO ARTICULO, B.DESCRIPCION NOMBRE"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY A, SOL_PEDIDOS.VITALICIA.ARTICULO B "
        query += " WHERE FASE = '" & material & "' "
        query += " And A.ARTICULO = B.ARTICULO ORDER BY DESCRIPCION"
        If opcion = 1 Then
            fn.llenarComboBox(cmb, query, columna1, columna2)
        Else
            fn.llenarComboBoxOpciones(cmb, query, columna1, columna2, columna3)
        End If

    End Sub
    Sub llenarComboArticuloAux(ByVal cmb As DropDownList, ByVal material As String, ByVal columna1 As String, ByVal columna2 As String, ByVal columna3 As String, ByVal opcion As Integer)
        query = " SELECT DISTINCT B.ARTICULO ARTICULO, B.DESCRIPCION NOMBRE FROM SOL_PEDIDOS.VITALICIA.ARTICULO B ORDER BY DESCRIPCION"

        If opcion = 1 Then
            fn.llenarComboBox(cmb, query, columna1, columna2)
        Else
            fn.llenarComboBoxOpciones(cmb, query, columna1, columna2, columna3)
        End If
    End Sub

    Sub llenarComboUM(ByVal cmb As DropDownList, ByVal codigo As String, ByVal columna1 As String, ByVal columna2 As String, ByVal columna3 As String, ByVal opcion As Integer)
        query = " SELECT UNIDAD_MEDIDA,DESCRIPCION FROM SOL_PEDIDOS.VITALICIA.UNIDAD_DE_MEDIDA UNION ALL SELECT '--','--'  ORDER BY DESCRIPCION "

        If opcion = 1 Then
            fn.llenarComboBox2(cmb, query, columna1, columna2)
        Else
            fn.llenarComboBoxOpciones2(cmb, query, columna1, columna2, columna3)
        End If
    End Sub

    Function obtenerUMAlmacen(ByVal articulo As String, ByVal umDef As String) As String
        Dim um As String
        query = "SELECT TOP 1 ISNULL((SELECT UNIDAD_ALMACEN FROM SOL_PEDIDOS.VITALICIA.ARTICULO WHERE ARTICULO='" & articulo & "'),'')UM"
        um = fn.DevolverDatoQuery(query)
        If um = "" Then
            um = umDef
        End If
        Return um
    End Function

    Function obtenerUMPresupuesto(ByVal proyecto As String, ByVal articulo As String, ByVal umDef As String) As String
        Dim um As String
        query = "SELECT  TOP 1  ISNULL((SELECT TOP 1 UM_P FROM SOL_PEDIDOS.PEDIDOS.CONVERSION WHERE PROYECTO= '" & proyecto & "' AND ARTICULO='" & articulo & "'), "
        query += " ISNULL((SELECT UNIDAD_ALMACEN FROM SOL_PEDIDOS.VITALICIA.ARTICULO B WHERE B.ARTICULO='" & articulo & "' ),''))UM "

        um = fn.DevolverDatoQuery(query)

        If um = "" Then
            um = umDef
        End If
        Return um
    End Function

    Function obtenerFactorConversion(ByVal proyecto As String, ByVal articulo As String) As Double
        Dim factor As Double

        query = "SELECT  TOP 1  ISNULL((SELECT TOP 1 FACTOR FROM SOL_PEDIDOS.PEDIDOS.CONVERSION WHERE PROYECTO= '" & proyecto & "' AND ARTICULO='" & articulo & "' AND FACTOR IS NOT NULL ORDER BY ORDEN_CAMBIO), 1)UM "

        Try
            factor = CDbl(fn.DevolverDatoQuery(query))
        Catch ex As Exception
            factor = 1

        End Try


        Return factor
    End Function

    Function validarNumero(ByVal numero As String) As Double
        Dim cant As Double
        'Cambiamos el punto decimal por coma para hacer las operaciones en el sistema
        numero = RTrim(LTrim(numero))
        numero = Replace(numero, ",", "")
        numero = Replace(numero, ".", ",")
        numero = Replace(numero, " ", "")
        'convertimos la cantidad en tipo Double
        Try
            cant = CDbl(numero)
        Catch ex As Exception
            cant = -1
        End Try
        'verificamos que las cantidades coincidan, si no coinciden cambiamos el caracter decimal de coma por punto
        If numero <> cant.ToString Then
            numero = Replace(numero, ",", ".")

            Try
                cant = CDbl(numero)
            Catch ex As Exception
                cant = -1
            End Try
        End If
        Return cant
    End Function

    Function contarPixeles(texto1 As String) As Integer
        Dim aux1 As Integer = 0
        For Each val As Char In texto1.ToCharArray()
            '7+ 9+9+9+9+4+7+4+7+8+7+4+9+9+4+7+6+7+9
            Dim a As Integer = 1
            Select Case val
                Case " "
                    aux1 += 5 + a
                Case "I", "i", "j", "l"
                    aux1 += 4 + a
                Case "J", "f", "r", "t", "|", "-"
                    aux1 += 5 + a
                Case "s", "z", ")", "(", "°", "_"
                    aux1 += 6 + a
                Case "F", "L", "S", "Z", "c", "g", "k", "v", "x", "y", "/", "."
                    aux1 += 6 + a
                Case "E", "K", "P", "T", "X", "Y", "a", "b", "d", "e", "h", "n", "p", "q", "u", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                    aux1 += 7 + a
                Case "A", "B", "C", "R", "V", "o"
                    aux1 += 8 + a
                Case "D", "G", "H", "="
                    aux1 += 9 + a
                Case "N", "O", "Q", "U"
                    aux1 += 9 + a
                Case "w"
                    aux1 += 10 + a
                Case "m"
                    aux1 += 11 + a
                Case "M"
                    aux1 += 12 + a
                Case "W"
                    aux1 += 12 + a
                Case Else
                    aux1 += 10 + a
            End Select
        Next

        Return aux1
    End Function

    Function quitarCeroFinal(ByVal numero As String) As String
        numero = Trim(numero)
        While Right(numero, 1) = "0"
            numero = Left(numero, Len(numero) - 1)
        End While
        Return numero
    End Function

    Sub abrirNuevaVentana(ByVal url As String)
        Dim sScript As String = "<script language =javascript> "
        sScript += "window.open('" & url & "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=300,left=100,top=100');"
        sScript += "</script> "
        HttpContext.Current.Response.Write(sScript)
    End Sub


    Sub sincronizar()

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.UNIDAD_DE_MEDIDA"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.UNIDAD_DE_MEDIDA"
        query += " FROM VITALERP.VITALICIA.UNIDAD_DE_MEDIDA"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.ARTICULO"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.ARTICULO"
        query += " FROM VITALERP.VITALICIA.ARTICULO"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.BODEGA"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.BODEGA"
        query += " FROM VITALERP.VITALICIA.BODEGA"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.CONVERSION_UM"
        query += " SELECT  *"
        query += " INTO SOL_PEDIDOS.VITALICIA.CONVERSION_UM"
        query += " FROM VITALERP.VITALICIA.CONVERSION_UM"
        query += " WHERE presupuesto NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY"
        query += " FROM VITALERP.VITALICIA.FASE_DESGLOSE_PY"
        query += " WHERE PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        query += " AND ORDEN_CAMBIO='1'"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY_LOG"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY_LOG"
        query += " FROM VITALERP.VITALICIA.FASE_DESGLOSE_PY"
        query += " WHERE PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.FASE_PRESUP_PY"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.FASE_PRESUP_PY"
        query += " FROM VITALERP.VITALICIA.FASE_PRESUP_PY"
        query += " WHERE PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        query += " AND ORDEN_CAMBIO='1'"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.FASE_PY"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.FASE_PY"
        query += " FROM VITALERP.VITALICIA.FASE_PY"
        query += " WHERE PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.LOCALIZACION"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.LOCALIZACION"
        query += " FROM VITALERP.VITALICIA.LOCALIZACION"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.PROY_FASE_CANT"
        query += " FROM VITALERP.VITALICIA.PROY_FASE_CANT"
        query += " WHERE PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.PROYECTO_PY"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.PROYECTO_PY"
        query += " FROM VITALERP.VITALICIA.PROYECTO_PY"
        query += " WHERE PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " DROP TABLE SOL_PEDIDOS.VITALICIA.U_BODEGA_RUTA"
        query += " SELECT *"
        query += " INTO SOL_PEDIDOS.VITALICIA.U_BODEGA_RUTA"
        query += " FROM VITALERP.VITALICIA.U_BODEGA_RUTA"
        fn.ejecutarComandoSQL2(query)

        query = " INSERT INTO [SOL_PEDIDOS].[PEDIDOS].[ACCESOS] (PROYECTO)"
        query += " SELECT PROYECTO  FROM [VITALERP].[VITALICIA].[PROYECTO_PY] B WHERE PROYECTO NOT IN ("
        query += " SELECT A.PROYECTO FROM [SOL_PEDIDOS].[PEDIDOS].[ACCESOS] A,[VITALERP].[VITALICIA].[PROYECTO_PY] B"
        query += " WHERE A.PROYECTO=B.PROYECTO "
        query += " )"
        query += " AND PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " UPDATE SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY"
        query += " SET CANTIDAD=0"
        query += " WHERE FASE IN (SELECT FASE FROM "
        query += " SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE U_PRESUPUESTADO='NO')"
        query += " AND PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

        query = " UPDATE SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY"
        query += " SET CANTIDAD=0"
        query += " WHERE FASE IN (SELECT FASE FROM "
        query += " SOL_PEDIDOS.VITALICIA.FASE_PY B"
        query += " WHERE U_PRESUPUESTADO IS NULL)"
        query += " AND PROYECTO NOT IN ('EDIFICIO_ALMENDROS_I','EDIFICIO_ALMENDROS_II','EDIFICIO_TERRA_II','EDIFICIO_TERRA')"
        fn.ejecutarComandoSQL2(query)

    End Sub

End Class
