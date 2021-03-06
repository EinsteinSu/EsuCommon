﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using Microsoft.Office.Interop.Word;
using NUnit.Framework;

namespace Supeng.Office.Tests
{
  [TestFixture]
  public class TestWordWrite
  {
    private readonly string modleFileName = string.Format("{0}\\TestDocumentModel.docx", Environment.CurrentDirectory);
    private readonly string fileName = string.Format("{0}\\TestDocument.docx", Environment.CurrentDirectory);

    private void CreateNewModel()
    {
      File.Copy(modleFileName, fileName, true);
    }

    [Test]
    public void TestInserText()
    {
      CreateNewModel();
      using (var word = new WordOperationBase())
      {
        word.Open(fileName, true);
        word.InserText("InsertBookMark", "Insert book mark test text. :)");
      }
    }

    [Test]
    public void TestInsertTable()
    {
      CreateNewModel();
      using (var word = new WordOperationBase())
      {
        word.Open(fileName, true);
        var tableInsert = new TestTableInsert();
        Table table = word.InsertTable("TableBookMark", tableInsert, -20);
        table.Cell(tableInsert.RowCount + 1, 1).Merge(table.Cell(tableInsert.RowCount + 1, 5));
        table.Cell(tableInsert.RowCount + 1, 1).Range.Text = "This is a cell that has been merged.";
      }
    }

    [Test]
    public void TestReplaceText()
    {
      CreateNewModel();
      var replace = new List<WordReplaceInfo>();
      replace.Add(new WordReplaceInfo("Test", "Test1"));
      using (var word = new WordOperationBase())
      {
        word.Open(fileName, true);
        word.Replace(replace);
      }
    }
  }

  internal class TestTableInsert : IWordTableOperates
  {
    public ObservableCollection<WordTableCellInfo> CellCollection
    {
      get
      {
        var collection = new ObservableCollection<WordTableCellInfo>();
        for (int i = 0; i < 5; i++)
        {
          collection.Add(new WordTableCellInfo {Widht = 36, Header = "Column:" + i});
        }
        return collection;
      }
    }

    public int RowCount
    {
      get { return 10; }
    }

    public void FillData(Table table)
    {
      int j = 2;
      foreach (TestData data in TestData.GetTestDatas())
      {
        table.Cell(j, 1).Range.Text = data.ID.ToString(CultureInfo.InvariantCulture);
        table.Cell(j, 2).Range.Text = data.Name;
        table.Cell(j, 3).Range.Text = data.ID.ToString(CultureInfo.InvariantCulture);
        table.Cell(j, 4).Range.Text = data.Name;
        table.Cell(j, 5).Range.Text = data.ID.ToString(CultureInfo.InvariantCulture);
        j++;
      }
    }
  }

  internal class TestData
  {
    public TestData(int id, string name)
    {
      ID = id;
      Name = name;
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public static List<TestData> GetTestDatas()
    {
      var list = new List<TestData>();
      for (int i = 0; i < 100; i++)
      {
        list.Add(new TestData(i, "Name" + i));
      }
      return list;
    }
  }
}