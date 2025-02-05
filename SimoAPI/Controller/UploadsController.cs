using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace api.Controllers;

[Route("[controller]")]
[ApiController]
public class UploadsController : ControllerBase
{

    #region ctors
    public UploadsController(IConfiguration configuration, apiContext dbcontext)
    {
        Configuration = configuration;
        Con = dbcontext;
    }

    public IConfiguration Configuration { get; }
    public apiContext Con { get; }
    #endregion

    #region services

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult CreateFolder(string folderPath)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            string dir = @"Uploads/" + folderPath;
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            result.message = "فولدر ایجاد شد";
            result.messageType = messageType.ok;
            result.objectResult = null;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult DeleteFolder(string filePath)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            string dir = @"Uploads/" + filePath;
            // If directory does not exist, create it
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }

            result.message = "فولدر حذف شد";
            result.messageType = messageType.ok;
            result.objectResult = null;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult MoveFolder(string folderPath, string newFolderPath)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            string dir = @"Uploads/" + folderPath;
            // If directory does not exist, create it
            if (Directory.Exists(dir))
            {
                Directory.Move(dir, newFolderPath);
            }

            result.message = "فولدر حذف شد";
            result.messageType = messageType.ok;
            result.objectResult = null;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult UploadFile(fileView file)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            var file64 = file.filedata.Split(',');
            var base64EncodedBytes = System.Convert.FromBase64String(file64[1]);

            string dir = @"Uploads/" + file.filePath;
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            System.IO.File.WriteAllBytes(dir + "/" + file.fileName, base64EncodedBytes);

            result.message = "فایل با موفقیت ذخیره شد";
            result.messageType = messageType.ok;
            result.objectResult = null;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult DeleteFile(fileView file)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            string dir = @"Uploads/" + file.filePath;
            System.IO.File.Delete(dir);

            result.message = "فایل حذف شد";
            result.messageType = messageType.ok;
            result.objectResult = null;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult MoveFile(string filePath, string newFilePath)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            string dir = @"Uploads/" + filePath;
            System.IO.File.Move(dir, newFilePath);

            result.message = "فایل منتقل شد";
            result.messageType = messageType.ok;
            result.objectResult = null;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult GetFiles(fileView file)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {
            string dir = @"Uploads/" + file.filePath;
            if (!Directory.Exists(dir))
            {
                result.message = "مسیر یافت نشد";
                result.messageType = messageType.error;
                result.objectResult = null;
                return result;
            }

            string[] filePaths = Directory.GetFiles(dir);
            var lis = new List<fileView>();
            foreach (string item in filePaths)
            {
                lis.Add(new fileView() { filePath = item.Replace("\\", "/"), fileName = item.Replace("\\", "/").Split('/').Last() });
            }

            result.message = "";
            result.messageType = messageType.ok;
            result.objectResult = lis;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult GetDirectories(fileView file)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {
            string dir = @"Uploads/" + file.filePath;
            var filePaths = Directory.GetDirectories(dir);
            var lis = new List<fileView>();
            foreach (var item in filePaths)
            {
                lis.Add(new fileView() { filePath = item.Replace("\\", "/"), fileName = item.Replace("\\", "/").Split('/').Last() });
            }

            result.message = "";
            result.messageType = messageType.ok;
            result.objectResult = lis;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }
    
    
    [HttpPost("[action]")]
    [Authorize]
    public serviceresult downloadFile(string url)
    {
        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();
        try
        {

            string file = System.IO.Path.GetFileName(url);
            WebClient cln = new WebClient();

            result.message = "";
            result.messageType = messageType.ok;
            result.objectResult = cln.DownloadData(url);
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }


    
    #endregion

    #region functions

    #endregion
}


