Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class Funciones

#Region "VARIABLES"
    Public conexion As SqlConnection
    Public CN As String = Desencriptar(ObtenerCadenaConexion("conn"))
    Public enunciado As SqlCommand
    Public respuesta As SqlDataReader
    Public Con As String
#End Region

#Region "CIFRADO"
    'Esta función encripta una cadena de caracteres.
    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("softland") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("CleverLimitadaLaPazBolivia2015V1") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
    'Esta función desencripta una cadena encriptada, en caso de no encontrar una cadena válida, se retorna la cadena ingresada
    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("softland") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("CleverLimitadaLaPazBolivia2015V1") 'No se puede alterar la cantidad de caracteres pero si la clave
        Try
            Dim buffer() As Byte = Convert.FromBase64String(Input)
            Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
            des.Key = EncryptionKey
            des.IV = IV
            Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
        Catch ex As Exception
            Return Input
        End Try
    End Function
#End Region

#Region "CONEXION A BASE DE DATOS"

    Sub conectar()
        Try
            conexion = New SqlConnection(CN)
            conexion.Open()
        Catch ex As Exception
            MesgBox("No se conecto debido a: " + ex.ToString)
        End Try
    End Sub
    Public Sub Desconectar()
        conexion.Close()
        conexion = Nothing
    End Sub
#End Region

#Region "FUNCIONES"
    'con esta función verificamos que todos los materiales tengan uniidad de conversión
    Sub verificarUM(ByVal material As String, ByVal descMaterial As String, ByVal contMensaje As HtmlContainerControl, ByVal mensaje As Label)
        Dim query As String

        query = " DECLARE @valores VARCHAR(1000)"
        query += " SELECT @valores= COALESCE(@valores + ', ', '') + DESCRIPCION "
        query += " FROM (SELECT C.DESCRIPCION"
        query += " FROM ("
        query += " SELECT A.PROYECTO,A.ARTICULO,B.UM_A,B.UM_P,B.FACTOR"
        query += " FROM SOL_PEDIDOS.VITALICIA.FASE_DESGLOSE_PY A LEFT JOIN SOL_PEDIDOS.PEDIDOS.CONVERSION B"
        query += " ON A.PROYECTO=B.PROYECTO AND A.ARTICULO=B.ARTICULO"
        query += " WHERE A.FASE='" & material & "'"
        query += " GROUP BY A.PROYECTO,A.ARTICULO,B.UM_A,B.UM_P,B.FACTOR"
        query += " )VISTA JOIN SOL_PEDIDOS.VITALICIA.ARTICULO C ON VISTA.ARTICULO=C.ARTICULO"
        query += " WHERE FACTOR IS NULL)VISTA"
        query += " select  @valores as valores"

        If DevolverDatoQuery(query) <> "" Then
            contMensaje.Attributes("Style") = ""
            mensaje.Text = "Los siguientes artículos no tienen factor de conversión para el desglose de " & descMaterial & ": " & DevolverDatoQuery(query) & ". Esto puede ocasionar problemas en el funcionamiento general del módulo."
        Else

            contMensaje.Attributes("Style") = "display:none;"
            mensaje.Text = ""
        End If


    End Sub

    'Funcion que llena el contenido de un combobox a partir de el objeto ComboBox, un query y dos columnas, columna 2 ID, columna 1 NOMBRE
    Sub llenarComboBox(ByVal cb As DropDownList, ByVal query As String, ByVal columna As String, ByVal columna2 As String)
        conectar()
        cb.Items.Clear()
        Try
            enunciado = New SqlCommand(query, conexion)

            respuesta = enunciado.ExecuteReader

            While respuesta.Read
                ' Crea un nuevo Item
                Dim oItem As New ListItem(respuesta.Item(columna), respuesta.Item(columna2))
                ' Lo agrega a la colección de Items del DropDownList

                cb.Items.Add(oItem)
            End While
            Dim UItem As New ListItem("Otro...", "Otro...")
            If cb.ID.ToString <> "cmbUM" And cb.ID.ToString <> "cmbCodigoProyecto" And cb.ID.ToString <> "cmbBodegaDestino" Then
                cb.Items.Add(UItem)
            End If

            respuesta.Close()
        Catch ex As Exception
        End Try
        Desconectar()
    End Sub

    'Funcion que llena el contenido de un combobox a partir de el objeto ComboBox, un query y dos columnas
    Sub llenarComboBoxOpciones(ByVal cb As DropDownList, ByVal query As String, ByVal columna As String, ByVal columna2 As String, ByVal columna3 As String)
        conectar()
        cb.Items.Clear()
        Try
            enunciado = New SqlCommand(query, conexion)

            respuesta = enunciado.ExecuteReader

            While respuesta.Read
                ' Crea un nuevo Item
                Dim oItem As New ListItem(respuesta.Item(columna) & " --- " & respuesta.Item(columna3), respuesta.Item(columna2))
                ' Lo agrega a la colección de Items del DropDownList

                cb.Items.Add(oItem)
            End While
            Dim UItem As New ListItem("Otro...", "Otro...")
            cb.Items.Add(UItem)
            respuesta.Close()
        Catch ex As Exception
        End Try
        Desconectar()
    End Sub

    Sub llenarComboBox2(ByVal cb As DropDownList, ByVal query As String, ByVal columna As String, ByVal columna2 As String)
        conectar()
        cb.Items.Clear()
        Try
            enunciado = New SqlCommand(query, conexion)

            respuesta = enunciado.ExecuteReader

            While respuesta.Read
                ' Crea un nuevo Item
                Dim oItem As New ListItem(respuesta.Item(columna), respuesta.Item(columna2))
                ' Lo agrega a la colección de Items del DropDownList

                cb.Items.Add(oItem)
            End While

            respuesta.Close()
        Catch ex As Exception
        End Try
        Desconectar()
    End Sub

    Sub llenarComboBoxOpciones2(ByVal cb As DropDownList, ByVal query As String, ByVal columna As String, ByVal columna2 As String, ByVal columna3 As String)
        conectar()
        cb.Items.Clear()
        Try
            enunciado = New SqlCommand(query, conexion)

            respuesta = enunciado.ExecuteReader

            While respuesta.Read
                ' Crea un nuevo Item
                Dim oItem As New ListItem(respuesta.Item(columna) & " --- " & respuesta.Item(columna3), respuesta.Item(columna2))
                ' Lo agrega a la colección de Items del DropDownList

                cb.Items.Add(oItem)
            End While
            respuesta.Close()
        Catch ex As Exception
        End Try
        Desconectar()
    End Sub

    Sub llenarComboBoxOpcionesList(ByVal cb As ListBox, ByVal query As String, ByVal columna As String, ByVal columna2 As String, ByVal columna3 As String)
        conectar()
        cb.Items.Clear()
        Try
            enunciado = New SqlCommand(query, conexion)

            respuesta = enunciado.ExecuteReader

            While respuesta.Read
                ' Crea un nuevo Item
                Dim oItem As New ListItem(respuesta.Item(columna) & " --- " & respuesta.Item(columna3), respuesta.Item(columna2))
                ' Lo agrega a la colección de Items del DropDownList

                cb.Items.Add(oItem)
            End While
            respuesta.Close()
        Catch ex As Exception
        End Try
        Desconectar()
    End Sub

    Public Function separarCadena(ByRef cad As String, ByRef posicion As Boolean, ByRef separador As String) As String '
        Dim b As String
        Dim pos As Integer
        Dim largo As Integer = cad.Length

        pos = InStr(cad, separador)
        If pos <> 0 Then
            If posicion = False Then
                b = cad.Substring(0, pos - 1)
            Else
                b = cad.Substring(pos, largo - pos)
                b = b.Substring(separador.Length - 1, b.Length - separador.Length + 1)
            End If
        Else
            b = "Otro..."
        End If
        Return b
    End Function

    Public Function limpiarComillas(ByVal palabra As String) As String
        palabra = palabra.Replace("'", "''")

        Return palabra
    End Function

    'Retorna el primer registro de una consulta, utilizando un query como parametro
    Public Function DevolverDatoQuery(ByVal query As String) As String
        Try
            conectar()
            enunciado = New SqlCommand(query, conexion)
            Dim Devolver As String = ""
            Devolver = enunciado.ExecuteScalar()
            enunciado = Nothing
            Desconectar()
            Return Devolver
        Catch ex As Exception

            Return ""
        End Try
    End Function

    'Funcion que ejecuta un comando sql, como: Insert,Update,Delete, Etc.
    Sub ejecutarComandoSQL(ByVal query As String, ByVal lblmens As Label, ByVal lblmensS As HtmlContainerControl)


        Try
            conectar()
            enunciado = New SqlCommand(query, conexion)
            enunciado.CommandTimeout = 6000
            Dim t As Integer = enunciado.ExecuteNonQuery()
            Desconectar()
        Catch ex As Exception
            lblmens.Text = "Error en el comando para el módulo: " & ex.Message
            lblmensS.Attributes("class") = "alert alert-danger"
        End Try


    End Sub

    'Funcion que ejecuta un comando sql, como: Insert,Update,Delete, Etc.
    Sub ejecutarComandoSQL2(ByVal query As String)
        Try
            conectar()
            enunciado = New SqlCommand(query, conexion)
            enunciado.CommandTimeout = 6000
            Dim t As Integer = enunciado.ExecuteNonQuery()
            Desconectar()
        Catch ex As Exception
            MesgBox(" No se pudo ejecutar la instrucción. " + query + ex.Message)
        End Try


    End Sub

    'Funcion que ejecuta un comando sql, como: Insert,Update,Delete, Etc.
    Function ejecutarComandoSQL3(ByVal query As String) As Integer
        Try
            conectar()
            enunciado = New SqlCommand(query, conexion)
            enunciado.CommandTimeout = 6000
            Dim t As Integer = enunciado.ExecuteNonQuery()
            Desconectar()
        Catch ex As Exception
            Return -1
        End Try
        Return 1

    End Function

#End Region

#Region "CONEXION XML"

    Public Function ObtenerCadenaConexion(ByVal nombreCon As String) As String
        Dim Cadena As String = ConfigurationManager.ConnectionStrings(nombreCon).ConnectionString()
        Return Cadena
    End Function

#End Region

#Region "Funciones Personalizadas"
    Public Sub MesgBox(ByVal sMessage As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('" & sMessage & "')</SCRIPT>")
    End Sub
    'ENVIAMOS EL ID DE LA TABLA A_RENDIR
    Function generarNumeroRendicion(ByVal idR As String) As String
        Dim query As String
        Dim asiento As String
        Dim numero As String
        Dim contador As String

        query = " SELECT ASIENTO FROM FAR.FAR.A_RENDIR WHERE ID='" & idR & "'"
        asiento = DevolverDatoQuery(query)

        query = " SELECT COUNT(*) FROM ("
        query += " SELECT NRO_RENDICION FROM FAR.FAR.DOCS_FAR WHERE NRO_RENDICION LIKE '" & asiento & "%' AND ESTADO IN ('N','D') "
        query += " GROUP BY NRO_RENDICION"
        query += " )VISTA"

        contador = CStr(CInt(DevolverDatoQuery(query)) + 1)
        While Len(contador) < 2
            contador = "0" & contador
        End While
        numero = asiento & "-" & contador

        Return numero

    End Function

#End Region



End Class
