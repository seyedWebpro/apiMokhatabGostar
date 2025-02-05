using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
public static class SimoFunctions
{
    /// <summary>
    /// setPropsBy
    /// </summary>
    /// <param name="source"></param>
    /// <param name="by"></param>
    /// <param name="unsetNulls"></param>
    public static void setPropsBy(this object source, object by, bool unsetNulls = false)
    {

        var props = by.GetType().GetProperties();
        foreach (var prop in props)
        {
            if (prop.Name.ToLower() == "id" 
            || prop.Name.ToLower() == "createtime"
            || prop.Name.ToLower() == "editetime"
            || prop.Name.ToLower() == "createdby"
            || prop.Name.ToLower() == "editedby"
            )
            {
                continue;
            }

            if (!prop.CanWrite)
            {
                continue;
            }

            if (unsetNulls && prop.GetValue(by) == null)
            {
                continue;
            }

            try
            {
                source?.GetType()?.GetProperty(prop.Name)?.SetValue(source, prop.GetValue(by));

            }
            catch
            {
            }


        }

    }

    // <summary>
    /// رمز نگاری مقدار متنی
    /// </summary>
    /// <param name="clearText"></param>
    /// <returns></returns>
    public static string Encrypt(this string clearText)
    {
        string EncryptionKey = "MohsenKheybari";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    /// <summary>
    /// رمز گشایی مقدار متنی
    /// </summary>
    /// <param name="cipherText"></param>
    /// <returns></returns>
    public static string Decrypt(this string cipherText)
    {
        string EncryptionKey = "MohsenKheybari";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }


        /// <summary>
        /// کاربر جاری
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static long GetCurrentUserId(HttpContext httpContext)
        {
            try
            {
                return Convert.ToInt64(httpContext.User.Claims.Select(x => x.Value).ToArray()[0].ToString());
            }
            catch
            {
            }

            return 0;
        }

        /// <summary>
        /// کاربر جاری
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetCurrentUserName(HttpContext httpContext)
        {
            try
            {
                return httpContext.User.Claims.Select(x => x.Value).ToArray()[2].ToString();
            }
            catch
            {
            }

            return "";
        }

    /// <summary>
    /// ادمین های شرکتهای کاربر جاری
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static string? GetCurrentCompanyAdmins(HttpContext httpContext, apiContext context)
    {
        try
        {
            var userid = GetCurrentUserId(httpContext);
            var companys = context.companys.Where(x => x.companyAdmins.Contains(userid.ToString())).ToList();
            if (companys.Count == 0)
            {
                return "-1";
            }

            string userAdmins = "-1";
            foreach (var item in companys)
            {
                userAdmins += "," + item.companyAdmins;
            }

            return userAdmins;
        }
        catch
        {
        }

        return null;

    }

    /// <summary>
    /// شرکتهای کاربر جاری
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static string GetCurrentCompanys(HttpContext httpContext, apiContext context)
    {
        try
        {
            var userid = GetCurrentUserId(httpContext);
            var companys = context.companys.Where(x => x.companyAdmins.Contains(userid.ToString())).ToList();
            if (companys.Count == 0)
            {
                return "-1";
            }

            string companyids = "-1";
            foreach (var item in companys)
            {
                companyids += "," + item.id;
            }

            return companyids;
        }
        catch
        {
        }

        return null;

    }

    /// <summary>
    /// ایجاد کننده
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="httpContext"></param>
    public static void setCreator(this entityParent entity, HttpContext httpContext)
    {
        try
        {
            entity.createdBy = Convert.ToInt64(httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
        }
        catch
        {
        }
        entity.createTime = DateTime.Now;
    }

    /// <summary>
    /// ویرایشگر
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="httpContext"></param>
    public static void setEditor(this entityParent entity, HttpContext httpContext)
    {

        try
        {
            entity.editedBy = Convert.ToInt64(httpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
        }
        catch
        {
        }
        entity.editeTime = DateTime.Now;
    }

    /// <summary>
    /// remoteIp
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="httpContext"></param>
    public static void setRemoteIp(this entityParent entity, HttpContext httpContext)
    {
        //remoteIp
        entity.rempoteIp = httpContext.Connection.RemoteIpAddress.ToString();
    }


    public static void VerifyCode(apiContext context , ref serviceresult result , string phonenumber , string code)
    {
        var entity = context.otps.Where(x => x.phone_number == phonenumber).OrderByDescending(x => x.createTime).FirstOrDefault() ?? null;
            if (entity != null)
            {
                if (entity.createTime.Value.AddMinutes(2) >= DateTime.Now)
                {
                    if (entity.verify_code == code)
                    {
                        entity.status = 2;

                        context.Entry(entity).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        entity.status = 3;

                        context.Entry(entity).State = EntityState.Modified;
                        context.SaveChanges();

                        result = new serviceresult()
                        {
                            messageType = messageType.error,
                            message = "کد وارد شده صحیح نیست",
                            objectResult = entity
                        };
                    }
                }
                else
                {
                    entity.status = 4;

                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();

                    result = new serviceresult()
                    {
                        messageType = messageType.error,
                        message = "کد وارد شده منقضی شده است",
                        objectResult = entity
                    };
                }
            }
            
    }


    //checktoken
    public static async Task<string> checktoken(string baseurl,string token)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, baseurl + "/Security/CheckToken");
        request.Headers.Add("Authorization", "Bearer " + token);
        var response = await client.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }


    public static string ToPersian(this DateTime d)
    {
        PersianCalendar pc = new PersianCalendar();

        return pc.GetYear(d) +"/"+ pc.GetMonth(d) +"/"+ pc.GetDayOfMonth(d);
    }

    public static string ToPersian2(this DateTime d)
    {
        PersianCalendar pc = new PersianCalendar();
        string strmon = "";
        switch (pc.GetMonth(d))
        {
            case 1:
                strmon = "فروردین";
            break;
            case 2:
                strmon = "اردیبهشت";
            break;
            case 3:
                strmon = "خرداد";
            break;
            case 4:
                strmon = "تیر";
            break;
            case 5:
                strmon = "مرداد";
            break;
            case 6:
                strmon = "شهریور";
            break;
            case 7:
                strmon = "مهر";
            break;
            case 8:
                strmon = "آبان";
            break;
            case 9:
                strmon = "آذر";
            break;
            case 10:
                strmon = "دی";
            break;
            case 11:
                strmon = "بهمن";
            break;
            case 12:
                strmon = "اسفند";
            break;
        }

        return  pc.GetDayOfMonth(d) +" "+ strmon +" "+ pc.GetYear(d);
    }


}