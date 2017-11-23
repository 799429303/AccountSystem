using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace DataBase.DataHelper
{
  public static  class ListHelper
    {
      public static string ListToJason<T>(this List<T> list)
      {
          return JsonConvert.SerializeObject(list);
          
      }
    }
}
