﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Collections.ObjectModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;

namespace XamarinImage
{
    static public class ExcelHelper
    {
        static public DataTable MakeTable(List<Tuple<int, Tuple<int, int>>> list)
        {
            // Create a new DataTable.
            System.Data.DataTable table = new DataTable("PulseTable");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, 

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Pulse";
            column.AutoIncrement = false;
            column.Caption = "Pulse";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Time";
            column.AutoIncrement = false;
            column.Caption = "Time";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Make the ID column the primary key column.
            //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            //PrimaryKeyColumns[0] = table.Columns["id"];
            //table.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            var dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table);

            // Create three new DataRow objects and add 
            // them to the DataTable
            for (int i = 0; i < list.Count; i++)
            {
                row = table.NewRow();
                row["Pulse"] = list[i].Item1;
                row["Time"] = list[i].Item2.Item1 * 60 + list[i].Item2.Item2;
                table.Rows.Add(row);
            }
            return table;
        }
        static public DataTable MakeTable(List<Tuple<int, int, Tuple<int, int>>> list)
        {
            // Create a new DataTable.
            System.Data.DataTable table = new DataTable("PulseTable");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "PressureTop";
            column.AutoIncrement = false;
            column.Caption = "PressureTop";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "PressureDown";
            column.AutoIncrement = false;
            column.Caption = "PressureDown";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Time";
            column.AutoIncrement = false;
            column.Caption = "Time";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"];
            table.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            var dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table);

            // Create three new DataRow objects and add 
            // them to the DataTable
            for (int i = 0; i < list.Count; i++)
            {
                row = table.NewRow();
                row["id"] = i;
                row["PressureTop"] = list[i].Item1;
                row["PressureDown"] = list[i].Item2;
                row["Time"] = list[i].Item3.Item1 * 60 + list[i].Item3.Item2;
                table.Rows.Add(row);
            }
            return table;
        }
        static public void DataTableToExcel(DataTable pDatos, string pFilePath)
        {
            try
            {
                if (pDatos != null && pDatos.Rows.Count > 0)
                {
                    IWorkbook workbook = null;
                    ISheet worksheet = null;

                    using (FileStream stream = new FileStream(pFilePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        string Ext = System.IO.Path.GetExtension(pFilePath); //<-Extension del archivo
                        switch (Ext.ToLower())
                        {
                            case ".xls":
                                HSSFWorkbook workbookH = new HSSFWorkbook();
                                NPOI.HPSF.DocumentSummaryInformation dsi = NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                                dsi.Company = "Cutcsa"; dsi.Manager = "Departamento Informatico";
                                workbookH.DocumentSummaryInformation = dsi;
                                workbook = workbookH;
                                break;

                            case ".xlsx": workbook = new XSSFWorkbook(); break;
                        }

                        worksheet = workbook.CreateSheet(pDatos.TableName); //<-Usa el nombre de la tabla como nombre de la Hoja

                        //CREAR EN LA PRIMERA FILA LOS TITULOS DE LAS COLUMNAS
                        int iRow = 0;
                        if (pDatos.Columns.Count > 0)
                        {
                            int iCol = 0;
                            IRow fila = worksheet.CreateRow(iRow);
                            foreach (DataColumn columna in pDatos.Columns)
                            {
                                ICell cell = fila.CreateCell(iCol, CellType.String);
                                cell.SetCellValue(columna.ColumnName);
                                iCol++;
                            }
                            iRow++;
                        }

                        //FORMATOS PARA CIERTOS TIPOS DE DATOS
                        //ICellStyle _doubleCellStyle = workbook.CreateCellStyle();
                        //_doubleCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.###");

                        //ICellStyle _intCellStyle = workbook.CreateCellStyle();
                        //_intCellStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0");

                        //AHORA CREAR UNA FILA POR CADA REGISTRO DE LA TABLA
                        foreach (DataRow row in pDatos.Rows)
                        {
                            IRow fila = worksheet.CreateRow(iRow);
                            int iCol = 0;
                            foreach (DataColumn column in pDatos.Columns)
                            {
                                ICell cell = null; //<-Representa la celda actual                               
                                object cellValue = row[iCol]; //<- El valor actual de la celda

                                switch (column.DataType.ToString())
                                {
                                    case "System.String":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.String);
                                            cell.SetCellValue(Convert.ToString(cellValue));
                                        }
                                        break;

                                    case "System.Int32":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToInt32(cellValue));
                                            //cell.CellStyle = _intCellStyle;
                                        }
                                        break;
                                    case "System.Int64":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToInt64(cellValue));
                                            //cell.CellStyle = _intCellStyle;
                                        }
                                        break;
                                    case "System.Decimal":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDouble(cellValue));
                                            //cell.CellStyle = _doubleCellStyle;
                                        }
                                        break;
                                    case "System.Double":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = fila.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDouble(cellValue));
                                            //cell.CellStyle = _doubleCellStyle;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                iCol++;
                            }
                            iRow++;
                        }

                        IDrawing drawing = worksheet.CreateDrawingPatriarch();
                        int colLeft = 0;
                        int rowTop = 0;
                        int colRight = 6;
                        int rowDown = 10;
                        IClientAnchor anchor1 = drawing.CreateAnchor(0, 0, 0, 0, colLeft, rowTop, colRight, rowDown);
                        CreateChart(drawing, worksheet, anchor1, "title1", "title2");

                        workbook.Write(stream);
                        stream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void CreateChart(IDrawing drawing, ISheet sheet, IClientAnchor anchor, string serie1, string serie2)
        {
            IChart chart = drawing.CreateChart(anchor);
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.TopRight;

            ILineChartData<double, double> data = chart.ChartDataFactory.CreateLineChartData<double, double>();

            // Use a category axis for the bottom axis.
            IChartAxis bottomAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
            IValueAxis leftAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            //leftAxis.SetCrosses(AxisCrosses.AutoZero);
            leftAxis.Minimum = 55;
            leftAxis.Maximum = 90;
            int firstRow = 1;
            int lastRow = 48;
            int firstCol = 0;
            int lastCol = 0;
            IChartDataSource<double> xs = DataSources.FromNumericCellRange(sheet, new CellRangeAddress(1, lastRow, 1, 1));
            IChartDataSource<double> ys1 = DataSources.FromNumericCellRange(sheet, new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
            IChartDataSource<double> ys2 = DataSources.FromNumericCellRange(sheet, new CellRangeAddress(2, 2, 0, 10 - 1));

            var s1 = data.AddSeries(xs, ys1);
            s1.SetTitle("");
            //var s2 = data.AddSeries(xs, ys2);
            //s2.SetTitle(serie2);

            chart.Plot(data, bottomAxis, leftAxis);
        }
    }
}
