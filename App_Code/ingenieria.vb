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
        Wsheet.Cells(filadatos + 1, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 1).Value = "DESCRIPCIÓN"
        Wsheet.Cells(filadatos + 1, 1).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 2).Value = "UNIDAD"
        Wsheet.Cells(filadatos + 1, 2).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 3).Value = "CANTIDAD"
        Wsheet.Cells(filadatos + 1, 3).Style.Font.Bold = True
        filadatos = filadatos + 2

        For i As Integer = 0 To tabla.Rows.Count - 1
            If (HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(5).Text)) = "NO" Then

                Wsheet.Cells(i - k + filadatos, 0).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text)
                Wsheet.Cells(i - k + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text)
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
        Wsheet.Cells(filadatos + 1, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 1).Value = "DESCRIPCIÓN"
        Wsheet.Cells(filadatos + 1, 1).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 2).Value = "UNIDAD"
        Wsheet.Cells(filadatos + 1, 2).Style.Font.Bold = True
        Wsheet.Cells(filadatos + 1, 3).Value = "CANTIDAD"
        Wsheet.Cells(filadatos + 1, 3).Style.Font.Bold = True
        filadatos = filadatos + 2

        k = 0

        For i As Integer = 0 To tabla.Rows.Count - 1
            If (HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(5).Text)) = "SI" Then

                Wsheet.Cells(i - k + filadatos, 0).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(1).Text)
                Wsheet.Cells(i - k + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(2).Text)
                Wsheet.Cells(i - k + filadatos, 2).Value = HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(3).Text)
                Wsheet.Cells(i - k + filadatos, 3).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla.Rows(i).Cells(4).Text))

                Wsheet.Cells("A" & (i - k + filadatos).ToString & ":D" & (i - k + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
            Else
                k += 1
            End If
        Next
        Wsheet.Cells("A" & (filadatos - k + tabla.Rows.Count).ToString & ":D" & (filadatos - k + tabla.Rows.Count).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

        filadatos = filadatos + tabla.Rows.Count + 4


        Wsheet.Cells(filadatos - 4, 0).Value = "PRESUPUESTO"
        Wsheet.Cells(filadatos - 4, 0).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 4, 0).Style.Font.Size = 14

        Wsheet.Cells("A" & (filadatos - 2).ToString & ":A" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 2).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 2).Value = ""
        Wsheet.Cells(filadatos - 1, 2).Style.Font.Bold = True

        Wsheet.Cells("B" & (filadatos - 2).ToString & ":B" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 2).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 2).Value = ""
        Wsheet.Cells(filadatos - 1, 2).Style.Font.Bold = True

        Wsheet.Cells("C" & (filadatos - 2).ToString & ":C" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 2).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 2).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 2).Value = "CANTIDAD PRESUPUESTO APROBADO"



        Wsheet.Cells("D" & (filadatos - 2).ToString & ":D" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 3).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 3).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 3).Value = "CANTIDAD EJECUTADA"


        Wsheet.Cells("E" & (filadatos - 2).ToString & ":E" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 4).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 4).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 4).Value = "CANTIDAD NO EJECUTADA"

        Wsheet.Cells("F" & (filadatos - 2).ToString & ":F" & (filadatos + 1).ToString).IsMerged = True
        Wsheet.Cells(filadatos - 1, 5).Style.WrapText = True
        Wsheet.Cells(filadatos - 1, 5).Style.Font.Bold = True
        Wsheet.Cells(filadatos - 1, 5).Value = "CANTIDAD EJECUTADA C/ACTA"
        filadatos += 1
        Wsheet.Cells("A" & (filadatos - 3).ToString & ":F" & (filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)
        For i As Integer = 0 To tabla2.Rows.Count - 1


            Wsheet.Cells(i + filadatos, 0).Value = HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(0).Text)
            Wsheet.Cells(i + filadatos, 1).Value = HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(1).Text)
            Wsheet.Cells(i + filadatos, 2).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(2).Text))
            Wsheet.Cells(i + filadatos, 3).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(3).Text))
            Wsheet.Cells(i + filadatos, 4).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(5).Text))
            Wsheet.Cells(i + filadatos, 5).Value = CDbl(HttpContext.Current.Server.HtmlDecode(tabla2.Rows(i).Cells(4).Text))

            Wsheet.Cells("A" & (i + filadatos).ToString & ":F" & (i + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

        Next

        Wsheet.Cells("A" & (tabla2.Rows.Count + filadatos).ToString & ":F" & (tabla2.Rows.Count + filadatos).ToString).SetBordersStyles(TypeOfMultipleBorders.All, TypeOfBorderLine.Thin, ColorPalette.Black)

        Wbook.WriteXLS(rutaPlantilla & "WriteXLS.xls")

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
