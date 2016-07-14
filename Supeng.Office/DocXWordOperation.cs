using System.Collections.Generic;
using System.IO;
using Novacode;

namespace Supeng.Office
{
    public class DocXWordOperation : IWordOperation
    {
        private readonly DocX _doc;

        public DocXWordOperation(string fileName)
        {
            _doc = !File.Exists(fileName) ? DocX.Create(fileName) : DocX.Load(fileName);
        }

        public void Dispose()
        {
            _doc.Save();
            _doc.Dispose();
        }

        public DocX Doc
        {
            get { return _doc; }
        }

        public void InsertText(string bookMark, string text)
        {
            var item = _doc.Bookmarks[bookMark];
            if (item != null)
            {
                item.Paragraph.InsertText(text);
            }
        }

        public void InsertTable(string bookMark, ITableInsert tableInsert,
            TableDesign tableDesign = TableDesign.TableGrid)
        {
            var item = _doc.Bookmarks[bookMark];
            if (item != null)
            {
                var table = _doc.AddTable(tableInsert.RowCount, tableInsert.ColumnCount);
                table.Design = tableDesign;
                tableInsert.InsertHead(table);
                tableInsert.FillData(table);
                item.Paragraph.InsertTableAfterSelf(table);
            }
        }

        public void Replace(IList<WordReplaceInfo> replaces)
        {
            foreach (var item in replaces)
            {
                _doc.ReplaceText(item.Oldsring, item.Newstring);
            }
        }
    }
}