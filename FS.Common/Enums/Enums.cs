using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.Common.Enums
{
  public class Enums
  {
    public enum Status
    {
      Active = 1,
      Inactive = 2,
      Delete = 9
    }

    public enum ResponseCode
    {
      Success = 1,
      Failed = 2,
      Warning =3
    }

   

  }
}
