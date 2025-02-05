using System.Diagnostics;
using System.Globalization;
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

[ApiController]
[Route("[controller]")]
public class messageController : ControllerBase
{

    public messageController(apiContext context, IHttpContextAccessor httpContextAccessor)
    {
        Con = context;
        Con.httpContext = httpContextAccessor.HttpContext;
    }

    public apiContext Con { get; }

    [HttpPost("[action]")]
    [Authorize]
    public serviceresult Insertmessage(message messageinfo)
    {
        serviceresult result = new serviceresult();
        try
        {

            #region valedation
            messageinfo.requiredValidation(ref result);
            messageinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.messages.Add(messageinfo);
            Con.SaveChanges();
            result.messageType = messageType.ok;
            result.message = "ثبت شد";
            result.objectResult = messageinfo;
            return result;

        }
        catch (Exception ex)
        {
            #region 
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }


    [HttpPost("[action]")]
    [Authorize]
    public serviceresult Updatemessage(message messageinfo)
    {
        serviceresult result = new serviceresult();
        try
        {
            var entity = Con.messages.Where(x => x.id == messageinfo.id).FirstOrDefault();
            entity.setPropsBy(messageinfo);

            #region valedation
            messageinfo.requiredValidation(ref result);
            messageinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.messages.Update(entity);
            Con.SaveChanges();
            result.messageType = messageType.ok;
            result.message = "ویرایش شد";
            result.objectResult = messageinfo;
            return result;

        }
        catch (Exception ex)
        {
            #region 
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpGet("[action]")]
    [Authorize]
    public serviceresult GetAll(long chatid)
    {
        serviceresult result = new serviceresult();
        try
        {
            var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var user = Con.users.Where(x => x.id == userid).FirstOrDefault();

            var chat = Con.chats.Where(x => x.id == chatid && (x.phonenumber1 == user.username || x.phonenumber2 == user.username)).FirstOrDefault();
            if (chat != null)
            {
                result.objectResult = Con.messages.Where(x => x.chatid == chatid);
            }

            result.objectResult = null;
        }
        catch (Exception ex)
        {
            #region 
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpPost("[action]")]
    public serviceresult Delete(long id)
    {
        serviceresult result = new serviceresult();
        try
        {

            var entity = Con.messages.Where(x => x.id == id).FirstOrDefault();

            #region delete valedation
            entity.deleteValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.messages.Remove(entity);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "با موفقیت حذف شد";
            result.objectResult = id;
            return result;

        }
        catch (Exception ex)
        {
            #region 
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

}
