using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DataBase.DataHelper
{
  public static class ObjectHelper
    {
      public static string ToJson(this object tempobject)
      {
          return JsonConvert.SerializeObject(tempobject);
      }
    }
}
