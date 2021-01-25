// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Utils
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Repository.Dapper
{
  public static class Utils
  {
    public static string GetPKColumnName(Type type)
    {
      foreach (PropertyInfo property in type.GetProperties())
      {
        if (property.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (PrimaryKeyAttribute))))
          return property.Name;
      }
      return (string) null;
    }

    public static string GetPKColumnName(object entity, out int id)
    {
      id = 0;
      foreach (PropertyInfo property in entity.GetType().GetProperties())
      {
        if (property.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (PrimaryKeyAttribute))))
        {
          id = Convert.ToInt32(property.GetValue(entity));
          return property.Name;
        }
      }
      return (string) null;
    }

    public static Dictionary<string, object> GetPropertiesAndValues(object entity, bool excludePk, bool saveZeroValue)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      foreach (PropertyInfo property in entity.GetType().GetProperties())
      {
        if (excludePk && property.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (PrimaryKeyAttribute))))
        {
          if (property.CustomAttributes.Count<CustomAttributeData>() == 0)
          {
            object obj = property.GetValue(entity);
            if (obj != null)
            {
              string stringFromObject = Utils.GetValueInStringFromObject(obj, saveZeroValue);
              if (!string.IsNullOrEmpty(stringFromObject))
                dictionary.Add(property.Name, (object) stringFromObject);
            }
          }
        }
        else if (property.CustomAttributes.Count<CustomAttributeData>() == 0)
        {
          object obj = property.GetValue(entity);
          if (obj != null)
          {
            string stringFromObject = Utils.GetValueInStringFromObject(obj, saveZeroValue);
            if (!string.IsNullOrEmpty(stringFromObject))
              dictionary.Add(property.Name, (object) stringFromObject);
          }
        }
      }
      return dictionary;
    }

    private static string GetValueInStringFromObject(object value, bool saveZeroValue)
    {
      string str = string.Empty;
      switch (value.GetType().ToString().ToLower())
      {
        case "system.boolean":
          if (!string.IsNullOrEmpty(value.ToString()))
          {
            value = !Convert.ToBoolean(value) ? (object) 0 : (object) 1;
            str = value.ToString();
            break;
          }
          break;
        case "system.datetime":
          if (value != null && value != (object) "0001-01-01 00:00:00")
          {
            str = "'" + Convert.ToDateTime(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            break;
          }
          break;
        case "system.decimal":
        case "system.double":
        case "system.int":
        case "system.int16":
        case "system.int64":
        case "system.long":
          if ((ulong) Convert.ToInt64(value) > 0UL | saveZeroValue)
          {
            str = value.ToString();
            break;
          }
          break;
        case "system.int32":
          if ((uint) Convert.ToInt32(value) > 0U | saveZeroValue)
          {
            str = value.ToString();
            break;
          }
          break;
        case "system.string":
          if (!string.IsNullOrEmpty(value.ToString()))
          {
            if (value.ToString().Contains("'"))
              value = (object) value.ToString().Replace("'", "''");
            str = "'" + value.ToString().Trim() + "'";
            break;
          }
          break;
      }
      return str;
    }

    public static void GetColumnValueStrings(Dictionary<string, object> dict, out string columns, out string values)
    {
      columns = string.Empty;
      values = string.Empty;
      foreach (KeyValuePair<string, object> keyValuePair in dict)
      {
        columns = columns + keyValuePair.Key + ",";
        values = values + keyValuePair.Value + ",";
      }
      columns = columns.Remove(columns.Length - 1);
      values = values.Remove(values.Length - 1);
    }

    public static string GetWhereClause(Dictionary<object, object> filters)
    {
      string str = " WHERE ";
      foreach (KeyValuePair<object, object> filter in filters)
        str += Utils.GetName(filter);
      return str;
    }

    public static string GetName(KeyValuePair<object, object> exp)
    {
      return "";
    }
  }
}
