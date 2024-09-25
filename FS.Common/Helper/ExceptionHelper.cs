
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidCare.Common.Helper
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// it's throw new exception and help to write in db
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <exception cref="Exception"></exception>
      
        public static string BaseUrl(this HttpRequest httpRequest)
        {
            return string.Join("://", httpRequest.Scheme, httpRequest.Host + httpRequest.PathBase);
        }
        public static string FilePathToUrl(this string filePath)
        {
            return filePath.Replace(@"\", @"/");
        }
    }
}
