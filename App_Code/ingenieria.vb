Imports Microsoft.VisualBasic
Imports Docs.Excel
Imports System.Data
Imports System.Data.SqlClient

Public Class ingenieria
    Dim fn As New Funciones
    Dim query As String


    Sub generarExcelCuadroCom(ByVal tabla As GridView, ByVal tabla2 As GridView, ByVal codProyecto As String)

        Dim Wbook As New ExcelWorkbook()
        Dim plantilla, ext, filadatos, columnadatos, rutaPlantilla, Archivo As String
        Dim k As Integer = 0
        Dim ancho0 As Integer = 0
        Dim ancho1 As Integer = 0
        Dim aux1 As Integer = 0
        Dim aux2 As Integer = 0

        Dim texto1 As String = ""
        Dim texto2 As String = ""
        plantilla = "Comunicacion"
        ext = ".xls"
        filadatos = "3"
        columnadatos = "0"
        rutaPlantilla = "C:\Plantillas Control de Pedidos\"
        Wbook = ExcelWorkbook.ReadXLS(rutaPlantilla & plantilla & ext)
        Dim Wsheet As ExcelWorksheet = Wbook.Worksheets(0)

        filadatos = filadatos - 1
        Dim Extension, ArchivoFecha As String
        Dim NombrePlantilla As String = ""

        Archivo = "Cuadro de Comunicacion " & codProyecto
        Extension = ".xls"
        ArchivoFecha = Archivo & "_" & Now.ToString("yyyyMMddhhmmss")
        ArchivoFecha += Extension
        Archivo += Extension


        Dim WorksheetName As String = Wbook.Worksheets(0).Name
        'GENERAMOS LOS MATERIALES NO PRESUPUESTADOS
        Wsheet.Cells(filadatos, 0).Value = "MATERIALES NO PRESUPUESTADOS"
        Wsheet.Cells(filadatos, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos, 0).Style.Font.Size = 14
        Wsheet.Cells(filadatos + 1, 0).Value = "ACTIVIDAD"
        Wsheet.Cells(filadatos + 1, 0).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 0).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 1).Value = "DESCRIPCIÓN"
        Wsheet.Cells(filadatos + 1, 1).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 1).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 1).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 2).Value = "UNIDAD"
        Wsheet.Cells(filadatos + 1, 2).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 2).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 2).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 3).Value = "CANTIDAD"
        Wsheet.Cells(filadatos + 1, 3).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 3).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 3).Style.Font.Bold = True
        filadatos = filadatos + 2

        For i As Integer = 0 To tabla.Rows.Count - 1
            If (HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(5).Text)) = "NO" Then
                Wsheet.Cells(i - k + filadatos, 0).Style.WrapText = True
                Wsheet.Cells(i - k + filadatos, 1).Style.WrapText = True
                Wsheet.Cells(i - k + filadatos, 2).Style.WrapText = True
                Wsheet.Cells(i - k + filadatos, 3).Style.WrapText = True
                'If tabla.Rows(i).Cells(1).Text.Length > 40 Then
                '    Wsheet.Rows(i - k + filadatos).Height = 40
                'Else
                '    Wsheet.Rows(i - k + filadatos).Height = 16
                'End If
                aux1 = 0
                aux2 = 0

                texto1 = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text)
                texto2 = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text)
                For Each val As Char In texto1.ToCharArray()

                    Select Case val
                        Case " "
                            aux1 += 4
                        Case "I", "i", "j", "l"
                            aux1 += 5
                        Case "J", "f", "r", "t", "|", "-"
                            aux1 += 8
                        Case "s", "z", ")", "(", "°", "_"
                            aux1 += 7
                        Case "F", "L", "S", "Z", "c", "g", "k", "v", "x", "y"
                            aux1 += 8
                        Case "E", "K", "P", "T", "X", "Y", "a", "b", "d", "e", "h", "n", "p", "q", "u", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                            aux1 += 9
                        Case "A", "B", "C", "R", "V", "o"
                            aux1 += 10
                        Case "D", "G", "H"
                            aux1 += 11
                        Case "N", "O", "Q", "U"
                            aux1 += 12
                        Case "w"
                            aux1 += 13
                        Case "m"
                            aux1 += 14
                        Case "M"
                            aux1 += 15
                        Case "W"
                            aux1 += 16
                        Case Else
                            aux1 += 10
                    End Select
                Next

                For Each val As Char In texto2.ToCharArray()

                    Select Case val
                        Case " "
                            aux2 += 4
                        Case "I", "i", "j", "l"
                            aux2 += 5
                        Case "J", "f", "r", "t", "|", "-"
                            aux2 += 8
                        Case "s", "z", ")", "(", "°", "_"
                            aux2 += 7
                        Case "F", "L", "S", "Z", "c", "g", "k", "v", "x", "y"
                            aux2 += 8
                        Case "E", "K", "P", "T", "X", "Y", "a", "b", "d", "e", "h", "n", "p", "q", "u", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                            aux2 += 9
                        Case "A", "B", "C", "R", "V", "o"
                            aux2 += 10
                        Case "D", "G", "H"
                            aux2 += 11
                        Case "N", "O", "Q", "U"
                            aux2 += 12
                        Case "w"
                            aux2 += 13
                        Case "m"
                            aux2 += 14
                        Case "M"
                            aux2 += 15
                        Case "W"
                            aux2 += 16
                        Case Else
                            aux2 += 10
                    End Select

                Next



                If aux1 > ancho0 Then
                    ancho0 = aux1
                End If

                If aux2 > ancho1 Then
                    ancho1 = aux2
                End If



                Wsheet.Cells(i - k + filadatos, 0).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text) 'ancho0 & " ancho0" ' 
                Wsheet.Cells(i - k + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text) 'ancho1 & " ancho1" '
                Wsheet.Cells(i - k + filadatos, 2).Style.VerticalAlignment = TypeOfVAlignment.Center
                    Wsheet.Cells(i - k + filadatos, 2).Style.HorizontalAlignment = TypeOfHAlignment.Center
                    Wsheet.Cells(i - k + filadatos, 2).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(3).Text)
                    Wsheet.Cells(i - k + filadatos, 3).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(4).Text))

                    Wsheet.Cells("A" & (i - k + filadatos).ToString & ":D" & (i - k + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
                Else
                    k += 1
            End If
        Next
        Wsheet.Cells("A" & (filadatos - k + tabla.Rows.Count).ToString & ":D" & (filadatos - k + tabla.Rows.Count).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)


        filadatos = filadatos + tabla.Rows.Count + 2 - k

        'GENERAMOS LOS MATERIALES PRESUPUESTADOS

        Wsheet.Cells(filadatos, 0).Value = "MATERIALES PRESUPUESTADOS"
        Wsheet.Cells(filadatos, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos, 0).Style.Font.Size = 14

        Wsheet.Cells(filadatos + 1, 0).Value = "ACTIVIDAD"
        Wsheet.Cells(filadatos + 1, 0).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 0).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 1).Value = "DESCRIPCIÓN"
        Wsheet.Cells(filadatos + 1, 1).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 1).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 1).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 2).Value = "UNIDAD"
        Wsheet.Cells(filadatos + 1, 2).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 2).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 2).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 3).Value = "CANTIDAD"
        Wsheet.Cells(filadatos + 1, 3).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos + 1, 3).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos + 1, 3).Style.Font.Bold = True
        filadatos = filadatos + 2

        k = 0

        For i As Integer = 0 To tabla.Rows.Count - 1
            If (HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(5).Text)) = "SI" Then

                Wsheet.Cells(i - k + filadatos, 0).Style.WrapText = True
                Wsheet.Cells(i - k + filadatos, 1).Style.WrapText = True
                Wsheet.Cells(i - k + filadatos, 2).Style.WrapText = True
                Wsheet.Cells(i - k + filadatos, 3).Style.WrapText = True
                'If tabla.Rows(i).Cells(1).Text.Length > 40 Then
                '    Wsheet.Rows(i - k + filadatos).Height = 40
                'Else
                '    Wsheet.Rows(i - k + filadatos).Height = 16
                'End If
                aux1 = 0
                aux2 = 0

                texto1 = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text)
                texto2 = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text)
                For Each val As Char In texto1.ToCharArray()
                    Select Case val
                        Case " "
                            aux1 += 4
                        Case "I", "i", "j", "l"
                            aux1 += 5
                        Case "J", "f", "r", "t", "|", "-"
                            aux1 += 6
                        Case "s", "z", ")", "(", "°", "_"
                            aux1 += 7
                        Case "F", "L", "S", "Z", "c", "g", "k", "v", "x", "y"
                            aux1 += 8
                        Case "E", "K", "P", "T", "X", "Y", "a", "b", "d", "e", "h", "n", "p", "q", "u", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                            aux1 += 9
                        Case "A", "B", "C", "R", "V", "o"
                            aux1 += 10
                        Case "D", "G", "H"
                            aux1 += 11
                        Case "N", "O", "Q", "U"
                            aux1 += 12
                        Case "w"
                            aux1 += 13
                        Case "m"
                            aux1 += 14
                        Case "M"
                            aux1 += 15
                        Case "W"
                            aux1 += 16
                        Case Else
                            aux1 += 5
                    End Select
                Next

                For Each val As Char In texto2.ToCharArray()

                    Select Case val
                        Case " "
                            aux2 += 4
                        Case "I", "i", "j", "l"
                            aux2 += 5
                        Case "J", "f", "r", "t", "|", "-"
                            aux2 += 6
                        Case "s", "z", ")", "(", "°", "_"
                            aux2 += 7
                        Case "F", "L", "S", "Z", "c", "g", "k", "v", "x", "y"
                            aux2 += 8
                        Case "E", "K", "P", "T", "X", "Y", "a", "b", "d", "e", "h", "n", "p", "q", "u", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                            aux2 += 9
                        Case "A", "B", "C", "R", "V", "o"
                            aux2 += 10
                        Case "D", "G", "H"
                            aux2 += 11
                        Case "N", "O", "Q", "U"
                            aux2 += 12
                        Case "w"
                            aux2 += 13
                        Case "m"
                            aux2 += 14
                        Case "M"
                            aux2 += 15
                        Case "W"
                            aux2 += 16
                        Case Else
                            aux2 += 5
                    End Select

                Next





                If aux1 > ancho0 Then
                    ancho0 = aux1
                End If

                If aux2 > ancho1 Then
                    ancho1 = aux2
                End If
                Wsheet.Cells(i - k + filadatos, 0).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text) 'ancho0 & " ancho0" 
                Wsheet.Cells(i - k + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text) 'ancho1 & " ancho1" '
                Wsheet.Cells(i - k + filadatos, 2).Style.VerticalAlignment = TypeOfVAlignment.Center
                Wsheet.Cells(i - k + filadatos, 2).Style.HorizontalAlignment = TypeOfHAlignment.Center
                Wsheet.Cells(i - k + filadatos, 2).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(3).Text)
                Wsheet.Cells(i - k + filadatos, 3).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(4).Text))

                Wsheet.Cells("A" & (i - k + filadatos).ToString & ":D" & (i - k + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
            Else
                k += 1
            End If
        Next
        Wsheet.Cells("A" & (filadatos - k + tabla.Rows.Count).ToString & ":D" & (filadatos - k + tabla.Rows.Count).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

        If ancho0 > 500 Then
            ancho0 -= 75
        Else
            If ancho0 > 300 Then
                ancho0 -= 50
            Else
                ancho0 -= 25
            End If
        End If

        If ancho1 > 500 Then
            ancho1 -= 75
        Else
            If ancho1 > 300 Then
                ancho1 -= 50
            Else
                ancho1 -= 25
            End If
        End If
        Wsheet.Columns(0).Width = ancho0
        Wsheet.Columns(1).Width = ancho1



        '---------------------------------- comienzo de la generación del PRESUPUESTO APROBADO --------------------------------------------------
        'filadatos = filadatos + tabla.Rows.Count + 4
        filadatos = filadatos + 2


        Wsheet.Cells(filadatos - 4, 5).Value = "PRESUPUESTO"
        Wsheet.Cells(filadatos - 4, 5).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 4, 5).Style.Font.Size = 14

        Wsheet.Cells("F" & (filadatos - 2).ToString & ":G" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 7).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 7).Value = ""
        Wsheet.Cells(filadatos - 1, 7).Style.Font.Bold = True


        Wsheet.Cells(filadatos - 1, 7).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 7).Value = ""
        Wsheet.Cells(filadatos - 1, 7).Style.Font.Bold = True

        Wsheet.Cells("H" & (filadatos - 2).ToString & ":H" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 7).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 7).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 7).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos - 1, 7).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos - 1, 7).Value = "CANTIDAD PRESUPUESTO APROBADO"



        Wsheet.Cells("I" & (filadatos - 2).ToString & ":I" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 8).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 8).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 8).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos - 1, 8).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos - 1, 8).Value = "CANTIDAD EJECUTADA"


        Wsheet.Cells("J" & (filadatos - 2).ToString & ":J" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 9).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 9).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 9).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos - 1, 9).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos - 1, 9).Value = "CANTIDAD NO EJECUTADA"

        Wsheet.Cells("K" & (filadatos - 2).ToString & ":K" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 10).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 10).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 10).Style.VerticalAlignment = TypeOfVAlignment.Center
        Wsheet.Cells(filadatos - 1, 10).Style.HorizontalAlignment = TypeOfHAlignment.Center
        Wsheet.Cells(filadatos - 1, 10).Value = "CANTIDAD EJECUTADA C/ACTA"
        filadatos += 1
        Wsheet.Cells("H" & (filadatos - 3).ToString & ":K" & (filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
        Dim aux As Integer = 10
        For i As Integer = 0 To tabla2.Rows.Count - 1
            Wsheet.Cells(i - k + filadatos, 5).Style.WrapText = True
            Wsheet.Cells(i - k + filadatos, 6).Style.WrapText = True
            Wsheet.Cells(i - k + filadatos, 7).Style.WrapText = True
            Wsheet.Cells(i - k + filadatos, 8).Style.WrapText = True
            Wsheet.Cells(i - k + filadatos, 9).Style.WrapText = True
            Wsheet.Cells(i - k + filadatos, 10).Style.WrapText = True
            'If tabla.Rows(i).Cells(1).Text.Length > 40 Then
            '    Wsheet.Rows(i - k + filadatos).Height = 40
            'Else
            '    Wsheet.Rows(i - k + filadatos).Height = 16
            'End If

            aux2 = 0

            texto2 = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text)




            For Each val As Char In texto2.ToCharArray()

                Select Case val
                    Case " "
                        aux2 += 4
                    Case "I", "i", "j", "l"
                        aux2 += 5
                    Case "J", "f", "r", "t", "|", "-"
                        aux2 += 6
                    Case "s", "z", ")", "(", "°", "_"
                        aux2 += 7
                    Case "F", "L", "S", "Z", "c", "g", "k", "v", "x", "y"
                        aux2 += 8
                    Case "E", "K", "P", "T", "X", "Y", "a", "b", "d", "e", "h", "n", "p", "q", "u", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                        aux2 += 9
                    Case "A", "B", "C", "R", "V", "o"
                        aux2 += 10
                    Case "D", "G", "H"
                        aux2 += 11
                    Case "N", "O", "Q", "U"
                        aux2 += 12
                    Case "w"
                        aux2 += 13
                    Case "m"
                        aux2 += 14
                    Case "M"
                        aux2 += 15
                    Case "W"
                        aux2 += 16
                    Case Else
                        aux2 += 5
                End Select

            Next







            Wsheet.Cells(i + filadatos, 5).Value = HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(0).Text) '
            Wsheet.Cells(i + filadatos, 6).Style.VerticalAlignment = TypeOfVAlignment.Center
            Wsheet.Cells(i + filadatos, 6).Style.HorizontalAlignment = TypeOfHAlignment.Center
            Wsheet.Cells(i + filadatos, 6).Value = HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(1).Text) 'ancho1 & " ancho1" '
            Wsheet.Cells(i + filadatos, 7).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(2).Text))
            Wsheet.Cells(i + filadatos, 8).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(3).Text))
            Wsheet.Cells(i + filadatos, 9).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(5).Text))
            Wsheet.Cells(i + filadatos, 10).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(4).Text))
            If i <> 0 Then
                Wsheet.Cells("F" & (i + filadatos).ToString & ":K" & (i + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

            End If

        Next
        If ancho0 > 500 Then
            ancho0 -= 75
        Else
            If ancho0 > 300 Then
                ancho0 -= 50
            Else
                ancho0 -= 25
            End If
        End If

        If ancho1 > 500 Then
            ancho1 -= 75
        Else
            If ancho1 > 300 Then
                ancho1 -= 50
            Else
                ancho1 -= 25
            End If
        End If
        ' Wsheet.Columns(0).Width = ancho0
        Wsheet.Columns(5).Width = ancho1

        ' le damos ancho fijo a las columnas del excel
        Wsheet.Columns(2).Width = 62 ' +
        Wsheet.Columns(3).Width = 75 '+
        Wsheet.Columns(6).Width = 62 ' +
        Wsheet.Columns(7).Width = 105 ' +
        Wsheet.Columns(8).Width = 82
        Wsheet.Columns(9).Width = 82
        Wsheet.Columns(10).Width = 82








        Wsheet.Cells("F" & (tabla2.Rows.Count + filadatos).ToString & ":K" & (tabla2.Rows.Count + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
        Wbook.ConvertingOptions.SavePictureMode = True
        Wbook.WriteXLS(rutaPlantilla & "WriteXLS2s.xls")

        HttpContext.Current.Response.AppendHeader("Content-Type", "application/vnd.ms-excel")
        HttpContext.Current.Response.AppendHeader("Content-Disposition", [String].Format("attachment; filename={0}", ArchivoFecha))
        HttpContext.Current.Response.BinaryWrite(Wbook.WriteXLS().ToArray())
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()


    End Sub


    'genera un archivo excel para una nueva solitud o una solicitud cursada
    Sub generarExcelPresupuestos(ByVal tabla As GridView, ByVal titulo As String, ByVal material As String)

        Dim Wbook As New ExcelWorkbook()
        Dim plantilla, ext, filadatos, columnadatos, rutaPlantilla, Archivo As String

        plantilla = "Presupuestos"
        ext = ".xls"
        filadatos = "4"
        columnadatos = "1"
        rutaPlantilla = "C:\Plantillas Control de Pedidos\"
        Wbook = ExcelWorkbook.ReadXLS(rutaPlantilla & plantilla & ext)
        Dim Wsheet As ExcelWorksheet = Wbook.Worksheets(0)

        filadatos = filadatos - 1
        Dim Extension, ArchivoFecha As String
        Dim NombrePlantilla As String = ""



        Archivo = titulo & " - " & material
        Extension = ".xls"
        ArchivoFecha = Archivo & "_" & Now.ToString("yyyyMMddhhmmss")
        ArchivoFecha += Extension
        Archivo += Extension



        Dim WorksheetName As String = Wbook.Worksheets(0).Name
        For i As Integer = 0 To tabla.Rows.Count - 1
            Dim j As Integer = 0
            j += 1
            Wsheet.Cells(i + filadatos, 0).Value = (i + 1)

            Wsheet.Cells(i + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text)
            Wsheet.Cells(i + filadatos, 2).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text)

            Wsheet.Cells(i + filadatos, 3).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(3).Text)
            Wsheet.Cells(i + filadatos, 4).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(4).Text))
            Wsheet.Cells(i + filadatos, 5).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(5).Text))
            Wsheet.Cells(i + filadatos, 6).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(6).Text))
            Wsheet.Cells(i + filadatos, 7).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(7).Text))
            Try
                Wsheet.Cells(i + filadatos, 8).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(8).Text))
            Catch ex As Exception
                Wsheet.Cells(i + filadatos, 8).Value = "1000000000000"
            End Try

            Wsheet.Cells(i + filadatos, 9).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(9).Text)


            Wsheet.Cells("A" & (i + filadatos).ToString & ":J" & (i + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
        Next
        Wsheet.Cells("A" & (filadatos + tabla.Rows.Count).ToString & ":J" & (filadatos + tabla.Rows.Count).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

        Wbook.WriteXLS(rutaPlantilla & "WriteXLS.xls")


        Wsheet.Cells(0, 0).Value = titulo & " - " & material
        HttpContext.Current.Response.AppendHeader("Content-Type", "application/vnd.ms-excel")
        HttpContext.Current.Response.AppendHeader("Content-Disposition", [String].Format("attachment; filename={0}", ArchivoFecha))
        HttpContext.Current.Response.BinaryWrite(Wbook.WriteXLS().ToArray())
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.End()
    End Sub


End Class
