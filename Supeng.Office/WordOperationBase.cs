﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.Office.Interop.Word;

namespace Supeng.Office
{
  public class WordOperationBase : IDisposable
  {
    private readonly ApplicationClass wordApp;
    protected Document Document;

    public WordOperationBase()
    {
      wordApp = new ApplicationClass();
    }

    public bool Open(string wordName, bool showApp)
    {
      bool result = false;
      if (string.IsNullOrEmpty(wordName)) return false;
      try
      {
        object fileName = wordName;
        object readOnly = false;
        object isVisible = true;
        object missing = Missing.Value;
        Document = wordApp.Documents.Open(ref fileName, ref missing, ref readOnly,
          ref missing, ref missing, ref missing, ref missing, ref missing,
          ref missing,
          ref missing, ref missing, ref isVisible);
        Document.Activate();
        wordApp.Visible = showApp;
        result = true;
      }
      catch
      {
        Quit();
      }
      return result;
    }

    public virtual Table InsertTable(string bookMark, IWordTableOperates table)
    {
      Object nothing = Missing.Value;
      Range rang = Document.Bookmarks.get_Item(bookMark).Range;
      Table newTable = Document.Tables.Add(rang, table.RowCount + 1, table.CellCollection.Count, ref nothing, ref nothing);
      newTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
      newTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

      #region set column width and header
      int i = 1;
      foreach (var cell in table.CellCollection)
      {
        newTable.Columns[i].Width = cell.Widht;
        newTable.Cell(1, i).Range.Text = cell.Header;
        i++;
      }
      #endregion

      SetDefaultTableHeader(newTable, table.CellCollection.Count);

      table.FillData(newTable);
      return newTable;
    }

    protected virtual void SetDefaultTableHeader(Table table, int columnCount, int headerStart = 1)
    {
      for (int i = 0; i < columnCount; i++)
      {
        table.Cell(headerStart, i + 1).Range.Bold = 2;
        table.Cell(headerStart, i + 1).Select();
        table.Cell(headerStart, i + 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter; //垂直居中
        table.Cell(headerStart, i + 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
      }
    }

    public virtual void Replace(List<WordReplaceInfo> lst)
    {
      object missingValue = Type.Missing;

      foreach (WordReplaceInfo w in lst)
      {
        Document.Content.Find.Text = w.Oldsring;
        object findText = w.Oldsring;
        object replaceWith = w.Newstring;
        object replace = WdReplace.wdReplaceAll;
        Document.Content.Find.ClearFormatting();
        Document.Content.Find.Execute(ref findText, ref missingValue, ref missingValue, ref missingValue,
          ref missingValue, ref missingValue, ref missingValue, ref missingValue,
          ref missingValue, ref replaceWith, ref replace, ref missingValue,
          ref missingValue, ref missingValue, ref missingValue);
      }
    }

    public void InserText(string bookMark, string insertText)
    {
      if (Document.Bookmarks.Exists(bookMark))
      {
        Document.Bookmarks.get_Item(bookMark).Range.Text = insertText;
      }
    }

    public void Quit()
    {
      Object nothing = Missing.Value;
      if (Document != null)
        Document.Close(ref nothing, ref nothing, ref nothing);
      if (wordApp != null)
        wordApp.Quit(ref nothing, ref nothing, ref nothing);
      GC.Collect();
    }

    public void Dispose()
    {
      Quit();
    }
  }

  public interface IWordTableOperates
  {
    ObservableCollection<WordTableCellInfo> CellCollection { get; }

    int RowCount { get; }

    void FillData(Table table);

  }

  public class WordTableCellInfo
  {
    public int Widht { get; set; }

    public string Header { get; set; }
  }

  public class WordReplaceInfo
  {
    public WordReplaceInfo(string oldString, string newString)
    {
      Oldsring = oldString;
      Newstring = newString;
    }

    public string Oldsring { get; set; }

    public string Newstring { get; set; }
  }
}