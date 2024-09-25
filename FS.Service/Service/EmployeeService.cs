using FS.Common.Constant;
using FS.Common.DTO;
using FS.Common.Enums;
using FS.Common.Models;
using FS.DataAccess;
using FS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RapidCare.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FS.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(EmployeeDbContext context,
            IConfiguration configuration,
              IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponseMessage> DeleteEmployee(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                Employee objemployee = JsonConvert.DeserializeObject<Employee>(requestMessage?.RequestObj?.ToString());
                if (objemployee != null)
                {
                    Employee existemployee = await _context.Employee.FirstOrDefaultAsync(x => x.Id == objemployee.Id && x.Status == (int)Enums.Status.Active);
                    if (existemployee != null)
                    {
                        existemployee.Status = (int)Enums.Status.Delete;
                        _context.Update(existemployee);
                        await _context.SaveChangesAsync();
                        responseMessage.ResponseCode = (int)Enums.ResponseCode.Success;
                        responseMessage.Message = MessageConstant.DeletedSuccessfully;
                    }
                }
            }
            catch (Exception ex)
            {
                responseMessage.ResponseCode = (int)Enums.ResponseCode.Failed;
            }
            return responseMessage;
        }

        public async Task<ResponseMessage> GetAllEmployee(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            try
            {
                if (responseMessage != null)
                {
                    List<Employee> lstEmployee = new List<Employee>();
                    lstEmployee = await _context.Employee.Where(e => e.Status == (int)Enums.Status.Active).ToListAsync();
                    foreach (Employee employee in lstEmployee)
                    {
                        employee.ImageURL = string.IsNullOrEmpty(employee.ImageURL) ? employee.ImageURL :
                            string.Join('/', _httpContextAccessor.HttpContext.Request.BaseUrl(), employee.ImageURL.FilePathToUrl());
                    }

                    responseMessage.ResponseObj = lstEmployee;

                    responseMessage.ResponseCode = (int)Enums.ResponseCode.Success;

                }

                else
                {
                    responseMessage.ResponseCode = (int)Enums.ResponseCode.Failed;
                    responseMessage.Message = "Invalid request";
                }
            }

            catch (Exception ex)
            {
                responseMessage.ResponseCode = (int)Enums.ResponseCode.Failed;
            }

            return responseMessage;
        }
        private bool IsPermitToUpdate(Employee objemployee, ResponseMessage responseMessage)
        {
            Employee existingEmployee = new Employee();
            return true;
        }
        public async Task<ResponseMessage> SaveEmployee(RequestMessage requestMessage)
        {
            ResponseMessage responseMessage = new ResponseMessage();

            try
            {
                Employee objemployee = JsonConvert.DeserializeObject<Employee>(requestMessage?.RequestObj?.ToString());
                if (objemployee != null)
                {
                    if (objemployee?.Attachment is not null)
                    {
                        string directory = "Files";

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        string filePath = Path.Combine(directory, objemployee.Attachment.FileName);
                        await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(objemployee.Attachment.Byte64.Split(",")[1]));
                        objemployee.ImageURL = filePath;
                    }
                    if (!IsPermitToUpdate(objemployee, responseMessage))
                    {
                        responseMessage.ResponseCode = (int)Enums.ResponseCode.Failed;
                        responseMessage.Message = MessageConstant.InvalidRequest;
                        return responseMessage;
                    }

                    if (objemployee.Id > 0)
                    {
                        Employee existemployee = await _context.Employee.AsNoTracking().FirstOrDefaultAsync(x => x.Id == objemployee.Id && x.Status == (int)Enums.Status.Active);

                        if (objemployee.Attachment is not null)
                        {
                            if (File.Exists(existemployee.ImageURL))
                            {
                                File.Delete(existemployee.ImageURL);
                            }
                        }
                        else
                        {
                            objemployee.ImageURL = existemployee.ImageURL;
                        }

                        if (existemployee != null)
                        {
                            objemployee.Status = (int)Enums.Status.Active;
                            _context.Employee.Update(objemployee);
                            responseMessage.Message = MessageConstant.UpdatedSuccessfully;
                        }
                    }
                    else
                    {
                        objemployee.Status = (int)Enums.Status.Active;
                        await _context.Employee.AddAsync(objemployee);
                        responseMessage.Message = MessageConstant.SavedSuccessfully;
                    }
                    await _context.SaveChangesAsync();
                    objemployee.ImageURL = string.IsNullOrEmpty(objemployee.ImageURL) ? objemployee.ImageURL :
                          string.Join('/', _httpContextAccessor.HttpContext.Request.BaseUrl(), objemployee.ImageURL.FilePathToUrl());

                    responseMessage.ResponseObj = objemployee;
                    responseMessage.ResponseCode = (int)Enums.ResponseCode.Success;

                }



            }

            catch (Exception ex)
            {
                responseMessage.ResponseCode = (int)Enums.ResponseCode.Failed;
            }
            return responseMessage;


        }
    }
}
