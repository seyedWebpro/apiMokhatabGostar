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
public class chatContactController : ControllerBase
{

    public chatContactController(apiContext context, IHttpContextAccessor httpContextAccessor)
    {
        Con = context;
        Con.httpContext = httpContextAccessor.HttpContext;
    }

    public apiContext Con { get; }



    [HttpPost("[action]")]
    [Authorize]
    public serviceresult InsertContact(chatcontact chatContactinfo)
    {
        serviceresult result = new serviceresult();
        try
        {

            #region valedation
            chatContactinfo.requiredValidation(ref result);
            chatContactinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.chatContacts.Add(chatContactinfo);
            Con.SaveChanges();
            result.messageType = messageType.ok;
            result.message = "ثبت شد";
            result.objectResult = chatContactinfo;
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
    public serviceresult UpdateContact(chatcontact chatContactinfo)
    {
        serviceresult result = new serviceresult();
        try
        {
            var entity = Con.chatContacts.Where(x => x.id == chatContactinfo.id).FirstOrDefault();
            entity.setPropsBy(chatContactinfo);

            #region valedation
            chatContactinfo.requiredValidation(ref result);
            chatContactinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
            return result;
            #endregion

            Con.chatContacts.Update(entity);
            Con.SaveChanges();
            result.messageType = messageType.ok;
            result.message = "ویرایش شد";
            result.objectResult = chatContactinfo;
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

            result.objectResult = Con.chatContacts.Where(x => x.createdBy == userid);

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
    public serviceresult DeleteContact(long id)
    {
        serviceresult result = new serviceresult();
        try
        {

            var entity = Con.chatContacts.Where(x => x.id == id).FirstOrDefault();

            #region delete valedation
            entity.deleteValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.chatContacts.Remove(entity);
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


  [HttpGet("[action]")]
    [Authorize]
    public serviceresult GetById(long id)
    {
        serviceresult result = new serviceresult();
        try
        {

            var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);

            result.objectResult = Con.chatContacts.Where(x => x.createdBy == userid && x.id == id).FirstOrDefault();

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
    public serviceresult setblock(long id , bool tf)
    {
        serviceresult result = new serviceresult();
        try
        {

            var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var entity = Con.chatContacts.Where(x => x.createdBy == userid && x.id == id).FirstOrDefault();
            
            entity.isblock = tf;

            Con.chatContacts.Update(entity);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message ="ویرایش شد";
            result.objectResult = entity;

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
