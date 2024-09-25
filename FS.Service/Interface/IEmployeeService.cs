using FS.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS.Service.Interface
{
    public interface IEmployeeService
    {
    Task<ResponseMessage> GetAllEmployee(RequestMessage requestMessage);
    Task<ResponseMessage> SaveEmployee(RequestMessage requestMessage);
    Task<ResponseMessage> DeleteEmployee(RequestMessage requestMessage);
    }
}
