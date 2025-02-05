using System.Diagnostics;
using System.Text.Json.Serialization;
using allAPIs.SimoAPI.Models;
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
public class userController : ControllerBase
{

    public userController(apiContext context, IHttpContextAccessor httpContextAccessor)
    {
        Con = context;
        Con.httpContext = httpContextAccessor.HttpContext;
    }

    public apiContext Con { get; }

    [HttpGet("[action]")]
    public serviceresult getAll()
    {
        try
        {
            var users = Con.users.Select(u => new UserDto
            {
                Username = u.username,
                Roles = u.roles,
                Pic = u.pic,
                DisplayName = u.displayName,
                PhoneNumber = u.PhoneNumber
            }).ToList();

            return new serviceresult
            {
                messageType = messageType.ok,
                message = "Successful",
                objectResult = users,
            };
        }
        catch (Exception ex)
        {
            return new serviceresult
            {
                messageType = messageType.error,
                message = ex.Message,
            };
        }
    }

    [Authorize]
    [HttpGet("[action]")]
    public serviceresult getcurrentuser()
    {
        try
        {
            var currentuserid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var entity = Con.users.Find(currentuserid);

            return new serviceresult
            {
                messageType = messageType.ok,
                message = "َsuccessful",
                objectResult = new
                {
                    userId = entity?.id,
                    username = entity?.username,
                    roles = entity?.roles,
                    displayName = entity?.displayName,
                    pic = entity?.pic,
                }
            };

        }
        catch (Exception ex)
        {
            return new serviceresult
            {
                messageType = messageType.error,
                message = ex.Message,
            };
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("[action]")]
    public serviceresult getById(idModel idModel)
    {
        try
        {
            var currentuserid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var entity = Con.users.Find(idModel.id);

            entity.password = entity.password.Decrypt();
            entity.password2 = entity.password;

            return new serviceresult
            {
                messageType = messageType.ok,
                message = "َsuccessful",
                objectResult = entity
            };

        }
        catch (Exception ex)
        {
            return new serviceresult
            {
                messageType = messageType.error,
                message = ex.Message,
            };
        }
    }


    // [HttpPost("[action]")]
    // public serviceresult insert([FromBody] user newentity)
    // {
    //     var result = new serviceresult();
    //     try
    //     {
    //         // 1. دریافت شناسه کاربر فعلی
    //         var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);
    //         var user = Con.users.Where(x => x.id == userid).FirstOrDefault();

    //         // 2. تنظیم نقش پیشفرض کاربر
    //         if (string.IsNullOrEmpty(newentity.roles))
    //         {
    //             newentity.roles += "public";
    //         }

    //         // 3. اعتبارسنجی اطلاعات ورودی
    //         #region validation
    //         newentity.requiredValidation(ref result);
    //         newentity.uniqueValidation(ref result, Con);
    //         if (result.messageType != messageType.ok)
    //         {
    //             return result;
    //         }
    //         #endregion

    //         // 4. بررسی تکراری بودن نام کاربری
    //         user entity = null;
    //         if (Con.users.Where(x => x.username == newentity.username).FirstOrDefault() != null)
    //         {
    //             entity = Con.users.Where(x => x.username == newentity.username).FirstOrDefault();
    //             entity.password = newentity.password;
    //             entity.password2 = newentity.password2;
    //         }

    //         // 5. بررسی تطابق کلمه عبور و تکراری آن
    //         if (newentity.password != newentity.password2)
    //         {
    //             result.messageType = messageType.error;
    //             result.message = "تکرار کلمه عبور اشتباه است";
    //             return result;
    //         }

    //         // 6. ثبت نام کاربر جدید یا بروزرسانی کلمه عبور 
    //         if (entity == null)
    //         {
    //             // 6.1. ثبت نام کاربر جدید
    //             newentity.password = newentity.password.Encrypt();
    //             Con.users.Add(newentity);
    //             Con.SaveChanges();
    //             result.message = "ثبت نام با موفقیت انجام شد";
    //             newentity.password = newentity.password.Decrypt();
    //         }
    //         else
    //         {
    //             // 6.2. بروزرسانی کلمه عبور 
    //             entity.password = entity.password.Encrypt();
    //             Con.users.Update(entity);
    //             Con.SaveChanges();
    //             result.message = "کلمه عبور با موفقیت تغییر یافت";
    //             newentity.password = entity.password.Decrypt();
    //         }

    //         // 7. تنظیم نتیجه عملیات 
    //         result.messageType = messageType.ok;
    //         result.objectResult = newentity;

    //         // 8. تنظیم CampaignId به 0 (اگر می‌خواهید آن را در این متد تنظیم کنید)
    //         newentity.CampaignId = 0;

    //     }
    //     catch (Exception ex)
    //     {
    //         result.messageType = messageType.error;
    //         result.message = ex.Message;
    //     }

    //     return result;
    // }

    // با استفاده از DTO

    [HttpPost("[action]")]
    public serviceresult insert([FromBody] UserDto newUserDto)
    {
        var result = new serviceresult();
        try
        {
            if (string.IsNullOrEmpty(newUserDto.Password))
            {
                result.messageType = messageType.error;
                result.message = "کلمه عبور را وارد کنید";
                return result;
            }

            var newUser = new user
            {
                username = newUserDto.Username,
                roles = string.IsNullOrEmpty(newUserDto.Roles) ? "public" : newUserDto.Roles,
                pic = newUserDto.Pic,
                displayName = newUserDto.DisplayName,
                PhoneNumber = newUserDto.PhoneNumber,
                password = newUserDto.Password.Encrypt(), // رمز عبور از DTO گرفته و رمزگذاری می‌شود
            };

            newUser.requiredValidation(ref result);
            newUser.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
            {
                return result;
            }

            Con.users.Add(newUser);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "ثبت نام با موفقیت انجام شد";
            result.objectResult = newUserDto;
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
        }

        return result;
    }


    [Authorize]
    [HttpPost("[action]")]
    public serviceresult update(user userinfo)
    {
        var result = new serviceresult();
        try
        {
            #region valedation
            userinfo.requiredValidation(ref result);
            userinfo.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            userinfo.password = userinfo.password.Encrypt();

            Con.Update(userinfo);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "تغییرات با موفقیت ذخیره شد";
            result.objectResult = userinfo;


        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
        }

        return result;
    }

    [Authorize]
    [HttpPost("[action]")]
    public serviceresult UpdateByPropsuser(user userInfo)
    {
        serviceresult result = new serviceresult();
        try
        {

            var entity = Con.users.Find(userInfo.id);
            entity.setPropsBy(userInfo, true);

            #region valedation
            entity.requiredValidation(ref result);
            entity.uniqueValidation(ref result, Con);
            if (result.messageType != messageType.ok)
                return result;
            #endregion

            Con.users.Update(entity);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "تغییرات با موفقیت ذخیره شد";
            result.objectResult = userInfo;

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

    [Authorize(Roles = "admin")]
    [HttpPost("[action]")]
    public serviceresult delete(idModel idModel)
    {
        try
        {
            var entity = Con.users.Find(idModel.id);
            Con.users.Remove(entity);
            Con.SaveChanges();

            return new serviceresult
            {
                messageType = messageType.ok,
                message = "َsuccessful",
                objectResult = entity,
            };

        }
        catch (Exception ex)
        {
            return new serviceresult
            {
                messageType = messageType.error,
                message = ex.Message,
            };
        }
    }

    [Authorize]
    [HttpPost("[action]")]
    public serviceresult changePassword(userview_changepassword changepasswordinfo)
    {
        try
        {

            changepasswordinfo.password = changepasswordinfo.password.Encrypt();
            var entity = Con.users.Where(u => u.username == changepasswordinfo.username && u.password == changepasswordinfo.oldpassword).FirstOrDefault();

            if (entity == null)
            {
                return new serviceresult
                {
                    messageType = messageType.error,
                    message = "The username or password is incorrect",
                };
            }

            if (changepasswordinfo.password != changepasswordinfo.repeadpassword)
            {
                return new serviceresult
                {
                    messageType = messageType.error,
                    message = "Repeating the password is wrong",
                };
            }

            entity.setPropsBy(changepasswordinfo);
            Con.Update(entity);
            Con.SaveChanges();


            return new serviceresult
            {
                messageType = messageType.ok,
                message = "َsuccessful",
                objectResult = entity,
            };

        }
        catch (Exception ex)
        {
            return new serviceresult
            {
                messageType = messageType.error,
                message = ex.Message,
            };
        }
    }



    [Authorize]
    [HttpPost("[action]")]
    public serviceresult UpdateDisplayName(string displayName)
    {
        var result = new serviceresult();
        try
        {
            var userid = SimoFunctions.GetCurrentUserId(Con.httpContext);
            var finduser = Con.users.Where(x => x.id == userid).FirstOrDefault();
            finduser.displayName = displayName;

            Con.Update(finduser);
            Con.SaveChanges();

            result.messageType = messageType.ok;
            result.message = "ثبت شد";
            result.objectResult = finduser;

        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
        }

        return result;
    }

  
  [HttpPost("upgrade-role")]
public async Task<IActionResult> UpgradeUserRole([FromBody] UpgradeUserRoleDto request)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new
        {
            StatusCode = 400,
            Message = "اطلاعات ارسال شده نامعتبر است.",
            Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
        });
    }

    var user = await Con.users.FindAsync(request.UserId);  // تغییر از int به long

    if (user == null)
    {
        return NotFound(new
        {
            StatusCode = 404,
            Message = "کاربر مورد نظر یافت نشد."
        });
    }

    // تغییر نقش کاربر
    user.roles = request.NewRole;

    try
    {
        Con.users.Update(user);
        await Con.SaveChangesAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "نقش کاربر با موفقیت تغییر یافت.",
            UserId = user.id,
            NewRole = user.roles
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            StatusCode = 500,
            Message = "خطای داخلی سرور.",
            Error = ex.Message
        });
    }
}



}
