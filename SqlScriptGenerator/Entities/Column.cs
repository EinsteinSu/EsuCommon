using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using SqlScriptGenerator.Commons;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Strings;
using Supeng.Data;

namespace SqlScriptGenerator.Entities
{
  internal class Column : EsuInfoBase, IDataCreator<Column>
  {
    private bool choice;
    private bool identity;
    private string name;
    private bool pk;
    private string type;

    #region properties

    public bool Choice
    {
      get { return choice; }
      set
      {
        if (value.Equals(choice)) return;
        choice = value;
        NotifyOfPropertyChange(() => Choice);
      }
    }

    public string Name
    {
      get { return name; }
      set
      {
        if (value == name) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public string Type
    {
      get { return type; }
      set
      {
        if (value == type) return;
        type = value;
        NotifyOfPropertyChange(() => Type);
      }
    }

    public bool Pk
    {
      get { return pk; }
      set
      {
        if (value.Equals(pk)) return;
        pk = value;
        NotifyOfPropertyChange(() => Pk);
      }
    }

    public bool Identity
    {
      get { return identity; }
      set
      {
        if (value.Equals(identity)) return;
        identity = value;
        NotifyOfPropertyChange(() => Identity);
      }
    }

    #endregion

    [Display(AutoGenerateField = false)]
    public new string Description
    {
      get { return base.Description; }
      set { base.Description = value; }
    }

    public string CreatorString
    {
      get
      {
        if (name.Equals("memo", StringComparison.InvariantCultureIgnoreCase))
          name = "description";
        return string.Format("data.{0} = reader[\"{1}\"].ToString();", name.GetPascalName(), name.ToLower());
      }
    }

    public string PrivateProperty
    {
      get { return string.Format("private {0} {1};", type.ConvertToSystemType(), name.ToLower()); }
    }

    public string Property
    {
      get { return string.Format(@"public {0} {1}
    【
      get 【 return {2}; 】
      set
      【
        if (value.Equals({2})) return;
        {2} = value;
        NotifyOfPropertyChange(() => {1});
      】
    】{3}", type.ConvertToSystemType(), name.GetPascalName(), name.ToLower(), Environment.NewLine).ReplaceBracket(); }
    }

    public string ParameterName
    {
      get { return string.Format("@{0}", name.ToLower()); }
    }

    public string DataEntityName
    {
      get
      {
        string n = name.GetPascalName();
        if (name.Equals("guid", StringComparison.InvariantCultureIgnoreCase))
          n = "ID";
        else if (name.Equals("memo", StringComparison.InvariantCultureIgnoreCase))
          n = "Description";
        return string.Format("data.{0}", n);
      }
    }

    public Column CreateData(IDataReader reader)
    {
      var column = new Column
      {
        identity = reader["bidentity"].ToString().Equals("1"),
        choice = !reader["bidentity"].ToString().Equals("1"),
        name = reader["columnName"].ToString(),
        pk = reader["primekey"].ToString().Equals("1"),
        type = reader["type"].ToString(),
        Description = reader["des"].ToString()
      };
      return column;
    }

    public string GetSetValue()
    {
      return string.Format("{0} = {1}", name.ToLower(), ParameterName);
    }

    public string GetWhereCondition()
    {
      return string.Format("Where {0} = {1}", name, ParameterName);
    }
  }

  internal class ColumnCollection : EsuInfoCollection<Column>
  {
    private readonly string tableName;

    #region columns sql script

    private const string SqlScript = @"Select
d.name as tablename,
a.name as columnname, 
(case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then 1 else 0
end) as bidentity, 
(case when (SELECT count(*) 
FROM sysobjects 
WHERE (name in 
(SELECT name 
FROM sysindexes 
WHERE (id = a.id) AND (indid in 
(SELECT indid 
FROM sysindexkeys 
WHERE (id = a.id) AND (colid in 
(SELECT colid 
FROM syscolumns 
WHERE (id = a.id) AND (name = a.name))))))) AND 
(xtype = 'PK'))>0 then 1 else 0 end) as primekey, 
b.name as type, 
a.length as lenth, 
COLUMNPROPERTY(a.id,a.name,'PRECISION') as stringlenth, 
isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0) as decimaldigits, 
(case when a.isnullable=1 then 1 else 0 end) as empty, 
isnull(e.text,'') as defualt, 
isnull(g.[value],'') as des 
FROM syscolumns a 
left join systypes b 
on a.xtype=b.xusertype 
inner join sysobjects d 
on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' 
left join syscomments e 
on a.cdefault=e.id 
left join sys.extended_properties g 
on a.id=g.major_id AND a.colid = g.minor_id 
where d.name = '{0}'
order by object_name(a.id),a.colorder";

    #endregion

    public ColumnCollection(string tableName, DataStorageBase storage)
    {
      this.tableName = tableName;
      string sql = SqlScript.Replace("{0}", tableName);
      foreach (Column column in storage.ReadToCollection(sql, new Column()))
      {
        Add(column);
      }
    }

    public string GetCreatorString()
    {
      return this.Where(w => w.Choice)
        .Aggregate(string.Empty, (current, column) => current + (column.CreatorString + Environment.NewLine));
    }

    public string GetEntities()
    {
      var sb = new StringBuilder();
      var sbPrivate = new StringBuilder();
      sb.AppendLine("#region properties");
      foreach (Column column in this.Where(w => w.Choice))
      {
        if (column.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase) ||
            column.Name.Equals("guid", StringComparison.InvariantCultureIgnoreCase) ||
            column.Name.Equals("description", StringComparison.InvariantCultureIgnoreCase))
          continue;
        sbPrivate.AppendLine(column.PrivateProperty);
        sb.AppendLine(column.Property);
      }
      sb.AppendLine("#endregion");
      sb.Insert(0, sbPrivate + Environment.NewLine);
      return sb.ToString();
    }

    #region get sql scripts

    public string GetMappingParameters()
    {
      var sb = new StringBuilder();
      sb.AppendLine(string.Format("IDataParameter[] parameters = new IDataParameter[{0}];", this.Count(w => w.Choice)));
      int i = 0;
      foreach (Column column in this.Where(w => w.Choice))
      {
        sb.AppendLine(string.Format("parameters[{0}] = new SqlParameter(\"{1}\", {2});", i, column.ParameterName,
          column.DataEntityName));
        i++;
      }
      sb.AppendLine("return parameters;");
      return sb.ToString();
    }

    public string GetInsertSqlScript()
    {
      string insertColumns = string.Empty;
      string insertValue = string.Empty;
      int i = 0;
      foreach (Column column in this.Where(w => w.Choice))
      {
        insertColumns += string.Format("{0},", column.Name.ToLower());
        insertValue += string.Format("{0},", column.ParameterName);
        i++;
      }
      return string.Format("return \"Insert into {0}({1}) values({2})\";", tableName,
        insertColumns.TrimEnd(','), insertValue.TrimEnd(','));
    }

    public string GetUpdateSql()
    {
      string setColumns = string.Empty;
      int i = 0;
      foreach (Column column in this.Where(w => !w.Pk && w.Choice))
      {
        setColumns += string.Format("{0},", column.GetSetValue());
        i++;
      }
      Column pk = this.FirstOrDefault(f => f.Pk);
      if (pk != null)
        return string.Format("return \"Update {0} set {1} {2}\";", tableName, setColumns.TrimEnd(','),
          pk.GetWhereCondition());
      return string.Format("return \"Update {0} set {1} \";", tableName, setColumns.TrimEnd(','));
    }

    public string GetDeleteSql()
    {
      Column pk = this.FirstOrDefault(f => f.Pk);
      if (pk != null)
      {
        string whereCondition = pk.GetWhereCondition();
        return string.Format("return \"Delete from {0} {1}\";", tableName, whereCondition);
      }
      return string.Empty;
    }

    #endregion
  }
}