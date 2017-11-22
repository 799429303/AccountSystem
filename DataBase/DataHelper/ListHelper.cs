using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DataBase.DataHelper
{
  public static  class ListHelper
    {
      public static string ListToJason<T>(this List<T> list)
      {
          if (list == null)
          {
              return "";
          }
          StringBuilder sb = new StringBuilder();
          sb.Append("[");
          foreach (T t in list)
          {
              PropertyInfo[] props = t.GetType().GetProperties();
              sb.Append("{");
              foreach (PropertyInfo p in props)
              {

                  sb.Append(p.Name);
                  sb.Append(":\"");
                  sb.Append(p.GetValue(t, null));
                  sb.Append("\"");
                  sb.Append(",");
              }
             
              sb.Remove(sb.Length-1, 1);
              sb.Append("}");
          }
          sb.Append("]");
          sb.Replace("}{", "},{");
          return sb.ToString();
      }
    }
}
