using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public partial class ServicesController
{
    //[Authorize(Roles = "admin,cms")]
    [HttpGet("Template/[action]")]
    public serviceresult AllTemplate()
    {
        serviceresult result = new serviceresult();
        try{

            result.messageType = messageType.ok;
            result.message = "";
            result.objectResult = Con.templates;
            
        }
        catch(Exception ex){
            #region 
            result.messageType = messageType.error; 
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpPost("Template/[action]")]
    public serviceresult GetByIdTemplate(idModel idModel) 
    {
        serviceresult result = new serviceresult();
        try{

            var entity = Con.templates.Find(idModel.id);

            result.messageType = messageType.ok;
            result.message = "";
            result.objectResult = entity;
            
        }
        catch(Exception ex){
            #region 
            result.messageType = messageType.error; 
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpPost("Template/[action]")]
    public serviceresult InsertTemplate(template newitem)
    {
        serviceresult result = new serviceresult();
        try{

            #region valedation
            newitem.requiredValidation(ref result);
            newitem.uniqueValidation(ref result,Con);
            if(result.messageType != messageType.ok) 
                return result;
            #endregion

            Con.templates.Add(newitem);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "با موفقیت ثبت شد";
            result.objectResult = newitem;
            
        }
        catch(Exception ex){
            #region 
            result.messageType = messageType.error; 
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpPost("Template/[action]")]
    public serviceresult UpdateTemplate(template entityinfo)
    {
        serviceresult result = new serviceresult();
        try{

            #region valedation
            entityinfo.requiredValidation(ref result);
            entityinfo.uniqueValidation(ref result,Con);
            if(result.messageType != messageType.ok) 
                return result;
            #endregion

            var entity = Con.templates.Find(entityinfo.id);
            entity.setPropsBy(entityinfo,false);


            Con.templates.Update(entity);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "با موفقیت ویرایش شد";
            result.objectResult = entity;
            
        }
        catch(Exception ex){
            #region 
            result.messageType = messageType.error; 
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpPost("Template/[action]")]
    public serviceresult UpdateByPropsTemplate(template templateInfo)
    {
        serviceresult result = new serviceresult();
        try{

            var entity = Con.templates.Find(templateInfo.id);
            entity.setPropsBy(templateInfo,true);

            #region valedation
            entity.requiredValidation(ref result);
            entity.uniqueValidation(ref result,Con);
            if(result.messageType != messageType.ok) 
                return result;
            #endregion

            Con.templates.Update(entity);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "با موفقیت ویرایش شد";
            result.objectResult = templateInfo;
            
        }
        catch(Exception ex){
            #region 
            result.messageType = messageType.error; 
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }

    [HttpPost("Template/[action]")]
    public serviceresult DeleteTemplate(idModel idModel)
    {
        serviceresult result = new serviceresult();
        try{

            var entity = Con.templates.Find(idModel.id);

            #region delete valedation
            entity.deleteValidation(ref result , Con);
            if(result.messageType != messageType.ok) 
                return result;
            #endregion

            Con.templates.Remove(entity);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "حذف شد";
            result.objectResult = idModel.id;
            
        }
        catch(Exception ex){
            #region 
            result.messageType = messageType.error; 
            result.message = ex.Message;
            result.objectResult = null;
            #endregion
        }

        return result;

    }


}