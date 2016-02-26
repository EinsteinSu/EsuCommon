﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Supeng.Office.MsTest
{
    [TestClass]
    public class ExcelOperationTest
    {
        [TestMethod]
        public void InsertTable()
        {
            var excel = new ExcelOperationBase();
            var workSheet = excel.CreateSheet("Test");
            excel.CreateTable(workSheet, new TestTableInsertModel());
            excel.Save(@"C:\Users\esu\Downloads\test.xlsx");
        }
    }

    public class TestTableInsertModel : IExcelTableInfo<string>
    {
        public IList<ExcelTableHead> HeadList
        {
            get
            {
                var list = new List<ExcelTableHead>();
                for (int i = 0; i < 5; i++)
                {
                    list.Add(new ExcelTableHead() { HeadText = string.Format("Head{0}", i) });
                }
                return list;
            }
        }

        public IList<string> Data
        {
            get
            {
                return new List<string> { "data1", "data2", "data3", "data4", "data5" };
            }
        }

        public void FillRow(ExcelWorksheet worksheet, int startRow, string data)
        {
            worksheet.Cells[startRow, 1].Value = data;
            worksheet.Cells[startRow, 1].AddHyperLinkText("http://google.com",data);
            worksheet.Cells[startRow, 2].Value = data;
            worksheet.Cells[startRow, 3].Value = data;
            worksheet.Cells[startRow, 4].Value = data;
            worksheet.Cells[startRow, 5].Value = data;
        }
    }
}
