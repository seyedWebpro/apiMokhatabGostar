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
public class chatController : ControllerBase
{

    public chatController(apiContext context, IHttpContextAccessor httpContextAccessor)
    {
        Con = context;
        Con.httpContext = httpContextAccessor.HttpContext;
    }

    public apiContext Con { get; }



    [HttpPost("[action]")]
    [Authorize]
    public serviceresult Insertchat(chat chatinfo)
    {
        serviceresult result = new serviceresult();
        try
        {
            var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var user = Con .users.Where(x=>x.id == userid).FirstOrDefault();
            chatinfo.phonenumber1 = user.username;

            var date = DateTime.Now;
            var pdate = date.ToPersian();
            chatinfo.datetime = pdate;
            

            #region valedation
            chatinfo.requiredValidation(ref result);
            chatinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.chats.Add(chatinfo);
            Con.SaveChanges();
            result.messageType = messageType.ok;
            result.message = "ثبت شد";
            result.objectResult = chatinfo;
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
    public serviceresult Updatechat(chat chatinfo)
    {
        serviceresult result = new serviceresult();
        try
        {
            var entity = Con.chats.Where(x => x.id == chatinfo.id).FirstOrDefault();
            entity.setPropsBy(chatinfo);

            #region valedation
            chatinfo.requiredValidation(ref result);
            chatinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.chats.Update(entity);
            Con.SaveChanges();
            result.messageType = messageType.ok;
            result.message = "ویرایش شد";
            result.objectResult = chatinfo;
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
    public serviceresult GetAll()
    {
        serviceresult result = new serviceresult();
        try
        {
            var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var user = Con.users.Where(x=>x.id == userid ).FirstOrDefault();
            result.objectResult = Con.chats.Where(x => x.phonenumber1 == user.username || x.phonenumber2 == user.username);

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

            var entity = Con.chats.Where(x => x.id == id).FirstOrDefault();

            #region delete valedation
            entity.deleteValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.chats.Remove(entity);
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
