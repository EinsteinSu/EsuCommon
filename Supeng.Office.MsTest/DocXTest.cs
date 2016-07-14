using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacode;

namespace Supeng.Office.MsTest
{
    [TestClass]
    public class DocXTest
    {
        private DocXWordOperation docx; // = new DocXWordOperation("Test");
        private string target;

        [TestInitialize]
        public void Initialize()
        {
            var template = Path.Combine(Directory.GetCurrentDirectory(), "Data", "template.docx");
            target = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid() + ".docx");
            File.Copy(template, target);
            docx = new DocXWordOperation(target);
        }

        [TestMethod]
        public void ReplaceText()
        {
            var list = new List<WordReplaceInfo>();
            list.Add(new WordReplaceInfo("Test", "Test1"));
            docx.Replace(list);
            docx.Dispose();
        }

        [TestMethod]
        public void InsertText()
        {
            docx.InsertText("bookmark1", "This is bookmark1");
            docx.Dispose();
        }

        [TestMethod]
        public void InsertTable()
        {
            docx.InsertTable("bookmark2", new TableTest());
            docx.Dispose();
        }

        [TestCleanup]
        public void Clean()
        {
            if (!File.Exists(target))
            {
                File.Delete(target);
            }
        }
    }

    public class TableTest : ITableInsert
    {
        public int RowCount
        {
            get { return 5; }
        }

        public int ColumnCount
        {
            get { return 5; }
        }

        public void FillData(Table table)
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    table.Rows[i].Cells[j].Paragraphs[0].Append(string.Format("Rows:{0} Columns:{1}", i, j));
                }
            }

        }

        public void InsertHead(Table table)
        {
            for (int i = 0; i < 5; i++)
            {
                table.Rows[0].Cells[i].Paragraphs[0].Append("Column Head " + i);
            }
        }
    }
}