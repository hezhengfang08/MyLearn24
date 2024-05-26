
using Microsoft.VisualBasic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace MySelf.QOSM.Common
{
    public class ExcelHelper
    {

        /// <summary>
        /// ��DataTable���ݵ��뵽excel��
        /// </summary>
        /// <param name="data">Ҫ���������</param>
        /// <param name="fileName">д���Ŀ��Excel����������</param>
        /// <param name="isColumnWritten">DataTable�������Ƿ�Ҫ����</param>
        /// <param name="sheetName">Ҫ�����excel��sheet������</param>
        /// <returns>������������(����������һ��)</returns>
        public static int DataTableToExcel(DataTable data, string fileName, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            IWorkbook workbook = null;

            if (fileName.IndexOf(".xlsx") > 0) // 2007�汾
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003�汾
                workbook = new HSSFWorkbook();

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (workbook != null)
                    {
                        sheet = workbook.CreateSheet(sheetName);
                    }
                    else
                    {
                        return -1;
                    }

                    if (isColumnWritten == true) //д��DataTable������
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }
                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }
                        ++count;
                    }
                    workbook.Write(fs); //д�뵽excel
                    return count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }

        }

        /// <summary>
        /// ��excel�е����ݵ��뵽DataTable��
        /// </summary>
        /// <param name="fileName">��ȡ��Excel����������</param>
        /// <param name="sheetName">excel������sheet������</param>
        /// <param name="isFirstRowColumn">��һ���Ƿ���DataTable������</param>
        /// <returns>���ص�DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                IWorkbook workbook = null;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (fileName.IndexOf(".xlsx") > 0) // 2007�汾
                        workbook = new XSSFWorkbook(fs);
                    else if (fileName.IndexOf(".xls") > 0) // 2003�汾
                        workbook = new HSSFWorkbook(fs);

                    if (sheetName != null)
                    {
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet == null) //���û���ҵ�ָ����sheetName��Ӧ��sheet�����Ի�ȡ��һ��sheet
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                    }
                    else
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                    if (sheet != null)
                    {
                        IRow firstRow = sheet.GetRow(0);
                        int cellCount = firstRow.LastCellNum; //һ�����һ��cell�ı�� ���ܵ�����

                        if (isFirstRowColumn)
                        {
                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue;
                                    if (cellValue != null)
                                    {
                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                    }
                                }
                            }
                            startRow = sheet.FirstRowNum + 1;
                        }
                        else
                        {
                            startRow = sheet.FirstRowNum;
                        }

                        //���һ�еı��
                        int rowCount = sheet.LastRowNum;
                        for (int i = startRow; i <= rowCount; ++i)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue; //û�����ݵ���Ĭ����null��������������

                            DataRow dataRow = data.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                if (row.GetCell(j) != null) //ͬ��û�����ݵĵ�Ԫ��Ĭ����null
                                {
                                    ICell cell = row.GetCell(j);
                                    if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = cell.DateCellValue.ToString();
                                    }
                                    else
                                    {
                                        dataRow[j] = cell.ToString();
                                    }
                                }
                            }
                            data.Rows.Add(dataRow);
                        }
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// ��Excel��stream���뵽��
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(Stream fs, string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                IWorkbook workbook = null;
                if (fileName.IndexOf(".xlsx") > 0) // 2007�汾
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003�汾
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //���û���ҵ�ָ����sheetName��Ӧ��sheet�����Ի�ȡ��һ��sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //һ�����һ��cell�ı�� ���ܵ�����

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //���һ�еı��
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //û�����ݵ���Ĭ����null��������������

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //ͬ��û�����ݵĵ�Ԫ��Ĭ����null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// ��List<T>�б����ݵ�����Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="colNames"></param>
        /// <returns></returns>
        public static int ListToExcel<T>(List<T> list, string fileName, string sheetName, Dictionary<string, string> colNames)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            IWorkbook workbook = null;
            Type type = typeof(T);

            if (fileName.IndexOf(".xlsx") > 0) // 2007�汾
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003�汾
                workbook = new HSSFWorkbook();

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (workbook != null)
                    {
                        sheet = workbook.CreateSheet(sheetName);
                    }
                    else
                    {
                        return -1;
                    }
                    List<string> keys = new List<string>(colNames.Keys);
                    if (colNames.Count > 0) //д������
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < keys.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(colNames[keys[j]]);
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum;
                    PropertyInfo[] props = type.GetProperties();
                    for (i = 0; i < list.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = firstRow.FirstCellNum; j < cellCount; ++j)
                        {
                            var p = type.GetProperty(keys[j]);
                            object val = p.GetValue(list[i]);
                            if (val == null)
                                val = "";
                            row.CreateCell(j).SetCellValue(val.ToString());
                        }
                        ++count;
                    }
                    workbook.Write(fs); //д�뵽excel
                    return count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }

        }

        /// <summary>
        /// ����IWorkbookʵ��
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static IWorkbook CreateWorkBook(string exName)
        {
            IWorkbook workbook = null;
            if (exName == ".xlsx") // 2007�汾
                workbook = new XSSFWorkbook();
            else if (exName == ".xls") // 2003�汾
                workbook = new HSSFWorkbook();
            return workbook;
        }

        /// <summary>
        /// ����ISheetʵ��
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static ISheet CreateSheet(IWorkbook workbook, string sheetName)
        {
            ISheet sheet = null;
            if (workbook != null)
            {
                sheet = workbook.CreateSheet(sheetName);
            }
            return sheet;
        }

    }
}